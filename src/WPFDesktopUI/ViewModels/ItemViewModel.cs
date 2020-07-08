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
    /// A dynamic search filter that refines UniqueReplaceWith to
    /// only show data that contains this string
    /// </summary>
    public string UniqueReplaceWithFilter { get; set; } = "";

    public string SelectedKey { get; set; }

    /// <summary>
    /// A list of distinct values from the ReplaceWith property
    /// to populate the primary datagrid / listview
    /// </summary>
    public ObservableCollection<IItemReplacer> UniqueReplaceWith {
      get {
        return new ObservableCollection<IItemReplacer>(ItemReplacers
          .GroupBy(x => x.ReplaceWith)
          .Select(x => x.First())
          .Where(x => x.ReplaceWith.ToLower().Contains(UniqueReplaceWithFilter.ToLower()))
          .ToList());
      }
    }

    public void OnSelected() {
    }

    /// <summary>
    /// Fires when the top datagrid / listview is selected.
    /// Updates the secondary datagrid with a list of rows with
    /// matching [ReplaceWith] properties
    /// </summary>
    /// <param name="itemReplacerObj">
    /// A single ItemReplacer indicating which ListViewItem was selected
    /// If whitespace was selected instead, indicates the last selected /
    /// currently active ListViewItem
    /// </param>
    public void OnKeyUp(object itemReplacerObj) {
      var IsValidType = itemReplacerObj is IItemReplacer;
      if (!IsValidType) {
        throw new ArgumentException(@"OnKeyUp() parameter not of type " + typeof(IItemReplacer), nameof(itemReplacerObj));
      }

      // Cast to KeyValuePair like Dict
      var itemReplacer = (IItemReplacer)itemReplacerObj;
      SelectedKey = itemReplacer.ReplaceWith;

      // Update SelectedItem
      SelectedItem = new ObservableCollection<IItemReplacer>(ItemReplacers
        .Where(x => x.ReplaceWith == SelectedKey)
        .ToList());
      var a = ItemReplacers;
    }

    public void OnCellEditEnding() {
    }

  }
}
