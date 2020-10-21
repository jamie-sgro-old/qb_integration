using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  internal class AddressModel : SearchReplaceModel {
    public AddressModel() {
      _tableName = "address";
      _sourceData = new ObservableCollection<IItemReplacer>(Read());
    }

    protected override string _tableName { get; }
  }
}
