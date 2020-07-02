using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;

namespace WeekendFinder
{
  [Export(typeof(IPreprocessor))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "WeekendFinder")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "In CSV import, find which rows have dates that occured on a weekend")]
  public class WeekendFinder : IPreprocessor {
    public DataTable Preprocess(DataTable dt) {
      if (dt.Columns.Contains("Date") == false) return dt;
      if (dt.Columns.Contains("Position") == false) return dt;

      dt.Columns.Add("PositionWkd", typeof(string));
      dt.Columns["PositionWkd"].Expression = null;
      dt.Columns["PositionWkd"].ReadOnly = false;

      foreach (DataRow row in dt.Rows) {
        var day = Convert.ToDateTime(row["Date"]);


        if ((day.DayOfWeek == DayOfWeek.Saturday) || (day.DayOfWeek == DayOfWeek.Sunday)) {
          row["PositionWkd"] = row["Position"] + " - WKD";
        } else {
          row["PositionWkd"] = row["Position"];
        }
      }

      return dt;
    }
  }
}
