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
      return null;
    }
  }
}
