using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public interface IShellViewModel : IWindow {
    IImportViewModel ImportViewModel { get; }
    IQuickBooksViewModel QuickBooksViewModel { get; }
    ICustomerViewModel<ICustomer> CustomerViewModel { get; }
    bool TabImportIsSelected { get; set; }
    bool TabQuickBooksIsSelected { get; set; }
    bool TabCustomerIsSelected { get; set; }

    /// <summary>
    /// Event triggers when a tab is selected in the ShellView
    /// </summary>
    void TabChange();
  }
}