using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.DataAccess;

namespace WPFDesktopUI.Controllers {
  public static class DbController {
    private static T GetUserVersion<T>() {
      var userVersion = SqliteDataAccess.LoadData<T>(@"PRAGMA user_version");
      return userVersion.FirstOrDefault();
    }

    /// <summary>
    /// First initialization of `customer`, `item`, and `plugin` tables
    /// </summary>
    private static void Update_1() {
      SqliteDataAccess.SaveData(
        @"CREATE TABLE IF NOT EXISTS 'customer' (
	          'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	          'Name'	TEXT NOT NULL UNIQUE,
	          'PoNumber'	TEXT,
	          'TermsRefFullName'	TEXT,
	          'AppendLineItem1'	TEXT,
	          'AppendLineItem2'	TEXT,
	          'AppendLineItem3'	TEXT
          );
          CREATE TABLE IF NOT EXISTS 'item' (
	          'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	          'ReplaceWith'	TEXT NOT NULL,
	          'ToReplace'	TEXT NOT NULL
          );
          CREATE TABLE IF NOT EXISTS 'plugin' (
	          'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	          'IsEnabled'	INTEGER NOT NULL,
	          'Name'	TEXT NOT NULL UNIQUE
          );
          CREATE TABLE IF NOT EXISTS 'csv_data' (
	          'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	          'Preset'	TEXT NOT NULL,
	          'ItemRef'	TEXT,
	          'ORRatePriceLevelRate'	TEXT,
	          'Quantity'	TEXT,
	          'Desc'	TEXT,
	          'Other1'	TEXT,
	          'Other2'	TEXT,
	          'CustomerRefFullName'	TEXT,
	          'ClassRefFullName'	TEXT,
	          'TemplateRefFullName'	TEXT,
	          'TxnDate'	TEXT,
	          'BillAddress'	TEXT,
	          'ShipAddress'	TEXT,
	          'TermsRefFullName'	TEXT,
	          'PONumber'	TEXT,
	          'Other'	TEXT
          );
          PRAGMA user_version = 1;");
    }

    private static void Update_2() {
      SqliteDataAccess.SaveData(
        @"ALTER TABLE customer
            ADD Class TEXT;
          PRAGMA user_version = 2;");
		}

    private static void Update_3() {
      SqliteDataAccess.SaveData(
        @"CREATE TABLE IF NOT EXISTS 'address' (
	        'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	        'ReplaceWith'	TEXT NOT NULL,
	        'ToReplace'	TEXT NOT NULL
        );
        PRAGMA user_version = 3;");
    }

    private static void Update_4() {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Check SQLite's user's PRAGMA user_version and incrementally update to current release version
    /// </summary>
    public static void UpdateDataBase() {
      var userVersion = GetUserVersion<double>();

      if (userVersion < 1) Update_1();
      if (userVersion < 2) Update_2();
      if (userVersion < 3) Update_3();
      //if (userVersion < 4) Update_4();
    }
  }
}
