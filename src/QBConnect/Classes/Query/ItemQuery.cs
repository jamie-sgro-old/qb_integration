using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class ItemQuery : Query {
    internal ItemQuery(IMsgSetRequest msgSetRequest, QBSessionManager qbSessionManager) {
      MsgSetRequest = msgSetRequest;
      QbSessionManager = qbSessionManager;
    }

    protected override dynamic Type { get; } = ENResponseType.rtItemQueryRs;
    protected override void SpecifyQuery() {
      // Query all without any limitations
      var itemQueryRq = MsgSetRequest.AppendItemQueryRq();
    }

    /* Code Snippet:
    var itemQuery = new ItemQuery(requestMsgSet, sessionManager);
    itemQuery.GetList<IORItemRetList>();
    */
    protected override List<string> CompileList<T>(T retList) {
      var itemRetList = (IORItemRetList)retList;
      if (itemRetList == null) return new List<string>();

      var fullNames = new List<string>();

      for (var i = 0; i < itemRetList.Count; i++) {
        var item = itemRetList.GetAt(i);

        // Get all possible item types
        var name = item?.ItemServiceRet?.FullName?.GetValue() ??
                   item?.ItemNonInventoryRet?.FullName?.GetValue() ??
                   item?.ItemOtherChargeRet?.FullName?.GetValue() ??
                   item?.ItemInventoryRet?.FullName?.GetValue() ??
                   item?.ItemInventoryAssemblyRet?.FullName?.GetValue() ??
                   item?.ItemFixedAssetRet?.Name?.GetValue() ??
                   item?.ItemSubtotalRet?.Name?.GetValue() ??
                   item?.ItemDiscountRet?.FullName?.GetValue() ??
                   item?.ItemPaymentRet?.Name?.GetValue() ??
                   item?.ItemSalesTaxRet?.Name?.GetValue() ??
                   item?.ItemSalesTaxGroupRet?.Name?.GetValue() ??
                   item?.ItemGroupRet?.Name?.GetValue();

        if (name == null) continue;

        fullNames.Add(name);
      }

      return fullNames;
    }
  }
}
