using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes.Interfaces {
  internal interface IQuery {
    IMsgSetRequest MsgSetRequest { get; set; }
    QBSessionManager QbSessionManager { get; set; }
    IResponseList ResponseList { get; set; }
    void SpecifyQuery();

    List<string> GetList();
  }
}
