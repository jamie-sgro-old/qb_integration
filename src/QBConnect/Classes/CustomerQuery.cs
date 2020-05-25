using System;
using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class CustomerQuery : Query {
    public CustomerQuery(IMsgSetRequest msgSetRequest, QBSessionManager qbSessionManager) {
      MsgSetRequest = msgSetRequest;
      QbSessionManager = qbSessionManager;
    }

    protected override dynamic Type { get; } = ENResponseType.rtCustomerQueryRs;
    protected override void SpecifyQuery() {
      ICustomerQuery CustomerQuery = MsgSetRequest.AppendCustomerQueryRq();
      //CustomerQuery.ORCustomerListQuery.CustomerListFilter.TotalBalanceFilter.Operator.SetValue(ENOperator.oGreaterThanEqual);
      //CustomerQuery.ORCustomerListQuery.CustomerListFilter.TotalBalanceFilter.Amount.SetValue(0);
    }

    /* Code Snippet:
    var customerQuery = new CustomerQuery(requestMsgSet, sessionManager);
    customerQuery.GetList<ICustomerRetList>();
    */
    protected override List<string> CompileList<T>(T retList) {
      var customerRetList = (ICustomerRetList)retList;
      if (customerRetList == null) return new List<string>();

      var names = new List<string>();

      for (var i = 0; i < customerRetList.Count; i++) {
        var template = customerRetList.GetAt(i);
        var name = (string)template.FullName.GetValue();

        names.Add(name);
      }

      return names;
    }
  }
}
