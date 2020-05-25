using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes.Interfaces {
  internal interface IQuery {
    IMsgSetRequest MsgSetRequest { get; set; }
    QBSessionManager QbSessionManager { get; set; }
    IResponseList ResponseList { get; set; }
    dynamic Type { get;  }

    void SpecifyQuery();
    List<string> GetList();
    List<string> CompileList();
  }
}
