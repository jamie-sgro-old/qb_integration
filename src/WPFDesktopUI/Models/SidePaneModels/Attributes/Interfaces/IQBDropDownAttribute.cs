using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces.Payloads;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces {
  public interface IQBDropDownAttribute : IQbAttribute, IDropDownPayload {
    IQbComboBox DropDownComboBox { get; set; }
  }
}
