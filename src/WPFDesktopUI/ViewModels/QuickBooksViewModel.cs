using Caliburn.Micro;
using ErrHandler = WPFDesktopUI.Controllers.QbImportExceptionHandler;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using MCBusinessLogic.Models;
using WPFDesktopUI.Controllers;
using WPFDesktopUI.ViewModels.QuickBooks;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels;
using WPFDesktopUI.Models.SidePaneModels.Presents;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Conductor<object>, IQuickBooksViewModel {
		public QuickBooksViewModel() {
      log.Info("Creating QuickBooks side pane view model");
			QuickBooksSidePaneViewModel = Factory.CreateQuickBooksSidePaneViewModel();
    }



    public IQuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }
    public string ConsoleMessage { get; set; }
    public bool CanQbInteract { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;
    public string TabHeader { get; set; } = "QuickBooks";


    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
      QuickBooksSidePaneViewModel.OnSelected();
    }

    public async Task QbInteract() {
      log.Info("QuickBooks interact button pressed. Data import starting");
      try {
				SessionStart();


        var attr = QuickBooksSidePaneViewModel.QbspModel.Attr;

        log.Debug("Saving presets for QBDP combobox selected items");
        var preset = new Preset();
        preset.Update(attr, "Default");
        IQuickBooksModel qbModel = Factory.CreateQuickBooksModel(attr);

        log.Debug("Get cxs from CustomerViewModel");
        var cxs = CustomerViewModel.StaticCxs;

        await Task.Run(() => {
          log.Info("Accessing QB Importer through MCBusinessLogic");
          return qbModel.QbImport(ImportViewModel.CsvData, cxs);
        });

        log.Debug("Import has successfully completed");
        ConsoleMessage = "Import has successfully completed";
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
        log.Error(ConsoleMessage, e);
			} finally {
        SessionEnd();
			}
		}

    private void SessionStart() {
      CanQbInteract = false;
      QbProgressBarIsVisible = true;
      ConsoleMessage = "Importing, please stand by...";
    }

    private void SessionEnd() {
      CanQbInteract = true;
      QbProgressBarIsVisible = false;
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}