using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbNullAttribute : QbAbstractAttribute {
    public QbNullAttribute() {
      Payload = null;
    }
  }
}
