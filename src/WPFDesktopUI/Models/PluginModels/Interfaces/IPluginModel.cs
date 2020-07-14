using System.Collections.Generic;
using WPFDesktopUI.Models.DbModels.Interfaces;

namespace WPFDesktopUI.Models.PluginModels {
  public interface IPluginModel<T1, T2> : IDbModel<T2> {
    /// <summary>
    /// Acts as a constructor
    /// </summary>
    void Init();
    List<T1> PluginModels { get; set; }
  }
}