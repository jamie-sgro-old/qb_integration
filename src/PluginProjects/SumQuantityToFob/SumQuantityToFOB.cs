using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;
using MCBusinessLogic.Models.Interfaces;

namespace SumQuantityToFob {
  [Export(typeof(IAfterGroupBy))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "SumQuantityToFob")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "After data has been grouped into separate invoices," +
                                 " modify the FOB header box to equal the sum of all" +
                                 " quantities in the line item section for each invoice")]
  public class SumQuantityToFob : IAfterGroupBy {
    public List<IInvoice> ModifyGrouped(List<IInvoice> groupBy) {
      Console.WriteLine("The plugin was accessed correct;y");

      foreach (var group in groupBy) {
        double? qty = 0;
        foreach (var line in group.Lines) {
          if (line.Quantity == null) continue;
          qty += line.Quantity;
        }

        group.Header.FOB = qty.ToString();
      }

      return groupBy;
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