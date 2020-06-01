using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MCBusinessLogic.Controllers;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : Screen {
		private string _otherHeaderTextBlock;
		private bool _hasHeaderOther;
		private string _headerOtherTextBox;
		private DateTime _headerDateTextBox = DateTime.Now;
    private string _classRefFullName;
    private List<string> _templateRefFullName = new List<string> {""};
    private bool _canTemplateRefFullName = false;
    private string _selectedTemplateRefFullName;
    private bool _canQbExport = true;

    public string Other {
			get => _headerOtherTextBox;
      set {
        _headerOtherTextBox = value;
        NotifyOfPropertyChange(() => Other);
			}
		}
    
    
		public DateTime TxnDate {
			get => _headerDateTextBox;
      set {
				_headerDateTextBox = value;
				NotifyOfPropertyChange(() => TxnDate);
			}
		}

    public string ClassRefFullName {
      get => _classRefFullName;
      set {
        _classRefFullName = value;
        NotifyOfPropertyChange(() => ClassRefFullName);
			}
		}


    public bool CanTemplateRefFullName {
      get => _canTemplateRefFullName;
      set {
        _canTemplateRefFullName = value;
        NotifyOfPropertyChange(() => CanTemplateRefFullName);
      }
    }

    public List<string> TemplateRefFullName {
      get => _templateRefFullName;
      set {
        _templateRefFullName = value; 
				NotifyOfPropertyChange(() => TemplateRefFullName);
      }
    }


    public string SelectedTemplateRefFullName {
      get => _selectedTemplateRefFullName;
      set {
        _selectedTemplateRefFullName = value;
        NotifyOfPropertyChange(() => SelectedTemplateRefFullName);
      }
    }



    public bool HasHeaderOther {
			get {
        _hasHeaderOther = stn.QbInvHasHeaderOther();
        return _hasHeaderOther;
			}
		}


		public string HeaderOtherTextBlock {
			get {
        var name = stn.QbInvHeaderOtherName();
        _otherHeaderTextBlock = HasHeaderOther ? name : "OTHER";
        return _otherHeaderTextBlock;
      }
		}


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

    private string _btnNotification = "Please select 'Query QuickBooks' before custom lists can be generated";

    public string BtnNotification {
      get => _btnNotification;
      set {
        _btnNotification = value;
        NotifyOfPropertyChange(() => BtnNotification);
      }
    }



    public async void QbExport() {
      SessionStart();
      var qbFilePath = stn.QbFilePath();
      TemplateRefFullName = await InitTemplateRefFullName(qbFilePath);
      SessionEnd();
    }

    private void SessionStart() {
      CanQbExport = false;
      QbProgressBarIsVisible = true;
    }

    private void SessionEnd() {
      CanTemplateRefFullName = true;
      CanQbExport = true;
      QbProgressBarIsVisible = false;
      BtnNotification = "";
    }

    /// <summary>
    /// Returns a list of templates used in QuickBooks based on their name
    /// </summary>
    /// <param name="qbFilePath">The full path for the QuickBooks file</param>
    /// <returns></returns>
    private static async Task<List<string>> InitTemplateRefFullName(string qbFilePath) {
      var qbExportController = new QbExportController(qbFilePath);
      var templates = await Task.Run(() => {
        return qbExportController.GetTemplateNamesList();
      });
      templates.Insert(0, "");
      return templates;
    }
  }
}
