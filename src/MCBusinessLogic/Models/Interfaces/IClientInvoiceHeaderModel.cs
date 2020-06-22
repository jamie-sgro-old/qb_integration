﻿using System;

namespace MCBusinessLogic.Models {
  public interface IClientInvoiceHeaderModel {
    string ClassRefFullName { get; set; }
    string CustomerRefFullName { get; set; }
    string TemplateRefFullName { get; set; }
    string TermsRefFullName { get; set; }
    DateTime? TxnDate { get; set; }
    string BillAddress { get; set; }
    string ShipAddress { get; set; }
    string PONumber { get; set; }
    string Other { get; set; }
  }
}