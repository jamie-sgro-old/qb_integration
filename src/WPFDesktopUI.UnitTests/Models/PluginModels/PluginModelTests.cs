using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using InterfaceLibraries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.PluginModels;

namespace WPFDesktopUI.UnitTests.Models.PluginModels {
  [TestClass]
  public class PluginModelTests {
    [TestMethod]
    public void SingleData_true() {
      var pMod = new PluginModel();
      var ess = new List<PluginModel.pluginEssentials>();
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = true,
        Name = "Name1"
      });
      Compose();
      var res = pMod.GetPluginModels(ess, _plugins);

      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual(true, res[0].IsEnabled);
      Assert.AreEqual("Name1", res[0].Name);
      Assert.AreEqual("Author1", res[0].Author);
      Assert.AreEqual("Description1", res[0].Description);
    }

    [TestMethod]
    public void SingleData_false() {
      var pMod = new PluginModel();
      var ess = new List<PluginModel.pluginEssentials>();
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = false,
        Name = "Name1"
      });
      Compose();
      var res = pMod.GetPluginModels(ess, _plugins);

      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual(false, res[0].IsEnabled);
      Assert.AreEqual("Name1", res[0].Name);
      Assert.AreEqual("Author1", res[0].Author);
      Assert.AreEqual("Description1", res[0].Description);
    }

    [TestMethod]
    public void NewData_FirstTrue_SecondEmpty() {
      var pMod = new PluginModel();
      var ess = new List<PluginModel.pluginEssentials>();
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = true,
        Name = "Name1"
      });
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = false,
        Name = "Name2"
      });
      Compose();
      var res = pMod.GetPluginModels(ess, _plugins);

      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual(true, res[0].IsEnabled);
      Assert.AreEqual("Name1", res[0].Name);
      Assert.AreEqual("Author1", res[0].Author);
      Assert.AreEqual("Description1", res[0].Description);
    }

    [TestMethod]
    public void NullData_Empty() {
      var pMod = new PluginModel();
      var ess = new List<PluginModel.pluginEssentials>();
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = true,
        Name = "Name1"
      });
      Compose();
      var res = pMod.GetPluginModels(ess, _pluginsNull1);

      Assert.IsTrue(res.Count == 0);
    }

    [TestMethod]
    public void DoubleData_FirstTrue_SecondFalse() {
      var pMod = new PluginModel();
      var ess = new List<PluginModel.pluginEssentials>();
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = true,
        Name = "Name1"
      });
      ess.Add(new PluginModel.pluginEssentials {
        IsEnabled = false,
        Name = "Name2"
      });
      Compose();
      var res = pMod.GetPluginModels(ess, _pluginsOfTwo);

      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual(true,           res[0].IsEnabled);
      Assert.AreEqual("Name1",        res[0].Name);
      Assert.AreEqual("Author1",      res[0].Author);
      Assert.AreEqual("Description1", res[0].Description);
      Assert.AreEqual(false,          res[1].IsEnabled);
      Assert.AreEqual("Name2",        res[1].Name);
      Assert.AreEqual("Author2",      res[1].Author);
      Assert.AreEqual("Description2", res[1].Description);
    }



    [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _plugins;

    [ImportMany(typeof(IPluginNull1), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _pluginsNull1;

    [ImportMany(typeof(IPluginTest1), AllowRecomposition = true)]
    private IEnumerable<Lazy<IPlugin, IPluginMetaData>> _pluginsOfTwo;

    private void Compose() {
      AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
      CompositionContainer container = new CompositionContainer(catalog);
      container.SatisfyImportsOnce(this);
    }

    [Export(typeof(IPlugin))]
    [Export(typeof(IPluginTest1))]
    [ExportMetadata("Name", "Name1")]
    [ExportMetadata("Author", "Author1")]
    [ExportMetadata("Description", "Description1")]
    public class Plugin : IPlugin {
    }

    [Export(typeof(IPluginTest1))]
    [ExportMetadata("Name", "Name2")]
    [ExportMetadata("Author", "Author2")]
    [ExportMetadata("Description", "Description2")]
    public class PluginOfTwo : IPlugin {
    }
  }

  public interface IPluginTest1 {
  }
  public interface IPluginNull1 {
  }
}
