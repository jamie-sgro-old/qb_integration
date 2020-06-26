using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.CustomerModels.Interfaces {
  public interface ICustomer {

    string Name { get; }

    string PoNumber { get; set; }

    string TermsRefFullName { get; set; }

    bool ServiceCharge { get; set; }
  }
}
