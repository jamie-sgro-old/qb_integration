using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.ImportModels.Interfaces {
  interface IImportModel {
    DataTable GetCsvData(string csvFilePath, string sep);
  }
}
