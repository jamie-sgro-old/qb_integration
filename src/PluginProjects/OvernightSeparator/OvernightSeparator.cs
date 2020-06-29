using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;

namespace OvernightSeparator
{
  [Export(typeof(IPreprocessor))]
  [ExportMetadata("Name", "OvernightSeparator")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "In CSV import, finds shifts that span several days and separates into new rows for each day")]
  public class OvernightSeparator : IPreprocessor {
    public string Preprocess(string dt) {
      return "This is OvernightSeparator.";
    }
  }
}
