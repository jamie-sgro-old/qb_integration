using System;
using System.Collections.Generic;
using QBConnect.Classes.Interfaces;
using QBConnect.Classes.Rollback.Interfaces;
using QBFC13Lib;

namespace QBConnect.Classes.Rollback {
  internal abstract class AbstractRollback : IRollback {

    protected IClientSessionManager _sessionManager { get; set; }

    public Tuple<bool, string> Invoice(string txnId) {
      var txnIds = new List<string> { txnId };
      return Invoice(txnIds);
    }

    public Tuple<bool, string> Invoice(List<string> txnIds) {
      IMsgSetRequest msgSetRequest = GetMsgSetRq();

      //Set field value for TxnID
      if (txnIds.Count <= 0) return new Tuple<bool, string>(true, "No invoices to rollback");
      foreach (var txnId in txnIds) {
        GetTxnRollbackRq(msgSetRequest, txnId);
      }

      return Commit(msgSetRequest);
    }

    protected abstract void GetTxnRollbackRq(IMsgSetRequest msgSetRequest, string txnId);

    protected IMsgSetRequest GetMsgSetRq() {
      var req = _sessionManager.CreateMsgSetRequest("US", 13, 0);
      req.Attributes.OnError = ENRqOnError.roeContinue;
      return req;
    }

    protected Tuple<bool, string> Commit(IMsgSetRequest msgSetRequest) {
      var responseMsgSet = _sessionManager.DoRequests(msgSetRequest);

      // Check all response statuses for potential error
      for (var i = 0; i < responseMsgSet.ResponseList.Count; i++) {
        if (responseMsgSet.ResponseList.GetAt(i).StatusMessage != "Status OK") {
          return new Tuple<bool, string>(false, "Could not void all Invoices. " + responseMsgSet.ResponseList.GetAt(i).StatusMessage);
        }
      }

      return new Tuple<bool, string>(true, "Rollback successfully completed");
    }
  }
}
