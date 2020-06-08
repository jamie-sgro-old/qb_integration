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


    private Company _selectedCompany;

    public Company SelectedCompany {
      get { return _selectedCompany; }
      set { _selectedCompany = value; }
    }

    public Dictionary<string, Company> Companies { get; set; }

    public class Company {
      public string Name { get; set; }
      public string Url { get; set; }
      public List<string> Combo { get; set; }
    }







    public QuickBooksSidePaneModel() {
      CustomerRefFullName = new QbAttribute<string>(true, false);
      ItemRef = new QbAttribute<List<string>>(true, true);
      Quantity = new QbAttribute<List<string>>(false, true);
      Rate = new QbAttribute<List<string>>(false, true);
      Attr = new Dictionary<string, QbAttribute<string>>();
      Attr.Add("CustomerRefFullName", new QbAttribute<string>(true, false, "Attr"));
      Companies = new Dictionary<string, Company>
      {
        { "one",
          new Company {
            Name = "Child.Microsoft", Url="www.microsoft.com", Combo=new List<string> {"a","b","c"}
          }
        }, {
          "two",
          new Company {
            Name = "Child.Google", Url="www.google.com", Combo=new List<string> {"a","b","c"}
          }
        }, {
          "three",
          new Company {
            Name = "Child.Apple", Url="www.apple.com", Combo=new List<string> {"a","b","c"}
          }
        }
      };
    }

    public QbAttribute<string> CustomerRefFullName { get; set; }
    public QbAttribute<List<string>> ItemRef { get; set; }
    public QbAttribute<List<string>> Quantity { get; set; }
    public QbAttribute<List<string>> Rate { get; set; }

    public Dictionary<string, QbAttribute<string>> Attr { get; set; }

    public List<QbAttribute<List<string>>> GetQbAttributes() {
      var QbList = new List<QbAttribute<List<string>>>();

      QbList.Add(ItemRef);
      QbList.Add(Quantity);
      QbList.Add(Rate);

      return QbList;
    }

  }
}
