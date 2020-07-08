using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.ItemReplacerModels.Interfaces {
  public interface IItemModel {

    /// <summary>
    /// A dynamic search filter that refines UniqueReplaceWith to
    /// only show data that contains this string
    /// </summary>
    string Filter { get; set; }
  }
}
