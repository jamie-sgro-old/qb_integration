using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models {
  public class QuickBooksModel : IQuickBooksModel
  {
    public QuickBooksModel(Dictionary<string, IQbAttribute> attr, IQbImportController qbImportController) {
      _attr = attr;
      _qbImportController = qbImportController;
    }

    
    
    private Dictionary<string, IQbAttribute> _attr { get; }
    private IQbImportController _qbImportController { get; }



    public async Task QbImport(DataTable dt, List<Customer> cxList) {
      ValidateDt(dt);

      var csvModels = MapDataTableToModel(dt);

      var appliedCsvModels = ApplyCxRules(csvModels, cxList);

      await Task.Run(() => {
        var qbImportController = _qbImportController;
        qbImportController.Import(appliedCsvModels);
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

    private List<ICsvModel> ApplyCxRules(List<ICsvModel> csvModels, List<Customer> cxList) {
      // Overwrite values based on Customer Rules
      foreach (var cx in cxList) {
        foreach (var row in csvModels) {
          if (row.CustomerRefFullName == cx.Name) {
            row.PONumber = cx.PoNumber;
            row.TermsRefFullName = cx.TermsRefFullName;
          }
        }
      }

      return csvModels;
    }
  }
}
