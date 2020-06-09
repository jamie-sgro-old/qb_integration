using System;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbDateTimeAttribute : QbAbstractAttribute, IQbDateTimeAttribute {
    public DateTime? Payload { get; set; } = null;
    public bool HasDateTimePayload { get; } = true;
  }
}
