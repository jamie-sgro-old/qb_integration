using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.ItemReplacerModels.Interfaces {
  public interface IItemReplacer {
    double Id { get; }
    string ReplaceWith { get; }
    string ToReplace { get; set; }
  }
}
