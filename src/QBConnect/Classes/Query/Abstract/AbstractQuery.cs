using System.Collections.Generic;
using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal abstract class AbstractQuery : IQuery {

    #region Properties

    protected abstract IMsgSetRequest MsgSetRequest { get; }
    public QBSessionManager QbSessionManager { get; set; }
    protected abstract dynamic Type { get;  }

    #endregion Properties



    #region Functions

    /// <summary>
    /// Generate request for a template query
    /// to be executed when .DoRequests() is run
    /// </summary>
    protected abstract void SpecifyQuery();

    public List<string> GetList<T>() {
      SpecifyQuery();
      var responseList = GetResponseList();
      var response = new Response(responseList);
      if (!response.IsValid(Type)) return new List<string>();

      //upcast to more specific type here, based on the caller of this method
      var retList = (T)response.Payload.Detail;

      return CompileList(retList);
    }

    private IResponseList GetResponseList() {
      var responseMsgSet = QbSessionManager.DoRequests(MsgSetRequest);
      return responseMsgSet?.ResponseList;
    }

    protected IMsgSetRequest GetMsgSetRequest() {
      var msgSetRequest = QbSessionManager.CreateMsgSetRequest("US", 13, 0);
      msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;
      return msgSetRequest;
    }

    /// <summary>
    /// From a specified return list, compile and return a list of all desired names
    /// </summary>
    /// <param name="retList">A return list of type generic</param>
    /// <returns>Empty list of strings if null, else a list of desired names</returns>
    protected abstract List<string> CompileList<T>(T retList);

    #endregion Functions
  }
}
