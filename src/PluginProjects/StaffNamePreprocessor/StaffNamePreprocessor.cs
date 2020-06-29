using System.ComponentModel.Composition;
using InterfaceLibraries;

namespace StaffNamePreprocessor
{
  [Export(typeof(IPreprocessor))]
  [ExportMetadata("Name", "StaffNamePreprocessor")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "In CSV import, make new column called 'FullName'"+
                                 " that combines 'First Name' and 'Last Name' columns")]
  [ExportMetadata("IsActive", true)]
  public class StaffNamePreprocessor : IPreprocessor {
    public string Preprocess(string dt) {
      return "This is StaffNamePreprocessor.";
    }
  }
}
