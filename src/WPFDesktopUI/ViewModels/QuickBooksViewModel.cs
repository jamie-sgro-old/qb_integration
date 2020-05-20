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
				if (e.ParamName == "TemplateRefListID" || e.ParamName == "TemplateRefFullName") {
					ConsoleMessage = GetTemplateNull();
					return;
				}
			} catch (ArgumentException e) {
				if (e.ParamName == "TemplateRefFullName") {
					ConsoleMessage = GetTemplateWrong();
					return;
				}
				ConsoleMessage = "ParamName was caught by modelview";
			} catch (Exception e) {
				if (e.TargetSite.Name.ToString() == "BeginSession") {
					ConsoleMessage = GetBeginSessionFailed(e.Message);
					return;
				}

				// Default Behaviour
				ConsoleMessage = e.Message;
				Console.WriteLine(e.StackTrace);
			}
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

		private string GetBeginSessionFailed(string errMsg) {
			return errMsg + ". This could be an issue with your .QBW file.\n" +
						 "To resolve this issue:\n" +
							 "\t1. Click on the Settings Menu\n" +
								"\t2. Select Preferences\n" +
								"\t3. Go to the QuickBooks tab\n" +
								"\t4. Under 'File' locate the '.qbw File Location' section\n" +
								"\t5. Below the header: '.qbw File Location' verify the file path matches a valid QuickBooks company file\n" +
							  "\t\t Note: You can verify the integrity of the file by running it in QuickBooks Desktop\n" +
								"\t6. Click Close and try again" +
								"\t\t If the issue persists, please contact your system administrator.";
		}
	}
}
