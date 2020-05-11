using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using QBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models;

namespace WPFDesktopUI.ViewModels {
  public class ShellViewModel : Screen {
		private string _firstName = "Jamie";
		private string _lastName;
		private BindableCollection<PersonModel> _people = new BindableCollection<PersonModel>();
		private PersonModel _seletedPerson;
		private string _qbFilePath;

		public ShellViewModel() {
			People.Add(new PersonModel { FirstName = "Amy", LastName = "Adams" });
			People.Add(new PersonModel { FirstName = "Bill", LastName = "Bobert" });
			People.Add(new PersonModel { FirstName = "Cory", LastName = "Chase" });
		}

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

		public void BtnOpenFile(object sender) {
			string FileName = FileSystemHelper.GetFilePath("Quickbooks |*.qbw");
			QbFilePath = FileName;
		}


		public string QbFilePath {
			get { return _qbFilePath; }
			set {
				_qbFilePath = value;
				NotifyOfPropertyChange(() => QbFilePath);
			}
		}


		public void BtnQbImport() {
			BasicImporter.Import(QbDescription);
		}
	}
}
