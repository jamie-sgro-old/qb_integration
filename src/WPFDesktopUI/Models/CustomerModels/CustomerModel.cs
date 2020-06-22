using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.Models.CustomerModels {
  class CustomerModel : ICustomerModel {
    public CustomerModel() {
      Cxs = new Dictionary<string, ICustomer>();
      Cxs.Add("CLASS", new Customer());
      Cxs["CLASS"].Name = "Customer1";
      Cxs["CLASS"].PoNumber = "1234a";
      Cxs["CLASS"].TermsRefFullName = "Net 30";

      Cxs.Add("test", new Customer());
      Cxs["test"].Name = "Customer1";
      Cxs["test"].PoNumber = "1234a";
      Cxs["test"].TermsRefFullName = "Net 30";

    }

    public IDb Db { get; }
    public Dictionary<string, ICustomer> Cxs { get; set; }
  }
}
