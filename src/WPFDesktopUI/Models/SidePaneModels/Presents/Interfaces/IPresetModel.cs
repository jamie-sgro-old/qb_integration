namespace WPFDesktopUI.Models.SidePaneModels.Presents {
  public interface IPresetModel {
    string CustomerRefFullName { get; set; }
    string ClassRefFullName { get; set; }
    string TemplateRefFullName { get; set; }
    string TxnDate { get; set; }
    string BillAddress { get; set; }
    string ShipAddress { get; set; }
    string TermsRefFullName { get; set; }
    string PONumber { get; set; }
    string Other { get; set; }
    string Preset { get; set; }
    string ItemRef { get; set; }
    string ORRatePriceLevelRate { get; set; }
    string Quantity { get; set; }
    string Desc { get; set; }
    string Other1 { get; set; }
    string Other2 { get; set; }
  }
}