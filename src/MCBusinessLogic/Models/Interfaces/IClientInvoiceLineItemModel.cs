using System;

namespace MCBusinessLogic.Models {
  public interface IClientInvoiceLineItemModel {
    string ItemRef { get; set; }
    double? Quantity { get; set; }
    string Other1 { get; set; }
    string Other2 { get; set; }
    DateTime? ServiceDate { get; set; }
    double? ORRatePriceLevelRate { get; set; }
  }
}