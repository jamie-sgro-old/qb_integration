using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDesktopUI.ViewModels.Preferences {
  public class PreferencesQuickBooksViewModel : Screen {
    /*private string _qbFilePath;

    public string QbFilePath {
      get { return _qbFilePath; }
      set {
        _qbFilePath = value;
        NotifyOfPropertyChange(() => QbFilePath);
      }
    }*/

    public void BtnOpenQbwFile() {
      var fileName = FileSystemHelper.GetFilePath("Quickbooks |*.qbw");
      if (fileName != "") {
        Properties.Settings.Default["StnQbFilePath"] = fileName;
      }
    }
  }
}
