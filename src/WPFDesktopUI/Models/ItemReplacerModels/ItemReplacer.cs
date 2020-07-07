using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;

namespace WPFDesktopUI.Models.ItemReplacerModels {
  public class ItemReplacer : IItemReplacer {
    public string ReplaceWith { get; set; }
    public string ToReplace { get; set; }
  }
}
