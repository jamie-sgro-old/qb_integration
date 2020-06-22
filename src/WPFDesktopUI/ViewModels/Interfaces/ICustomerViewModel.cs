using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.ViewModels.Interfaces {
  public interface ICustomerViewModel : IMainTab, IQbInteractable {


    DataGrid CustomerGrid { get; set; }

    //List<ICustomer> Cxs { get; set; }
  }
}
