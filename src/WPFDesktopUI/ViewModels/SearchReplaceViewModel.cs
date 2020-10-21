using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public abstract class SearchReplaceViewModel : Screen, IItemViewModel<IItemReplacer> {

    /// <summary>
    /// A list of rows with unsaved changes.
    /// </summary>
    protected HashSet<IItemReplacer> _backlog { get; set; }

    public abstract ISearchReplaceModel<IItemReplacer> SearchReplaceModel { get; set; }
    public bool CanBtnUpdate { get; set; } = false;
    public bool CanBtnInsert => SecondaryPane != null;
    public bool CanBtnAdd => !string.IsNullOrEmpty(SearchBar);


    public string SearchBar {
      get => SearchReplaceModel.Filter;
      set {
        SearchReplaceModel.Filter = value;
        NotifyOfPropertyChange(() => PrimaryPane);
      }
    }

    public ObservableCollection<IItemReplacer> PrimaryPane => SearchReplaceModel.GetUnique();

    public ObservableCollection<IItemReplacer> SecondaryPane {
      get => SearchReplaceModel.SelectedItem;
      set => SearchReplaceModel.SelectedItem = value;
    }

    public string TabHeader { get; set; } = "Item";
    public void OnSelected() {
    }




    public void OnKeyUp(object itemReplacerObj) {
      if (itemReplacerObj == null) return;
      var itemReplacer = SafeCast<IItemReplacer>(itemReplacerObj);

      // Pass to model
      SearchReplaceModel.ItemSelected(itemReplacer);

      NotifyOfPropertyChange(() => SecondaryPane);
      NotifyOfPropertyChange(() => CanBtnInsert);
    }

    public void BtnAdd() {
      // First save any unsaved changes
      BtnUpdate();

      if (string.IsNullOrEmpty(SearchBar)) return;
      var itemReplacer = Factory.CreateItemReplacer(SearchBar, "");

      SearchReplaceModel.Create(new List<IItemReplacer> { itemReplacer });

      NotifyOfPropertyChange(() => PrimaryPane);
      NotifyOfPropertyChange(() => SecondaryPane);
    }


    public void BtnInsert() {
      if (SearchReplaceModel.SelectedKey == null) return;

      // First save any unsaved changes
      BtnUpdate();

      var itemReplacer = Factory.CreateItemReplacer(SearchReplaceModel.SelectedKey.ReplaceWith, "");

      SearchReplaceModel.Create(new List<IItemReplacer> { itemReplacer });

      NotifyOfPropertyChange(() => PrimaryPane);
      NotifyOfPropertyChange(() => SecondaryPane);
    }

    public void OnCellEditEnding(object itemReplacerObj) {
      if (itemReplacerObj == null) return;
      var itemReplacer = SafeCast<IItemReplacer>(itemReplacerObj);

      _backlog.Add(itemReplacer);

      TabHeader = TabHeader.Replace("*", "");
      TabHeader += "*";
      CanBtnUpdate = true;
    }

    public void Update() {
      if (_backlog.Count <= 0) return;

      // Update SQL database with user changes
      SearchReplaceModel.Update(new ObservableCollection<IItemReplacer>(_backlog));
      _backlog.Clear();
    }

    public void BtnUpdate() {
      Update();
      TabHeader = TabHeader.Replace("*", "");
      CanBtnUpdate = false;
    }

    public void BtnDelete(object itemReplacerObj) {
      if (itemReplacerObj == null) return;
      var itemReplacer = SafeCast<IItemReplacer>(itemReplacerObj);

      SearchReplaceModel.Destroy(new ObservableCollection<IItemReplacer> { itemReplacer });

      NotifyOfPropertyChange(() => PrimaryPane);
      NotifyOfPropertyChange(() => SecondaryPane);
    }

    /// <summary>
    /// Try to cast variable based on generic type T.
    /// Throws an error if the cast cannot be completed.
    /// Used to convert XAML parameters back to the original type from MV.
    /// </summary>
    /// <typeparam name="T">Interface to cast to</typeparam>
    /// <param name="obj">Parameter that needs to be cast</param>
    /// <returns>Cast variable</returns>
    private T SafeCast<T>(object obj) {
      if (!(obj is T)) {
        throw new ArgumentException(@"object parameter could not be cast to " + typeof(T), nameof(obj));
      }
      return (T)obj;
    }
  }
}
