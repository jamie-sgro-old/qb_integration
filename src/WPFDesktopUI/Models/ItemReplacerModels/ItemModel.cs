using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  public class ItemModel : IItemModel<IItemReplacer> {
    public ItemModel() {
      _sourceData = new ObservableCollection<IItemReplacer> {
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

    /// <summary>
    /// Model containing all data used to populate this Screen / UserControl
    /// </summary>
    private ObservableCollection<IItemReplacer> _sourceData { get; set; }

    /// <summary>
    /// The data of the currently selected data item / row
    /// </summary>
    private IItemReplacer _selectedKey { get; set; }

    public ObservableCollection<IItemReplacer> SelectedItem { get; set; }

    public ObservableCollection<IItemReplacer> GetUnique() {
      return new ObservableCollection<IItemReplacer>(_sourceData
        .GroupBy(x => x.ReplaceWith)
        .Select(x => x.First())
        .Where(x => x.ReplaceWith.ToLower().Contains(Filter.ToLower()))
        .ToList());
    }

    public void ItemSelected(IItemReplacer selectedItem) {
      _selectedKey = selectedItem;
      UpdateSelectedItem();
    }

    public string Filter { get; set; } = "";

    public void Create<T>(List<T> dataList) {



      // TODO: add sql

    }

    public ObservableCollection<T> Read<T>() {
      throw new NotImplementedException();
      // TODO: add sql

    }

    public void Update<T>(ObservableCollection<T> dataList) {
      throw new NotImplementedException();
      // TODO: add sql

    }

    public void Destroy<T>(ObservableCollection<T> dataList) {
      if (!(dataList is ObservableCollection<IItemReplacer> itemReplacer)) {
        throw new ArgumentException(@"itemReplacer parameter could not be cast to " + typeof(IItemReplacer), nameof(dataList));
      }

      foreach (var datum in itemReplacer) {
        _sourceData
          .Remove(_sourceData
            .Single(x => x.ToReplace == datum.ToReplace &&
                    x.ReplaceWith == datum.ReplaceWith));
      }

      UpdateSelectedItem();

      // TODO: add sql
    }

    private void UpdateSelectedItem() {
      SelectedItem = new ObservableCollection<IItemReplacer>(_sourceData
        .Where(x => x.ReplaceWith == _selectedKey.ReplaceWith)
        .ToList());
    }
  }
}
