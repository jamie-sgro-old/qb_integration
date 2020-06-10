using System;
using System.ComponentModel;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public abstract class QbAbstractAttribute : IQbAttribute, INotifyPropertyChanged {
    public string Name { get; set; }
    public virtual string Payload { get; set; }
    public bool IsMandatory { get; set; } = false;
    public IQbComboBox ComboBox { get; set; }

    public string ToolTip { get; set; } = "Please import a .csv file in the 'Import'"+
                                          " tab before custom lists can be generated";


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
