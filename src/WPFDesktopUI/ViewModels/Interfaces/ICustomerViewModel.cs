using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface ICustomerViewModel<T> : IMainTab, IQbInteractable, IDb<T> {
    bool CanBtnUpdate { get; set; }
    void BtnUpdate();
    void OnCellEditEnding();
  }
}
