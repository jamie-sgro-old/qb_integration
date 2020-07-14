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
using WPFDesktopUI.Controllers;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;
using ErrHandler = WPFDesktopUI.Controllers.QbImportExceptionHandler;

namespace WPFDesktopUI.ViewModels {
  public class CustomerViewModel : Screen, ICustomerViewModel<ICustomer> {
    private ObservableCollection<ICustomer> _reactiveCollection;

    public CustomerViewModel() {
      log.Debug("Getting Customer data from sql");
      ReactiveCollection = Read();
    }

    public ObservableCollection<ICustomer> ReactiveCollection {
      get => _reactiveCollection;
      set {
        StaticCxs = value.ToList();
        _reactiveCollection = value;
      }
    }

    /// <summary>
    /// A static version of ReactiveCollection to be called from other classes (tabs)
    /// </summary>
    public static List<ICustomer> StaticCxs { get; private set; }
    public string ConsoleMessage { get; set; }
    public bool CanQbInteract { get; set; } = true;
    public bool QbProgressBarIsVisible { get; set; }
    public string TabHeader { get; set; } = "Customer";
    public bool CanBtnUpdate { get; set; } = false;

    
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnCellEditEnding() {
      TabHeader = TabHeader.Replace("*", "");
      TabHeader += "*";
      CanBtnUpdate = true;
    }

    public void BtnUpdate() {
      Update(ReactiveCollection);
      TabHeader = TabHeader.Replace("*", "");
      CanBtnUpdate = false;
    }

    private class NameList {
      public string Name { get; set; }
    }

    public async Task QbInteract() {
      log.Info("QuickBooks interact button pressed. Data query starting");
      SessionStart();
      try {
        // Update items list from QB
        var customerList = await InitCustomersFullName();

        // Convert itemsList into a List where each item contains a property Name
        var nameList = customerList.Select(name => new NameList {Name = name}).ToList();

        Create(nameList);
        ReactiveCollection = Read();
        SessionEnd();
      }
      catch (Exception e) {
        ConsoleMessage = ErrHandler.DelegateHandle(e);
        log.Error(ConsoleMessage, e);
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

    /// <summary>
    /// Dapper requires concrete implementations for sql queries
    /// Essentially a private version of Customer
    /// </summary>
    private class TempCustomer : ICustomer {
      public string Name { get; }
      public string PoNumber { get; set; }
      public string TermsRefFullName { get; set; }
      public string AppendLineItem1 { get; set; }
      public string AppendLineItem2 { get; set; }
      public string AppendLineItem3 { get; set; }
    }

    public ObservableCollection<ICustomer> Read() {
      const string query = "SELECT Id, * FROM customer";
      var list = SqliteDataAccess.LoadData<TempCustomer>(query);

      // Cast to observable collection
      var collection = new ObservableCollection<ICustomer>(list);
      return collection;
    }

    public void Update<T>(ObservableCollection<T> dataList) {
      SqliteDataAccess.SaveData(
        @"UPDATE `customer`
        SET
          PoNumber = @PoNumber,
          TermsRefFullName = @TermsRefFullName,
          AppendLineItem1 = @AppendLineItem1,
          AppendLineItem2 = @AppendLineItem2,
          AppendLineItem3 = @AppendLineItem3
        WHERE Name = @Name;", dataList);
    }

    public void Destroy<T>(ObservableCollection<T> dataList) {
      throw new NotImplementedException();
    }

    private static readonly log4net.ILog log = LogHelper.GetLogger();
  }
}
