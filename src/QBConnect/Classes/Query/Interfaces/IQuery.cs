using System.Collections.Generic;
using QBFC13Lib;

namespace QBConnect.Classes.Interfaces {
  internal interface IQuery {
    QBSessionManager QbSessionManager { get; set; }

    List<string> GetList<T>();
  }
}
