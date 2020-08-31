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
    }

  }

  public class Invoice {
    struct Header {
      public string FOB { get; set; }
    }
    struct Lines {
      public double? Quantity { get; set; }
    }
  }
}