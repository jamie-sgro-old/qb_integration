using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Models;
using MCBusinessLogic.Models.Interfaces;
using QBConnect;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.QuickBooksModels {
  internal static class GroupBy {
    private struct Invoice : IInvoice {
      public IClientInvoiceHeaderModel Header { get; set; }
      public List<ICsvModel> Lines { get; set; }
    }

    private static List<IInvoice> Invoices { get; set; }

    public static List<IInvoice> GroupInvoices(List<ICsvModel> csvModels) {
      Invoices = new List<IInvoice>();

      var header = Factory.CreateClientInvoiceHeaderModel();

      // Get list of all props that need a nested GroupBy
      var propList = new List<string>();
      foreach (var prop in header.GetType().GetProperties()) {
        propList.Add(prop.Name);
      }

      GroupFromListRecursively(header, csvModels, propList);

      return Invoices;
    }

    /// <summary>
    /// Since every unique invoice can have several lines, but a single value per header field,
    /// group List<ICsvModel> so each header-value is the same per subgroup.
    /// </summary>
    /// <param name="header">Dynamically writes and rewrites header data for each import based on GroupBy</param>
    /// <param name="g">A list of dataModels that can be further grouped-by before import</param>
    /// <param name="propList">A list of each header property name that should be grouped</param>
    private static void GroupFromListRecursively(IClientInvoiceHeaderModel header, List<ICsvModel> g, List<string> propList) {
      // Using reflection (slow) group current dataset into subgroups based on the first element of propList
      var currentGroupBy = g
        .ToList()
        .GroupBy(x => x.GetType()
          .GetProperty(propList[0])
          .GetValue(x, null));
      foreach (var group in currentGroupBy) {
        // Update header with constant value
        // i.e. header.CustomerRefFullName = newGroup.Key; (where newGroup.Key == "Acme Inc.")
        header.GetType().GetProperty(propList[0]).SetValue(header, group.Key);

        // Copy all but first element from List
        var newPropList = propList.GetRange(1, propList.Count - 1);

        // Check if we've reached the end of the list (and thus, the recursion)
        if (newPropList.Count <= 0) {
          Invoices.Add(new Invoice() {
            Header = header.Clone() as IClientInvoiceHeaderModel,
            Lines = group.ToList()
          });
          continue;
        }

        // Otherwise, if there are more properties to loop through, recurse
        GroupFromListRecursively(header, group.ToList(), newPropList);
      }
    }
  }
}
