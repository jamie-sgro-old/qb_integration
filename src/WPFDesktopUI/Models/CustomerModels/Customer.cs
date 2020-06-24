using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.Models.CustomerModels {
  public class Customer : ICustomer {
    public string Name { get; }

    public string PoNumber { get; set; }
    public string TermsRefFullName { get; set; }
    public bool ServiceCharge { get; set; }
    public double PswRate { get; set; }
    public double RpnRate { get; set; }
    public double RnRate { get; set; }
    public double DswRate { get; set; }
    public double HcaRate { get; set; }
  }
}
