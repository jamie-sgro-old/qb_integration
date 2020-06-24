using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;
using ErrHandler = WPFDesktopUI.Controllers.QbImportExceptionHandler;

namespace WPFDesktopUI.ViewModels {
  public class CustomerViewModel : Screen, ICustomerViewModel<ICustomer> {
    public CustomerViewModel() {
      Cxs = Read<Customer>();
    }


    public static List<Customer> Cxs { get; set; }
    public string ConsoleMessage { get; set; } = "test";
    public bool CanQbInteract { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; }
    public string TabHeader { get; set; } = "Customer";
    public bool CanBtnUpdate { get; set; } = false;


    public void OnCellEditEnding() {
      TabHeader = TabHeader + "*";
      CanBtnUpdate = true;
    }

    public void BtnUpdate() {
      Update(Cxs);
      TabHeader = TabHeader.Replace("*", "");
      CanBtnUpdate = false;
    }

    public async Task QbInteract() {
      SessionStart();
      try {
        // Update items list from QB
        var itemsList = await InitItemsFullName();
        SessionEnd();
      }
      catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
      }
      finally {
        CanQbInteract = true;
        QbProgressBarIsVisible = false;
      }
    }

    private static async Task<List<string>> InitItemsFullName() {
      IQbExportController qbExportController = Factory.CreateQbExportController();
      var items = await Task.Run(() => {
        return qbExportController.GetItemNamesList();
      });

      return items;
    }

    private void SessionStart() {
      CanQbInteract = false;
      QbProgressBarIsVisible = true;
    }

    private void SessionEnd() {
      ConsoleMessage = "Query successfully completed";
    }

    public void OnSelected() {
    }

    public List<T> Read<T>() {
      var query = "SELECT id, * FROM customer";
      var cxList = SqliteDataAccess.LoadData<T>(query);
      return cxList;
    }

    public void Update<T>(List<T> dataList) {
      SqliteDataAccess.SaveData(
        @"UPDATE `customer`
        SET
          PoNumber = @PoNumber,
          TermsRefFullName = @TermsRefFullName
        WHERE Name = @Name;", dataList);
    }
  }
}
