using System.ComponentModel;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbAttribute : IQbAttribute, INotifyPropertyChanged {
    public string Name { get; set; }
    public string Payload { get; set; } = null;
    public bool IsMandatory { get; set; } = false;
    public IQbComboBox ComboBox { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
