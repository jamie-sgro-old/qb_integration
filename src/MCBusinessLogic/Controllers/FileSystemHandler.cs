using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  internal class FileSystemHandler : IFileSystemHandler {
    public string FileName { get; set; }
    public string Filter { get; set; }

    public bool SelectFile() {
      OpenFileDialog fso = new OpenFileDialog();
      fso.ValidateNames = false;
      fso.Filter = Filter;
      fso.ShowDialog();
      FileName = fso.FileName;
      return FileName != "";
    }
  }
}
