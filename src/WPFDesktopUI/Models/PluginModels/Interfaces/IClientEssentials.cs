using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.PluginModels.Interfaces {
  public interface IClientEssentials {
    bool IsEnabled { get; set; }
    string Name { get; }
  }
}
