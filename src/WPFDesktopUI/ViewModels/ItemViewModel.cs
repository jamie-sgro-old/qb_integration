using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ItemViewModel : Screen, IItemViewModel {
    public ItemViewModel() {
      ItemReplacers = new ObservableCollection<IItemReplacer> {
        Factory.CreateItemReplacer("PSW", "Barrie Connie Thompson- PSW"),
        Factory.CreateItemReplacer("PSW", "CLASS - PSW1"),
        Factory.CreateItemReplacer("RN", "Barrie Connie Thompson- RN"),
        Factory.CreateItemReplacer("RN", "CLASS - RN1")
      };
    }



    public string TabHeader { get; set; } = "Item";

    /// <summary>
    /// Model containing all data used to populate this Screen / UserControl
    /// </summary>
    public ObservableCollection<IItemReplacer> ItemReplacers { get; set; }

    /// <summary>
    /// The data populating the bottom table editable by the user
    /// </summary>
    public ObservableCollection<IItemReplacer> SelectedItem { get; set; }

    /// <summary>
    /// A list of distinct values from the ReplaceWith property
    /// to populate the first datagrid / listview
    /// </summary>
    public ObservableCollection<IItemReplacer> UniqueReplaceWith {
      get {
        return new ObservableCollection<IItemReplacer>(ItemReplacers
          .GroupBy(x => x.ReplaceWith)
          .Select(x => x.First())
          .ToList());
      }
      //set => _uniqueReplaceWith = value;
    }

    public void OnSelected() {
    }

    /// <summary>
    /// Fires when the top datagrid / listview is selected.
    /// Updates the bottom datagrid with a list of rows with
    /// matching [ReplaceWith] properties
    /// </summary>
    /// <param name="itemReplacerObj">
    /// A single ItemReplacer indicating which ListViewItem was selected
    /// If whitespace was selected instead, indicates the last selected /
    /// currently active ListViewItem
    /// </param>
    public void OnKeyUp(object itemReplacerObj) {
      var isDict = itemReplacerObj is IItemReplacer;
      if (! isDict) {
        throw new ArgumentException(@"OnKeyUp() parameter not of type " + typeof(IItemReplacer), nameof(itemReplacerObj));
      }

      // Cast to KeyValuePair like Dict
      var itemReplacer = (IItemReplacer)itemReplacerObj;
      var selectedKey = itemReplacer.ReplaceWith;

      // Update SelectedItem
      SelectedItem = new ObservableCollection<IItemReplacer>(ItemReplacers
        .Where(x => x.ReplaceWith == selectedKey)
        .ToList());
      var a = ItemReplacers;
    }

    public void OnCellEditEnding() {
    }

  }
}
