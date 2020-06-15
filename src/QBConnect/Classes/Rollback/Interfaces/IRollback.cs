using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect.Classes.Rollback.Interfaces {
  internal interface IRollback {
    void Invoice(string id);
    void Invoice(List<string> ids);
  }
}
