using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  internal interface IFileSystemHandler {
    string FileName { get; }
    string Filter { get; set; }
    bool SelectFile();
  }
}
