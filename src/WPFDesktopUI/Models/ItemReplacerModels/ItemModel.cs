using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  public class ItemModel : IItemModel<IItemReplacer> {
    public ItemModel() {
      _itemReplacers = new ObservableCollection<IItemReplacer> {
        Factory.CreateItemReplacer("PSW", "Barrie Connie Thompson- PSW"),
        Factory.CreateItemReplacer("PSW", "CLASS - PSW1"),
        Factory.CreateItemReplacer("PSW", "Villa (PSW)"),
        Factory.CreateItemReplacer("PSW", "Villa (PSW) Night Shift"),
        Factory.CreateItemReplacer("RN", "Barrie Connie Thompson- RN"),
        Factory.CreateItemReplacer("RN", "CLASS - RN1"),
        Factory.CreateItemReplacer("RN - WKD", "Barrie Connie Thompson- RN- Weekend"),
        Factory.CreateItemReplacer("RN - WKD", "CLASS - RN1- Weekend"),
        Factory.CreateItemReplacer("RN - STAT", "Barrie Connie Thompson- RN - Stat Holiday"),
        Factory.CreateItemReplacer("RN - STAT", "CLASS - RN1 - STAT")
      };
    }

    // TODO: Replace with _sourceData
    private ObservableCollection<IItemReplacer> _itemReplacers { get; set; }

    public string SelectedKey { get; set; }
    public ObservableCollection<IItemReplacer> SelectedItem { get; set; }

    public ObservableCollection<IItemReplacer> UniqueReplaceWith() {
      return new ObservableCollection<IItemReplacer>(_itemReplacers
        .GroupBy(x => x.ReplaceWith)
        .Select(x => x.First())
        .Where(x => x.ReplaceWith.ToLower().Contains(Filter.ToLower()))
        .ToList());
    }

    public void ItemSelected(IItemReplacer selectedItem) {
      SelectedKey = selectedItem.ReplaceWith;

      // Update SelectedItem
      SelectedItem = new ObservableCollection<IItemReplacer>(_itemReplacers
        .Where(x => x.ReplaceWith == SelectedKey)
        .ToList());
    }

    public string Filter { get; set; } = "";
  }
}
