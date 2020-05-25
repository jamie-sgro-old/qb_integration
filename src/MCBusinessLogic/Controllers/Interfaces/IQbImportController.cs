using System.Collections.Generic;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers.Interfaces {
  internal interface IQbImportController
  {
    void Import();
    List<InvoiceLineItemModel> MapLineItems(List<QbStaffModel> lineItems);
    InvoiceHeaderModel MapHeader(DefaultInvoiceHeaderModel preHeader);

    string QbFilePath { get; set; }
    DefaultInvoiceHeaderModel PreHeader { get; set; }
    List<QbStaffModel> PreLineItems { get; set; }
  }
}
