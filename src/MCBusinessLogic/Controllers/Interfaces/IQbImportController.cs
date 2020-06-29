using System.Collections.Generic;
using MCBusinessLogic.Models;
using MCBusinessLogic.Models.Interfaces;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  public interface IQbImportController
  {
    string QbFilePath { get; set; }

    IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader);
    List<IInvoiceLineItemModel> MapLineItems(List<ICsvModel> lineItems);
    void Import(List<IInvoice> invoices);
  }
}
