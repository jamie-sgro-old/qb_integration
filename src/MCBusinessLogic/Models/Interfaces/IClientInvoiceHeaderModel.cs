using System;

namespace MCBusinessLogic.Models {
  public interface IClientInvoiceHeaderModel {
    string ClassRefFullName { get; set; }
    string CustomerRefFullName { get; set; }
    string TemplateRefFullName { get; set; }
    DateTime? TxnDate { get; set; }
    string Other { get; set; }
  }
}