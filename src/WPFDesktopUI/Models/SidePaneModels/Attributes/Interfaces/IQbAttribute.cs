using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces {
  internal interface IQbAttribute<T> {
    T Payload { get; set; }
    bool IsMandatory { get; }
    bool HasHeaderDropDown { get; }

  }
}
