using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.PluginModels {
  /// <summary>
  /// Searches for and curates a list of all .dll files in
  /// the designated plugin location both in debug and release mode
  /// </summary>
  public static class PluginHelper {
    public static CompositionContainer GetContainer() {
      #if DEBUG
        var path = "Plugins";
      #else
        var path = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\x86\\Release\\Plugins";
      #endif

      DirectoryCatalog catalog = new DirectoryCatalog(path, "*.dll");
      CompositionContainer container = new CompositionContainer(catalog);
      return container;
    }
  }
}
