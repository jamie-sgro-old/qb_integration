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

namespace WPFDesktopUI.ViewModels {
  public class ImportViewModel : Screen, IImportViewModel {
    [ImportMany(typeof(IPreprocessor), AllowRecomposition = true)]
    IEnumerable<Lazy<IPreprocessor, IPreprocessorMetaData>> _preprocessors;

    public string CsvFilePath { get; set; }
    public DataView CsvDataView { get; set; }
    public static DataTable CsvData { get; set; }
    public string TabHeader { get; set; } = "Import";



    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
    }

    public async Task BtnOpenCsvFile() {
      var fileName = FileSystemHelper.GetFilePath("CSV (Comma delimited) |*.csv");
      CsvFilePath = fileName;
      var sep = Properties.Settings.Default["StnCsvSeparation"].ToString();

      await Task.Run(() => {
        CsvData = CsvParser.ParseFromFile(fileName, sep);

        // Sanitize column headers
        foreach (DataColumn col in CsvData.Columns) {
          col.ColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
        }

        Compose();


        foreach (Lazy<IPreprocessor, IPreprocessorMetaData> processor in _preprocessors) {
          Console.WriteLine(processor.Metadata.Name);
          if (processor.Metadata.Name == "StaffNamePreprocessor") {
            Console.WriteLine(processor.Value.Preprocess(""));
          }
        }


        //Lazy<IPreprocessor, IPreprocessorMetaData> selectedProcessor = _preprocessors.Where(s => (string)s.Metadata.Name == "AlphaProcessor").FirstOrDefault();
        //selectedProcessor.Value.Preprocess("null");
        /*foreach (var preprocessor in _preprocessors) {
          Console.WriteLine(preprocessor.Preprocess("null"));
        }*/

        // Match data structure to the UI view (this lets the user see the data)
        CsvDataView = CsvData.DefaultView;
      });
    }

    private void Compose() {
      DirectoryCatalog catalog = new DirectoryCatalog("Plugins", "*.dll");
      //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
      CompositionContainer container = new CompositionContainer(catalog);
      //container.SatisfyImportsOnce(this);
      container.ComposeParts(this);
    }
  }
}
