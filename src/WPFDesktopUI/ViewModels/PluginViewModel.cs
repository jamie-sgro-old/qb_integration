using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
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

      foreach (Lazy<IPreprocessor, IPreprocessorMetaData> processor in _preprocessors) {
        Plugins.Add(new ClientPlugin {
          IsEnabled = false,
          Name = processor.Metadata.Name,
          Author = processor.Metadata.Author,
          Description = processor.Metadata.Description
        });
      }
    }

    internal struct ClientPlugin : IClientPlugin {
      public bool IsEnabled { get; set; }
      public string Name { get; set; }
      public string Author { get; set; }
      public string Description { get; set; }
    }

    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    IEnumerable<Lazy<IPreprocessor, IPreprocessorMetaData>> _preprocessors;

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
    string Name { get; set; }
    string Author { get; set; }
    string Description { get; set; }
  }
}
