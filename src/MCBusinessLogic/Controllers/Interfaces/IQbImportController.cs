using System.Collections.Generic;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  public interface IQbImportController
  {
    void Import();
    List<IInvoiceLineItemModel> MapLineItems(List<IClientInvoiceLineItemModel> lineItems);
    IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader);

    string QbFilePath { get; set; }
    IClientInvoiceHeaderModel PreHeader { get; set; }
    List<IClientInvoiceLineItemModel> PreLineItems { get; set; }
  }
}
