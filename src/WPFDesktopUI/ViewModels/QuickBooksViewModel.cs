using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Screen {

		#region Properties

		private string _consoleMessage;
		private bool _canBtnQbImport = true;
		private bool _qbProgressBarIsVisible = false;
		private bool _sessionBegin;
		private bool _hasTemplate;
		private string _template;

		public string ConsoleMessage {
			get { return _consoleMessage; }
			set {
				_consoleMessage = value;
				NotifyOfPropertyChange(() => ConsoleMessage);
			}
		}

		public bool CanBtnQbImport {
			get { return _canBtnQbImport; }
			set { 
				_canBtnQbImport = value;
				NotifyOfPropertyChange(() => CanBtnQbImport);
			}
		}

		public bool QbProgressBarIsVisible {
			get { return _qbProgressBarIsVisible; }
			set {
				_qbProgressBarIsVisible = value;
				NotifyOfPropertyChange(() => QbProgressBarIsVisible);
			}
		}

		private bool SessionBegin {
			get { return _sessionBegin; }
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
			get {
				// Use template if preference is check, else let DB.dll return ArgumentNullException
				if (HasTemplate) {
					_template = Properties.Settings.Default["StnQbInvTemplateName"].ToString();
				}
				else {
					_template = null;
				}
				return _template;
			}
		}

		#endregion Properties


		#region Methods

		public async Task BtnQbImport() {
			try {
				SessionBegin = true;
				ConsoleMessage = "Importing, please stand by...";

				bool hasTemplate = HasTemplate;
				string template = Template;
				string qbFilePath = Properties.Settings.Default["StnQbFilePath"].ToString();

				// We currently are testing on: "NEXIM's Invoice with credits &"
				await Task.Run(() => {
					QbImportController.Import(qbFilePath, template);
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
