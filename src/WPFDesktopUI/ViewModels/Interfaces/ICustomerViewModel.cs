using System.Collections.ObjectModel;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface ICustomerViewModel<T> : IMainTab, IQbInteractable, IDb<T> {
    ObservableCollection<Customer> Cxs { get; set; }
    bool CanBtnUpdate { get; set; }
    void BtnUpdate();
    void OnCellEditEnding();
  }
}
