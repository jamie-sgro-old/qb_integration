using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ItemViewModel : Screen, IItemViewModel<IItemReplacer> {
    private string _type;

    public ItemViewModel() {
      ItemModel = Factory.CreateItemModel();
    }



    public IItemModel<IItemReplacer> ItemModel { get; set; }

    public string SearchBar {
      get => ItemModel.Filter;
      set {
        ItemModel.Filter = value;
        NotifyOfPropertyChange(() => PrimaryPane);
      } 
    }

    public ObservableCollection<IItemReplacer> PrimaryPane => ItemModel.GetUnique();

    public ObservableCollection<IItemReplacer> SecondaryPane {
      get => ItemModel.SelectedItem;
      set => ItemModel.SelectedItem = value;
    }

    public string TabHeader { get; set; } = "Item";
    public void OnSelected() {
    }




    public void OnKeyUp(object itemReplacerObj) {
      var itemReplacer = SafeCast<IItemReplacer>(itemReplacerObj);

      // Pass to model
      ItemModel.ItemSelected(itemReplacer);

      NotifyOfPropertyChange(() => SecondaryPane);
    }

    public void OnCellEditEnding() {
    }

    public void BtnDelete(object itemReplacerObj) {
      var itemReplacer = SafeCast<IItemReplacer>(itemReplacerObj);

      var a = itemReplacer.ToReplace;
      var b = itemReplacer.ReplaceWith;
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
      return (T) obj;
    }
  }
}
