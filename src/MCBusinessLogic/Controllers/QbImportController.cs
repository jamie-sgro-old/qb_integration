using System;
using System.Collections.Generic;
using System.Linq;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public class QbImportController : IQbImportController {
    public QbImportController(string qbFilePath, Func<IClientInvoiceHeaderModel> headerFunc, Func<string, IInvoiceImporter> invoiceFunc) {
      QbFilePath = qbFilePath;
      _headerFunc = headerFunc;
      _invoiceFunc = invoiceFunc;
    }

    public string QbFilePath { get; set; }
    private Func<IClientInvoiceHeaderModel> _headerFunc { get; }
    private Func<string, IInvoiceImporter> _invoiceFunc { get; }


    public void Import(List<ICsvModel> csvModels) {
      var header = _headerFunc();

      // Get list of all props that need a nested GroupBy
      var propList = new List<string>();
      foreach (var prop in header.GetType().GetProperties()) {
        propList.Add(prop.Name);
      }

      using (var invoiceImporter = _invoiceFunc(QbFilePath)) {
        try {
          GroupFromListRecursively(invoiceImporter, header, csvModels, propList);
        } catch (Exception) {
          invoiceImporter.Rollback();
          throw;
        }
      }
    }



    /// <summary>
    /// Since every unique invoice can have several lines, but a single value per header field,
    /// group List<ICsvModel> so each header-value is the same per subgroup.
    /// Then execute the IInvoiceImporter.Import with every nested subgroup where each subgroup
    /// constitutes a single completed invoice
    /// </summary>
    /// <param name="invoiceImporter">Reference to the object conducting the import()</param>
    /// <param name="header">Dynamically writes and rewrites header data for each import based on GroupBy</param>
    /// <param name="g">A list of dataModels that can be further grouped-by before import</param>
    /// <param name="propList">A list of each header property name that should be grouped</param>
    private void GroupFromListRecursively(IInvoiceImporter invoiceImporter, IClientInvoiceHeaderModel header, List<ICsvModel> g, List<string> propList) {
      // Using reflection (slow) group current dataset into subgroups based on the first element of propList
      var currentGroupBy = g
        .ToList()
        .GroupBy(x => x.GetType()
          .GetProperty(propList[0])
          .GetValue(x, null));
      foreach (var group in currentGroupBy) {
        // Update header with constant value
        // i.e. header.CustomerRefFullName = newGroup.Key; (where newGroup.Key == "Acme Inc.")
        header.GetType().GetProperty(propList[0]).SetValue(header, group.Key);

        // Copy all but first element from List
        var newPropList = propList.GetRange(1, propList.Count - 1);

        // Check if we've reached the end of the list (and thus, the recursion)
        if (newPropList.Count <= 0) {
          var lineItems = MapLineItems(group.ToList());
          var mappedHeader = MapHeader(header);
          invoiceImporter.Import(mappedHeader, lineItems);
          continue;
        }

        // Otherwise, if there are more properties to loop through, recurse
        GroupFromListRecursively(invoiceImporter, header, group.ToList(), newPropList);
      }

    }


    /// <summary>
    /// Converts IClientInvoiceHeaderModel data into IInvoiceHeaderModel to be safe for importing to QB
    /// Functionally equivalent to:
    /// headerModel.property1 = preHeader.property1;
    /// headerModel.property2 = preHeader.property2;
    /// </summary>
    /// <param name="preHeader">A model with shared properties between
    /// IClientInvoiceHeaderModel and IInvoiceHeaderModel</param>
    /// <returns></returns>
    public IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader) {
      var headerModel = McFactory.CreateInvoiceHeaderModel();

      if (!(preHeader is IClientInvoiceHeaderModel)) {
        throw new ArgumentNullException(nameof(preHeader), "Could not map header items from Csv Model.");
      }

      // Reflection: i.e. headerModel.someProperty = preHeader.someProperty;
      foreach (var prop in typeof(IClientInvoiceHeaderModel).GetProperties()) {
        var propStr = prop.Name;
        headerModel.GetType().GetProperty(propStr)
          .SetValue(headerModel, preHeader.GetType().GetProperty(propStr).GetValue(preHeader));
      }

      return headerModel;
    }


    /// <summary>
    /// Converts Lists of ICSVModels into IInvoiceLineItemModels using reflection.
    /// Functionally equivalent to:
    /// lineModel.property1 = line.property1;
    /// lineModel.property2 = line.property2;
    /// </summary>
    /// <param name="lineItems">A list of data with shared properties between
    /// ICSVModel and IInvoiceLineItemModel</param>
    /// <returns></returns>
    public List<IInvoiceLineItemModel> MapLineItems(List<ICsvModel> lineItems) {
      var lineModelsList = new List<IInvoiceLineItemModel>();
      foreach (var line in lineItems) {

        if (!(line is IClientInvoiceLineItemModel)) {
          throw new ArgumentNullException(nameof(lineItems), "Could not map line items from Csv Model.");
        }

        var lineModel = McFactory.CreateInvoiceLineItemModel();
        
        // Reflection: i.e. lineModel.someProperty = line.someProperty;
        foreach (var prop in typeof(IClientInvoiceLineItemModel).GetProperties()) {
          var propStr = prop.Name;
          lineModel.GetType().GetProperty(propStr)
            .SetValue(lineModel, line.GetType().GetProperty(propStr).GetValue(line));
        }

        lineModelsList.Add(lineModel);
      }
      return lineModelsList;
    }
  }
}
