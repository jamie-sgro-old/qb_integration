using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.Micro;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public interface IQuickBooksSidePaneViewModel : ITabComponent, IQbInteractable {
    IQuickBooksSidePaneModel QbspModel { get; set; }

    event PropertyChangedEventHandler PropertyChanged;
    void AutopopulateComboBoxes(List<string> csvHeaders);
  }
}