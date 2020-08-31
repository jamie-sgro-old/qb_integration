using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;
using MCBusinessLogic.Models.Interfaces;

namespace SumQuantityToFOB
{
  [Export(typeof(IPreprocessor))]
  [Export(typeof(IPlugin))]
  [ExportMetadata("Name", "SumQuantityToFOB")]
  [ExportMetadata("Author", "Jamie Sgro")]
  [ExportMetadata("Description", "After data has been grouped into separate invoices," +
                                 " modify the FOB header box to equal the sum of all" +
                                 " quantities in the line item section for each invoice")]
  public class SumQuantityToFOB : IAfterGroupBy {
    public List<IInvoice> ModifyGrouped(List<IInvoice> groupBy) {
      Console.WriteLine("The plugin was accessed correct;y");
      throw new NotImplementedException();
    }
  }
}
