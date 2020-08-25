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
    public string Class { get; set; }
    public string AppendLineItem1 { get; set; }
    public string AppendLineItem2 { get; set; }
    public string AppendLineItem3 { get; set; }
  }
}
