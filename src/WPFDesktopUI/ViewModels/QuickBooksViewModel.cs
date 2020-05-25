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
		private bool _sessionBegin;
		private bool _hasTemplate;
		private string _template;

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

		private bool SessionBegin {
			get => _sessionBegin;
      set {
				_sessionBegin = value;
				CanBtnQbImport = !value;
				QbProgressBarIsVisible = value;
				NotifyOfPropertyChange(() => SessionBegin);
			}
		}

		private bool HasTemplate {
			get {
				_hasTemplate = (bool)Properties.Settings.Default["StnQbInvHasTemplate"];
				return _hasTemplate;
			}
		}

		private string Template {
			get
      {
				// Use template if preference is check, else let DB.dll return ArgumentNullException
        var name = Properties.Settings.Default["StnQbInvTemplateName"].ToString();
				_template = HasTemplate ? name : null;
        return _template;
      }
		}

		private string _qbFilePath;

		public string QbFilePath {
      get {
				_qbFilePath = Properties.Settings.Default["StnQbFilePath"].ToString();
				return _qbFilePath;
      }
		}


		#endregion Properties


		#region Methods

		public async Task BtnQbImport() {
			Console.WriteLine(QuickBooksSidePaneViewModel.HeaderDateTextBox);
			try {
				SessionBegin = true;
				ConsoleMessage = "Importing, please stand by...";

				var hasTemplate = HasTemplate;
				var template = Template;

        var header = new NxInvoiceHeaderModel {TemplateRefFullName = template};

        var qbFilePath = QbFilePath;

				await Task.Run(() => {
					var qbImport = new NxQbImportController(qbFilePath, header);
          qbImport.Import();
				});

				ConsoleMessage = "Import has successfully completed";
			}
			catch (ArgumentNullException e) {
				ConsoleMessage = ErrHandler.HandleArgumentNullException(e) ?? ErrHandler.GetDefaultError(e);
				return;
			} catch (ArgumentException e) {
				ConsoleMessage = ErrHandler.HandleArgumentException(e) ?? ErrHandler.GetDefaultError(e);
				return;
			} catch (System.Runtime.InteropServices.COMException e) {
				ConsoleMessage = ErrHandler.HandleCOMException(e) ?? ErrHandler.GetDefaultError(e);
				return;
			} catch (Exception e) {
				ConsoleMessage = ErrHandler.GetDefaultError(e);
				return;
			} finally {
				SessionBegin = false;
			}
		}

		#endregion Methods
	}
}
