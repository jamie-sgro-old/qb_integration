using System.Collections.Generic;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Presents {
  public interface IPreset {
    void Update(Dictionary<string, IQbAttribute> attr, string preset);
    List<T> Read<T>(string preset);
  }
}