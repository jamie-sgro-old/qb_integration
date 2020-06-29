using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models.Interfaces {
  public interface IInvoice {
    IClientInvoiceHeaderModel Header { get; set; }
    List<ICsvModel> Lines { get; set; }
  }
}
