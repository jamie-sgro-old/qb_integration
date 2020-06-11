using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace MCBusinessLogic.Controllers {
  public class NxQbImportController : QbImportController {
    public NxQbImportController(string qbFilePath, IClientInvoiceHeaderModel preHeader, List<ICsvModel> csvData) {
      QbFilePath = qbFilePath;
      PreHeader = preHeader;
      PreLineItems = MapCsvDataToLineItems(csvData);
    }

    public List<IClientInvoiceLineItemModel> MapCsvDataToLineItems(List<ICsvModel> lineItems) {
      var sqlLineItems = new List<IClientInvoiceLineItemModel>();
      foreach (var line in lineItems) {

        sqlLineItems.Add(new ClientInvoiceLineItemModel() {
          ItemRef = line.ItemRef,
          Quantity = line.Quantity,
          Other1 = line.Other1,
          Other2 = line.Other2,
          ServiceDate = line.ServiceDate,
          ORRatePriceLevelRate = line.ORRatePriceLevelRate
        });
      }

      var a = sqlLineItems;
      return sqlLineItems;
    }
  }
}
