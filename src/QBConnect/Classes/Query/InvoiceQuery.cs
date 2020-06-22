using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes.Query {
  internal sealed class InvoiceQuery : AbstractQuery {
    public InvoiceQuery(QBSessionManager qbSessionManager) {
      QbSessionManager = qbSessionManager;
      MsgSetRequest = GetMsgSetRequest();
    }

    protected override IMsgSetRequest MsgSetRequest { get; }
    protected override dynamic Type { get; } = ENResponseType.rtInvoiceQueryRs;
    protected override void SpecifyQuery() {
      var invoiceQueryRq = MsgSetRequest.AppendInvoiceQueryRq();
    }

    protected override List<string> CompileList<T>(T retList) {
      var invoiceRetList = (IInvoiceRetList) retList;
      if (invoiceRetList == null) return new List<string>();

      var ids = new List<string>();

      for (var i = 0; i < invoiceRetList.Count; i++) {
        var invoice = invoiceRetList.GetAt(i);
        var id = invoice.TxnID.GetValue();

        ids.Add(id);
      }

      return ids;
    }
  }
}
