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
				if (!hasTemplate) {
					ConsoleMessage = "Could not complete import. No QuickBooks invoice template has been specified.\n"+
														 "To resolve this issue:\n"+
														 "\t1. Click on the Settings Menu\n"+
														 "\t2. Select Preferences\n"+
														 "\t3. Go to the QuickBooks tab\n"+
														 "\t4. Under 'Invoice' check 'Using Custom Template'\n"+
														 "\t5. Beside 'Template Name' write the name of the QuickBooks template"+
																	"name you wish to use on import\n"+
														 "\t6. Click Close";

					return;
				}

				string template = Properties.Settings.Default["StnQbInvTemplateName"].ToString();
				// "NEXIM's Invoice with credits &"
				QbImportController.Import(template);
				ConsoleMessage = "Import has successfully completed";
			}
			catch (Exception e) {
				ConsoleMessage = e.Message;
				Console.WriteLine(e.StackTrace);
			}
		}


	}
}
