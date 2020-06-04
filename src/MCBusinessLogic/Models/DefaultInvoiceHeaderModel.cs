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
      // Customer is a mandatory field
      if (CustomerRefFullName == "") {
        if (string.IsNullOrEmpty(CustomerRefFullName)) {
          throw new ArgumentNullException(paramName: nameof(CustomerRefFullName),
            message: "No CUSTOMER:JOB name was supplied. " +
                     "The Importer was expecting at least 1.");
        }
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
