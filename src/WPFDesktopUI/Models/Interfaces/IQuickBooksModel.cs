using System;
using System.Data;
using System.Threading.Tasks;
using MCBusinessLogic.Models;

namespace WPFDesktopUI.Models {
  public interface IQuickBooksModel {
    Task QbImport(string s, DataTable dt, Func<IClientInvoiceHeaderModel> c);
  }
}