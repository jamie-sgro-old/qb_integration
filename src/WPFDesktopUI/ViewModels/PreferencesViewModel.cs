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

    /// <summary>
    /// Window close event triggered by user pressing the red 
    /// x on the top right corner of the screen
    /// </summary>
    public void OnClose() {
      SaveSettings();
    }

    public void BtnClose() {
      SaveSettings();
      Shutdown("Preferences");
    }

    private void SaveSettings() {
      Properties.Settings.Default.Save();
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

    #region McFactory

    public void General() {
        ActivateItem(new PreferencesGeneralViewModel());
    }

    public void CsvImport() {
      ActivateItem(new PreferencesCsvImportViewModel());
    }

    public void QuickBooks() {
      ActivateItem(new PreferencesQuickBooksViewModel());
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

    #endregion McFactory

  }
}
