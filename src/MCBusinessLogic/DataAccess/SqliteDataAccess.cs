using Dapper;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.DataAccess {
  public class SqliteDataAccess {
    private static string LoadConnectionString(string id = "Default") {
      return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }

    public static List<CustomerModel> LoadData() {
      using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
        var output = cnn.Query<CustomerModel>("SELECT * FROM customer", new DynamicParameters());
        return output.ToList();
      }
    }

    public static void SaveData(CustomerModel customer) {
      using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
        cnn.Execute("INSERT INTO customer (StaffName, Quantity) VALUES (@StaffName, @Quantity)", customer);
      }
    }
  }
}
