using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  class QbDropDownAttribute : QbAbstractAttribute, IQBDropDownAttribute, INotifyPropertyChanged {

    public QbDropDownAttribute(Func<IQbComboBox> qbComboBox) {
      DropDownComboBox = qbComboBox();
    }

    private string _payload;
    
    public IQbComboBox DropDownComboBox { get; set; }
    public bool HasDropDownPayload { get; } = true;

    /// <summary>
    /// The static payload for drop down attributes must be
    /// from a list based on the dropdown
    /// </summary>
    public override string Payload {
      get => DropDownComboBox.SelectedItem;
      set => _payload = value;
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
