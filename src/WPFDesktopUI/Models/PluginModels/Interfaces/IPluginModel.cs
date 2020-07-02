using System.Collections.Generic;
using WPFDesktopUI.Models.DbModels.Interfaces;

namespace WPFDesktopUI.Models.PluginModels {
  public interface IPluginModel : IDbModel<ClientPlugin> {
    /// <summary>
    /// Acts as a constructor
    /// </summary>
    void Init();
    List<ClientPlugin> PluginModels { get; set; }
  }
}