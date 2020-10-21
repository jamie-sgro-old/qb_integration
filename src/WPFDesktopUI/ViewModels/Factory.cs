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
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.Models.ItemReplacerModels;
using WPFDesktopUI.Models.ItemReplacerModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.Models.PluginModels.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Presents;
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

    public static ICustomerViewModel<ICustomer> CreateCustomerViewModel() {
      return new CustomerViewModel();
    }

    public static IItemViewModel<IItemReplacer> CreateItemViewModel() {
      return new ItemViewModel();
    }

    public static IItemViewModel<IItemReplacer> CreateAddressViewModel() {
      return new AddressViewModel();
    }

    #endregion View Models

    #region Screen Models

    public static ICustomer CreateCustomer() {
      return new Customer();
    }

    public static ICustomer CreateCustomer(string name) {
      return new Customer(name);
    }

    public static IQuickBooksModel CreateQuickBooksModel(Dictionary<string, IQbAttribute> attr) {
      return new QuickBooksModel(attr, CreateQbImportController());
    }

    public static IQuickBooksSidePaneModel CreateQuickBooksSidePaneModel() {
      return new QuickBooksSidePaneModel(CreateQbComboBox);
    }

    public static IPluginModel<IClientPlugin, IClientEssentials> CreatePluginModel() {
      var rtn = new PluginModel();
      rtn.Init();
      return rtn;
    }

    public static IClientPlugin CreateClientPlugin(bool isEnabled, string name, string author, string description) {
      return new ClientPlugin(isEnabled, name, author, description);
    }

    public static ISearchReplaceModel<IItemReplacer> CreateItemModel() {
      return new ItemModel();
    }

    public static ISearchReplaceModel<IItemReplacer> CreateAddressModel() {
      return new AddressModel();
    }

    public static IItemReplacer CreateItemReplacer(string replaceWith, string toReplace) {
      return new ItemReplacer(replaceWith, toReplace);
    }

    #endregion Screen Models


    #region Invoice Models

    public static IClientInvoiceHeaderModel CreateClientInvoiceHeaderModel() {
      return new ClientInvoiceHeaderModel();
    }

    public static ICsvModel CreateCsvModel() {
      return new CsvModel();
    }

    public static IPresetModel CreatePresetModel() {
      return new PresetModel();
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
      return new QbImportController(SettingsController.QbFilePath(), McFactory.CreateInvoiceImporter);
    }

    public static IQbExportController CreateQbExportController() {
      return new QbExportController(SettingsController.QbFilePath());
    }

    #endregion Controllers
  }
}
