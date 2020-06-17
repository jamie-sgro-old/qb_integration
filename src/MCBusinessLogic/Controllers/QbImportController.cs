using System;
using System.Collections.Generic;
using System.Linq;
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
      
      using (var invoiceImporter = McFactory.CreateInvoiceImporter(QbFilePath)) {

        var invoiceGroupByCx = csvModel.GroupBy(x => x.CustomerRefFullName);
        foreach (var groupCx in invoiceGroupByCx) {
          // Write Header with grouped Customer name
          header.CustomerRefFullName = groupCx.Key;

          var invoiceGroupByClass = groupCx.ToList().GroupBy(x => x.ClassRefFullName);
          foreach (var groupClass in invoiceGroupByClass) {
            // Write Header with grouped Class name
            header.ClassRefFullName = groupClass.Key;

            // Map and import to QuickBooks
            var lineItems = MapLineItems(groupClass.ToList());
            invoiceImporter.Import(header, lineItems);
          }
        }
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
