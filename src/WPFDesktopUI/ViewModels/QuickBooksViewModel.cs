using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using stn = WPFDesktopUI.Controllers.SettingsController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDesktopUI.ViewModels.QuickBooks;
using MCBusinessLogic.Models;
using WPFDesktopUI.Controllers;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Conductor<object> {

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

        var header = new NxInvoiceHeaderModel {
          ClassRefFullName = QuickBooksSidePaneViewModel.ClassRefFullName, // "Barrie Area:Barrie Corporate"
          TemplateRefFullName = QuickBooksSidePaneViewModel.SelectedTemplateRefFullName, 
          TxnDate = QuickBooksSidePaneViewModel.TxnDate,
					Other = QuickBooksSidePaneViewModel.Other,
				};

        var csvData = ImportViewModel.CsvData;
        if (csvData == null) {
          throw new ArgumentNullException(paramName: nameof(csvData),
            message: "No Invoice lineItems were supplied. " +
                     "The Importer was expecting at least 1.");
        }

        var csvModel = MapDataTableToCsvModel(csvData);

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
      var convertedList = (from rw in dt.AsEnumerable()
        select new CsvModel() {
          Item = Convert.ToString(rw[QuickBooksSidePaneViewModel.SelectedItemRef]),
          Quantity = Convert.ToString(rw["Quantity"]),
          StaffName = Convert.ToString(rw["StaffName"]),
          TimeInOut = Convert.ToString(rw["TimeInOut"]),
          ServiceDate = Convert.ToString(rw["ServiceDate"]),
          Rate = Convert.ToString(rw["Rate"]),
        });

      return convertedList.ToList();
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