using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbStringAttribute : QbAbstractAttribute, IQbStringAttribute {
    public bool HasStringPayload { get; } = true;
  }
}
