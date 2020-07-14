using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IDataGrid<T> {
    /// <summary>
    /// The data used to autopopulate DataGrids
    /// </summary>
    ObservableCollection<T> ReactiveCollection { get; set; }
    /// <summary>
    /// Function that triggers just before a user edit on a datagrid is committed or cancelled
    /// </summary>
    void OnCellEditEnding();
  }
}
