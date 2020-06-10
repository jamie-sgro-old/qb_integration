using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace MCBusinessLogic.Controllers {
  public class NxQbImportController : QbImportController {
    public NxQbImportController(string qbFilePath, ClientInvoiceHeaderModel preHeader, List<CsvModel> csvData) {
      QbFilePath = qbFilePath;
      PreHeader = preHeader;
      PreLineItems = MapCsvDataToLineItems(csvData);
    }
    public sealed override List<ClientInvoiceLineItemModel> PreLineItems { get; set; }

    public List<ClientInvoiceLineItemModel> MapCsvDataToLineItems(List<CsvModel> lineItems) {
      var sqlLineItems = new List<ClientInvoiceLineItemModel>();
      foreach (var line in lineItems) {

        sqlLineItems.Add(new ClientInvoiceLineItemModel() {
          ItemRef = line.ItemRef,
          Quantity = GetNullableDouble(line.Quantity),
          Other1 = line.Other1,
          Other2 = line.Other2,
          ServiceDate = Convert.ToDateTime(line.ServiceDate),
          ORRatePriceLevelRate = GetNullableDouble(line.ORRatePriceLevelRate)
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
