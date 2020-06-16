using System.ComponentModel;
using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using System.Data;
using System.Threading.Tasks;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen, IImportViewModel {
   

    public string CsvFilePath { get; set; }
    public DataView CsvDataView { get; set; }
    public static DataTable CsvData { get; set; }



    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
    }

    public async Task BtnOpenCsvFile() {
      var fileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = fileName;
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      await Task.Run(() => {
        CsvData = CsvParser.ParseFromFile(fileName, sep);
        // Match data structure to the UI view (this let's the user see the data)
        CsvDataView = CsvData.DefaultView;
      });



      /*
      // Import to SQLite
      SqliteDataAccess.SaveData<CsvModel>(@"INSERT INTO csv_data
        (Item, Quantity, StaffName, TimeInOut, ServiceDate, Rate)
        VALUES(@Item, @Quantity, @StaffName, @TimeInOut, @ServiceDate, @Rate);", CsvData);
      */
    }
  }
}
