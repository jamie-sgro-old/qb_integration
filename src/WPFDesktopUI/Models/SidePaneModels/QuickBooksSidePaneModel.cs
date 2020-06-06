using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.Models.SidePaneModels.Attributes;

namespace WPFDesktopUI.Models {
  public class QuickBooksSidePaneModel {
    public QuickBooksSidePaneModel() {
      CustomerRefFullName = new QbAttribute<string>(true, false);
      ItemRef = new QbAttribute<List<string>>(true, true);
      Quantity = new QbAttribute<List<string>>(false, true);
      Rate = new QbAttribute<List<string>>(false, true);
    }

    public QbAttribute<string> CustomerRefFullName { get; set; }
    public QbAttribute<List<string>> ItemRef { get; set; }
    public QbAttribute<List<string>> Quantity { get; set; }
    public QbAttribute<List<string>> Rate { get; set; }

    public List<QbAttribute<List<string>>> GetQbAttributes() {
      var QbList = new List<QbAttribute<List<string>>>();

      QbList.Add(ItemRef);
      QbList.Add(Quantity);
      QbList.Add(Rate);

      return QbList;
    }

  }
}
