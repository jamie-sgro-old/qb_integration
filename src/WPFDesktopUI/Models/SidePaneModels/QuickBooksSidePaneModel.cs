using System;
using System.Collections.Generic;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models {
  public class QuickBooksSidePaneModel : IQuickBooksSidePaneModel {
    
    public QuickBooksSidePaneModel(Func<IQbAttribute> qbAttribute, Func<IQbComboBox> qbComboBox) {
      _qbAttribute = qbAttribute;
      _qbComboBox = qbComboBox;

      Attr = new Dictionary<string, IQbAttribute>();
    }

    private readonly Func<IQbAttribute> _qbAttribute;
    private readonly Func<IQbComboBox> _qbComboBox;

    public Dictionary<string, IQbAttribute> Attr { get; set; }
    // public QbAttribute<string> CustomerRefFullName { get; set; }

    public void AttrAdd(string key, string name) {
      Attr.Add(key, _qbAttribute());
      Attr[key].Name = name;
      Attr[key].IsMandatory = false;
      Attr[key].ComboBox = _qbComboBox();
    }
  }
}
