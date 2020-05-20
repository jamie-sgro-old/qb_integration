using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDesktopUI.Models;

namespace WPFDesktopUI.ViewModels {
  public class ShellViewModel : Conductor<object> {

		public ShellViewModel() {
			this.CustomerViewModel = new CustomerViewModel();
			this.ImportViewModel = new ImportViewModel();
			this.QuickBooksViewModel = new QuickBooksViewModel();
		}

		public CustomerViewModel CustomerViewModel { get; }
		public ImportViewModel ImportViewModel { get; }
		public QuickBooksViewModel QuickBooksViewModel { get; }

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

		public void MenuItemPreferences() {
			IWindowManager manager = new WindowManager();

			manager.ShowWindow(new PreferencesViewModel(), null, null);
			//PreferencesView preferences = new PreferencesView();
			//preferences.Show();
		}







		private string _firstName;
		private string _lastName;
		private BindableCollection<PersonModel> _people = new BindableCollection<PersonModel>();
		private PersonModel _seletedPerson;

		public void LoadPageOne() {
			ActivateItem(new SecondChildViewModel());
		}

		public void LoadCustomerViewModel() {
			ActivateItem(new CustomerViewModel());
		}
	}
}
