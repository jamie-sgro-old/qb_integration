using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Classes.Interfaces;
using QBConnect.Classes.Rollback.Interfaces;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal class QbVoid : IRollback {
    public QbVoid(IClientSessionManager sessionManager) {
      _sessionManager = sessionManager;
    }

    private IClientSessionManager _sessionManager { get; }

    public void Invoice(string txnId) {
      var txnIds = new List<string> {txnId};
      Invoice(txnIds);
    }

    public void Invoice(List<string> txnIds) {
      IMsgSetRequest msgSetRequest = GetMsgSetRq();

      //Set field value for TxnID
      foreach (var txnId in txnIds) {
        GetTxnVoidRq(msgSetRequest, txnId);
      }

      Commit(msgSetRequest);
    }

    private IMsgSetRequest GetMsgSetRq() {
      var req = _sessionManager.CreateMsgSetRequest("US", 13, 0);
      req.Attributes.OnError = ENRqOnError.roeContinue;
      return req;
    }

    private void GetTxnVoidRq(IMsgSetRequest msgSetRequest, string txnId) {
      // Create new void request
      ITxnVoid txnVoidRq = msgSetRequest.AppendTxnVoidRq();

      //Set field value for TxnVoidType
      txnVoidRq.TxnVoidType.SetValue(ENTxnVoidType.tvtInvoice);

      txnVoidRq.TxnID.SetValue(txnId);
    }

    private void Commit(IMsgSetRequest msgSetRequest) {
      var responseMsgSet = _sessionManager.DoRequests(msgSetRequest);

      // Check all response statuses for potential error
      for (var i = 0; i < responseMsgSet.ResponseList.Count; i++) {
        if (responseMsgSet.ResponseList.GetAt(i).StatusMessage != "Status OK") {
          throw new Exception("Could not void all Invoices. " + responseMsgSet.ResponseList.GetAt(i).StatusMessage);
        }
      }
    }
  }
}
