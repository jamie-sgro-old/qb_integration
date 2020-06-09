using System.Collections.Generic;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces {
  public interface IQbComboBox {
    List<string> ItemsSource { get; set; }
    bool IsEnabled { get; set; }
    string SelectedItem { get; set; }


  }
}
