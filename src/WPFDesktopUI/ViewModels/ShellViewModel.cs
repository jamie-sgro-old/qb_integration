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
			// this.PreferencesViewModel = new PreferencesViewModel();
		}

		public CustomerViewModel CustomerViewModel { get; }
		public ImportViewModel ImportViewModel { get; }
		// public PreferencesViewModel PreferencesViewModel { get; }

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

		/*public ShellViewModel() {
			People.Add(new PersonModel { FirstName = "Amy", LastName = "Adams" });
			People.Add(new PersonModel { FirstName = "Bill", LastName = "Bobert" });
			People.Add(new PersonModel { FirstName = "Cory", LastName = "Chase" });
		}*/

		public string FirstName {
			get { 
				return _firstName;
			}
			set {
				_firstName = value;
				NotifyOfPropertyChange(() => FirstName);
				NotifyOfPropertyChange(() => FullName);
			}
		}


		public string LastName {
			get { return _lastName; }
			set { 
				_lastName = value;
				NotifyOfPropertyChange(() => LastName);
				NotifyOfPropertyChange(() => FullName);
			}
		}

		public string FullName{
			get { return $"{FirstName} {LastName}"; }
		}


		public BindableCollection<PersonModel> People {
			get { return _people; }
			set { _people = value; }
		}


		public PersonModel SelectedPerson {
			get { return _seletedPerson; }
			set {
				_seletedPerson = value;
				NotifyOfPropertyChange(() => SelectedPerson);
			}
		}

		private string _qbDescription;

		public string QbDescription {
			get { return _qbDescription; }
			set { _qbDescription = value; }
		}


		public bool CanClearText(string firstName, string lastName) {
			if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)) {
				return false;
			}
			return true;
		}

		public void ClearText(string firstName, string lastName) {
			FirstName = "";
			LastName = "";
		}

		private string _consoleMessage;

		public string ConsoleMessage {
			get { return _consoleMessage; }
			set { 
				_consoleMessage = value;
				NotifyOfPropertyChange(() => ConsoleMessage);

			}
		}


		public void BtnQbImport() {
			try {
				ConsoleMessage = "Importing, please stand by...";
				QbImportController.Import();
				ConsoleMessage = "Import has successfully completed";
			}
			catch (Exception e) {
				ConsoleMessage = e.Message;
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
