using System.ComponentModel.Composition;
using System.Data;
using InterfaceLibraries;

namespace StaffNamePreprocessor
{
  [Export(typeof(IPreprocessor))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "StaffNamePreprocessor")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "In CSV import, make new column called 'FullName' that"+
                                 " combines 'First Name' and 'Last Name' columns")]
  public class StaffNamePreprocessor : IPreprocessor {
    public DataTable Preprocess(DataTable dt) {

      if (dt.Columns.Contains("First Name") == false) return dt; 
      if (dt.Columns.Contains("Last Name") == false) return dt;

      dt.Columns.Add("FullName", typeof(string), "First Name+'/'+Last Name");
      return dt;
    }
  }
}
