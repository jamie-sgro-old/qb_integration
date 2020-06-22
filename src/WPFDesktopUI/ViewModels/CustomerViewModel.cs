using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class CustomerViewModel : Screen, ICustomerViewModel {
    public CustomerViewModel() {
      CustomerModel = Factory.CreateCustomerModel();

      Cxs = new List<ICustomer> {
        new Customer {
          Name = "Customer1",
          PoNumber = "1234a",
          TermsRefFullName = "Net 30"
        },
        new Customer {
          Name = "Customer2",
          PoNumber = "1234b",
          TermsRefFullName = "Net 15"
        }
      };
    }

    public string ConsoleMessage { get; set; }
    public bool CanQbInteract { get; set; }
    public bool QbProgressBarIsVisible { get; set; }
    public Task QbInteract() {
      throw new NotImplementedException();
    }

    public static ICustomerModel CustomerModel { get; set; }
    public DataGrid CustomerGrid { get; set; }
    public static List<ICustomer> Cxs { get; set; }


    public void OnSelected() {
    }
  }
}
