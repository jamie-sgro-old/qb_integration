using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;

namespace SumQuantityToFOB {
  [Export(typeof(IPreprocessor))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "SumQuantityToFOB")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "After data has been grouped into separate invoices," +
                                 " modify the FOB header box to equal the sum of all" +
                                 " quantities in the line item section for each invoice")]
  public class SumQuantityToFOB : IAfterGroupBy<Invoice> {
    public List<Invoice> ModifyGrouped(List<Invoice> groupBy) {
      Console.WriteLine("The plugin was accessed correct;y");
      throw new NotImplementedException();

      foreach (var group in groupBy) {
        double? qty = 0;
        foreach (var line in group.Lines) {
          if (line.Quantity == null) continue;
          qty += line.Quantity;
        }

        group.Header.FOB = qty.ToString();
      }
    }

  }

  public class Invoice {
    public Header Header { get; set; }
    public List<Lines> Lines { get; set; }

  }
  public class Header {
    public string FOB { get; set; }
  }
  public class Lines {
    public double? Quantity { get; set; }
  }
}