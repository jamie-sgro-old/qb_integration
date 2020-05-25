using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : Screen {
		private string _otherHeaderTextBlock;
		private bool _hasHeaderOther;
		private string _headerOtherTextBox;
		private DateTime _headerDateTextBox = DateTime.Now;


		public string HeaderOtherTextBox {
			get => _headerOtherTextBox;
      set {
        _headerOtherTextBox = value;
        NotifyOfPropertyChange(() => HeaderOtherTextBox);
			}
		}

		public DateTime HeaderDateTextBox {
			get => _headerDateTextBox;
      set {
				_headerDateTextBox = value;
				NotifyOfPropertyChange(() => HeaderDateTextBox);
			}
		}


		public bool HasHeaderOther {
			get {
				_hasHeaderOther = (bool)Properties.Settings.Default["StnQbInvHasHeaderOther"];
				return _hasHeaderOther;
			}
		}


		public string HeaderOtherTextBlock {
			get
      {
        var name = Properties.Settings.Default["StnQbInvHeaderOtherName"].ToString();
				_otherHeaderTextBlock = HasHeaderOther ? name : "OTHER";
        return _otherHeaderTextBlock;
      }
		}

	}
}
