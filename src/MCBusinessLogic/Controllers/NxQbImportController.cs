using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace MCBusinessLogic.Controllers {
  internal class NxQbImportController : QbImportController {
    internal NxQbImportController(string qbFilePath, DefaultInvoiceHeaderModel preHeader) {
      QbFilePath = qbFilePath;
      PreHeader = preHeader;
      var csvData = SqliteDataAccess.LoadData<CsvModel>("SELECT * FROM csv_data", null);
      PreLineItems = MapCsvDataToLineItems(csvData);
    }
    public sealed override List<ClientLineItemModel> PreLineItems { get; set; }

    public List<ClientLineItemModel> MapCsvDataToLineItems(List<CsvModel> lineItems) {
      var sqlLineItems = new List<ClientLineItemModel>();
      foreach (var line in lineItems) {
        sqlLineItems.Add(new ClientLineItemModel() {
          ItemRef = line.Item,
          Quantity = Convert.ToDouble(line.Quantity),
          Other1 = line.TimeInOut,
          Other2 = line.StaffName,
          ServiceDate = Convert.ToDateTime(line.ServiceDate),
          ORRatePriceLevelRate = Convert.ToDouble(line.Rate)
        });
      }
      return sqlLineItems;
    }

  }
}
