using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using stn = WPFDesktopUI.Controllers.SettingsController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WPFDesktopUI.ViewModels.QuickBooks;
using MCBusinessLogic.Models;
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
      if (string.IsNullOrEmpty(QuickBooksSidePaneViewModel.SelectedItemRef)) {
        throw new ArgumentNullException(paramName: nameof(QuickBooksSidePaneViewModel.SelectedItemRef),
          message: "No parameter specified for 'ITEM'.");
      }

      var item = QuickBooksSidePaneViewModel.SelectedItemRef;
      var quantity = QuickBooksSidePaneViewModel.SelectedQuantity;
      var rate = QuickBooksSidePaneViewModel.SelectedRate;

      // Todo: Make this less computationally expensive
      var convertedList = (from row in dt.AsEnumerable()
        select new CsvModel() {
          Item = string.IsNullOrEmpty(item) ? null : Convert.ToString(row[item]),
          Quantity = string.IsNullOrEmpty(quantity) ? null : Convert.ToString(row[quantity]),
          StaffName = Convert.ToString(row["StaffName"]),
          TimeInOut = Convert.ToString(row["TimeInOut"]),
          ServiceDate = Convert.ToString(row["ServiceDate"]),
          Rate = string.IsNullOrEmpty(rate) ? null : Convert.ToString(row[rate]),
        });
      var a = convertedList.ToList();

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