using QBConnect.Classes.Interfaces;
using QBFC13Lib;

namespace QBConnect.Classes.Rollback {
  internal sealed class QbDelete : AbstractRollback {
    public QbDelete(IClientSessionManager sessionManager) {
      _sessionManager = sessionManager;
    }

    protected override void GetTxnRollbackRq(IMsgSetRequest msgSetRequest, string txnId) {
      ITxnDel txnDelRq = msgSetRequest.AppendTxnDelRq();
      //Set field value for TxnDelType
      txnDelRq.TxnDelType.SetValue(ENTxnDelType.tdtInvoice);
      //Set field value for TxnID
      txnDelRq.TxnID.SetValue(txnId);
    }
  }
}
