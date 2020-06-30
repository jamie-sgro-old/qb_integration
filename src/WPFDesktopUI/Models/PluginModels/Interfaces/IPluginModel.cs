using System.Collections.Generic;
using WPFDesktopUI.Models.DbModels.Interfaces;

namespace WPFDesktopUI.Models.PluginModels {
  public interface IPluginModel : IDbModel<ClientPlugin> {
    List<ClientPlugin> PluginModels { get; set; }
  }
}