using System;
using System.Collections.Generic;
using System.Linq;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.DataAccess;
using MCBusinessLogic.Models;
using MCBusinessLogic.Models.Interfaces;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.Controllers {
  public class QbImportController : IQbImportController {
    public QbImportController(string qbFilePath, Func<string, IInvoiceImporter> invoiceFunc) {
      QbFilePath = qbFilePath;
      _invoiceFunc = invoiceFunc;
    }



    public string QbFilePath { get; set; }
    private Func<string, IInvoiceImporter> _invoiceFunc { get; }



    public void Import(List<IInvoice> invoices) {
      using (var invoiceImporter = _invoiceFunc(QbFilePath)) {
        try {
          foreach (var invoice in invoices) {
            var mappedHeader = MapHeader(invoice.Header);
            var lineItems = MapLineItems(invoice.Lines.ToList());

            invoiceImporter.Import(mappedHeader, lineItems);
          }
        } catch (Exception) {
          invoiceImporter.Rollback();
          throw;
        }
      }
    }


    /// <summary>
    /// Converts IClientInvoiceHeaderModel data into IInvoiceHeaderModel to be safe for importing to QB
    /// Functionally equivalent to:
    /// headerModel.property1 = preHeader.property1;
    /// headerModel.property2 = preHeader.property2;
    /// </summary>
    /// <param name="preHeader">A model with shared properties between
    /// IClientInvoiceHeaderModel and IInvoiceHeaderModel</param>
    /// <returns></returns>
    public IInvoiceHeaderModel MapHeader(IClientInvoiceHeaderModel preHeader) {
      var headerModel = McFactory.CreateInvoiceHeaderModel();

      if (!(preHeader is IClientInvoiceHeaderModel)) {
        throw new ArgumentNullException(nameof(preHeader), "Could not map header items from Csv Model.");
      }

      // Reflection: i.e. headerModel.someProperty = preHeader.someProperty;
      foreach (var prop in typeof(IClientInvoiceHeaderModel).GetProperties()) {
        var propStr = prop.Name;
        headerModel.GetType().GetProperty(propStr)
          .SetValue(headerModel, preHeader.GetType().GetProperty(propStr).GetValue(preHeader));
      }

      return headerModel;
    }


    /// <summary>
    /// Converts Lists of ICSVModels into IInvoiceLineItemModels using reflection.
    /// Functionally equivalent to:
    /// lineModel.property1 = line.property1;
    /// lineModel.property2 = line.property2;
    /// </summary>
    /// <param name="lineItems">A list of data with shared properties between
    /// ICSVModel and IInvoiceLineItemModel</param>
    /// <returns></returns>
    public List<IInvoiceLineItemModel> MapLineItems(List<ICsvModel> lineItems) {
      var lineModelsList = new List<IInvoiceLineItemModel>();
      foreach (var line in lineItems) {

        if (!(line is IClientInvoiceLineItemModel)) {
          throw new ArgumentNullException(nameof(lineItems), "Could not map line items from Csv Model.");
        }

        var lineModel = McFactory.CreateInvoiceLineItemModel();
        
        // Reflection: i.e. lineModel.someProperty = line.someProperty;
        foreach (var prop in typeof(IClientInvoiceLineItemModel).GetProperties()) {
          var propStr = prop.Name;
          lineModel.GetType().GetProperty(propStr)
            .SetValue(lineModel, line.GetType().GetProperty(propStr).GetValue(line));
        }

        lineModelsList.Add(lineModel);
      }
      return lineModelsList;
    }
  }
}
