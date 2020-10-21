using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class AddressViewModel : SearchReplaceViewModel{
    public AddressViewModel() {
      TabHeader = "Address";
      SearchReplaceModel = Factory.CreateAddressModel();
      _backlog = new HashSet<IItemReplacer>();
    }

    public override ISearchReplaceModel<IItemReplacer> SearchReplaceModel { get; set; }
    public override string TabHeader { get; set; }
  }
}
