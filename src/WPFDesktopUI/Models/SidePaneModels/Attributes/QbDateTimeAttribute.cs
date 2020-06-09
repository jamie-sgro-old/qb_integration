using System;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbDateTimeAttribute : QbAbstractAttribute, IQbDateTimeAttribute {
    public QbDateTimeAttribute() {
      Payload = DateTime.Now.ToString();
    }
    public bool HasDateTimePayload { get; } = true;
  }
}
