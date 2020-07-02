using System.Data;

namespace InterfaceLibraries {
  public interface IPreprocessor : IPlugin {
    DataTable Preprocess(DataTable dt);
  }
}
