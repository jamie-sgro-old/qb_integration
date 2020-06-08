using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public static class Factory {

    public static IQuickBooksSidePaneModel CreateQuickBooksSidePaneModel() {
      return new QuickBooksSidePaneModel(CreateQbAttribute, CreateQbComboBox);
    }

    public static IQbAttribute CreateQbAttribute() {
      return new QbAttribute();
    }

    public static IQbComboBox CreateQbComboBox() {
      return new QbComboBox();
    }
  }
}
