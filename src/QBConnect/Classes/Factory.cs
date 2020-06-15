using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Classes.Interfaces;
using QBConnect.Classes.Rollback.Interfaces;

namespace QBConnect.Classes {
  internal static class Factory {
    public static IRollback CreateRollback(IClientSessionManager sessionManager) {
      return new QbVoid(sessionManager);
    }
  }
}
