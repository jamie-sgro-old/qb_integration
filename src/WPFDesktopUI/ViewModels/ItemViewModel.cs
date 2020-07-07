using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class ItemViewModel : Screen, IItemViewModel {
    private string _replacer;

    public ItemViewModel() {
      ItemReplacer = new Dictionary<string, ObservableCollection<SimpleStr>> {
        {
          "PSW", new ObservableCollection<SimpleStr> {
          new SimpleStr{ Payload = "Barrie Connie Thompson- PSW"},
          new SimpleStr { Payload = "CLASS - DSW1" }
          }
        },{
        "RN", new ObservableCollection<SimpleStr> {
          new SimpleStr{ Payload = "Barrie Connie Thompson- RN"},
          new SimpleStr { Payload = "CLASS - DSW1" }
        }},
      };
    }

    public string SelectedKey { get; set; } = null;

    /// <summary>
    /// Property that lets the user edit the values inside ItemReplacer
    /// </summary>
    public ObservableCollection<SimpleStr> Replacer {
      get => ItemReplacer[SelectedKey];
      set => ItemReplacer[SelectedKey] = value;
    }

    public struct SimpleStr {
      public string Payload { get; set; }
    }

    public List<string> TempList { get; set; } = new List<string>{"a","b","c"};
    public TextBox ReplacerTextBox { get; set; }

    public Dictionary<string, ObservableCollection<SimpleStr>> ItemReplacer { get; set; }
    public string TabHeader { get; set; } = "Item";

    public void OnSelected() {
    }

    public void OnKeyUp(object itemReplacerObj) {
      var isDict = itemReplacerObj is KeyValuePair<string, ObservableCollection<SimpleStr>>;
      if (! isDict) {
        throw new ArgumentException(@"OnKeyUp() parameter not of type KeyValuePair", nameof(itemReplacerObj));
      }

      // Cast to KeyValuePair like Dict
      var itemReplacer = (KeyValuePair<string, ObservableCollection<SimpleStr>>)itemReplacerObj;
      SelectedKey = itemReplacer.Key;

      // Temp
      var a = ItemReplacer;
    }

    public void OnCellEditEnding() {
    }

  }
}
