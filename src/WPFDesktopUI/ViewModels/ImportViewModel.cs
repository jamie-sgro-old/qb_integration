using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen {

    #region Properties

    private string _csvFilePath;
    private DataView _csvDataView;

    public string CsvFilePath {
      get => _csvFilePath;
      set {
        _csvFilePath = value;
        NotifyOfPropertyChange(() => CsvFilePath);
      }
    }

    public DataView CsvDataView {
      get => _csvDataView;
      set {
        _csvDataView = value;
        NotifyOfPropertyChange(() => CsvDataView);
      }
    }

    public static DataTable CsvData { get; set; }

    #endregion Properties

    #region Methods

    public async Task BtnOpenCsvFile() {
      var fileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = fileName;
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      await Task.Run(() => {
        CsvData = CsvParser.ParseFromFile(fileName, sep);
        CsvDataView = CsvData.DefaultView;
      });



      /*
      // Import to SQLite
      SqliteDataAccess.SaveData<CsvModel>(@"INSERT INTO csv_data
        (Item, Quantity, StaffName, TimeInOut, ServiceDate, Rate)
        VALUES(@Item, @Quantity, @StaffName, @TimeInOut, @ServiceDate, @Rate);", CsvData);
      */
    }



    #endregion Methods
  }
}
