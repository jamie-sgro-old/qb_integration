using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces {
  public interface IDateTimePayload {
    DateTime? Payload { get; set; }
    bool HasDateTimePayload { get; }
  }
}
