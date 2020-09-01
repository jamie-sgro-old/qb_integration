using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Models.Interfaces;

namespace InterfaceLibraries {
  public interface IAfterGroupBy : IPlugin {
    List<IInvoice> ModifyGrouped(List<IInvoice> groupBy);
  }
}
