using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.CustomerModels;
using WPFDesktopUI.Models.CustomerModels.Interfaces;
using WPFDesktopUI.ViewModels.Interfaces;

namespace WPFDesktopUI.ViewModels {
  public class CustomerViewModel : Screen, ICustomerViewModel<ICustomer> {
    public CustomerViewModel() {
      Cxs = Read<Customer>();
    }


    public static List<Customer> Cxs { get; set; }
    public string ConsoleMessage { get; set; }
    public bool CanQbInteract { get; set; }
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

    public Task QbInteract() {
      throw new NotImplementedException();
    }

    public void OnSelected() {
    }

    public List<T> Read<T>() {
      var query = "SELECT id, * FROM customer";
      var cxList = SqliteDataAccess.LoadData<T>(query);
      return cxList;
    }

    public void Update<T>(List<T> dataList) {
      SqliteDataAccess.SaveData<T>(
        @"UPDATE `customer`
        SET
          PoNumber = @PoNumber,
          TermsRefFullName = @TermsRefFullName
        WHERE Name = @Name;", dataList);
    }
  }
}
