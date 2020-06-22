using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.Models.CustomerModels {
  class Customer : ICustomer {
    public string Name { get; set; }
    public string PoNumber { get; set; }
    public string TermsRefFullName { get; set; }
  }
}
