using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using Caliburn.Micro;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ItemViewModel : SearchReplaceViewModel {

    public ItemViewModel() {
      SearchReplaceModel = Factory.CreateItemModel();
      _backlog = new HashSet<IItemReplacer>();
    }

    public override ISearchReplaceModel<IItemReplacer> SearchReplaceModel { get; set; }
  }
}
