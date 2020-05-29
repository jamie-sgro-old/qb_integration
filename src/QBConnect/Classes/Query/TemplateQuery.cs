using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class TemplateQuery : Query {
    public TemplateQuery(QBSessionManager qbSessionManager) {
      QbSessionManager = qbSessionManager;
      MsgSetRequest = GetMsgSetRequest();
    }


    protected override IMsgSetRequest MsgSetRequest { get; }
    protected override dynamic Type { get; } = ENResponseType.rtTemplateQueryRs;
    protected override void SpecifyQuery() {
      var templateQuery = MsgSetRequest.AppendTemplateQueryRq();
      templateQuery.metaData.SetValue(ENmetaData.mdMetaDataAndResponseData);
    }


    protected override List<string> CompileList<T>(T retList) {
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
