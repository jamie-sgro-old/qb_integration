using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  class QbDoubleAttribute : QbAbstractAttribute, IQbStringAttribute {
    public bool HasStringPayload { get; } = true;

    /// <summary>
    /// Decide whether to use data from the sidepane dropdown or textbox, default to
    /// selected combobox item if possible.
    /// </summary>
    /// <param name="row">A row from a DataTable</param>
    /// <param name="key">The dictionary Key for a QbAttribute</param>
    /// <returns>String for the data in a cell/a constant value</returns>
    public override dynamic GetRow(DataRow row) {
      var colName = ComboBox.SelectedItem;

      if (!string.IsNullOrEmpty(colName)) {
        // TODO: Add error checking here:
        return Convert.ToDouble(row[colName]);
      }

      if (!string.IsNullOrEmpty(Payload)) {
        // TODO: Add error checking here:
        return Convert.ToDouble(Payload);
      }

      return null;
    }
  }
}
