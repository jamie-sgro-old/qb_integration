using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  public class NxInvoiceHeaderModel : DefaultInvoiceHeaderModel {
    public NxInvoiceHeaderModel() {
      ClassRefFullName = "Barrie Area:Barrie Corporate";
      CustomerRefFullName = "CLASS";
      TemplateRefFullName = "NEXIM's Invoice with credits &";
    }
  }
}
