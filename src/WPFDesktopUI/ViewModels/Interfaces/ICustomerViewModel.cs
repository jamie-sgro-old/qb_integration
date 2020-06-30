using System.Collections.ObjectModel;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.DbModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface ICustomerViewModel<T> : IMainTab, IQbInteractable, IDbModel<T>, IDbViewModel, IDataGrid<T> {
  }
}
