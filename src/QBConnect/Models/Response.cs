using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Models {
  internal class Response {
    internal Response(IResponseList responseList) {
      // Get data from first response from DoRequest
      // (this only works if our desired req is the first one specified)
      if (responseList.Count > 1) {
        throw new ArgumentOutOfRangeException(
          paramName: nameof(responseList),
          message: "More than one element found is responseList. "+
                   "The Response Model was expecting 1.");
      }

      Payload = responseList.GetAt(0);
    }

    internal IResponse Payload { get; set; }

    internal bool IsValid(ENResponseType type) {
      // Check status code, 0=ok, >0 is warning
      if (Payload.StatusCode < 0) return false;

      // The request-specific response is in the details
      if (Payload.Detail == null) return false;

      // Make sure the response is the type we're expecting
      var responseType = (ENResponseType)Payload.Type.GetValue();
      // if (responseType != ENResponseType.rtItemQueryRs)
      if (responseType != type) return false;
      return true;
    }
  }
}
