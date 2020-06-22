using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.CustomerModels.Interfaces {
  public interface IDb {

    DataTable Read();    
    /*
    // Import to SQLite
    SqliteDataAccess.SaveData<CsvModel>(@"INSERT INTO csv_data
      (Item, Quantity, StaffName, TimeInOut, ServiceDate, Rate)
      VALUES(@Item, @Quantity, @StaffName, @TimeInOut, @ServiceDate, @Rate);", CsvData);
    */

    void Update(List<string> name);

  }
}
