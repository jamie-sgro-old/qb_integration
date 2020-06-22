using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect.Classes.Interfaces {
  internal interface IInvoiceLineAppender {
    IORInvoiceLineAdd Line { get; }
    void AddLine(IInvoiceLineItemModel lineItem);
  }
}
