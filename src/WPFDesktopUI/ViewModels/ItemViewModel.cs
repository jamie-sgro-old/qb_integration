using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ItemViewModel : Screen, IItemViewModel {
    private string _replacer;
    private ObservableCollection<ItemReplacer> _uniqueReplaceWith;

    public ItemViewModel() {
      ItemReplacers = new ObservableCollection<ItemReplacer> {
        new ItemReplacer() {ReplaceWith = "PSW", ToReplace = "Barrie Connie Thompson- PSW"},
        new ItemReplacer() {ReplaceWith = "PSW", ToReplace = "CLASS - PSW1"},
        new ItemReplacer() {ReplaceWith = "RN",  ToReplace = "Barrie Connie Thompson- RN"},
        new ItemReplacer() {ReplaceWith = "RN",  ToReplace = "CLASS - RN1"}
      };
    }

    public string SelectedKey { get; set; } = null;


    public ObservableCollection<ItemReplacer> ItemReplacers { get; set; }
    public string TabHeader { get; set; } = "Item";

    public ObservableCollection<ItemReplacer> SelectedItem { get; set; }

    public ObservableCollection<ItemReplacer> UniqueReplaceWith {
      get {
        return new ObservableCollection<ItemReplacer>(ItemReplacers
          .GroupBy(x => x.ReplaceWith)
          .Select(x => x.First())
          .ToList());
      }
      set => _uniqueReplaceWith = value;
    }

    public void OnSelected() {
    }

    public void OnKeyUp(object itemReplacerObj) {
      var isDict = itemReplacerObj is ItemReplacer;
      if (! isDict) {
        throw new ArgumentException(@"OnKeyUp() parameter not of type " + typeof(ItemReplacer), nameof(itemReplacerObj));
      }

      // Cast to KeyValuePair like Dict
      var itemReplacer = (ItemReplacer)itemReplacerObj;
      SelectedKey = itemReplacer.ReplaceWith;

      // Update SelectedItem
      SelectedItem = new ObservableCollection<ItemReplacer>(ItemReplacers
        .Where(x => x.ReplaceWith == SelectedKey)
        .ToList());
    }

    public void OnCellEditEnding() {
    }

  }

  public class ItemReplacer : IItemReplacer {
    public string ReplaceWith { get; set; }
    public string ToReplace { get; set; }
  }

  public interface IItemReplacer {
    string ReplaceWith { get; set; }
    string ToReplace { get; set; }
  }
}
