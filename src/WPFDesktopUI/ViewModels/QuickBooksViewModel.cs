using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using System;
using System.Collections.Generic;
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

    public static List<CsvModel> CsvData { get; set; }

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

		public async Task BtnQbImport() {
      try {
				SessionStart();

        var qbFilePath = SettingsController.GetQbFilePath();

        var header = new NxInvoiceHeaderModel {
          ClassRefFullName = QuickBooksSidePaneViewModel.ClassRefFullName, // "Barrie Area:Barrie Corporate"
          TemplateRefFullName = QuickBooksSidePaneViewModel.SelectedTemplateRefFullName, 
          TxnDate = QuickBooksSidePaneViewModel.TxnDate,
					Other = QuickBooksSidePaneViewModel.Other,
				};

        if (CsvData == null) {
          throw new ArgumentNullException(paramName: nameof(CsvData),
            message: "No Invoice lineItems were supplied. " +
                     "The Importer was expecting at least 1.");
        }

        await Task.Run(() => {
          var qbImport = new NxQbImportController(qbFilePath, header, CsvData);
          qbImport.Import();
        });

        ConsoleMessage = "Import has successfully completed";
			} catch (ArgumentNullException e) {
        ConsoleMessage = ErrHandler.Handle(e) ?? ErrHandler.GetDefaultError(e);
      } catch (ArgumentOutOfRangeException e) {
        ConsoleMessage = ErrHandler.Handle(e) ?? ErrHandler.GetDefaultError(e);
      } catch (ArgumentException e) {
        ConsoleMessage = ErrHandler.Handle(e) ?? ErrHandler.GetDefaultError(e);
      } catch (System.Runtime.InteropServices.COMException e) {
        ConsoleMessage = ErrHandler.Handle(e) ?? ErrHandler.GetDefaultError(e);
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.GetDefaultError(e);
			} finally {
        SessionEnd();
			}
		}

    private static string GetTemplate() {
      var hasTemplate = (bool)Properties.Settings.Default["StnQbInvHasTemplate"];

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
