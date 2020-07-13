using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  public class ItemReplacer : IItemReplacer {
    public ItemReplacer(string replaceWith, string toReplace) {
      ReplaceWith = replaceWith;
      ToReplace = toReplace;
    }

    public double Id { get; }
    public string ReplaceWith { get; }
    public string ToReplace { get; set; }
  }
}
