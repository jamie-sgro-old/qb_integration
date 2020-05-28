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

		public async Task BtnQbImport() {
			Console.WriteLine(QuickBooksSidePaneViewModel.HeaderDateTextBox);
      try {
        SessionStart();

        var template = GetTemplate();

        var header = new NxInvoiceHeaderModel {
          TemplateRefFullName = template, 
          TxnDate = QuickBooksSidePaneViewModel.HeaderDateTextBox,
					Other = QuickBooksSidePaneViewModel.HeaderOtherTextBox,
				};

        var qbFilePath = GetQbFilePath();

        await Task.Run(() => {
          var qbImport = new NxQbImportController(qbFilePath, header);
          qbImport.Import();
        });

      }
      catch (ArgumentNullException e) {
        ConsoleMessage = ErrHandler.HandleArgumentNullException(e) ?? ErrHandler.GetDefaultError(e);
      } catch (ArgumentOutOfRangeException e) {
        ConsoleMessage = ErrHandler.HandleArgumentOutOfRangeException(e) ?? ErrHandler.GetDefaultError(e);
			} catch (ArgumentException e) {
				ConsoleMessage = ErrHandler.HandleArgumentException(e) ?? ErrHandler.GetDefaultError(e);
			} catch (System.Runtime.InteropServices.COMException e) {
				ConsoleMessage = ErrHandler.HandleCOMException(e) ?? ErrHandler.GetDefaultError(e);
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

		private static string GetQbFilePath() {
      return Properties.Settings.Default["StnQbFilePath"].ToString();
    }

    private void SessionStart() {
      CanBtnQbImport = false;
      QbProgressBarIsVisible = true;
      ConsoleMessage = "Importing, please stand by...";
    }

    private void SessionEnd() {
      CanBtnQbImport = true;
      QbProgressBarIsVisible = false;
      ConsoleMessage = "Import has successfully completed";
    }

		#endregion Methods
	}
}
