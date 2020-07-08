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
      var IsValidType = itemReplacerObj is IItemReplacer;
      if (!IsValidType) {
        throw new ArgumentException(@"OnKeyUp() parameter not of type " + typeof(IItemReplacer), nameof(itemReplacerObj));
      }

      // Cast to KeyValuePair like Dict
      var itemReplacer = (IItemReplacer)itemReplacerObj;

      // Pass to model
      ItemModel.ItemSelected(itemReplacer);

      NotifyOfPropertyChange(() => SecondaryPane);
    }

    public void OnCellEditEnding() {
    }
  }
}
