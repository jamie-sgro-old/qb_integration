using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Windows;
using InterfaceLibraries;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.PluginModels.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.PluginModels {
  public class PluginModel : IPluginModel<IClientPlugin, IClientEssentials> {
    public void Init() {
      Compose();

      var essentials = Read().AsEnumerable();

      _initialized = true;
      PluginModels = GetPluginModels(essentials, _plugins);

      Create(PluginModels);
    }

    [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _plugins;

    private List<IClientPlugin> _pluginModels;

    public List<IClientPlugin> PluginModels {
      get {
        if (!_initialized) {
          log.Warn("Property cannot be evoked before running Init()");
          throw new ArgumentException(@"Property cannot be evoked before running Init();", nameof(PluginModels));
        }
        return _pluginModels;
      }
      set => _pluginModels = value;
    }

    private bool _initialized { get; set; } = false;

    internal List<IClientPlugin> GetPluginModels(IEnumerable<IClientEssentials> essentials, IEnumerable<Lazy<IPlugin, IPluginMetaData>> plugins) {
      var pluginModels = new List<IClientPlugin>();

      foreach (Lazy<IPlugin, IPluginMetaData> plugin in plugins) {
        var pluginDatabaseMatch = essentials.Where(x => x.Name == plugin.Metadata.Name);

        pluginModels.Add(Factory.CreateClientPlugin(
          pluginDatabaseMatch.FirstOrDefault().IsEnabled,
          plugin.Metadata.Name,
          plugin.Metadata.Author,
          plugin.Metadata.Description));
      }

      return pluginModels;
    }

    private void Compose() {
      CompositionContainer container = PluginHelper.GetContainer();
      container.ComposeParts(this);
    }

    public void Create<T>(List<T> dataList) {
      log.Debug("Writing to sql: Create");
      SqliteDataAccess.SaveData(
        @"INSERT OR IGNORE INTO `plugin` (IsEnabled, Name)
          VALUES (@IsEnabled, @Name);", dataList);
    }

    /// <summary>
    /// Dapper requires concrete implementations for sql queries
    /// Essentially a private version of PluginEssentials
    /// </summary>
    internal class TempPluginEssentials : IClientEssentials {
      public bool IsEnabled { get; set; }
      public string Name { get; internal set; }
    }

    public ObservableCollection<IClientEssentials> Read() {
      log.Debug("Reading from sql");
      const string query = "SELECT IsEnabled, Name FROM plugin";
      var list = SqliteDataAccess.LoadData<TempPluginEssentials>(query);

      // Cast to observable collection
      var collection = new ObservableCollection<IClientEssentials>(list);
      return collection;
    }

    public void Update<T>(ObservableCollection<T> dataList) {
      log.Debug("Writing to sql: Update");
      SqliteDataAccess.SaveData(
        @"UPDATE `plugin`
        SET
          IsEnabled = @IsEnabled,
          Name = @Name
        WHERE Name = @Name;", dataList);
    }

    public void Destroy<T>(ObservableCollection<T> dataList) {
      throw new NotImplementedException();
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
