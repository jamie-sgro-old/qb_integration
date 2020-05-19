using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDesktopUI.ViewModels.Preferences {
  public class PreferencesCsvImportViewModel : Screen {
    public void CommaSeparation() {
      Properties.Settings.Default["StnCsvSeparation"] = ",";
    }

    public void TabSeparation() {
      Properties.Settings.Default["StnCsvSeparation"] = "\t";
    }

    public bool CommaSeparationIsChecked {
      get => Properties.Settings.Default["StnCsvSeparation"].ToString() == ",";
    }

    public bool TabSeparationIsChecked {
      get => Properties.Settings.Default["StnCsvSeparation"].ToString() == "\t";
    }

    public bool CustomSeparationIsChecked {
      get => Properties.Settings.Default["StnCsvSeparation"].ToString() != "," &&
             Properties.Settings.Default["StnCsvSeparation"].ToString() != "\t";
    }
  }
}
