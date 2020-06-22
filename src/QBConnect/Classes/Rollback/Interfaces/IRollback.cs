using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect.Classes.Rollback.Interfaces {
  internal interface IRollback {
    Tuple<bool, string> Invoice(string id);
    Tuple<bool, string> Invoice(List<string> ids);
  }
}
