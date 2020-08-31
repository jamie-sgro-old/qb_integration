using System;
using System.ComponentModel.Composition;
using System.Data;
using InterfaceLibraries;

namespace TimeInOut
{
  [Export(typeof(IPreprocessor))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "TimeInOut")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "In CSV import, make new column called 'TimeInOut' that" +
                                 " combines 'Start Time' and 'End Time' columns")]
  public class TimeInOut : IPreprocessor {

    public string Col1 { get; } = "Start Time";
    public string Col2 { get; } = "End Time";

    public DataTable Preprocess(DataTable dt) {
      if (dt.Columns.Contains(Col1) == false) return dt;
      if (dt.Columns.Contains(Col2) == false) return dt;

      Convert24To12Hour(dt, Col1);
      Convert24To12Hour(dt, Col2);

      dt.Columns.Add("TimeInOut", typeof(string), "[" + Col1 + "] + ' - ' + [" + Col2 + "]");
      return dt;
    }

    /// <summary>
    /// Takes a 24 hours string (i.e. "15:00:00") and converts to
    /// 12 hour time ("03:00 PM")
    /// </summary>
    /// <param name="dt">Data table with column data to convert</param>
    /// <param name="colName">Name of the column with data to convert</param>
    private static void Convert24To12Hour(DataTable dt, string colName) {
      foreach (DataRow row in dt.Rows) {
        var date = DateTime.Parse(row[colName].ToString());
        row[colName] = date.ToString("hh:mm tt");
      }
    }
  }
}
