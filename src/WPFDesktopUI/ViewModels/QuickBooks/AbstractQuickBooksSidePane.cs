using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.Micro;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public abstract class AbstractQuickBooksSidePane : Screen {
    public event PropertyChangedEventHandler PropertyChanged;

    public string Other { get; set; }
    public DateTime TxnDate { get; set; } = DateTime.Now;
    public string ClassRefFullName { get; set; }
    public bool CanTemplateRefFullName { get; set; } = false;
    public List<string> TemplateRefFullName { get; set; } = new List<string> { "" };
    public string SelectedTemplateRefFullName { get; set; }

    private string _headerOtherTextBlock;
    public string HeaderOtherTextBlock {
      get => _headerOtherTextBlock;
      set {
        var name = value;
        _headerOtherTextBlock = stn.QbInvHasHeaderOther() ? name : "OTHER";
        NotifyOfPropertyChange(() => HeaderOtherTextBlock);
      }
    }

    public bool CanQbExport { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;
    public bool CanCsvHeader { get; set; }
    public List<string> ItemRef { get; set; }
    public string SelectedItemRef { get; set; }
    public List<string> Quantity { get; set; }
    public string SelectedQuantity { get; set; }
    public List<string> Rate { get; set; }

    public string SelectedRate { get; set; }
    public string ConsoleMessage { get; set; }  = "Please select 'Query QuickBooks' before custom lists can be generated";
    public string CustomerRefFullName { get; set; }


    //TODO: 
    // BillAddressAddr1
    // ShipAddressAddr1
    // also add check for name change from preferences for things like "OTHER"
    // Other1 (same as above)
    // Other2 (same as above)
    // Desc
    // make sure all mapable columns convert "" into null


  }
}
