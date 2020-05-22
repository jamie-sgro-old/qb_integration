using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using QBConnect;
using QBConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public class QbImportController {
    public static void Import(string qbFilePath, string template) {
      List<CsvModel> csvData = SqliteDataAccess.LoadData<CsvModel>("SELECT * FROM csv_data", null);
      // new DynamicParameters()

      // Temp hardcoded data
      QbInvoiceModel invoiceTemplate = new QbInvoiceModel {
        ClassRefFullName = "Barrie Area:Barrie Corporate",
        CustomerRefFullName = "CLASS",
        TemplateRefFullName = template
      };

      // Modify the model to match QB types
      List<InvoiceLineItemModel> sqlLineItems  = MapLineItems(csvData);
      InvoiceHeaderModel header = MapHeader(invoiceTemplate);

      BasicImporter.Import(qbFilePath, header, sqlLineItems);
    }

    private static List<InvoiceLineItemModel> MapLineItems(List<CsvModel> lineItems) {
      // TODO: Verify this column (Other1) corresponds with the "TIME IN - TIME OUT" column in QB
      // TODO: Verify this column (Other2) corresponds with the "STAFF NAME" column in QB
      var sqlLineItems = new List<InvoiceLineItemModel>();
      foreach (CsvModel item in lineItems) {
        sqlLineItems.Add(new InvoiceLineItemModel() {
          ItemRef = item.Item,
          Quantity = Convert.ToDouble(item.Quantity),
          Other1 = item.TimeInOut,
          Other2 = item.StaffName,
          ServiceDate = Convert.ToDateTime(item.ServiceDate)
        });
      }
      return sqlLineItems;
    }

    private static InvoiceHeaderModel MapHeader(QbInvoiceModel invoiceTemplate) {
      return _ = new InvoiceHeaderModel {
        ClassRefFullName = invoiceTemplate.ClassRefFullName,
        CustomerRefFullName = invoiceTemplate.CustomerRefFullName,
        TemplateRefFullName = invoiceTemplate.TemplateRefFullName,
      };
    }
  }
}
