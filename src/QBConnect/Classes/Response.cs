using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal class Response {
    internal Response(IResponseList responseList) {
      // Get data from first response from DoRequest
      // (this only works if our desired req is the first one specified)
      Payload = responseList.GetAt(0);
    }

    public IResponse Payload { get; set; }

    internal bool IsValid() {
      // Check status code, 0=ok, >0 is warning
      if (Payload.StatusCode < 0) return false;

      // The request-specific response is in the details, make sure we have some
      if (Payload.Detail == null) return false;

      // Make sure the response is the type we're expecting
      var responseType = (ENResponseType)Payload.Type.GetValue();
      if (responseType != ENResponseType.rtTemplateQueryRs) return false;
      return true;
    }
  }
}
