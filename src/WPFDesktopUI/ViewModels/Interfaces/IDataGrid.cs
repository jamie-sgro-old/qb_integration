using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IDataGrid<T> {
    ObservableCollection<T> ReactiveCollection { get; set; }
    void OnCellEditEnding();
  }
}
