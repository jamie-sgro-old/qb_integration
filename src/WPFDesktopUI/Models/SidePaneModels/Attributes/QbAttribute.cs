using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbAttribute<T> : INotifyPropertyChanged {
    public QbAttribute(bool isMandatory, bool hasHeaderDropDown, T payload) {
      Payload = payload;
      IsMandatory = isMandatory;
      HasHeaderDropDown = hasHeaderDropDown;
    }

    public QbAttribute(bool isMandatory, bool hasHeaderDropDown) {
      IsMandatory = isMandatory;
      HasHeaderDropDown = hasHeaderDropDown;
    }

    public T Payload { get; set; }

    public bool IsMandatory { get; }
    public bool HasHeaderDropDown { get; }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
