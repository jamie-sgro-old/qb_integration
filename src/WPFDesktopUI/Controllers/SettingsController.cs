using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Controllers {
  internal static class SettingsController {
    public static string GetQbFilePath() {
      return Properties.Settings.Default["StnQbFilePath"].ToString();
    }
  }
}
