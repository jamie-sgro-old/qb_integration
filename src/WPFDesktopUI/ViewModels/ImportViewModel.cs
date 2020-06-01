using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen {

    #region Properties

    private string _csvFilePath;
    private List<CsvModel> _csvData = new List<CsvModel>();

    public string CsvFilePath {
      get => _csvFilePath;
      set {
        _csvFilePath = value;
        NotifyOfPropertyChange(() => CsvFilePath);
      }
    }

    public List<CsvModel> CsvData {
      get => _csvData;
      set {
        _csvData = value;
        NotifyOfPropertyChange(() => CsvData);
      }
    }

    #endregion Properties

    #region Methods

    public async Task BtnOpenCsvFile() {
      var fileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = fileName;
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      await Task.Run(() => {
        CsvData = CsvParser.ParseFromFile(fileName, sep);
      });

      // Import to SQLite
      SqliteDataAccess.SaveData<CsvModel>(@"INSERT INTO csv_data
        (Item, Quantity, StaffName, TimeInOut, ServiceDate, Rate)
        VALUES(@Item, @Quantity, @StaffName, @TimeInOut, @ServiceDate, @Rate);", CsvData);
    }

    #endregion Methods
  }
}
