using System.Collections.Generic;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public abstract class QbImportController : IQbImportController {
    public string QbFilePath { get; set; }
    public DefaultInvoiceHeaderModel PreHeader { get; set; }
    public abstract List<QbStaffModel> PreLineItems { get; set; }
    public void Import() {
      var header = MapHeader(PreHeader);
      var sqlLineItems = MapLineItems(PreLineItems);
      BasicImporter.Import(QbFilePath, header, sqlLineItems);
    }
    public List<InvoiceLineItemModel> MapLineItems(List<QbStaffModel> lineItems) {
      var sqlLineItems = new List<InvoiceLineItemModel>();
      foreach (var line in lineItems) {
        sqlLineItems.Add(new InvoiceLineItemModel() {
          ItemRef = line.Item,
          Quantity = line.Quantity,
          Other1 = line.TimeInOut,
          Other2 = line.StaffName,
          ServiceDate = line.ServiceDate
        });
      }
      return sqlLineItems;
    }
    public InvoiceHeaderModel MapHeader(DefaultInvoiceHeaderModel preHeader) {
      return _ = new InvoiceHeaderModel {
        ClassRefFullName = preHeader.ClassRefFullName,
        CustomerRefFullName = preHeader.CustomerRefFullName,
        TemplateRefFullName = preHeader.TemplateRefFullName,
      };
    }
  }
}
