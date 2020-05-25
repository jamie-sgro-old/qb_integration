using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class TemplateQuery : Query {
    public TemplateQuery(IMsgSetRequest msgSetRequest, QBSessionManager qbSessionManager) {
      MsgSetRequest = msgSetRequest;
      QbSessionManager = qbSessionManager;
    }

    internal override dynamic Type { get; } = ENResponseType.rtTemplateQueryRs;


    internal override void SpecifyQuery() {
      var templateQuery = MsgSetRequest.AppendTemplateQueryRq();
      templateQuery.metaData.SetValue(ENmetaData.mdMetaDataAndResponseData);
    }


    internal override List<string> CompileList<T>(T retList) {
      var templateRetList = (ITemplateRetList)retList;
      if (templateRetList == null) return new List<string>();

      var names = new List<string>();

      for (var i = 0; i < templateRetList.Count; i++) {
        var template = templateRetList.GetAt(i);
        var name = (string)template.Name.GetValue();

        names.Add(name);
      }

      return names;
    }
  }
}
