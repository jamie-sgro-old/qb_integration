using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbStringAttribute : QbAbstractAttribute, IQbStringAttribute {
    public string Payload { get; set; } = null;
    public bool HasStringPayload { get; } = true;
  }
}
