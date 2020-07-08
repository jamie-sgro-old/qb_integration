using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IItemViewModel<T> : IMainTab {

    string SearchBar { get; set; }
    ObservableCollection<T> PrimaryPane { get; set; }
    ObservableCollection<T> SecondaryPane { get; set; }





    /// <summary>
    /// Model containing all data used to populate this Screen / UserControl
    /// </summary>
    ObservableCollection<T> ItemReplacers { get; set; }

    /// <summary>
    /// The data populating the bottom table editable by the user
    /// </summary>
    ObservableCollection<T> SelectedItem { get; set; }

    /// <summary>
    /// A list of distinct values from the ReplaceWith property
    /// to populate the primary datagrid / listview
    /// </summary>
    ObservableCollection<T> UniqueReplaceWith { get; }

    /// <summary>
    /// A dynamic search filter that refines UniqueReplaceWith to
    /// only show data that contains this string
    /// </summary>
    string UniqueReplaceWithFilter { get; set; }

    string SelectedKey { get; set; }

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
    void OnKeyUp(object itemReplacerObj);

  }
}
