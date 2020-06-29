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
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.PluginModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public sealed class PluginViewModel : Conductor<object>, IDataGrid<ClientPlugin>, IDb<ClientPlugin> {
    public PluginViewModel() {
      Compose();

      ReactiveCollection = new ObservableCollection<ClientPlugin>();

      var essentials = Read<pluginEssentials>().AsEnumerable();

      foreach (Lazy<IPlugin, IPluginMetaData> plugin in _plugins) {
        var pluginDatabaseMatch = essentials.Where(x => x.Name == plugin.Metadata.Name);
        
        ReactiveCollection.Add(new ClientPlugin(
          pluginDatabaseMatch.FirstOrDefault().IsEnabled,
          plugin.Metadata.Name,
          plugin.Metadata.Author,
          plugin.Metadata.Description));
      }

      Create(ReactiveCollection.ToList());
    }


    public DataGrid PluginDataGrid { get; set; }
    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _plugins;
    public ObservableCollection<ClientPlugin> ReactiveCollection { get; set; }
    public string Title { get; set; } = "Plugin Manager";



    public event PropertyChangedEventHandler PropertyChanged;
    public void OnCellEditEnding() {
      Title = Title.Replace("*", "");
      Title += "*";
      CanBtnUpdate = true;
    }

    private void Compose() {
      DirectoryCatalog catalog = new DirectoryCatalog("Plugins", "*.dll");
      //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
      CompositionContainer container = new CompositionContainer(catalog);
      //container.SatisfyImportsOnce(this);
      container.ComposeParts(this);
    }

    public bool CanBtnUpdate { get; set; } = false;

    public void BtnUpdate() {
      Update(ReactiveCollection);
      Title = Title.Replace("*", "");
      CanBtnUpdate = false;
    }

    public void Create<T>(List<T> dataList) {
      SqliteDataAccess.SaveData(
        @"INSERT OR IGNORE INTO `plugin` (IsEnabled, Name)
          VALUES (@IsEnabled, @Name);", dataList);
    }

    private struct pluginEssentials : IClientEssentials {
      public bool IsEnabled { get; set; }
      public string Name { get; }
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
