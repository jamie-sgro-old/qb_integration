using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  public class CsvModel : ICsvModel {

    #region Header

    public string CustomerRefFullName { get; set; }
    public string ClassRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public string TermsRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string BillAddress { get; set; }
    public string ShipAddress { get; set; }
    public string PONumber { get; set; }
    public string Other { get; set; }

    #endregion Header


    #region Line

    public string ItemRef { get; set; }
    public double? ORRatePriceLevelRate { get; set; }
    public double? Quantity { get; set; }
    public string Desc { get; set; }
    public DateTime? ServiceDate { get; set; }
    public string Other1 { get; set; }
    public string Other2 { get; set; }

    #endregion Line
    public object Clone() {
      return MemberwiseClone();
    }
  }
}
