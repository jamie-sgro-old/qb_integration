using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.Models.CustomerModels {
  class CustomerModel : ICustomerModel, INotifyPropertyChanged {
    public CustomerModel() {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public IDb Db { get; }
  }
}
