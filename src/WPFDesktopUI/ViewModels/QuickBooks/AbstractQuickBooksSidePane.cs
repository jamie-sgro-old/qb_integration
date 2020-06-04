using System;
using System.Collections.Generic;
using Caliburn.Micro;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public abstract class AbstractQuickBooksSidePane : Screen {

    private string _other;
    public string Other {
      get => _other;
      set {
        _other = value;
        NotifyOfPropertyChange(() => Other);
      }
    }

    private DateTime _headerDateTextBox = DateTime.Now;
    public DateTime TxnDate {
      get => _headerDateTextBox;
      set {
        _headerDateTextBox = value;
        NotifyOfPropertyChange(() => TxnDate);
      }
    }

    private string _classRefFullName;
    public string ClassRefFullName {
      get => _classRefFullName;
      set {
        _classRefFullName = value;
        NotifyOfPropertyChange(() => ClassRefFullName);
      }
    }

    private bool _canTemplateRefFullName = false;
    public bool CanTemplateRefFullName {
      get => _canTemplateRefFullName;
      set {
        _canTemplateRefFullName = value;
        NotifyOfPropertyChange(() => CanTemplateRefFullName);
      }
    }

    private List<string> _templateRefFullName = new List<string> { "" };
    public List<string> TemplateRefFullName {
      get => _templateRefFullName;
      set {
        _templateRefFullName = value;
        NotifyOfPropertyChange(() => TemplateRefFullName);
      }
    }

    private string _selectedTemplateRefFullName;
    public string SelectedTemplateRefFullName {
      get => _selectedTemplateRefFullName;
      set {
        _selectedTemplateRefFullName = value;
        NotifyOfPropertyChange(() => SelectedTemplateRefFullName);
      }
    }

    private bool _hasHeaderOther;
    public bool HasHeaderOther {
      get {
        _hasHeaderOther = stn.QbInvHasHeaderOther();
        return _hasHeaderOther;
      }
    }

    private string _headerOtherTextBlock;
    public string HeaderOtherTextBlock {
      get => _headerOtherTextBlock;
      set {
        var name = value;
        _headerOtherTextBlock = HasHeaderOther ? name : "OTHER";
        NotifyOfPropertyChange(() => HeaderOtherTextBlock);
      }
    }

    private bool _canQbExport = true;
    public bool CanQbExport {
      get { return _canQbExport; }
      set {
        _canQbExport = value;
        NotifyOfPropertyChange(() => CanQbExport);
      }
    }

    private bool _qbProgressBarIsVisible = false;
    public bool QbProgressBarIsVisible {
      get => _qbProgressBarIsVisible;
      set {
        _qbProgressBarIsVisible = value;
        NotifyOfPropertyChange(() => QbProgressBarIsVisible);
      }
    }

    private bool _canCsvHeader;
    public bool CanCsvHeader {
      get => _canCsvHeader;
      set {
        _canCsvHeader = value;
        NotifyOfPropertyChange(() => CanCsvHeader);
      }
    }

    private List<string> _itemRef;
    public List<string> ItemRef {
      get => _itemRef;
      set {
        _itemRef = value;
        NotifyOfPropertyChange(() => ItemRef);
      }
    }

    private string _selectedItemRef;
    public string SelectedItemRef {
      get { return _selectedItemRef; }
      set {
        _selectedItemRef = value;
        NotifyOfPropertyChange(() => SelectedItemRef);
      }
    }

    private List<string> _quantity;

    public List<string> Quantity {
      get => _quantity;
      set {
        _quantity = value;
        NotifyOfPropertyChange(() => Quantity);
      }
    }

    private string _selectedQuantity;

    public string SelectedQuantity {
      get => _selectedQuantity;
      set {
        _selectedQuantity = value;
        NotifyOfPropertyChange(() => SelectedQuantity);
      }
    }

    private List<string> _rate;

    public List<string> Rate {
      get => _rate;
      set {
        _rate = value;
        NotifyOfPropertyChange(() => Rate);
      }
    }

    private string _selectedRate;

    public string SelectedRate {
      get => _selectedRate;
      set {
        _selectedRate = value;
        NotifyOfPropertyChange(() => SelectedRate);
      }
    }


    private string _consoleMessage = "Please select 'Query QuickBooks' before custom lists can be generated";
    public string ConsoleMessage {
      get => _consoleMessage;
      set {
        _consoleMessage = value;
        NotifyOfPropertyChange(() => ConsoleMessage);
      }
    }

    private string _customerRefFullName;

    public string CustomerRefFullName {
      get => _customerRefFullName;
      set {
        _customerRefFullName = value;
        NotifyOfPropertyChange(() => CustomerRefFullName);
      }
    }


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
