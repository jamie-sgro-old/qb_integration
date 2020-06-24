using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.CustomerModels.Interfaces {
  public interface ICustomerModel<T> : IDb<T> {
    List<ICustomer> Cxs { get; set; }
  }
}
