using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Controllers {
  internal static class SettingsController {
    public static string QbFilePath() {
      return Properties.Settings.Default["StnQbFilePath"].ToString();
    }

    public static bool QbInvHasHeaderOther() {
      return (bool) Properties.Settings.Default["StnQbInvHasHeaderOther"];
    }

    public static bool QbInvHasHeaderOther1() {
      return (bool)Properties.Settings.Default["StnQbInvHasHeaderOther1"];
    }

    public static bool QbInvHasHeaderOther2() {
      return (bool)Properties.Settings.Default["StnQbInvHasHeaderOther2"];
    }

    public static string QbInvHeaderOtherName() {
      return Properties.Settings.Default["StnQbInvHeaderOtherName"].ToString();
    }

    public static string QbInvHeaderOtherName1() {
      return Properties.Settings.Default["StnQbInvHeaderOtherName1"].ToString();
    }

    public static string QbInvHeaderOtherName2() {
      return Properties.Settings.Default["StnQbInvHeaderOtherName2"].ToString();
    }

    public static bool QbInvHasTemplate() {
      return (bool)Properties.Settings.Default["StnQbInvHasTemplate"];
    }

    public static string QbInvTemplateName() {
      return Properties.Settings.Default["StnQbInvTemplateName"].ToString();
    }
  }
}
