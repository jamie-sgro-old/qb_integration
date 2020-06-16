using Caliburn.Micro;
using ErrHandler = MCBusinessLogic.Controllers.QbImportExceptionHandler;
using stn = WPFDesktopUI.Controllers.SettingsController;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WPFDesktopUI.ViewModels.QuickBooks;
using WPFDesktopUI.Models;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class QuickBooksViewModel : Conductor<object>, IQuickBooksViewModel {
		public QuickBooksViewModel() {
			QuickBooksSidePaneViewModel = Factory.CreateQuickBooksSidePaneViewModel();
    }



    public IQuickBooksSidePaneViewModel QuickBooksSidePaneViewModel { get; }
    public string ConsoleMessage { get; set; }
    public bool CanBtnQbImport { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; } = false;



    public event PropertyChangedEventHandler PropertyChanged;

    public void OnSelected() {
      QuickBooksSidePaneViewModel.OnSelected();
    }

    public async Task BtnQbImport() {
      try {
				SessionStart();

        var attr = QuickBooksSidePaneViewModel.QbspModel.Attr;
        IQuickBooksModel qbModel = Factory.CreateQuickBooksModel(attr);

        await Task.Run(() => {
          return qbModel.QbImport(ImportViewModel.CsvData);
        });

        ConsoleMessage = "Import has successfully completed";
      } catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
			} finally {
        SessionEnd();
			}
		}

    private void SessionStart() {
      CanBtnQbImport = false;
      QbProgressBarIsVisible = true;
      ConsoleMessage = "Importing, please stand by...";
    }

    private void SessionEnd() {
      CanBtnQbImport = true;
      QbProgressBarIsVisible = false;
    }
	}
}