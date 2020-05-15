using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen {

    #region Private

    private string _qbFilePath;
    private string _csvFilePath;
    private List<CsvModel> _csvData = new List<CsvModel>();

    #endregion Private

    #region Methods

    public void BtnOpenQbwFile(object sender) {
      string FileName = FileSystemHelper.GetFilePath("Quickbooks |*.qbw");
      QbFilePath = FileName;
    }


    public void BtnOpenCsvFile(object sender) {
      string FileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = FileName;
      string sep = Properties.Settings.Default["StnCsvSeparation"].ToString();
      CsvData = CsvParser.ParseFromFile(FileName, sep);
    }

    #endregion Methods

    #region Properties

    public string QbFilePath {
      get { return _qbFilePath; }
      set {
        _qbFilePath = value;
        NotifyOfPropertyChange(() => QbFilePath);
      }
    }

    public string CsvFilePath {
      get { return _csvFilePath; }
      set {
        _csvFilePath = value;
        NotifyOfPropertyChange(() => CsvFilePath);
      }
    }

    public List<CsvModel> CsvData {
      get { return _csvData; }
      set {
        _csvData = value;
        NotifyOfPropertyChange(() => CsvData);
      }
    }

    #endregion Properties
  }
}
