using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Screen {

		private string _consoleMessage;

		public string ConsoleMessage {
			get { return _consoleMessage; }
			set {
				_consoleMessage = value;
				NotifyOfPropertyChange(() => ConsoleMessage);

			}
		}

		public void BtnQbImport() {
			try {
				ConsoleMessage = "Importing, please stand by...";

				bool hasTemplate = Convert.ToBoolean(Properties.Settings.Default["StnQbInvHasTemplate"]);

				// Use template if preference is check, else let DB.dll return ArgumentNullException
				string template;
				if (hasTemplate) {
					template = Properties.Settings.Default["StnQbInvTemplateName"].ToString();
				} else {
					template = null;
				}

				string qbFilePath = Properties.Settings.Default["StnQbFilePath"].ToString();

				// We currently are testing on: "NEXIM's Invoice with credits &"
				QbImportController.Import(qbFilePath, template);
				ConsoleMessage = "Import has successfully completed";
			}
			catch (ArgumentNullException e) {
				ConsoleMessage = HandleArgumentNullException(e) ?? GetDefaultError(e);
				return;
			} catch (ArgumentException e) {
				ConsoleMessage = HandleArgumentException(e) ?? GetDefaultError(e);
				return;
			} catch (System.Runtime.InteropServices.COMException e) {
				ConsoleMessage = HandleCOMException(e) ?? GetDefaultError(e);
				return;
			} catch (Exception e) {
				ConsoleMessage = GetDefaultError(e);
				return;
			}
		}

		private string HandleArgumentNullException(ArgumentNullException e) {
			if (e.ParamName == "TemplateRefListID" || e.ParamName == "TemplateRefFullName") {
				return GetTemplateNull();
			}
			return null;
		}

		private string HandleArgumentException(ArgumentException e) {
			if (e.ParamName == "TemplateRefFullName") {
				return GetTemplateWrong();
			}
			return "An unhandled ArgumentNullException was caught by modelview";
		}

		private string HandleCOMException(System.Runtime.InteropServices.COMException e) {
			if (e.Source == "QBXMLRP2.RequestProcessor.2") {
				if (e.TargetSite.DeclaringType.FullName == "QBFC13Lib.IQBSessionManager") {
					if (e.TargetSite.Name.ToString() == "BeginSession") {
						return HandleBeginSessionFailed(e) ?? GetDefaultError(e);
					}
				}
			}
			return null;
		}

		private string HandleBeginSessionFailed(Exception e) {
			if (e.Message == "Could not start QuickBooks.") {
				return QbNoStart(e.Message);
			}

			string pemErr = GetPemSrcErrMsg();
			bool isPermissionErr = e.Message.Substring(0, pemErr.Count()) == pemErr;
			if (isPermissionErr) {
				return GetPermissionError(e.Message);
			}
			return null;
		}

		private string GetPermissionError(string errMsg) {
			return errMsg;
		}

		private string GetPemSrcErrMsg() {
			return "This application is unable to log into this QuickBooks company" +
					" data file automatically. The QuickBooks administrator must grant permission" +
					" for an automatic login through the Integrated Application preferences.";
		}

		private string GetTemplateNull() {
			return "Could not complete import. No QuickBooks invoice template has been specified.\n" +
					   "To resolve this issue:\n" +
							 "\t1. Click on the Settings Menu\n" +
				 			 "\t2. Select Preferences\n" +
				 			 "\t3. Go to the QuickBooks tab\n" +
				 			 "\t4. Under 'Invoice' check 'Using Custom Template'\n" +
				 			 "\t5. Beside 'Template Name' write the name of the QuickBooks template" +
				 					 " name you wish to use on import\n" +
				 			 "\t6. Click Close";
		}

		private string GetTemplateWrong() {
			return "Could not complete import. Could not find '" + 
						  Properties.Settings.Default["StnQbInvTemplateName"].ToString() +
						 "' in QuickBooks template list.\n" +
						 "To resolve this issue:\n" +
							 "\t1. Click on the Settings Menu\n" +
								"\t2. Select Preferences\n" +
								"\t3. Go to the QuickBooks tab\n" +
								"\t4. Under 'Invoice' check 'Using Custom Template'\n" +
								"\t5. Beside 'Template Name' write the name of the QuickBooks template" +
				 						" name you wish to use on import\n" +
								"\t6. Click Close";
		}

		private string QbNoStart(string errMsg) {
			return errMsg + ". This could be an issue with your .QBW file.\n" +
						 "To resolve this issue:\n" +
							 "\t1. Click on the Settings Menu\n" +
								"\t2. Select Preferences\n" +
								"\t3. Go to the QuickBooks tab\n" +
								"\t4. Under 'File' locate the '.qbw File Location' section\n" +
								"\t5. Below the header: '.qbw File Location' verify the file path matches a valid QuickBooks company file\n" +
							  "\t\t Note: You can verify the integrity of the file by running it in QuickBooks Desktop\n" +
								"\t6. Click Close and try again\n" +
								"\t\t If the issue persists, please contact your system administrator.";
		}

		private string GetDefaultError(Exception e) {
			Console.WriteLine(e.GetType());
			Console.WriteLine(e.StackTrace);
			return e.Message;
		}
	}
}
