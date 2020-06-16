using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public static class FileSystemHelper {
    public static string GetFilePath(string filter) {
      var fso = McFactory.CreateFileSystemHandler();
      fso.Filter = filter;
      fso.SelectFile();
      return fso.FileName;
    }
  }
}
