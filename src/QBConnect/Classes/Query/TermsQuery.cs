using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes.Query {
  internal sealed class TermsQuery : AbstractQuery {
    public TermsQuery(QBSessionManager qbSessionManager) {
      QbSessionManager = qbSessionManager;
      MsgSetRequest = GetMsgSetRequest();
    }

    protected override IMsgSetRequest MsgSetRequest { get; }
    protected override dynamic Type { get; } = ENResponseType.rtTermsQueryRs;
    protected override void SpecifyQuery() {
      var termsQueryRq = MsgSetRequest.AppendTermsQueryRq();
    }

    protected override List<string> CompileList<T>(T retList) {
      var termsRetList = (IORTermsRetList)retList;
      if (termsRetList == null) return new List<string>();

      var names = new List<string>();

      for (var i = 0; i < termsRetList.Count; i++) {
        var term = termsRetList.GetAt(i);

        // Get all possible term types
        var name = (string)term?.DateDrivenTermsRet?.Name?.GetValue() ??
                   (string)term?.StandardTermsRet?.Name?.GetValue();

        if (name == null) continue;

        names.Add(name);
      }

      return names;
    }
  }
}
