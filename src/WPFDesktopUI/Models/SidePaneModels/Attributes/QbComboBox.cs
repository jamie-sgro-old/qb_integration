using System.Collections.Generic;
using System.ComponentModel;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbComboBox : IQbComboBox, INotifyPropertyChanged {

    public List<string> ItemsSource { get; set; } = new List<string>();
    public string SelectedItem { get; set; } = "";

    public bool IsEnabled { get; set; } = false;

    private bool _isBlank;
    public bool IsBlank {
      get {
        _isBlank = string.IsNullOrEmpty(SelectedItem);
        return _isBlank;
      }
      set => _isBlank = value;
    }


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
