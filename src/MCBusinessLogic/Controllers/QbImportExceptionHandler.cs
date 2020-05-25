using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public static class QbImportExceptionHandler {

		public static string HandleArgumentNullException(ArgumentNullException e) {
			if (e.ParamName == "TemplateRefListID" || e.ParamName == "TemplateRefFullName") {
				return GetTemplateNull();
			}
			return null;
		}

    public static string HandleArgumentOutOfRangeException(ArgumentOutOfRangeException e) {
      if (e.ParamName == "lineItems") {
        return GetLineItemsOutOfRange(e.Message);
      }
      if (e.ParamName == "responseList") {
        throw new ArgumentOutOfRangeException(
          message: "More than one QuickBooks request was "+
                   "provided when only one was expected.",
					innerException: e);
      }
			return null;
		}

		public static string HandleArgumentException(ArgumentException e) {
			if (e.ParamName == "TemplateRefFullName") {
				return GetTemplateWrong();
			}
			return "An unhandled ArgumentNullException was caught by modelview";
		}

		public static string HandleCOMException(System.Runtime.InteropServices.COMException e) {
			if (e.Source == "QBXMLRP2.RequestProcessor.2") {
				if (e.TargetSite.DeclaringType.FullName == "QBFC13Lib.IQBSessionManager") {
					if (e.TargetSite.Name.ToString() == "BeginSession") {
						return HandleBeginSessionFailed(e) ?? GetDefaultError(e);
					}
				}
			}
			return null;
		}

		public static string GetDefaultError(Exception e) {
			Console.WriteLine(e.GetType());
			Console.WriteLine(e.StackTrace);
			return e.Message;
		}

		private static string HandleBeginSessionFailed(Exception e) {
			if (e.Message == "Could not start QuickBooks.") {
				return QbNoStart(e.Message);
			}

			bool isPermissionErr = e.Message.StartsWith(GetPemSrcErrMsg());
			if (isPermissionErr) {
				return GetPermissionError(e.Message);
			}

			bool isDiffCompanyFileErr = e.Message.StartsWith(GetDiffCompanySrcErrMsg());
			if (isDiffCompanyFileErr) {
				return GetDiffCompanyError(e.Message);
			}

			return null;
		}
    private static string GetPermissionError(string errMsg) {
			return errMsg;
		}
		private static string GetDiffCompanyError(string errMsg) {
			return errMsg;
		}
		private static string GetPemSrcErrMsg() {
			return "This application is unable to log into this QuickBooks company" +
					" data file automatically. The QuickBooks administrator must grant permission" +
					" for an automatic login through the Integrated Application preferences.";
		}
		private static string GetDiffCompanySrcErrMsg() {
			return "A QuickBooks company data file is already open" +
				" and it is different from the one requested or there" +
				" are multiple company files open.";
		}
		private static string GetTemplateNull() {
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
    private static string GetLineItemsOutOfRange(string errMsg) {
      return errMsg;
    }
    private static string GetTemplateWrong() {
			return "Could not complete import. Could not find template name in QuickBooks template list.\n" +
						 "To resolve this issue:\n" +
							 "\t1. Click on the Settings Menu\n" +
								"\t2. Select Preferences\n" +
								"\t3. Go to the QuickBooks tab\n" +
								"\t4. Under 'Invoice' check 'Using Custom Template'\n" +
								"\t5. Beside 'Template Name' write the name of the QuickBooks template" +
				 						" name you wish to use on import\n" +
								"\t6. Click Close";
		}
		private static string QbNoStart(string errMsg) {
			return errMsg + " This could be an issue with your .QBW file.\n" +
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

	}
}
