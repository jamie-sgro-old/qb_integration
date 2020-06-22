using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  /// <summary>
  /// Include all the attributes you want included in the UI
  /// Is thus a partial implementation of QbConnect's IInvoiceLineItemModel
  /// </summary>
  public class ClientInvoiceLineItemModel : IClientInvoiceLineItemModel {
    public string ItemRef { get; set; }
    public double? Quantity { get; set; }
    public string Desc { get; set; }
    public string Other1 { get; set; }
    public string Other2 { get; set; }
    public DateTime? ServiceDate { get; set; }
    public double? ORRatePriceLevelRate { get; set; }
  }
}
