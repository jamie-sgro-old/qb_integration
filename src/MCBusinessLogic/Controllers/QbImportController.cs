using System.Collections.Generic;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public abstract class QbImportController : IQbImportController {
    public string QbFilePath { get; set; }
    public IClientInvoiceHeaderModel PreHeader { get; set; }
    public abstract List<IClientInvoiceLineItemModel> PreLineItems { get; set; }



    public void Import() {
      var header = MapHeader(PreHeader);
      var sqlLineItems = MapLineItems(PreLineItems);
      using (var invoiceImporter = new InvoiceImporter(QbFilePath)) {
        header.Other = "this is the first invoice";
        invoiceImporter.Import(header, sqlLineItems);
        header.Other = "this is the second invoice";
        invoiceImporter.Import(header, sqlLineItems);
      }
    }



    public List<InvoiceLineItemModel> MapLineItems(List<IClientInvoiceLineItemModel> lineItems) {
      var sqlLineItems = new List<InvoiceLineItemModel>();
      foreach (var line in lineItems) {
        sqlLineItems.Add(new InvoiceLineItemModel() {
          ItemRef = line.ItemRef,
          Quantity = line.Quantity,
          Other1 = line.Other1,
          Other2 = line.Other2,
          ServiceDate = line.ServiceDate,
          ORRatePriceLevelRate = line.ORRatePriceLevelRate,
        });
      }
      return sqlLineItems;
    }



    public InvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader) {
      // todo: add unit test for when a new param is missing. i.e. mapping missed

      var newMap = new InvoiceHeaderModel {
        ClassRefFullName = preHeader.ClassRefFullName,
        CustomerRefFullName = preHeader.CustomerRefFullName,
        TemplateRefFullName = preHeader.TemplateRefFullName,
        TxnDate = preHeader.TxnDate,
        Other = preHeader.Other,
      };

      return newMap;
    }
  }
}
