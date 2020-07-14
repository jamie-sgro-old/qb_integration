using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.DbModels.Interfaces {
  public interface IDbViewModel {
    bool CanBtnUpdate { get; set; }
    void BtnUpdate();
  }
}
