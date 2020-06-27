using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.SidePaneModels.Presents {
  public class PresetModel : IPresetModel {
    public string CustomerRefFullName { get; set; }
    public string ClassRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public string TxnDate { get; set; }
    public string BillAddress { get; set; }
    public string ShipAddress { get; set; }
    public string TermsRefFullName { get; set; }
    public string PONumber { get; set; }
    public string Other { get; set; }

    public string Preset { get; set; }
    public string ItemRef { get; set; }
    public string ORRatePriceLevelRate { get; set; }
    public string Quantity { get; set; }
    public string Desc { get; set; }
    public string Other1 { get; set; }
    public string Other2 { get; set; }
  }
}
