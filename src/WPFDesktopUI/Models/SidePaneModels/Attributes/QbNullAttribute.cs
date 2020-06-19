using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public class QbNullAttribute : QbAbstractAttribute {
    public QbNullAttribute() {
      Payload = null;
    }

    /// <summary>
    /// NullAttributes currently don't use a normal dropdown to map column headers.
    /// Instead, pass the SelectedItem as a constant in lieu of a variable SelectedItem
    /// that maps to a column header.
    /// </summary>
    /// <param name="row">A row from a DataTable</param>
    /// <param name="key">The dictionary Key for a QbAttribute</param>
    /// <returns>String for the data in a cell/a constant value</returns>
    public override dynamic GetRow(DataRow row) {

      try {
        if (!string.IsNullOrEmpty(ComboBox.SelectedItem)) {
          return Convert.ToString(ComboBox.SelectedItem);
        }
      } catch (FormatException e) {
        throw new FormatException(e.Message +
          "\nThis error occured with the following text: '" +
          ComboBox.SelectedItem + "'");
      }

      return null;
    }
  }
}
