using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using InterfaceLibraries;
using WPFDesktopUI.Models.CustomerModels;

namespace WPFDesktopUI.ViewModels {
  public sealed class PluginViewModel : Conductor<object> {
    public PluginViewModel() {
      Compose();

      Plugins = new ObservableCollection<IClientPlugin>();

      foreach (Lazy<IPreprocessor, IPluginMetaData> processor in _preprocessors) {
        Plugins.Add(new ClientPlugin(false,
          processor.Metadata.Name, 
          processor.Metadata.Author, 
          processor.Metadata.Description));
      }
    }

    internal class ClientPlugin : IClientPlugin {
      public ClientPlugin(bool isEnabled, string name, string author, string description) {
        IsEnabled = isEnabled;
        Name = name;
        Author = author;
        Description = description;
      }
      public bool IsEnabled { get; set; }
      public string Name { get; }
      public string Author { get; }
      public string Description { get;  }
    }

    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    IEnumerable<Lazy<IPreprocessor, IPluginMetaData>> _preprocessors;

    public ObservableCollection<IClientPlugin> Plugins { get; set; }

    private void Compose() {
      DirectoryCatalog catalog = new DirectoryCatalog("Plugins", "*.dll");
      //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
      CompositionContainer container = new CompositionContainer(catalog);
      //container.SatisfyImportsOnce(this);
      container.ComposeParts(this);
    }
  }

  public interface IClientPlugin {
    bool IsEnabled { get; set; }
    string Name { get; }
    string Author { get; }
    string Description { get; }
  }
}
