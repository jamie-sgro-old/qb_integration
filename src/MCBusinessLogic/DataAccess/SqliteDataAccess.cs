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
    private static string GetConnectionString(string id = "Default") {
      return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }

    public static List<T> LoadData<T>(string sql, T data) {
      using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
        var output = cnn.Query<T>(sql, data);
        return output.ToList();
      }
    }
    public static List<T> LoadData<T>(string sql) {
      using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
        var output = cnn.Query<T>(sql, new DynamicParameters());
        return output.ToList();
      }
    }

    public static void SaveData(string sql) {
      using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
        cnn.Execute(sql);
      }
    }
    public static void SaveData<T>(string sql, T data) {
      using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
        cnn.Execute(sql, data);
      }
    }
    public static void SaveData<T>(string sql, List<T> data) {
      using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
        foreach (var item in data) {
          cnn.Execute(sql, item);
        }
      }
    }
  }
}
