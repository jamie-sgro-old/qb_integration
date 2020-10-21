using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.DbModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface IItemViewModel<T> : IMainTab, IDbViewModel {
    ISearchReplaceModel<IItemReplacer> SearchReplaceModel { get; set; }
    string SearchBar { get; set; }
    ObservableCollection<T> PrimaryPane { get; }
    ObservableCollection<T> SecondaryPane { get; set; }

    bool CanBtnInsert { get; }
    bool CanBtnAdd { get; }


    /// <summary>
    /// Fires when the top datagrid / listview is selected.
    /// </summary>
    /// <param name="itemReplacerObj">
    /// A single ItemReplacer indicating which ListViewItem was selected
    /// If whitespace was selected instead, indicates the last selected /
    /// currently active ListViewItem
    /// </param>
    void OnKeyUp(object itemReplacerObj);

    /// <summary>
    /// Event that triggers when the WPF datagrid cells have been edited by the user.
    /// </summary>
    /// <param name="itemReplacerObj"></param>
    void OnCellEditEnding(object itemReplacerObj);

    /// <summary>
    /// Add new data where the [ReplaceWith] property equals the text in
    /// the search bar
    /// </summary>
    void BtnAdd();


    /// <summary>
    /// Add new row to currently existing dataset based on shared
    /// [ReplaceWith] property
    /// </summary>
    void BtnInsert();

    /// <summary>
    /// Send all unsaved changes to model
    /// </summary>
    void Update();

    /// <summary>
    /// Event that triggers when the user pressed the 'Delete' button in the Datagrid row
    /// </summary>
    /// <param name="itemReplacerObj">Data in the current row</param>
    void BtnDelete(object itemReplacerObj);

  }
}
