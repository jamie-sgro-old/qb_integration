using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public sealed class PluginViewModel : Conductor<object>, IPluginViewModel {
    public PluginViewModel() {
      PluginModel = Factory.CreatePluginModel();

      ReactiveCollection = new ObservableCollection<ClientPlugin>(PluginModel.PluginModels);
    }

    public IPluginModel PluginModel { get; set; }
    public ObservableCollection<ClientPlugin> ReactiveCollection { get; set; }
    public string Title { get; set; } = "Plugin Manager";



    public event PropertyChangedEventHandler PropertyChanged;
    public void OnCellEditEnding() {
      Title = Title.Replace("*", "");
      Title += "*";
      CanBtnUpdate = true;
    }

    public bool CanBtnUpdate { get; set; } = false;

    public void BtnUpdate() {
      PluginModel.Update(ReactiveCollection);
      Title = Title.Replace("*", "");
      CanBtnUpdate = false;
    }
  }
}
