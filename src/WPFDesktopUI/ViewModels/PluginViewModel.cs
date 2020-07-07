using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.PluginModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public sealed class PluginViewModel : Conductor<object>, IPluginViewModel {
    public PluginViewModel() {
      log.Debug("Querying sql and directory for plugins");
      PluginModel = Factory.CreatePluginModel();

      ReactiveCollection = new ObservableCollection<ClientPlugin>(PluginModel.PluginModels);
    }

    public IPluginModel PluginModel { get; set; }
    public ObservableCollection<ClientPlugin> ReactiveCollection { get; set; }
    public string Title { get; set; } = "Plugin Manager";
    public string PluginDescription { get; set; } = "";



    public event PropertyChangedEventHandler PropertyChanged;
    public void OnCellEditEnding() {
      Title = Title.Replace("*", "");
      Title += "*";
      CanBtnUpdate = true;
    }

    public bool CanBtnUpdate { get; set; } = false;

    public void BtnUpdate() {
      log.Debug("Saving plugins to sql");
      PluginModel.Update(ReactiveCollection);
      Title = Title.Replace("*", "");
      CanBtnUpdate = false;
    }

    /// <summary>
    /// Fires when the datagrid / listview is selected.
    /// Updates the extention textbox with the detail
    /// property of this model
    /// </summary>
    /// <param name="ClientPluginObj">
    /// A single ClientPlugin indicating which DataGrid row was selected
    /// If whitespace was selected instead, indicates the last selected /
    /// currently active DataGrid Row
    /// </param>
    public void OnCellsChanged(object ClientPluginObj) {
      var IsValidType = ClientPluginObj is IClientPlugin;
      if (!IsValidType) {
        throw new ArgumentException(@"OnCellsChanged() parameter not of type " + typeof(IClientPlugin), nameof(ClientPluginObj));
      }

      var clientPlugin = (IClientPlugin)ClientPluginObj;
      PluginDescription = clientPlugin.Description;
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
