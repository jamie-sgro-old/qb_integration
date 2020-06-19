using System;
using System.ComponentModel;
using System.Data;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;

namespace WPFDesktopUI.Models.SidePaneModels.Attributes {
  public abstract class QbAbstractAttribute : IQbAttribute, INotifyPropertyChanged {
    public string Name { get; set; }
    public virtual string Payload { get; set; }
    public bool IsMandatory { get; set; } = false;
    public IQbComboBox ComboBox { get; set; }

    public string ToolTip { get; set; } = "Please import a .csv file in the 'Import'"+
                                          " tab before custom lists can be generated";


    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Decide whether to use data from the sidepane dropdown or textbox, default to
    /// selected combobox item if possible.
    /// </summary>
    /// <param name="row">A row from a DataTable</param>
    /// <param name="key">The dictionary Key for a QbAttribute</param>
    /// <returns>String for the data in a cell/a constant value</returns>
    public virtual dynamic GetRow(DataRow row) {
      var colName = ComboBox.SelectedItem;

      try {
        if (!string.IsNullOrEmpty(colName)) {
          return Convert.ToString(row[colName]);
        }

        if (!string.IsNullOrEmpty(Payload)) {
          return Convert.ToString(Payload);
        }
      } catch (FormatException e) {
        throw new FormatException(e.Message +
          "\nThis error occured in Column: '" + colName +
          "'\nWith the text: '" + row[colName] + "'");
      }

      return null;
    }
  }
}
