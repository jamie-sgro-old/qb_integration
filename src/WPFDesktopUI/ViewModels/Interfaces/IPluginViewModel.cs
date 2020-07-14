using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.PluginModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IPluginViewModel<T1, T2> : IDataGrid<T1>, IDbViewModel, IWindow {
    IPluginModel<T1, T2> PluginModel { get; set; }
  }
}
