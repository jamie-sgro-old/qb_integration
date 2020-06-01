using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers.Interfaces;
using QBConnect;

namespace MCBusinessLogic.Controllers {
  public class QbExportController : IQbExportController {
    public QbExportController(string qbFilePath) {
      QbFilePath = qbFilePath;
    }

    public string QbFilePath { get; set; }

    public List<string> GetTemplateNamesList() {
      using (var invoiceImporter = new InvoiceImporter(QbFilePath)) {
        return invoiceImporter.GetTemplateNamesList();
      }
    }
  }
}
