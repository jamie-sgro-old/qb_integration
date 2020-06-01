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

    public static string QbInvHeaderOtherName() {
      return Properties.Settings.Default["StnQbInvHeaderOtherName"].ToString();
    }

    public static bool QbInvHasTemplate() {
      return (bool)Properties.Settings.Default["StnQbInvHasTemplate"];
    }
  }
}
