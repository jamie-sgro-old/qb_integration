using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using System.Windows;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ShellViewModel : Conductor<object>, IShellViewModel {

		public ShellViewModel() {
			ImportViewModel = Factory.CreateImportViewModel();
			QuickBooksViewModel = Factory.CreateQuickBooksViewModel();
      CustomerViewModel = Factory.CreateCustomerViewModel();
    }

		public IImportViewModel ImportViewModel { get; }
		public IQuickBooksViewModel QuickBooksViewModel { get; }
		public ICustomerViewModel<ICustomer> CustomerViewModel { get; }

		public bool TabImportIsSelected { get; set; } = true;
    public bool TabQuickBooksIsSelected { get; set; } = false;
    public bool TabCustomerIsSelected { get; set; } = false;

		/// <summary>
		/// Event triggers when a tab is selected in the ShellView
		/// </summary>
		public void TabChange() {
      if (TabImportIsSelected) {
        ImportViewModel.OnSelected();
				return;
      }

      if (TabQuickBooksIsSelected) {
        QuickBooksViewModel.OnSelected();
				return;
      }

      if (TabCustomerIsSelected) {
        CustomerViewModel.OnSelected();
        return;
      }
		}

    public void MenuItemSaveCustomerRules() {
      if (!CustomerViewModel.CanBtnUpdate) return;
			CustomerViewModel.BtnUpdate();
    }


		public void MenuItemClose() {
			Application.Current.Shutdown();
		}

		public void MenuItemMinimize() {
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		public void MenuItemMaximize() {
			Application.Current.MainWindow.WindowState = WindowState.Maximized;
		}

		public void MenuItemRestore() {
			Application.Current.MainWindow.WindowState = WindowState.Normal;
		}

    public void MenuItemManagePresets() {
      QuickBooksViewModel.QuickBooksSidePaneViewModel.AutopopulateComboBoxes(null);
    }

		public void MenuItemPreferences() {
			IWindowManager manager = new WindowManager();

			manager.ShowWindow(new PreferencesViewModel(), null, null);
		}

    public void MenuItemPluginManager() {
      IWindowManager manager = new WindowManager();

      manager.ShowWindow(new PluginViewModel(), null, null);
    }

		public void MenuItemAbout() {
      IWindowManager manager = new WindowManager();

      manager.ShowWindow(new AboutViewModel(), null, null);
    }
	}
}
