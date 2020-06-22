using System;
using System.Data;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;

namespace WPFDesktopUI.Models {
  public interface IQuickBooksModel {
    Task QbImport(DataTable dt);
  }
}