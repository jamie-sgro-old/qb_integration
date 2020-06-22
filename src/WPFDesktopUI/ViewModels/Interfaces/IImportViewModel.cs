using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public interface IImportViewModel : IMainTab {
    string CsvFilePath { get; set; }
    DataView CsvDataView { get; set; }


    event PropertyChangedEventHandler PropertyChanged;
    Task BtnOpenCsvFile();
  }
}