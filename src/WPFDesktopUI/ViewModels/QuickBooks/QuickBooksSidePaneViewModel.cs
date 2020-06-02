using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MCBusinessLogic.Controllers;
using WPFDesktopUI.ViewModels.Interfaces;
using stn = WPFDesktopUI.Controllers.SettingsController;

namespace WPFDesktopUI.ViewModels.QuickBooks {
  public class QuickBooksSidePaneViewModel : AbstractQuickBooksSidePane, IMainTab {

    public async void OnSelected() {
      await Task.Run(() => {
        var csvData = ImportViewModel.CsvData;
        if (csvData == null) return;
        ItemRef = GetCsvHeaders(csvData);
        CanItemRef = true;
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
      templates.Insert(0, "");
      return templates;
    }

    private static List<string> GetCsvHeaders(DataTable dt) {
      string[] columnHeaders = dt?.Columns.Cast<DataColumn>()
        .Select(x => x.ColumnName)
        .ToArray();

      return columnHeaders?.ToList();
    }
  }
}
