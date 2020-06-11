using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  // A string-only version of both ClientInvoiceLineItemModel & ClientInvoiceHeaderModel
  public class CsvModel {

    #region Header

    public string CustomerRefFullName { get; set; }
    public string ClassRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string Other { get; set; }

    #endregion Header


    #region Line

    public string ItemRef { get; set; }
    public string ORRatePriceLevelRate { get; set; }
    public string Quantity { get; set; }
    public DateTime? ServiceDate { get; set; }
    public string Other1 { get; set; }
    public string Other2 { get; set; }

    #endregion Line
  }
}
