using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers.Interfaces {
  public interface IQbExportController {
    string QbFilePath { get; set; }

    List<string> GetTemplateNamesList();

    List<string> GetTermsNamesList();
  }
}
