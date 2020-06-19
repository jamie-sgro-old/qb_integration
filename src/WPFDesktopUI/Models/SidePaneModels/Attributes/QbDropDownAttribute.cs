using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  class QbDropDownAttribute : QbAbstractAttribute, IQBDropDownAttribute {
    public QbDropDownAttribute(Func<IQbComboBox> qbComboBox) {
      DropDownComboBox = qbComboBox();
    }
    public IQbComboBox DropDownComboBox { get; set; }
    public bool HasDropDownPayload { get; } = true;

  }
}
