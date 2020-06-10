using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  // A string-only version of both ClientInvoiceLineItemModel & ClientInvoiceHeaderModel
  public class CsvModel {
    public string ItemRef { get; set; }
    public string Quantity { get; set; }
    public string Other2 { get; set; }
    public string Other1 { get; set; }
    public string ServiceDate { get; set; }
    public string ORRatePriceLevelRate { get; set; }
  }
}
