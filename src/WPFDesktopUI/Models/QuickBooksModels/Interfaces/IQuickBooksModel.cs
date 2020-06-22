using System;
using System.Data;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models.CustomerModels.Interfaces;

namespace WPFDesktopUI.Models {
  public interface IQuickBooksModel {
    Task QbImport(DataTable dt, ICustomerModel cx);
  }
}