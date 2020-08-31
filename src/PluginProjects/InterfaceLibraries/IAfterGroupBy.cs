using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibraries {
  public interface IAfterGroupBy<T> : IPlugin {
    List<T> ModifyGrouped(List<T> groupBy);
  }
}
