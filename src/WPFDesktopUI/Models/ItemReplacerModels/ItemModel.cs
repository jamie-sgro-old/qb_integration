﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  public class ItemModel : IItemModel<IItemReplacer> {
    public ItemModel() {
      _sourceData = new ObservableCollection<IItemReplacer>(Read());
    }

    /// <summary>
    /// Model containing all data used to populate this Screen / UserControl
    /// </summary>
    private ObservableCollection<IItemReplacer> _sourceData { get; set; }

    public IItemReplacer SelectedKey { get; private set; }

    public ObservableCollection<IItemReplacer> SelectedItem { get; set; }

    public ObservableCollection<IItemReplacer> GetUnique() {
      return new ObservableCollection<IItemReplacer>(_sourceData
        .GroupBy(x => x.ReplaceWith)
        .Select(x => x.First())
        .Where(x => x.ReplaceWith.ToLower().Contains(Filter.ToLower()))
        .ToList());
    }

    public void ItemSelected(IItemReplacer selectedItem) {
      SelectedKey = selectedItem;
      UpdateSelectedItem();
    }

    public string Filter { get; set; } = "";

    public void Create<T>(List<T> dataList) {
      if (!(dataList is List<IItemReplacer> itemReplacer)) {
        throw new ArgumentException(@"dataList parameter could not be cast to " + typeof(IItemReplacer), nameof(dataList));
      }

      foreach (var item in itemReplacer) {
        _sourceData.Add(item);
      }

      UpdateSelectedItem();

      // Update SQL
      SqliteDataAccess.SaveData(
        @"INSERT OR IGNORE INTO `item` (ReplaceWith, ToReplace)
          VALUES (@ReplaceWith, @ToReplace);", dataList);
    }

    /// <summary>
    /// Dapper requires concrete implementations for sql queries
    /// Essentially a private version of ItemReplacer
    /// </summary>
    private class TempItemReplacer : IItemReplacer {
      public string ReplaceWith { get; }
      public string ToReplace { get; set; }
    }

    public ObservableCollection<IItemReplacer> Read() {
      const string query = "SELECT Id, * FROM item";
      var list = SqliteDataAccess.LoadData<TempItemReplacer>(query);

      // Cast to observable collection
      var collection = new ObservableCollection<IItemReplacer>(list);
      return collection;
    }

    public void Update<T>(ObservableCollection<T> dataList) {
      throw new NotImplementedException();
      // TODO: add sql

    }

    public void Destroy<T>(ObservableCollection<T> dataList) {
      if (!(dataList is ObservableCollection<IItemReplacer> itemReplacer)) {
        throw new ArgumentException(@"dataList parameter could not be cast to " + typeof(IItemReplacer), nameof(dataList));
      }

      foreach (var item in itemReplacer) {
        _sourceData
          .Remove(_sourceData
            .Single(x => x.ToReplace == item.ToReplace &&
                    x.ReplaceWith == item.ReplaceWith));
      }

      UpdateSelectedItem();

      // TODO: add sql
    }

    private void UpdateSelectedItem() {
      if (SelectedKey == null) return;
      if (string.IsNullOrEmpty(SelectedKey?.ReplaceWith)) return;

      SelectedItem = new ObservableCollection<IItemReplacer>(_sourceData
        .Where(x => x.ReplaceWith == SelectedKey.ReplaceWith)
        .ToList());
    }
  }
}
