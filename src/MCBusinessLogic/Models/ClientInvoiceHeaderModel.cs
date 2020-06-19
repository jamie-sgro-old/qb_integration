using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  /// <summary>
  /// Include all the attributes you want included in the UI
  /// Is thus a partial implementation of QbConnect's IInvoiceHeaderModel
  /// </summary>
  public class ClientInvoiceHeaderModel : IClientInvoiceHeaderModel {
    public string ClassRefFullName { get; set; }
    public string CustomerRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public string TermsRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string BillAddress { get; set; }
    public string ShipAddress { get; set; }
    public string Other { get; set; }
  }
}
