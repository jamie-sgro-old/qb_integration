using System;
using System.Collections.Generic;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Interfaces;

namespace WPFDesktopUI.Models {
  public class QuickBooksSidePaneModel : IQuickBooksSidePaneModel {
    
    public QuickBooksSidePaneModel(Func<IQbComboBox> qbComboBox) {
      _qbComboBox = qbComboBox;

      Attr = new Dictionary<string, IQbAttribute>();
    }

    // All QbAttributes currently should have the same dropdown style
    private readonly Func<IQbComboBox> _qbComboBox;

    public Dictionary<string, IQbAttribute> Attr { get; set; }
    // public QbAttribute<string> CustomerRefFullName { get; set; }

    public void AttrAdd(IQbAttribute qbAttribute, string key, string name) {
      Attr.Add(key, qbAttribute);
      Attr[key].Name = name;
      Attr[key].ComboBox = _qbComboBox();
    }
  }
}
