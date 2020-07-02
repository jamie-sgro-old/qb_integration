using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IPluginViewModel : IDataGrid<ClientPlugin>, IDbViewModel, IWindow {
    IPluginModel PluginModel { get; set; }
  }
}
