using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers;
using WPFDesktopUI.ViewModels.Interfaces;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : Screen, IMainTab {
		private string _otherHeaderTextBlock;
		private bool _hasHeaderOther;
		private string _headerOtherTextBox;
		private DateTime _headerDateTextBox = DateTime.Now;
    private string _classRefFullName;
    private List<string> _templateRefFullName = new List<string> {""};
    private bool _canTemplateRefFullName = false;
    private string _selectedTemplateRefFullName;
    private bool _canQbExport = true;
    private bool _qbProgressBarIsVisible = false;
    private string _btnNotification = "Please select 'Query QuickBooks' before custom lists can be generated";
    private List<string> _itemRef;
    private string _selectedItemRef;
    private bool _canItemRef;

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


    public bool QbProgressBarIsVisible {
      get => _qbProgressBarIsVisible;
      set {
        _qbProgressBarIsVisible = value;
        NotifyOfPropertyChange(() => QbProgressBarIsVisible);
      }
    }

    public bool CanItemRef {
      get => _canItemRef;
      set {
        _canItemRef = value;
        NotifyOfPropertyChange(() => CanItemRef);
      }
    }


    public List<string> ItemRef {
      get => _itemRef;
      set {
        _itemRef = value;
        NotifyOfPropertyChange(() => ItemRef);
      }
    }

    public string SelectedItemRef {
      get { return _selectedItemRef; }
      set {
        _selectedItemRef = value;
        NotifyOfPropertyChange(() => SelectedItemRef);
      }
    }

    private string _consoleMessage;

    public string ConsoleMessage {
      get => _consoleMessage;
      set {
        _consoleMessage = value;
        NotifyOfPropertyChange(() => ConsoleMessage);
      }
    }




    public void OnSelected() {
      var csvData = ImportViewModel.CsvData;
      if (csvData == null) return;
      ItemRef = GetCsvHeaders(csvData);
      CanItemRef = true;
    }

    public async void QbExport() {
      SessionStart();
      var qbFilePath = stn.QbFilePath();
      try {
        TemplateRefFullName = await InitTemplateRefFullName(qbFilePath);
        SessionEnd();
      } catch (Exception e) {
        ConsoleMessage = QbImportExceptionHandler.DelegateHandle(e);
      } finally {
        CanQbExport = true;
        QbProgressBarIsVisible = false;
      }
    }

    private void SessionStart() {
      CanQbExport = false;
      QbProgressBarIsVisible = true;
    }

    private void SessionEnd() {
      CanTemplateRefFullName = true;
      ConsoleMessage = "Query successfully completed";
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

    private static List<string> GetCsvHeaders(DataTable dt) {
      string[] columnHeaders = dt?.Columns.Cast<DataColumn>()
        .Select(x => x.ColumnName)
        .ToArray();

      return columnHeaders?.ToList();
    }
  }
}
