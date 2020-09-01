using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibraries;
using MCBusinessLogic.Controllers;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.ImportModels.Interfaces;
using WPFDesktopUI.Models.PluginModels;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.ImportModels {
  public class ImportModel : PluginHandler<IPreprocessor>, IImportModel {

    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    IEnumerable<Lazy<IPreprocessor, IPluginMetaData>> _pluginList;

    public DataTable GetCsvData(string csvFilePath, string sep) {
      log.Info("Parsing csv data");
      var csvData = CsvParser.ParseFromFile(csvFilePath, sep);

      // Sanitize column headers
      log.Debug("Sanitizing column header");
      foreach (DataColumn col in csvData.Columns) {
        col.ColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
      }

      // Try temporary data first
      log.Info("Loading plugins for csv importer");
      var tempData = PluginPreprocess(csvData);
      // Then overwrite final data property if everything went error free
      csvData = tempData;


      return csvData;
    }

    private DataTable PluginPreprocess(DataTable dt) {
      DataTable rtnData = null;

      Compose();
      var relevantPlugins = GetRelevantPlugins(_pluginList);

      foreach (Lazy<IPreprocessor, IPluginMetaData> relevantPlugin in relevantPlugins) {
        try {
          var newData = relevantPlugin.Value.Preprocess(dt);
          if (newData != null) {
            rtnData = newData;
          }
        }
        catch (Exception e) {
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

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
