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

    public DefaultInvoiceHeaderModel() {
      ClassRefFullName = null;
      CustomerRefFullName = null;
      TemplateRefFullName = null;
    }
  }
}
