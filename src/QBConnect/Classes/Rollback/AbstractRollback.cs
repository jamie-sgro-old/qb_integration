using System;
using System.Collections.Generic;
using QBConnect.Classes.Interfaces;
using QBConnect.Classes.Rollback.Interfaces;
using QBFC13Lib;

namespace QBConnect.Classes.Rollback {
  internal abstract class AbstractRollback : IRollback {

    protected IClientSessionManager _sessionManager { get; set; }

    public void Invoice(string txnId) {
      var txnIds = new List<string> { txnId };
      Invoice(txnIds);
    }

    public void Invoice(List<string> txnIds) {
      IMsgSetRequest msgSetRequest = GetMsgSetRq();

      //Set field value for TxnID
      foreach (var txnId in txnIds) {
        GetTxnRollbackRq(msgSetRequest, txnId);
      }

      Commit(msgSetRequest);
    }

    protected abstract void GetTxnRollbackRq(IMsgSetRequest msgSetRequest, string txnId);

    protected IMsgSetRequest GetMsgSetRq() {
      var req = _sessionManager.CreateMsgSetRequest("US", 13, 0);
      req.Attributes.OnError = ENRqOnError.roeContinue;
      return req;
    }

    protected void Commit(IMsgSetRequest msgSetRequest) {
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
