using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.ViewModels.Interfaces;
using WPFDesktopUI.ViewModels.QuickBooks;

namespace WPFDesktopUI.ViewModels {
  public interface IQuickBooksViewModel : IMainTab {
    IQuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }
    string ConsoleMessage { get; set; }
    bool CanBtnQbImport { get; set; }
    bool QbProgressBarIsVisible { get; set; }



    event PropertyChangedEventHandler PropertyChanged;
    void OnSelected();
    Task BtnQbImport();
  }
}