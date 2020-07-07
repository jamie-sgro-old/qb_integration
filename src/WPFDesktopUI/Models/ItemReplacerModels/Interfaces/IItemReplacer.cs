using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.ItemReplacerModels.Interfaces {
  public interface IItemReplacer {
    string ReplaceWith { get; set; }
    string ToReplace { get; set; }
  }
}
