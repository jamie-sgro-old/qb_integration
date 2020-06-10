using System.Collections.Generic;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  public interface IQbImportController
  {
    void Import();
    List<InvoiceLineItemModel> MapLineItems(List<IClientInvoiceLineItemModel> lineItems);
    InvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader);

    string QbFilePath { get; set; }
    IClientInvoiceHeaderModel PreHeader { get; set; }
    List<IClientInvoiceLineItemModel> PreLineItems { get; set; }
  }
}
