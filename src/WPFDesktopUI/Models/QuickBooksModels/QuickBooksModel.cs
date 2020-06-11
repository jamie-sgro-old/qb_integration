using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models {
  public class QuickBooksModel : IQuickBooksModel
  {
    public QuickBooksModel(Dictionary<string, IQbAttribute> attr) {
      _attr = attr;
    }

    private Dictionary<string, IQbAttribute> _attr { get; }

    public async Task QbImport(string qbFilePath, DataTable dt, Func<IClientInvoiceHeaderModel> clientInvoiceHeaderModel) {
      ValidateDt(dt);

      var header = clientInvoiceHeaderModel();

      header.CustomerRefFullName = _attr["CustomerRefFullName"].Payload;
      header.ClassRefFullName = _attr["ClassRefFullName"].Payload;
      header.TemplateRefFullName = _attr["TemplateRefFullName"].ComboBox.SelectedItem;
      header.TxnDate = Convert.ToDateTime(_attr["TxnDate"].Payload);
      header.Other = _attr["Other"].Payload;

      //var header = MapDataTableToHeaderModel(dt);

      var csvModel = MapDataTableToModel(dt);

      await Task.Run(() => {
        // TODO: Remove coupling to Factory
        var qbImportController = Factory.CreateQbImportController(qbFilePath, header, csvModel);
        qbImportController.Import();
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
  }
}
