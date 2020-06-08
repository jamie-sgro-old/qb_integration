using System.Collections.Generic;
using System.ComponentModel;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbComboBox : IQbComboBox, INotifyPropertyChanged {

    public List<string> ItemsSource { get; set; } = new List<string> {"a", "b", "c"};
    public bool IsEnabled { get; set; } = false;
    public string SelectedItem { get; set; } = "selected item";

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
