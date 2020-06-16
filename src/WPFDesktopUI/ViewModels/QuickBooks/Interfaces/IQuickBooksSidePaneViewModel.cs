using System;
using System.ComponentModel;
using Caliburn.Micro;
using WPFDesktopUI.Models;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public interface IQuickBooksSidePaneViewModel {
    IQuickBooksSidePaneModel QbspModel { get; set; }
    bool CanQbExport { get; set; }
    bool QbProgressBarIsVisible { get; set; }
    string ConsoleMessage { get; set; }

    void OnSelected();

    /// <summary>
    /// Executed when 'Query QuickBooks' button is pressed
    /// Connect to QB and get all data needed to populate smart dropdowns
    /// i.e. to decide which template to use, the user should decide from a list
    /// of actual templates used in QB, thus the combobox needs a list of
    /// template strings
    /// </summary>
    void QbExport();

    event PropertyChangedEventHandler PropertyChanged;
  }
}