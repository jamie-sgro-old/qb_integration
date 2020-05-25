using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal sealed class TemplateQuery : Query {
    public TemplateQuery(IMsgSetRequest msgSetRequest, QBSessionManager qbSessionManager) {
      MsgSetRequest = msgSetRequest;
      QbSessionManager = qbSessionManager;
    }
    
    /// <summary>
    /// Generate request for a template query
    /// to be executed when .DoRequests() is run
    /// </summary>
    internal override void SpecifyQuery() {
      var templateQuery = MsgSetRequest.AppendTemplateQueryRq();
      templateQuery.metaData.SetValue(ENmetaData.mdMetaDataAndResponseData);
    }

    public List<string> GetList() {
      SpecifyQuery();
      var responseList = ResponseList;
      var response = new Response(responseList);
      if (!response.IsValid()) return new List<string>();

      //upcast to more specific type here, this is safe because we checked with response
      var templateRetList = (ITemplateRetList)response.Payload.Detail;

      return GetTemplateNames(templateRetList);
    }

    /// <summary>
    /// From a template return list, compile and return a list of all template names
    /// </summary>
    /// <param name="templateRetList">A template return list</param>
    /// <returns>Empty list of strings if null, else a list of template names</returns>
    private static List<string> GetTemplateNames(ITemplateRetList templateRetList) {
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
