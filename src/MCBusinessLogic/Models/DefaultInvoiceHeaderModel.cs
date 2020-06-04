using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  public class DefaultInvoiceHeaderModel {
    public string ClassRefFullName { get; set; }
    public string CustomerRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string Other { get; set; }

    public DefaultInvoiceHeaderModel() {
      ClassRefFullName = null;
      CustomerRefFullName = null;
      TemplateRefFullName = null;
      TxnDate = null;
      Other = null;
    }

    public void ConvertEmptyToNull() {
      if (CustomerRefFullName == "") {
        CustomerRefFullName = null;
      }
      if (ClassRefFullName == "") {
        ClassRefFullName = null;
      }
      if (TemplateRefFullName == "") {
        TemplateRefFullName = null;
      }
      if (Other == "") {
        Other = null;
      }
    }
  }
}
