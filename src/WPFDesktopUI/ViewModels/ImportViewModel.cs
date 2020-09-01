using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.Micro;
using MCBusinessLogic.Controllers;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using InterfaceLibraries;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.ImportModels;
using WPFDesktopUI.Models.PluginModels;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen, IImportViewModel {
    public string CsvFilePath { get; set; }
    public DataView CsvDataView { get; set; }
    public static DataTable CsvData { get; set; }
    public string TabHeader { get; set; } = "Import";
    public string ConsoleMessage { get; set; }



    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
    }

    public async Task BtnOpenCsvFile() {
      log.Info("Importing csv file via btn");
      CsvFilePath = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      var importModel = new ImportModel();

      await Task.Run(() => {

        try {
          CsvData = importModel.GetCsvData(CsvFilePath, sep);
        } catch (PluginException e) {
          log.Error("Plugin could not be consumed by csv importer", e);
          ConsoleMessage = e.Message;
        }

        // Match data structure to the UI view (this lets the user see the data)
        CsvDataView = CsvData.DefaultView;
      });
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
