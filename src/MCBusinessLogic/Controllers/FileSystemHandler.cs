using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  interface IFileSystemHandler {
    string FileName { get; }
    string Filter { get; set; }
    bool SelectFile();
  }

  internal class FileSystemHandler : IFileSystemHandler {

    private string _fileName;
    private string _filter;

    public string FileName {
      get { return _fileName; }
      set { _fileName = value; }
    }

    public string Filter {
      get { return _filter; }
      set { _filter = value; }
    }

    public bool SelectFile() {
      OpenFileDialog fso = new OpenFileDialog();
      fso.Filter = _filter;
      fso.ShowDialog();
      _fileName = fso.FileName;
      if (_fileName == "") {
        return false;
      }
      return true; ;
    }
  }
}
