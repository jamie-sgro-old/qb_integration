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
  public class TimeInOut : IPreprocessor{
    public DataTable Preprocess(DataTable dt) {
      if (dt.Columns.Contains("Start Time") == false) return dt;
      if (dt.Columns.Contains("End Time") == false) return dt;

      dt.Columns.Add("TimeInOut", typeof(string), "[Start Time] + ' - ' + [End Time]");
      return dt;
    }
  }
}
