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

      // Temp while types are being figured out
      InvoiceLineItemModel sqlLineItem = new InvoiceLineItemModel {
        ItemRef = csvData[0].Item,
        Quantity = Convert.ToDouble(csvData[0].Quantity),
        Other1 = csvData[0].TimeInOut,
        Other2 = csvData[0].StaffName,
        ServiceDate = Convert.ToDateTime(csvData[0].ServiceDate)
      };


      // Temp hardcoded data
      //csvData[0].Quantity = Convert.ToDouble(csvData[0].Quantity);
      //csvData[0].ServiceDate = Convert.ToDateTime(csvData[0].ServiceDate);
      QbInvoiceModel invoiceTemplate = new QbInvoiceModel {
        ClassRefFullName = "Barrie Area:Barrie Corporate",
        CustomerRefFullName = "CLASS",
        TemplateRefFullName = template
      };

      QbStaffModel staff = new QbStaffModel {
        Item = "CLASS - DSW1",
        Quantity = 8,
        StaffName = "Jamie Sgro",
        TimeInOut = "03:00 PM - 11:00 PM",
        ServiceDate = new DateTime(2020, 04, 04)
      };

      // Modify the model to match QB types
      InvoiceLineItemModel lineItem = MapLineItem(staff);
      InvoiceHeaderModel header = MapHeader(invoiceTemplate);

      BasicImporter.Import(qbFilePath, header, sqlLineItem);
    }

    private static InvoiceLineItemModel MapLineItem(QbStaffModel staff) {
      // TODO: Verify this column (Other1) corresponds with the "TIME IN - TIME OUT" column in QB
      // TODO: Verify this column (Other2) corresponds with the "STAFF NAME" column in QB
      return _ = new InvoiceLineItemModel {
        ItemRef = staff.Item,
        Quantity = staff.Quantity,
        Other1 = staff.TimeInOut,
        Other2 = staff.StaffName,
        ServiceDate = staff.ServiceDate
      };
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
