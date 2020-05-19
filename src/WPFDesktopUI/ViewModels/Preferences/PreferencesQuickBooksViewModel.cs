using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDesktopUI.ViewModels.Preferences {
  public class PreferencesQuickBooksViewModel : Screen {

		private bool _usingCutomTemplate;
		private string _templateNameTextBox;

		public bool UsingCutomTemplate {
			get {
				return _usingCutomTemplate; 
			}
			set {
				_usingCutomTemplate = value;
				NotifyOfPropertyChange(() => UsingCutomTemplate);
			}
		}

		public bool CanTemplateNameTextBox() {
			return false;
		}

		public string TemplateNameTextBox {
			get {
				return _templateNameTextBox;
			}
			set { 
				_templateNameTextBox = value;
				NotifyOfPropertyChange(() => TemplateNameTextBox);
			}
		}

		/*public bool TemplateNameIsEnabled() {
			return false;
			return UsingCutomTemplate.IsChecked == true;
		}*/


	}
}
