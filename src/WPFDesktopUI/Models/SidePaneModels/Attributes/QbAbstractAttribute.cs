using System.ComponentModel;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public abstract class QbAbstractAttribute : IQbAttribute, INotifyPropertyChanged {
    public string Name { get; set; }
    public bool IsMandatory { get; set; } = false;
    public IQbComboBox ComboBox { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

  }
}
