using System.Collections.Generic;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public class QbImportController : IQbImportController {
    public QbImportController(string qbFilePath) {
      QbFilePath = qbFilePath;
    }

    public string QbFilePath { get; set; }
    public IClientInvoiceHeaderModel PreHeader { get; set; }



    public void Import(IClientInvoiceHeaderModel preHeader, List<ICsvModel> csvModel) {
      var header = MapHeader(preHeader);
      var sqlLineItems = MapLineItems(csvModel);
      using (var invoiceImporter = McFactory.CreateInvoiceImporter(QbFilePath)) {
        header.Other = "this is the first invoice";
        invoiceImporter.Import(header, sqlLineItems);
        header.Other = "this is the second invoice";
        invoiceImporter.Import(header, sqlLineItems);
      }
    }



    public IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader) {
      var headerModel = McFactory.CreateInvoiceHeaderModel();

      headerModel.ClassRefFullName = preHeader.ClassRefFullName;
      headerModel.CustomerRefFullName = preHeader.CustomerRefFullName;
      headerModel.TemplateRefFullName = preHeader.TemplateRefFullName;
      headerModel.TxnDate = preHeader.TxnDate;
      headerModel.Other = preHeader.Other;

      return headerModel;
    }



    public List<IInvoiceLineItemModel> MapLineItems(List<ICsvModel> lineItems) {
      var sqlLineItems = new List<IInvoiceLineItemModel>();
      foreach (var line in lineItems) {
        var lineModel = McFactory.CreateInvoiceLineItemModel();

        lineModel.ItemRef = line.ItemRef;
        lineModel.Quantity = line.Quantity;
        lineModel.Other1 = line.Other1;
        lineModel.Other2 = line.Other2;
        lineModel.ServiceDate = line.ServiceDate;
        lineModel.ORRatePriceLevelRate = line.ORRatePriceLevelRate;

        sqlLineItems.Add(lineModel);
      }
      return sqlLineItems;
    }
  }
}
