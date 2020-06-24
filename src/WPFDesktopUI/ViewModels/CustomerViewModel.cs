using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;
using ErrHandler = WPFDesktopUI.Controllers.QbImportExceptionHandler;

namespace WPFDesktopUI.ViewModels {
  public class CustomerViewModel : Screen, ICustomerViewModel<ICustomer> {
    private ObservableCollection<Customer> _cxs;

    public CustomerViewModel() {
      Cxs = Read<Customer>();
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public DataGrid CustomerDataGrid { get; set; }

    public ObservableCollection<Customer> Cxs {
      get => _cxs;
      set {
        StaticCxs = value.ToList();
        _cxs = value;
      }
    }

    public static List<Customer> StaticCxs { get; set; }
    public string ConsoleMessage { get; set; }
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

    private class NameList {
      public string Name { get; set; }
    }

    public async Task QbInteract() {
      SessionStart();
      try {
        // Update items list from QB
        var customerList = await InitCustomersFullName();

        // Convert itemsList into a List where each item contains a property Name
        var nameList = customerList.Select(name => new NameList {Name = name}).ToList();

        Create(nameList);
        Cxs = Read<Customer>();
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

    private static async Task<List<string>> InitCustomersFullName() {
      IQbExportController qbExportController = Factory.CreateQbExportController();
      var cxs = await Task.Run(() => {
        return qbExportController.GetCustomerNamesList();
      });

      return cxs;
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

    public void Create<T>(List<T> dataList) {
      SqliteDataAccess.SaveData(
        @"INSERT OR IGNORE INTO `customer` (Name)
          VALUES (@Name);", dataList);
    }

    public ObservableCollection<T> Read<T>() {
      var query = "SELECT id, * FROM customer";
      var cxList = SqliteDataAccess.LoadData<T>(query);

      // Cast to observable collection
      var collection = new ObservableCollection<T>(cxList);
      return collection;
    }

    public void Update<T>(ObservableCollection<T> dataList) {
      SqliteDataAccess.SaveData(
        @"UPDATE `customer`
        SET
          PoNumber = @PoNumber,
          TermsRefFullName = @TermsRefFullName
        WHERE Name = @Name;", dataList);
    }
  }
}
