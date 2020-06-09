using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using stn = WPFDesktopUI.Controllers.SettingsController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFDesktopUI.ViewModels.QuickBooks;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Conductor<object>, IMainTab {

		#region Constructor

		public QuickBooksViewModel() {
			QuickBooksSidePaneViewModel = new QuickBooksSidePaneViewModel();
    }

		#endregion Constructor


		#region Properties

		private string _consoleMessage;
		private bool _canBtnQbImport = true;
		private bool _qbProgressBarIsVisible = false;

		public QuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }

    public string ConsoleMessage {
			get => _consoleMessage;
      set {
				_consoleMessage = value;
				NotifyOfPropertyChange(() => ConsoleMessage);
			}
		}

    public bool CanBtnQbImport {
			get => _canBtnQbImport;
      set { 
				_canBtnQbImport = value;
				NotifyOfPropertyChange(() => CanBtnQbImport);
			}
		}

		public bool QbProgressBarIsVisible {
			get => _qbProgressBarIsVisible;
      set {
				_qbProgressBarIsVisible = value;
				NotifyOfPropertyChange(() => QbProgressBarIsVisible);
			}
		}

    #endregion Properties


    #region Methods

    public void OnSelected() {
      QuickBooksSidePaneViewModel.OnSelected();
    }

    public async Task BtnQbImport() {
      try {
				SessionStart();

        var qbFilePath = stn.QbFilePath();

        var header = new DefaultInvoiceHeaderModel {
          CustomerRefFullName = QuickBooksSidePaneViewModel.CustomerRefFullName, // "CLASS"
          //CustomerRefFullName = QuickBooksSidePaneViewModel.QbspModel.CustomerRefFullName.Payload, // "CLASS"
          ClassRefFullName = QuickBooksSidePaneViewModel.ClassRefFullName, // "Barrie Area:Barrie Corporate"
          TemplateRefFullName = QuickBooksSidePaneViewModel.SelectedTemplateRefFullName, 
          TxnDate = QuickBooksSidePaneViewModel.TxnDate,
					Other = QuickBooksSidePaneViewModel.Other,
				};

        header.ConvertEmptyToNull();


        var csvModel = MapDataTableToCsvModel(ImportViewModel.CsvData);

        await Task.Run(() => {
          var qbImport = new NxQbImportController(qbFilePath, header, csvModel);
          qbImport.Import();
        });

        ConsoleMessage = "Import has successfully completed";
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
			} finally {
        SessionEnd();
			}
		}

    private List<CsvModel> MapDataTableToCsvModel(DataTable dt) {
      // Throw if datatable is empty
      if (dt == null) {
        throw new ArgumentNullException(paramName: nameof(dt),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      if (dt.Rows.Count <= 0) {
        throw new ArgumentNullException(paramName: nameof(dt),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      // Throw if mandatory field isn't accounted for
      foreach (var attribute in QuickBooksSidePaneViewModel.QbspModel.Attr) {
        if (attribute.Value.IsMandatory == false) continue;
        if (string.IsNullOrEmpty(attribute.Value.ComboBox.SelectedItem)) {
          throw new ArgumentNullException(paramName: attribute.Value.Name,
            message: "No parameter specified for '" + attribute.Value.Name + "'.");
        }
      }

      var itemSelected = QuickBooksSidePaneViewModel.QbspModel.Attr["ItemRef"].ComboBox.SelectedItem;
      //var itemPayload = QuickBooksSidePaneViewModel.QbspModel.Attr["ItemRef"].Payload;
      var quantitySelected = QuickBooksSidePaneViewModel.QbspModel.Attr["Quantity"].ComboBox.SelectedItem;
      var rateSelected = QuickBooksSidePaneViewModel.QbspModel.Attr["Rate"].ComboBox.SelectedItem;

      // Todo: Make this less computationally expensive
      var convertedList = (from row in dt.AsEnumerable()
        select new CsvModel() {
          //Item = string.IsNullOrEmpty(itemSelected) ? null : Convert.ToString(row[itemSelected]),
          Item = GetRow(row, "Item"),
          Quantity = string.IsNullOrEmpty(quantitySelected) ? null : Convert.ToString(row[quantitySelected]),
          StaffName = Convert.ToString(row["StaffName"]),
          TimeInOut = Convert.ToString(row["TimeInOut"]),
          ServiceDate = Convert.ToString(row["ServiceDate"]),
          Rate = string.IsNullOrEmpty(rateSelected) ? null : Convert.ToString(row[rateSelected]),
        });

      return convertedList.ToList();
    }

    /// <summary>
    /// If a column name was specified, return that cell data from that row and column.
    /// Else if the static/constant payload from the textbox exists, use that as a constant.
    /// Else return null.
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

      string payload = null;

      if (attr is IQbStringAttribute) {
        var castAttr = (IQbStringAttribute) attr;
        payload = castAttr.Payload;
      }

      if (attr is IQbDateTimeAttribute) {
        var castAttr = (IQbDateTimeAttribute)attr;
        payload = Convert.ToString(castAttr.Payload);
      }

      if (!string.IsNullOrEmpty(payload)) {
        return payload;
      }

      return null;
    }

    private static string GetTemplate() {
      var hasTemplate = stn.QbInvHasTemplate();

      // Use template if preference is checked, else let DB.dll return ArgumentNullException
      var name = Properties.Settings.Default["StnQbInvTemplateName"].ToString();
      var template = hasTemplate ? name : null;
      return template;
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