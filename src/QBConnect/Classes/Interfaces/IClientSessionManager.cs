using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace QBConnect.Classes.Interfaces {
  interface IClientSessionManager : QBSessionManager  {
    bool ConnectionOpen { get; set; }
    bool SessionBegun { get; set; }
  }
}
