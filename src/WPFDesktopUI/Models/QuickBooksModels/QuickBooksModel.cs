using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using MCBusinessLogic.Models.Interfaces;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.QuickBooksModels;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models {
  public class QuickBooksModel : PluginHandler<IAfterGroupBy>, IQuickBooksModel {
    public QuickBooksModel(Dictionary<string, IQbAttribute> attr, IQbImportController qbImportController) {
      _attr = attr;
      _qbImportController = qbImportController;
    }

    private Dictionary<string, IQbAttribute> _attr { get; }
    private IQbImportController _qbImportController { get; }

    [ImportMany(typeof(IAfterGroupBy), AllowRecomposition = true)]
    new IEnumerable<Lazy<IAfterGroupBy, IPluginMetaData>> _pluginList;



    public async Task QbImport(DataTable dt, List<ICustomer> cxList) {
      ValidateDt(dt);

      var csvModels = MapDataTableToModel(dt);

      var appliedCsvModels = ApplyCxRules(csvModels, cxList);

      var itemReplacedCsvModels = ApplyItemReplacement(appliedCsvModels);

      var adrsReplacedCsvModels = ApplyAddressReplacement(itemReplacedCsvModels);

      var groupBy = GroupBy.GroupInvoices(adrsReplacedCsvModels);

      var pluginData = PluginPreprocess(groupBy);

      var appendLine = AppendLine(pluginData, cxList);

      await Task.Run(() => {
        var qbImportController = _qbImportController;
        qbImportController.Import(appendLine);
      });
    }

    private static void ValidateDt(DataTable dt) {
      // Throw if datatable is empty
      if (dt == null) {
        throw new ArgumentNullException(paramName: nameof(dt),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      if (dt.Rows.Count <= 0) {
        throw new IndexOutOfRangeException(
          "No Invoice lineItems were supplied. " +
          "The Importer was expecting at least 1.");
      }
    }

    private List<ICsvModel> MapDataTableToModel(DataTable dt) {
      // Throw if mandatory field isn't accounted for
      foreach (var attribute in _attr) {
        if (attribute.Value.IsMandatory == false) continue;
        var noDropDownSelected = string.IsNullOrEmpty(attribute.Value.ComboBox.SelectedItem);
        var noTextInTextBox = string.IsNullOrEmpty(attribute.Value.Payload);
        if (noDropDownSelected && noTextInTextBox) {
          throw new ArgumentNullException(paramName: attribute.Value.Name,
            message: "No parameter specified for '" + attribute.Value.Name + "'.");
        }
      }

      // Dynamically set props in model using reflection (slow)
      var convertedList = new List<ICsvModel>();
      foreach (var row in dt.AsEnumerable()) {
        // Construct row data to dynamically populate
        var csvModel = Factory.CreateCsvModel();
        foreach (var prop in csvModel.GetType().GetProperties()) {
          var propStr = prop.Name;
          csvModel.GetType().GetProperty(propStr).SetValue(csvModel, _attr[propStr].GetRow(row));
        }

        // Write new row to master List<Model>
        convertedList.Add(csvModel);
      }

      return convertedList.ToList();
    }

    /// <summary>
    /// Overwrite various header items with constant values based on customer names
    /// supplied in the 'Customers' tab
    /// </summary>
    /// <param name="csvModels"></param>
    /// <param name="cxList"></param>
    /// <returns></returns>
    private List<ICsvModel> ApplyCxRules(List<ICsvModel> csvModels, List<ICustomer> cxList) {
      // Overwrite values based on Customer Rules
      foreach (var cx in cxList) {
        foreach (var row in csvModels) {
          if (row.CustomerRefFullName != cx.Name) continue;

          row.PONumber = String.IsNullOrEmpty(cx.PoNumber) ? row.PONumber : cx.PoNumber;
          row.TermsRefFullName = String.IsNullOrEmpty(cx.TermsRefFullName) ? row.TermsRefFullName : cx.TermsRefFullName;
          row.ClassRefFullName = String.IsNullOrEmpty(cx.Class) ? row.ClassRefFullName : cx.Class;
        }
      }

      return csvModels;
    }

    /// <summary>
    /// Overwrite cells in the ItemRef column if there's a matching string
    /// in the Items tab (ItemViewModel / ItemModel) inside the item table in sqlite db.
    /// Convert that matching data to the corresponding data from the
    /// ReplaceWith property
    /// </summary>
    /// <param name="csvModels"></param>
    /// <returns>Dataset with replaced Items</returns>
    private List<ICsvModel> ApplyItemReplacement(List<ICsvModel> csvModels) {
      // Get Item names to replace (doesn't have to 'new-up' since it's a static read (no state interaction))
      var itemReplacers = SearchReplaceModel.StaticRead("item");

      // Convert Item names from the [ToReplace] property to the [ReplaceWith] property
      foreach (var item in itemReplacers) {
        csvModels
          .Where(x => x.ItemRef == item.ToReplace).ToList()
          .ForEach(x => x.ItemRef = item.ReplaceWith);
      }

      return csvModels;
    }

    private List<ICsvModel> ApplyAddressReplacement(List<ICsvModel> csvModels) {
      // Get Item names to replace (doesn't have to 'new-up' since it's a static read (no state interaction))
      var itemReplacers = SearchReplaceModel.StaticRead("address");

      // Convert Item names from the [ToReplace] property to the [ReplaceWith] property
      foreach (var item in itemReplacers) {
        csvModels
          .Where(x => x.ShipAddress == item.ToReplace).ToList()
          .ForEach(x => x.ShipAddress = item.ReplaceWith);
      }

      return csvModels;
    }

    private List<IInvoice> PluginPreprocess(List<IInvoice> invoices) {
      List<IInvoice> rtnData = null;

      Compose();
      var relevantPlugins = GetRelevantPlugins(_pluginList);

      foreach (Lazy<IAfterGroupBy, IPluginMetaData> relevantPlugin in relevantPlugins) {
        try {
          var newData = relevantPlugin.Value.ModifyGrouped(invoices);
          if (newData != null) {
            rtnData = newData;
          }
        } catch (Exception e) {
          log.Error("The following plugin resulted in an error: " +
                    relevantPlugin.Metadata.Name, e);
          throw new PluginException("The following plugin resulted in an error: " +
                                    relevantPlugin.Metadata.Name +
                                    ". The error report is as follows:\n" +
                                    e.Message);
        }
      }

      // Return newly processed data if anything changed
      // Otherwise just provide original data
      return rtnData ?? invoices;
    }

    /// <summary>
    /// Add final lines at the bottom of every invoice based on customer names
    /// supplied in the 'Customers' tab
    /// </summary>
    /// <param name="csvModels"></param>
    /// <param name="cxList"></param>
    /// <returns></returns>
    private List<IInvoice> AppendLine(List<IInvoice> invoices, List<ICustomer> cxList) {
      // Overwrite values based on Customer Rules
      foreach (var invoice in invoices) {
        foreach (var cx in cxList) {
          if (invoice.Header.CustomerRefFullName == cx.Name) {
            if (cx.AppendLineItem1 != null) {
              invoice.Lines.Add(new CsvModel {
                ItemRef = cx.AppendLineItem1
              });
            }

            if (cx.AppendLineItem2 != null) {
              invoice.Lines.Add(new CsvModel {
                ItemRef = cx.AppendLineItem2
              });
            }

            if (cx.AppendLineItem3 != null) {
              invoice.Lines.Add(new CsvModel {
                ItemRef = cx.AppendLineItem3
              });
            }
          }
        }
      }

      return invoices;
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
