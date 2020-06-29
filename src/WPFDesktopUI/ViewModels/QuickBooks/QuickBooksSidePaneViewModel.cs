using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Interfaces;
using WPFDesktopUI.Models.SidePaneModels.Presents;
using stn = WPFDesktopUI.Controllers.SettingsController;
using ErrHandler = WPFDesktopUI.Controllers.QbImportExceptionHandler;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : Screen, IQuickBooksSidePaneViewModel {
    public QuickBooksSidePaneViewModel() {
      QbspModel = Factory.CreateQuickBooksSidePaneModel();

      /*** HEADER ***/
      // Add a corresponding property to IClientInvoiceHeaderModel (and it's children)
      // Ensure a corresponding method exists in QBConnect.Classes.HeaderItem
      // Check Preset and IPreset methods as well

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "CustomerRefFullName", "CUSTOMER:JOB");
      QbspModel.Attr["CustomerRefFullName"].IsMandatory = true;

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "ClassRefFullName", "CLASS");

      QbspModel.AttrAdd(Factory.CreateQbNullAttribute(), "TemplateRefFullName", "TEMPLATE");
      QbspModel.Attr["TemplateRefFullName"].IsMandatory = true;
      QbspModel.Attr["TemplateRefFullName"].ComboBox.RequiresCsv = false;
      QbspModel.Attr["TemplateRefFullName"].ToolTip = "Please select 'Query QuickBooks' before"+
                                                      " custom lists can be generated";

      QbspModel.AttrAdd(Factory.CreateQbDateTimeAttribute(), "TxnDate", "DATE");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "BillAddress", "BILL ADDRESS");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "ShipAddress", "SHIP ADDRESS");

      QbspModel.AttrAdd(Factory.CreateQbDropDownAttribute(), "TermsRefFullName", "TERMS");
      QbspModel.Attr["TermsRefFullName"].ToolTip = "Please select 'Query QuickBooks' before" +
                                                      " custom lists can be generated";

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "PONumber", "P.O. NUMBER");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(),
        "Other", stn.QbInvHasHeaderOther() ? stn.QbInvHeaderOtherName() : "OTHER");

      /*** LINES ***/
      // Add a corresponding property to IClientInvoiceLineItemModel (and it's children)
      // Ensure a corresponding method exists in QBConnect.Classes.LineItem
      // Check Preset and IPreset methods as well

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "ItemRef", "ITEM");
      QbspModel.Attr["ItemRef"].IsMandatory = true;

      QbspModel.AttrAdd(Factory.CreateQbDoubleAttribute(), "ORRatePriceLevelRate", "RATE");

      QbspModel.AttrAdd(Factory.CreateQbDoubleAttribute(), "Quantity", "QUANTITY");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "Desc", "DESCRIPTION");

      QbspModel.AttrAdd(Factory.CreateQbDateTimeAttribute(), "ServiceDate", "SERVICE DATE");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(),
        "Other1", stn.QbInvHasHeaderOther1() ? stn.QbInvHeaderOtherName1() : "OTHER1");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(),
        "Other2", stn.QbInvHasHeaderOther2() ? stn.QbInvHeaderOtherName2() : "OTHER2");
    }

    public IQuickBooksSidePaneModel QbspModel { get; set; }
    public bool CanQbInteract { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;
    public string ConsoleMessage { get; set; } = "Please select 'Query QuickBooks' before custom lists can be generated";

    private bool _importedCsvHeaders { get; set; } = false;

    public async void OnSelected() {
      await Task.Run(() => {
        QbspModel.Attr["Other"].Name = stn.QbInvHasHeaderOther() ? stn.QbInvHeaderOtherName() : "OTHER";
        QbspModel.Attr["Other1"].Name = stn.QbInvHasHeaderOther1() ? stn.QbInvHeaderOtherName1() : "OTHER1";
        QbspModel.Attr["Other2"].Name = stn.QbInvHasHeaderOther2() ? stn.QbInvHeaderOtherName2() : "OTHER2";

        var csvHeaders = GetCsvHeaders();
        if (csvHeaders == null) return;

        // Loop through all QbAttribute ComboBoxes in QbspModel
        foreach (var attribute in QbspModel.Attr) {
          if (!QbspModel.Attr[attribute.Key].ComboBox.RequiresCsv) continue;
          QbspModel.Attr[attribute.Key].ComboBox.ItemsSource = csvHeaders;
          QbspModel.Attr[attribute.Key].ComboBox.IsEnabled = true;
        }

        if (_importedCsvHeaders == false) {
          AutopopulateComboBoxes();
          _importedCsvHeaders = true;
        }
      });
    }

    /// <summary>
    /// Try to auto-populate the selected items for the comboboxes based on what data
    /// was selected on last import (when the sqlite table was last updated)
    /// </summary>
    /// <param name="csvHeaders">A list of all options populating the combo boxes</param>
    public void AutopopulateComboBoxes(List<string> csvHeaders = null) {

      if (csvHeaders == null) {
        csvHeaders = GetCsvHeaders();
        if (csvHeaders == null) return;
      }

      var preset = new Preset();
      var presetModelsList = preset.Read<PresetModel>("Default");
      if (presetModelsList.Count < 1) {
        _importedCsvHeaders = true;
        return;
      }

      foreach (var attribute in QbspModel.Attr) {
        // Set default value to last imported setting
        var presetModel = presetModelsList[0];

        if (presetModel == null) continue;
        var presetStr = presetModel.GetType().GetProperty(attribute.Key)?.GetValue(presetModel)?.ToString();
        if (csvHeaders.Contains(presetStr) == false) continue;
        QbspModel.Attr[attribute.Key].ComboBox.SelectedItem = presetStr;
      }
    }

    public async Task QbInteract() {
      SessionStart();
      try {
        // Update template list from QB
        var templateList = await InitTemplateRefFullName();
        QbspModel.Attr["TemplateRefFullName"].ComboBox.ItemsSource = templateList;
        QbspModel.Attr["TemplateRefFullName"].ComboBox.IsEnabled = true;
        // Preset the selected item if the QB preferences setting matches one of the possible item sources
        if (templateList.Contains(stn.QbInvTemplateName())) {
          QbspModel.Attr["TemplateRefFullName"].ComboBox.SelectedItem = stn.QbInvHasTemplate() ? stn.QbInvTemplateName() : "";
        }

        // Update terms list from QB
        var termsList = await InitTermsRefFullName();
        IQBDropDownAttribute dropAttr = (QbspModel.Attr["TermsRefFullName"] as IQBDropDownAttribute);
        if (dropAttr != null) {
          dropAttr.DropDownComboBox.ItemsSource = termsList;
          dropAttr.DropDownComboBox.IsEnabled = true;

        }


        SessionEnd();
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
      } finally {
        CanQbInteract = true;
        QbProgressBarIsVisible = false;
      }
    }

    private void SessionStart() {
      CanQbInteract = false;
      QbProgressBarIsVisible = true;
    }

    private void SessionEnd() {
      ConsoleMessage = "Query successfully completed";
    }

    /// <summary>
    /// Returns a list of templates used in QuickBooks based on their name
    /// </summary>
    /// <param name="qbFilePath">The full path for the QuickBooks file</param>
    /// <returns></returns>
    private static async Task<List<string>> InitTemplateRefFullName() {
      IQbExportController qbExportController = Factory.CreateQbExportController();
      var templates = await Task.Run(() => {
        return qbExportController.GetTemplateNamesList();
      });

      // Add blank to start
      templates.Insert(0, "");
      return templates;
    }

    /// <summary>
    /// Returns a list of terms (i.e. net 30) used in QuickBooks based on their name
    /// </summary>
    /// <param name="qbFilePath">The full path for the QuickBooks file</param>
    /// <returns></returns>
    private static async Task<List<string>> InitTermsRefFullName() {
      IQbExportController qbExportController = Factory.CreateQbExportController();
      var terms = await Task.Run(() => {
        return qbExportController.GetTermsNamesList();
      });

      // Add blank to start
      terms.Insert(0, "");
      return terms;
    }

    private static List<string> GetCsvHeaders() {
      var dt = ImportViewModel.CsvData;
      if (dt == null) return null;

      string[] columnHeaders = dt?.Columns.Cast<DataColumn>()
        .Select(x => x.ColumnName)
        .ToArray();

      // Convert string[] to List<string>
      List<string> finalList = columnHeaders?.ToList();

      if (finalList == null) return new List<string>();

      // Add blank to start
      finalList.Insert(0, "");
      return finalList;
    }
  }
}
