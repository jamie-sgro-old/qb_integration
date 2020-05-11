using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public static class FileSystemHelper {
    public static string GetFile(string filter) {
      IFileSystemHandler fso = new FileSystemHandler {
        Filter = filter
      };
      fso.SelectFile();
      return fso.FileName;
    }
  }
}
