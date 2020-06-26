using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes.Query {
  internal sealed class CustomerQuery : AbstractQuery {
    public CustomerQuery(QBSessionManager qbSessionManager) {
      QbSessionManager = qbSessionManager;
      MsgSetRequest = GetMsgSetRequest();
    }

  protected override IMsgSetRequest MsgSetRequest { get; }
    protected override dynamic Type { get; } = ENResponseType.rtCustomerQueryRs;
    protected override void SpecifyQuery() {
      var customerQueryRq = MsgSetRequest.AppendCustomerQueryRq();
    }

    protected override List<string> CompileList<T>(T retList) {
      var customerRetList = (ICustomerRetList)retList;
      if (customerRetList == null) return new List<string>();

      var names = new List<string>();

      for (var i = 0; i < customerRetList.Count; i++) {
        var cx = customerRetList.GetAt(i);

        // Get all possible term types
        var name = cx?.Name.GetValue();

        if (name == null) continue;

        names.Add(name);
      }

      return names;
    }
  }
}
