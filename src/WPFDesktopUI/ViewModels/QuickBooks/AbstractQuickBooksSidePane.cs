using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.Micro;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {

  public abstract class AbstractQuickBooksSidePane : Screen {

    public bool CanQbExport { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;
    public string ConsoleMessage { get; set; }  = "Please select 'Query QuickBooks' before custom lists can be generated";

    //TODO: 
    // BillAddressAddr1
    // ShipAddressAddr1
    // also add check for name change from preferences for things like "OTHER"
    // Other1 (same as above)
    // Other2 (same as above)
    // Desc
    // make sure all mappable columns convert "" into null


  }
}
