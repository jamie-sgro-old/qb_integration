using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.ViewModels.Interfaces;
using WPFDesktopUI.ViewModels.QuickBooks;

namespace WPFDesktopUI.ViewModels {
  public interface IQuickBooksViewModel : IMainTab, IQbInteractable {
    IQuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }
    
    event PropertyChangedEventHandler PropertyChanged;
  }
}