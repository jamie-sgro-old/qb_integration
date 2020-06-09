using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace MCBusinessLogic.Controllers {
  public class NxQbImportController : QbImportController {
    public NxQbImportController(string qbFilePath, DefaultInvoiceHeaderModel preHeader, List<CsvModel> csvData) {
      QbFilePath = qbFilePath;
      PreHeader = preHeader;
      //var csvData = SqliteDataAccess.LoadData<CsvModel>("SELECT * FROM csv_data", null);
      PreLineItems = MapCsvDataToLineItems(csvData);
    }
    public sealed override List<ClientLineItemModel> PreLineItems { get; set; }

    public List<ClientLineItemModel> MapCsvDataToLineItems(List<CsvModel> lineItems) {
      var sqlLineItems = new List<ClientLineItemModel>();
      foreach (var line in lineItems) {

        sqlLineItems.Add(new ClientLineItemModel() {
          ItemRef = line.Item,
          Quantity = GetNullableDouble(line.Quantity),
          Other1 = line.TimeInOut,
          Other2 = line.StaffName,
          ServiceDate = Convert.ToDateTime(line.ServiceDate),
          ORRatePriceLevelRate = GetNullableDouble(line.Rate)
        });
      }

      var a = sqlLineItems;
      return sqlLineItems;
    }

    /// <summary>
    /// If a string is null return null rather than 0
    /// Conceptually equivalent to Convert.ToDouble?() instead of Convert.ToDouble()
    /// </summary>
    /// <param name="strNum">A string parseable as a number</param>
    /// <returns>A double, or null if input was null</returns>
    private static double? GetNullableDouble (string strNum) {
      if (strNum == null) return null;
      return Convert.ToDouble(strNum);
    }
  }
}
