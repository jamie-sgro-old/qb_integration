using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  internal class ItemModel : SearchReplaceModel {
    public ItemModel() {
      _tableName = "item";
      _sourceData = new ObservableCollection<IItemReplacer>(Read());
    }

    protected override string _tableName { get; }
  }
}
