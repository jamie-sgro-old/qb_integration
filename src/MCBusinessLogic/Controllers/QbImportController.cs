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
      SqliteDataAccess.LoadData<CustomerModel>("SELECT * FROM customer", null);
      // new DynamicParameters()

      // Temp hardcoded data
      QbStaffModel staff = new QbStaffModel {
        Item = "CLASS - DSW1",
        Quantity = 8,
        StaffName = "Jamie Sgro",
        TimeInOut = "03:00 PM - 11:00 PM",
        ServiceDate = new DateTime(2020, 04, 04),
        ItemRef = "CLASS - DSW1"
      };

      QbInvoiceModel invoiceTemplate = new QbInvoiceModel {
        ClassRefFullName = "Barrie Area:Barrie Corporate",
        CustomerRefFullName = "CLASS",
        TemplateRefFullName = template
      };

      // Modify the model to match QB types
      InvoiceLineItemModel lineItem = MapLineItem(staff);
      InvoiceHeaderModel header = MapHeader(invoiceTemplate);

      BasicImporter.Import(qbFilePath, header, lineItem);
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
