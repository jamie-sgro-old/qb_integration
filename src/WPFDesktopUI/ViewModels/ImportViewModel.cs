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
using WPFDesktopUI.Models.PluginModels;

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen, IImportViewModel {
    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    IEnumerable<Lazy<IPreprocessor, IPluginMetaData>> _preprocessors;

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
      var fileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = fileName;
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      await Task.Run(() => {
        log.Info("Parsing csv data");
        CsvData = CsvParser.ParseFromFile(fileName, sep);

        // Sanitize column headers
        log.Debug("Sanitizing column header");
        foreach (DataColumn col in CsvData.Columns) {
          col.ColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
        }


        try {
          // Try temporary data first
          log.Info("Loading plugins for csv importer");
          var tempData = PluginPreprocess(CsvData);
          // Then overwrite final data property if everything went error free
          CsvData = tempData;
        } catch (PluginException e) {
          log.Error("Plugin could not be consumed by csv importer", e);
          ConsoleMessage = e.Message;
        }


        // Match data structure to the UI view (this lets the user see the data)
        CsvDataView = CsvData.DefaultView;
      });
    }

    private DataTable PluginPreprocess(DataTable dt) {
      DataTable rtnData = null;

      // Process all plugins that are enabled by the user
      var plugins = Factory.CreatePluginModel().PluginModels;

      Compose();
      var relevantPlugins = PluginHandler<IPreprocessor>.GetRelevantPlugins(_preprocessors);

      foreach (Lazy<IPreprocessor, IPluginMetaData> relevantPlugin in relevantPlugins) {
        try {
          var newData = relevantPlugin.Value.Preprocess(dt);
          if (newData != null) {
            rtnData = newData;
          }
        } catch (Exception e) {
          log.Error("The following plugin resulted in an error: " +
                    relevantPlugin.Metadata.Name, e);
          throw new PluginException("The following plugin resulted in an error: " +
                                    relevantPlugin.Metadata.Name +
                                    ". The error report is as follows:\n" +
                                    e.Message);
        }
      }

      // Return newly processed data if anything changed
      // Otherwise just provide original data
      return rtnData ?? dt;
    }

    private void Compose() {
      log.Debug("Creating plugin container");
      CompositionContainer container = PluginHelper.GetContainer();
      container.ComposeParts(this);
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
