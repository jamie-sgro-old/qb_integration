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

    public static List<T> LoadData<T>(string sql, T data) {
      using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
        var output = cnn.Query<T>(sql, data);
        return output.ToList();
      }
    }

    public static List<T> LoadData<T>(string sql) {
      using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
        var output = cnn.Query<T>(sql, new DynamicParameters());
        return output.ToList();
      }
    }
    
    public static void SaveData<T>(string sql, T data) {
      using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
        cnn.Execute(sql, data);
        // cnn.Execute("INSERT INTO customer (name, po_number) VALUES (@name, @po_number)", data);
      }
    }
  }
}
