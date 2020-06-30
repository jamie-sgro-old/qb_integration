using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using InterfaceLibraries;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.PluginModels.Interfaces;

namespace WPFDesktopUI.Models.PluginModels {
  public class PluginModel : IPluginModel {
    public PluginModel() {
    }

    public void Init() {
      Compose();

      var essentials = Read<pluginEssentials>().AsEnumerable();

      _initialized = true;
      PluginModels = GetPluginModels(essentials, _plugins);

      Create(PluginModels);
    }

    [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _plugins;

    private List<ClientPlugin> _pluginModels;

    public List<ClientPlugin> PluginModels {
      get {
        if (!_initialized) {
          throw new ArgumentException(@"Property cannot be evoked before running Init();", nameof(PluginModels));
        }
        return _pluginModels;
      }
      set => _pluginModels = value;
    }

    private bool _initialized { get; set; } = false;
    
    internal struct pluginEssentials : IClientEssentials {
      public bool IsEnabled { get; set; }
      public string Name { get; }
    }
    
    internal List<ClientPlugin> GetPluginModels(IEnumerable<pluginEssentials> essentials, IEnumerable<Lazy<IPlugin, IPluginMetaData>> plugins) {
      var pluginModels = new List<ClientPlugin>();

      foreach (Lazy<IPlugin, IPluginMetaData> plugin in plugins) {
        var pluginDatabaseMatch = essentials.Where(x => x.Name == plugin.Metadata.Name);
        pluginModels.Add(new ClientPlugin(
          pluginDatabaseMatch.FirstOrDefault().IsEnabled,
          plugin.Metadata.Name,
          plugin.Metadata.Author,
          plugin.Metadata.Description));
      }

      return pluginModels;
    }

    private void Compose() {
      DirectoryCatalog catalog = new DirectoryCatalog("Plugins", "*.dll");
      //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
      CompositionContainer container = new CompositionContainer(catalog);
      //container.SatisfyImportsOnce(this);
      container.ComposeParts(this);
    }

    public void Create<T>(List<T> dataList) {
      SqliteDataAccess.SaveData(
        @"INSERT OR IGNORE INTO `plugin` (IsEnabled, Name)
          VALUES (@IsEnabled, @Name);", dataList);
    }

    public ObservableCollection<T> Read<T>() {

      var query = "SELECT IsEnabled, Name FROM plugin";
      var list = SqliteDataAccess.LoadData<T>(query);

      // Cast to observable collection
      var collection = new ObservableCollection<T>(list);
      return collection;
    }

    public void Update<T>(ObservableCollection<T> dataList) {
      SqliteDataAccess.SaveData(
        @"UPDATE `plugin`
        SET
          IsEnabled = @IsEnabled,
          Name = @Name
        WHERE Name = @Name;", dataList);
    }
  }
}
