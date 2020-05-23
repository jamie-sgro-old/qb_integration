using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace MCBusinessLogic.Controllers {
  public class NxQbImportController : QbImportController {
    public NxQbImportController(string qbFilePath, DefaultInvoiceHeaderModel preHeader) {
      QbFilePath = qbFilePath;
      PreHeader = preHeader;
      var csvData = SqliteDataAccess.LoadData<CsvModel>("SELECT * FROM csv_data", null);
      PreLineItems = MapCsvDataToLineItems(csvData);
    }

    public List<QbStaffModel> MapCsvDataToLineItems(List<CsvModel> lineItems) {
      var sqlLineItems = new List<QbStaffModel>();
      foreach (var line in lineItems) {
        sqlLineItems.Add(new QbStaffModel() {
          Item = line.Item,
          Quantity = Convert.ToDouble(line.Quantity),
          TimeInOut = line.TimeInOut,
          StaffName = line.StaffName,
          ServiceDate = Convert.ToDateTime(line.ServiceDate)
        });
      }
      return sqlLineItems;
    }
  }
}
