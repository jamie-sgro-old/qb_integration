using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes.Interfaces {
  internal interface IQuery {
    IMsgSetRequest MsgSetRequest { get; set; }
    QBSessionManager QbSessionManager { get; set; }

    List<string> GetList<T>();
  }
}
