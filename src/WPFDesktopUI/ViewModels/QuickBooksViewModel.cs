using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using stn = WPFDesktopUI.Controllers.SettingsController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFDesktopUI.ViewModels.QuickBooks;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Conductor<object>, IMainTab, INotifyPropertyChanged {

		#region Constructor

		public QuickBooksViewModel() {
			QuickBooksSidePaneViewModel = new QuickBooksSidePaneViewModel();
    }

		#endregion Constructor


		#region Properties

    public QuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }
    public string ConsoleMessage { get; set; }
    public bool CanBtnQbImport { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;

    #endregion Properties


    #region Methods

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
      QuickBooksSidePaneViewModel.OnSelected();
    }

    public async Task BtnQbImport() {
      try {
				SessionStart();

        var attr = QuickBooksSidePaneViewModel.QbspModel.Attr;
        IQuickBooksModel qbModel = Factory.CreateQuickBooksModel(attr);

        var dt = ImportViewModel.CsvData;

        await Task.Run(() => {
          qbModel.BtnQbImport(stn.QbFilePath(), dt, Factory.CreateClientInvoiceHeaderModel);
        });

        // Below is deprecated
        var header = new ClientInvoiceHeaderModel {
          CustomerRefFullName = attr["CustomerRefFullName"].Payload, // "CLASS"
          ClassRefFullName = attr["ClassRefFullName"].Payload, // "Barrie Area:Barrie Corporate"
          TemplateRefFullName = attr["TemplateRefFullName"].ComboBox.SelectedItem,
          TxnDate = Convert.ToDateTime(attr["TxnDate"].Payload),
					Other = attr["Other"].Payload,
				};


        //var header = MapDataTableToHeaderModel(dt);

        var csvModel = MapDataTableToCsvModel(dt);

        await Task.Run(() => {
          var qbImport = new NxQbImportController(stn.QbFilePath(), header, csvModel);
          qbImport.Import();
        });

        ConsoleMessage = "Import has successfully completed";
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
			} finally {
        SessionEnd();
			}
		}

    private ClientInvoiceHeaderModel MapDataTableToHeaderModel(DataTable dt) {
      throw new NotImplementedException();
    }

    private List<CsvModel> MapDataTableToCsvModel(DataTable dt) {
      // Throw if datatable is empty
      if (dt == null) {
        throw new ArgumentNullException(paramName: nameof(dt),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      if (dt.Rows.Count <= 0) {
        throw new IndexOutOfRangeException(
          "No Invoice lineItems were supplied. "+
          "The Importer was expecting at least 1.");
      }

      // Throw if mandatory field isn't accounted for
      foreach (var attribute in QuickBooksSidePaneViewModel.QbspModel.Attr) {
        if (attribute.Value.IsMandatory == false) continue;
        var noDropDownSelected = string.IsNullOrEmpty(attribute.Value.ComboBox.SelectedItem);
        var noTextInTextBox = string.IsNullOrEmpty(attribute.Value.Payload);
        if (noDropDownSelected && noTextInTextBox) {
          throw new ArgumentNullException(paramName: attribute.Value.Name,
            message: "No parameter specified for '" + attribute.Value.Name + "'.");
        }
      }

      // Dynamically set props in model using reflection (slow)
      var convertedList = new List<CsvModel>();
      foreach (var row in dt.AsEnumerable()) {
        // Construct row data to dynamically populate
        var csvModel = new CsvModel();
        foreach (var prop in csvModel.GetType().GetProperties()) {
          var propStr = prop.Name;
          csvModel.GetType().GetProperty(propStr).SetValue(csvModel, GetRow(row, propStr));
        }

        // Write new row to master List<Model>
        convertedList.Add(csvModel);
      }

      
      return convertedList.ToList();
    }

    /// <summary>
    /// Decide whether to use data from the sidepane dropdown or textbox, default to
    /// selected combobox item if possible.
    /// </summary>
    /// <param name="row">A row from a DataTable</param>
    /// <param name="key">The dictionary Key for a QbAttribute</param>
    /// <returns>String for the data in a cell/a constant value</returns>
    private string GetRow(DataRow row, string key) {
      var attr = QuickBooksSidePaneViewModel.QbspModel.Attr[key];
      var colName = attr.ComboBox.SelectedItem;

      if (!string.IsNullOrEmpty(colName)) {
        return Convert.ToString(row[colName]);
      }

      var payload = attr.Payload;

      if (!string.IsNullOrEmpty(payload)) {
        return Convert.ToString(payload);
      }

      return null;
    }

    private void SessionStart() {
      CanBtnQbImport = false;
      QbProgressBarIsVisible = true;
      ConsoleMessage = "Importing, please stand by...";
    }

    private void SessionEnd() {
      CanBtnQbImport = true;
      QbProgressBarIsVisible = false;
    }

		#endregion Methods
	}
}