using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.PluginModels.Interfaces;

namespace WPFDesktopUI.Models.PluginModels {
  public class ClientPlugin : IClientPlugin {
    public ClientPlugin(bool isEnabled, string name, string author, string description) {
      IsEnabled = isEnabled;
      Name = name;
      Author = author;
      Description = description;
    }
    public bool IsEnabled { get; set; }
    public string Name { get; }
    public string Author { get; }
    public string Description { get; }
  }
}
