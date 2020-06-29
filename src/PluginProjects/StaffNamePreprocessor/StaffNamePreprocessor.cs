using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffNamePreprocessor
{
  [Export(typeof(IPreprocessor))]
  [ExportMetadata("Name", "StaffNamePreprocessor")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("IsActive", true)]
  public class StaffNamePreprocessor : IPreprocessor {
    public string Preprocess(string dt) {
      return "This is StaffNamePreprocessor.";
    }
  }
}
