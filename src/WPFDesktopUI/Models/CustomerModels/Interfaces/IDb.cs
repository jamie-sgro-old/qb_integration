using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.CustomerModels.Interfaces {
  public interface IDb<T> {

    List<T> Read<T>();

    void Update<T>(List<T> dataList);

  }
}
