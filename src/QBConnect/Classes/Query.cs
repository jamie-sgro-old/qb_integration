using System.Collections.Generic;
using QBConnect.Models;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal abstract class Query {

    #region Properties

    private IResponseList _responseList;

    internal IMsgSetRequest MsgSetRequest { get; set; }
    internal QBSessionManager QbSessionManager { get; set; }
    internal IResponseList ResponseList {
      get {
        var responseMsgSet = QbSessionManager.DoRequests(MsgSetRequest);
        _responseList = responseMsgSet?.ResponseList;
        return _responseList;
      }
      set => _responseList = value;
    }
    internal abstract dynamic Type { get;  }

    #endregion Properties



    #region Functions

    /// <summary>
    /// Generate request for a template query
    /// to be executed when .DoRequests() is run
    /// </summary>
    internal abstract void SpecifyQuery();

    public List<string> GetList<T>() {
      SpecifyQuery();
      var responseList = ResponseList;
      var response = new Response(responseList);
      if (!response.IsValid(Type)) return new List<string>();

      //upcast to more specific type here, based on the caller of this method
      var retList = (T)response.Payload.Detail;

      return CompileList(retList);
    }

    /// <summary>
    /// From a specified return list, compile and return a list of all desired names
    /// </summary>
    /// <param name="retList">A return list of type generic</param>
    /// <returns>Empty list of strings if null, else a list of desired names</returns>
    internal abstract List<string> CompileList<T>(T retList);

    #endregion Functions
  }
}
