using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFDesktopUI.ViewModels.Preferences;

namespace WPFDesktopUI.ViewModels {
  public sealed class PreferencesViewModel : Conductor<object> {

    #region Button

    public void BtnClose() {
      Properties.Settings.Default.Save();
      Shutdown("Preferences");
    }

    /// <summary>
    /// Close a child window based on a matching title string
    /// </summary>
    /// <param name="title">The xaml title for the window elemen</param>
    private void Shutdown(string title) {
      foreach (Window win in App.Current.Windows) {
        if (win.Title.ToString() == title) {
          win.Close();
        }
      }
    }

    #endregion Button


    // This will init first
    public void PreferencesList(object sender, RoutedEventArgs e) {
    }

    #region Factory

    public void General() {
        ActivateItem(new PreferencesGeneralViewModel());
    }

    public void CsvImport() {
      ActivateItem(new PreferencesCsvImportViewModel());
    }

    public void Default() {
      ActivateItem(new PreferencesGeneralViewModel());
    }

    public bool CanNextItem() {
      return false;
    }

    public void NextItem() {
      throw new NotImplementedException();
    }

    #endregion Factory

  }
}
