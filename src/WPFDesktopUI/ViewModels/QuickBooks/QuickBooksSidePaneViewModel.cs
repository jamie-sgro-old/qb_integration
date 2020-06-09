﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers;
using WPFDesktopUI.Models;
using WPFDesktopUI.ViewModels.Interfaces;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : AbstractQuickBooksSidePane, IMainTab {
    public QuickBooksSidePaneViewModel() {
      QbspModel = Factory.CreateQuickBooksSidePaneModel();

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "CustomerRefFullName", "CUSTOMER:JOB");
      QbspModel.Attr["CustomerRefFullName"].IsMandatory = true;

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "TemplateRefFullName", "TEMPLATE");
      QbspModel.Attr["TemplateRefFullName"].IsMandatory = true;

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "ItemRef", "ITEM");
      QbspModel.Attr["ItemRef"].IsMandatory = true;

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "Rate", "RATE");

      QbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "Quantity", "QUANTITY");

      QbspModel.AttrAdd(Factory.CreateQbDateTimeAttribute(), "Date", "DATE");
    }

    public IQuickBooksSidePaneModel QbspModel { get; set; }



    public async void OnSelected() {
      await Task.Run(() => {
        HeaderOtherTextBlock = stn.QbInvHeaderOtherName();

        var csvData = ImportViewModel.CsvData;
        if (csvData == null) return;
        var csvHeaders = GetCsvHeaders(csvData);

        // Loop through all QbAttribute ComboBoxes in QbspModel
        foreach (var attribute in QbspModel.Attr) {
          QbspModel.Attr[attribute.Key].ComboBox.ItemsSource = csvHeaders;
          QbspModel.Attr[attribute.Key].ComboBox.IsEnabled = true;
        }
      });
    }

    /// <summary>
    /// Executed when 'Query QuickBooks' button is pressed
    /// Connect to QB and get all data needed to populate smart dropdowns
    /// i.e. to decide which template to use, the user should decide from a list
    /// of actual templates used in QB, thus the combobox needs a list of
    /// template strings
    /// </summary>
    public async void QbExport() {
      SessionStart();
      var qbFilePath = stn.QbFilePath();
      try {
        TemplateRefFullName = await InitTemplateRefFullName(qbFilePath);
        SessionEnd();
      } catch (Exception e) {
        ConsoleMessage = QbImportExceptionHandler.DelegateHandle(e);
      } finally {
        CanQbExport = true;
        QbProgressBarIsVisible = false;
      }
    }

    private void SessionStart() {
      CanQbExport = false;
      QbProgressBarIsVisible = true;
    }

    private void SessionEnd() {
      CanTemplateRefFullName = true;
      ConsoleMessage = "Query successfully completed";
    }

    /// <summary>
    /// Returns a list of templates used in QuickBooks based on their name
    /// </summary>
    /// <param name="qbFilePath">The full path for the QuickBooks file</param>
    /// <returns></returns>
    private static async Task<List<string>> InitTemplateRefFullName(string qbFilePath) {
      var qbExportController = new QbExportController(qbFilePath);
      var templates = await Task.Run(() => {
        return qbExportController.GetTemplateNamesList();
      });

      // Add blank to start
      templates.Insert(0, "");
      return templates;
    }

    private static List<string> GetCsvHeaders(DataTable dt) {
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
