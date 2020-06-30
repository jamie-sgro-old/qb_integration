using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using InterfaceLibraries;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.PluginModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public sealed class PluginViewModel : Conductor<object>, IPluginViewModel {
    public PluginViewModel() {
      PluginModel = Factory.CreatePluginModel();

      ReactiveCollection = new ObservableCollection<ClientPlugin>(PluginModel.PluginModels);
    }

    public IPluginModel PluginModel { get; set; }
    //public DataGrid PluginDataGrid { get; set; }
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
