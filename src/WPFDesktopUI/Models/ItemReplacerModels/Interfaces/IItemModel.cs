using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.ItemReplacerModels.Interfaces {
  public interface IItemModel<T> {

    /// <summary>
    /// A dynamic search filter that refines UniqueReplaceWith to
    /// only show data that contains this string
    /// </summary>
    string Filter { get; set; }

    /// <summary>
    /// The data populating the bottom table editable by the user
    /// </summary>
    ObservableCollection<T> SelectedItem { get; set; }

    string SelectedKey { get; set; }

    /// <summary>
    /// A list of distinct values from the ReplaceWith property
    /// to populate the primary datagrid / listview
    /// </summary>
    ObservableCollection<T> UniqueReplaceWith();

    void ItemSelected(IItemReplacer selectedItem);
  }
}
