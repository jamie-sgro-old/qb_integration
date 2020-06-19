using System.Collections.Generic;
using MCBusinessLogic.Controllers.Interfaces;

namespace MCBusinessLogic.Controllers {
  public class QbExportController : IQbExportController {
    public QbExportController(string qbFilePath) {
      QbFilePath = qbFilePath;
    }

    public string QbFilePath { get; set; }

    public List<string> GetTemplateNamesList() {
      using (var invoiceImporter = McFactory.CreateInvoiceImporter(QbFilePath)) {
        return invoiceImporter.GetTemplateNamesList();
      }
    }

    public List<string> GetTermsNamesList() {
      using (var invoiceImporter = McFactory.CreateInvoiceImporter(QbFilePath)) {
        return invoiceImporter.GetTermsNamesList();
      }
    }
  }
}
