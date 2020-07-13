using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.DbModels.Interfaces {
  public interface IDbModel<T> {

    void Create<T>(List<T> dataList);

    ObservableCollection<T> Read<T>();

    void Update<T>(ObservableCollection<T> dataList);

    void Destroy<T>(ObservableCollection<T> dataList);
  }
}
