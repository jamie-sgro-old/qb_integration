using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using System.Windows;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WPFDesktopUI.ViewModels {
  public class ShellViewModel : Conductor<object>, IShellViewModel {

		public ShellViewModel() {
      try {
        log.Info("Constructor initializing");
        ImportViewModel = Factory.CreateImportViewModel();
        QuickBooksViewModel = Factory.CreateQuickBooksViewModel();
        CustomerViewModel = Factory.CreateCustomerViewModel();
        ItemViewModel = Factory.CreateItemViewModel();
        log.Info("Constructor initialized");
      }
      catch (Exception e) {
        MessageBox.Show("An unexpected error occured.\nPlease consult the log file for more info.", "Error",
          MessageBoxButton.OK, MessageBoxImage.Error);
        log.Fatal("An unexpected error occured", e);
        throw;
      }
		}

		public IImportViewModel ImportViewModel { get; }
		public IQuickBooksViewModel QuickBooksViewModel { get; }
		public ICustomerViewModel<ICustomer> CustomerViewModel { get; }
		public IItemViewModel<IItemReplacer> ItemViewModel { get; }

    public bool TabImportIsSelected { get; set; } = true;
    public bool TabQuickBooksIsSelected { get; set; } = false;
    public bool TabCustomerIsSelected { get; set; } = false;
    public bool TabItemIsSelected { get; set; } = false;

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

      if (TabItemIsSelected) {
        ItemViewModel.OnSelected();
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

      try {
        manager.ShowDialog(new PreferencesViewModel(), null, null);
      } catch (Exception e) {
        MessageBox.Show("An unexpected error occured.\nPlease consult the log file for more info.", "Error",
          MessageBoxButton.OK, MessageBoxImage.Error);
        log.Fatal("An unexpected error occured", e);
        throw;
      }
		}

    public void MenuItemPluginManager() {
      IWindowManager manager = new WindowManager();

      try {
        manager.ShowDialog(new PluginViewModel(), null, null);

      } catch (Exception e) {
        MessageBox.Show("An unexpected error occured.\nPlease consult the log file for more info.", "Error",
          MessageBoxButton.OK, MessageBoxImage.Error);
        log.Fatal("An unexpected error occured", e);
        throw;
      }
    }

		public void MenuItemAbout() {
      IWindowManager manager = new WindowManager();

      try {
        manager.ShowDialog(new AboutViewModel(), null, null);
      } catch (Exception e) {
        MessageBox.Show("An unexpected error occured.\nPlease consult the log file for more info.", "Error",
          MessageBoxButton.OK, MessageBoxImage.Error);
        log.Fatal("An unexpected error occured", e);
        throw;
      }
    }

    public string Title { get; set; } = "Invoice Importer by Sangwa";

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
