using QBConnect.Classes.Interfaces;
using QBConnect.Classes.Rollback;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class QbVoid : AbstractRollback {
    public QbVoid(IClientSessionManager sessionManager) {
      _sessionManager = sessionManager;
    }

    protected override void GetTxnRollbackRq(IMsgSetRequest msgSetRequest, string txnId) {
      // Create new void request
      ITxnVoid txnVoidRq = msgSetRequest.AppendTxnVoidRq();

      //Set field value for TxnVoidType
      txnVoidRq.TxnVoidType.SetValue(ENTxnVoidType.tvtInvoice);

      txnVoidRq.TxnID.SetValue(txnId);
    }
  }
}
