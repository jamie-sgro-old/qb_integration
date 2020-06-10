using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  public class ClientInvoiceLineItemModel {
    public string ItemRef { get; set; }
    public double? Quantity { get; set; }
    public string Other1 { get; set; }
    public string Other2 { get; set; }
    public DateTime? ServiceDate { get; set; }
    public double? ORRatePriceLevelRate { get; set; }
  }
}
