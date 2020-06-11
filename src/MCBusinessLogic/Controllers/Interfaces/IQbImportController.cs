using System.Collections.Generic;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  public interface IQbImportController
  {
    string QbFilePath { get; set; }
    IClientInvoiceHeaderModel PreHeader { get; set; }

    IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader);
    List<IInvoiceLineItemModel> MapLineItems(List<ICsvModel> lineItems);
    void Import(List<ICsvModel> csvModels);
  }
}
