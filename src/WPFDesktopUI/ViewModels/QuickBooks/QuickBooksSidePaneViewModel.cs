using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MCBusinessLogic.Controllers;
using WPFDesktopUI.Controllers;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : Screen {
		private string _otherHeaderTextBlock;
		private bool _hasHeaderOther;
		private string _headerOtherTextBox;
		private DateTime _headerDateTextBox = DateTime.Now;
    private string _classRefFullName;
    private List<string> _templateRefFullName = new List<string> {""};

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



    public bool HasHeaderOther {
			get {
				_hasHeaderOther = (bool)Properties.Settings.Default["StnQbInvHasHeaderOther"];
				return _hasHeaderOther;
			}
		}


		public string HeaderOtherTextBlock {
			get
      {
        var name = Properties.Settings.Default["StnQbInvHeaderOtherName"].ToString();
				_otherHeaderTextBlock = HasHeaderOther ? name : "OTHER";
        return _otherHeaderTextBlock;
      }
		}

    public async void QbImport() {
      var qbFilePath = SettingsController.GetQbFilePath();
      TemplateRefFullName = await InitTemplateRefFullName(qbFilePath);
    }

    /// <summary>
    /// Returns a list of templates used in QuickBooks based on their name
    /// </summary>
    /// <param name="qbFilePath">The full path for the QuickBooks file</param>
    /// <returns></returns>
    private static async Task<List<string>> InitTemplateRefFullName(string qbFilePath) {
      var qbExportController = new QbExportController(qbFilePath);
      var templates = await Task.Run(() => qbExportController.GetTemplateNamesList());
      templates.Insert(0, "");
      return templates;
    }
  }
}
