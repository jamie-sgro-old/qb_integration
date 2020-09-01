using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using InterfaceLibraries;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.PluginModels {
  public abstract class PluginHandler<T> {
    public IEnumerable<Lazy<T, IPluginMetaData>> GetRelevantPlugins(IEnumerable<Lazy<T, IPluginMetaData>> dlls) {
      if (dlls == null) {
        throw new NullReferenceException("Could not import data from null dll input. " +
                                         "Did you forget to Compose() first or LazyImport " +
                                         "based on the correct interface?");
      }

      var relevantPlugins = new List<Lazy<T, IPluginMetaData>>();

      // Process all dll plugins that are enabled by the user
      var plugins = Factory.CreatePluginModel().PluginModels;
      foreach (Lazy<T, IPluginMetaData> processor in dlls) {
        var pluginDatabaseMatch = plugins.Where(x => x.Name == processor.Metadata.Name);
        if (!pluginDatabaseMatch.Any()) continue;

        var isEnabled = pluginDatabaseMatch.FirstOrDefault().IsEnabled;

        if (!isEnabled) continue;

        relevantPlugins.Add(processor);
      }

      return relevantPlugins;
    }

    public void Compose() {
      log.Debug("Creating plugin container");
      CompositionContainer container = PluginHelper.GetContainer();
      container.ComposeParts(this);
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
