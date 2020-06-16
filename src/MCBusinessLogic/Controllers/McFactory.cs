using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public static class McFactory {
    #region Invoice Models

    public static IInvoiceHeaderModel CreateInvoiceHeaderModel() {
      return new InvoiceHeaderModel();
    }

    public static IInvoiceLineItemModel CreateInvoiceLineItemModel() {
      return new InvoiceLineItemModel();
    }

    #endregion Invoice Models

    public static IInvoiceImporter CreateInvoiceImporter(string path) {
      return new InvoiceImporter(path);
    }

    public static IFileSystemHandler CreateFileSystemHandler() {
      return new FileSystemHandler();
    }
  }
}
