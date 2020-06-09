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
      return new QuickBooksSidePaneModel(CreateQbComboBox);
    }

    public static IQbStringAttribute CreateQbStringAttribute() {
      return new QbStringAttribute();
    }

    public static IQbDateTimeAttribute CreateQbDateTimeAttribute() {
      return new QbDateTimeAttribute();
    }
    public static IQbAttribute CreateQbNullAttribute() {
      return new QbNullAttribute();
    }

    public static IQbComboBox CreateQbComboBox() {
      return new QbComboBox();
    }
  }
}
