using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal abstract class Query {
    private IResponseList _msgSetResponse;

    internal IMsgSetRequest MsgSetRequest { get; set; }
    internal QBSessionManager QbSessionManager { get; set; }
    internal IResponseList ResponseList {
      get {
        var responseMsgSet = QbSessionManager.DoRequests(MsgSetRequest);
        _msgSetResponse = responseMsgSet?.ResponseList;
        return _msgSetResponse;
      }
      set => _msgSetResponse = value;
    }

    internal abstract void SpecifyQuery();
  }
}
