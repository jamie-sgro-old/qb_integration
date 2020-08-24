using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  class AboutViewModel : IWindow {
    public string Title { get; set; } = "About";

    public void BtnClose() {
      Shutdown("About");
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
  }
}
