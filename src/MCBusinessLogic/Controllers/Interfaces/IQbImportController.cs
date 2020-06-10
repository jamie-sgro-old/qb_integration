using System.Collections.Generic;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  internal interface IQbImportController
  {
    void Import();
    List<InvoiceLineItemModel> MapLineItems(List<ClientInvoiceLineItemModel> lineItems);
    InvoiceHeaderModel MapHeader(ClientInvoiceHeaderModel preHeader);

    string QbFilePath { get; set; }
    ClientInvoiceHeaderModel PreHeader { get; set; }
    List<ClientInvoiceLineItemModel> PreLineItems { get; set; }
  }
}
