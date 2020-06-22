using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;
using WPFDesktopUI.ViewModels.QuickBooks;

namespace WPFDesktopUI.ViewModels {
  public static class Factory {

    #region View Models

    public static IImportViewModel CreateImportViewModel() {
      return new ImportViewModel();
    }

    public static IQuickBooksViewModel CreateQuickBooksViewModel() {
      return new QuickBooksViewModel();
    }

    public static IQuickBooksSidePaneViewModel CreateQuickBooksSidePaneViewModel() {
      return new QuickBooksSidePaneViewModel();
    }

    public static ICustomerViewModel CreateCustomerViewModel() {
      throw new NotImplementedException();
    }

    #endregion View Models

    #region Screen Models

    public static IQuickBooksModel CreateQuickBooksModel(Dictionary<string, IQbAttribute> attr) {
      return new QuickBooksModel(attr, CreateQbImportController());
    }

    public static IQuickBooksSidePaneModel CreateQuickBooksSidePaneModel() {
      return new QuickBooksSidePaneModel(CreateQbComboBox);
    }

    public static ICustomerModel CreateCustomerModel() {
      throw new NotImplementedException();
    }

    #endregion Screen Models


    #region Invoice Models

    public static IClientInvoiceHeaderModel CreateClientInvoiceHeaderModel() {
      return new ClientInvoiceHeaderModel();
    }

    public static ICsvModel CreateCsvModel() {
      return new CsvModel();
    }

    #endregion Invoice Models


    #region QbAttribute

    public static IQbStringAttribute CreateQbStringAttribute() {
      return new QbStringAttribute();
    }

    public static IQbDateTimeAttribute CreateQbDateTimeAttribute() {
      return new QbDateTimeAttribute();
    }

    public static IQbStringAttribute CreateQbDoubleAttribute() {
      return new QbDoubleAttribute();
    }

    public static IQBDropDownAttribute CreateQbDropDownAttribute() {
      return new QbDropDownAttribute(CreateQbComboBox);
    }

    public static IQbAttribute CreateQbNullAttribute() {
      return new QbNullAttribute();
    }

    public static IQbComboBox CreateQbComboBox() {
      return new QbComboBox();
    }

    #endregion QbAttribute

    #region Controllers

    public static IQbImportController CreateQbImportController() {
      return new QbImportController(SettingsController.QbFilePath(), CreateClientInvoiceHeaderModel, McFactory.CreateInvoiceImporter);
    }

    public static IQbExportController CreateQbExportController() {
      return new QbExportController(SettingsController.QbFilePath());
    }

    #endregion Controllers
  }
}
