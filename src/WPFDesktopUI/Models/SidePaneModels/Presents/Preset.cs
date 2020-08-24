using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBusinessLogic.DataAccess;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.Models.SidePaneModels.Presents {
  public class Preset : IPreset {
    public void Update(Dictionary<string, IQbAttribute> attr, string preset) {
      var dataList = Factory.CreatePresetModel();
      dataList.Preset = preset;
      dataList.Desc = attr["Desc"].ComboBox.SelectedItem;
      dataList.ItemRef = attr["ItemRef"].ComboBox.SelectedItem;
      dataList.ORRatePriceLevelRate = attr["ORRatePriceLevelRate"].ComboBox.SelectedItem;
      dataList.Quantity = attr["Quantity"].ComboBox.SelectedItem;
      dataList.Other1 = attr["Other1"].ComboBox.SelectedItem;
      dataList.Other2 = attr["Other2"].ComboBox.SelectedItem;
      dataList.CustomerRefFullName = attr["CustomerRefFullName"].ComboBox.SelectedItem;
      dataList.ClassRefFullName = attr["ClassRefFullName"].ComboBox.SelectedItem;
      dataList.TemplateRefFullName = attr["TemplateRefFullName"].ComboBox.SelectedItem;
      dataList.TxnDate = attr["TxnDate"].ComboBox.SelectedItem;
      dataList.BillAddress = attr["BillAddress"].ComboBox.SelectedItem;
      dataList.ShipAddress = attr["ShipAddress"].ComboBox.SelectedItem;
      dataList.TermsRefFullName = attr["TermsRefFullName"].ComboBox.SelectedItem;
      dataList.PONumber = attr["PONumber"].ComboBox.SelectedItem;
      dataList.PONumber = attr["FOB"].ComboBox.SelectedItem;
      dataList.Other = attr["Other"].ComboBox.SelectedItem;

      SqliteDataAccess.SaveData(
        @"UPDATE `csv_data`
        SET
          Preset = @Preset,
          ItemRef = @ItemRef,
          ORRatePriceLevelRate = @ORRatePriceLevelRate,
          Quantity = @Quantity,
          Desc = @Desc,
          Other1 = @Other1,
          Other2 = @Other2,
          CustomerRefFullName = @CustomerRefFullName,
          ClassRefFullName = @ClassRefFullName,
          TemplateRefFullName = @TemplateRefFullName,
          TxnDate = @TxnDate,
          BillAddress = @BillAddress,
          ShipAddress = @ShipAddress,
          TermsRefFullName = @TermsRefFullName,
          PONumber = @PONumber,
          Other = @Other
        WHERE Preset = @Preset;", dataList);
    }

    public List<T> Read<T>(string preset) {
      var query = "SELECT id, * FROM csv_data WHERE Preset = '" + preset + "'";
      var dataList = SqliteDataAccess.LoadData<T>(query);

      return dataList;
    }
  }
}
