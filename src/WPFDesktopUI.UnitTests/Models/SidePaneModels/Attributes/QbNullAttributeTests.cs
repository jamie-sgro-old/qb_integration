using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels.Attributes {
  [TestClass]
  public class QbNullAttributeTests {
    [TestMethod]
    public void Name_Init_IsNull() {
      var dtAttr = new QbNullAttribute();

      var res = dtAttr.Name;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Name_OnChange_IsString() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Name = "John Smith";

      var res = dtAttr.Name;

      Assert.AreEqual("John Smith", res);
    }

    [TestMethod]
    public void Payload_Init_IsNull() {
      var dtAttr = new QbNullAttribute();

      var res = dtAttr.Payload;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Payload_OnChange_IsString() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Payload = "payload";

      var res = dtAttr.Payload;

      Assert.AreEqual("payload", res);
    }

    [TestMethod]
    public void IsMandatory_Init_False() {
      var dtAttr = new QbNullAttribute();

      var res = dtAttr.IsMandatory;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsMandatory_OnChange_True() {
      var dtAttr = new QbNullAttribute();
      dtAttr.IsMandatory = true;

      var res = dtAttr.IsMandatory;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void GetRow_BothValid_DataRow() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Payload = "unused payload";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_BothValidRow1_SelectedItem() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Payload = "unused payload";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_BothValidCol1_SelectedItem() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Payload = "unused payload";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_MissingPayload_SelectedItem() {
      var dtAttr = new QbNullAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadRow1_SelectedItem() {
      var dtAttr = new QbNullAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadCol1_SelectedItem() {
      var dtAttr = new QbNullAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(dtAttr.ComboBox.SelectedItem, res);
    }

    [TestMethod]
    public void GetRow_MissingSelectedItem_Null() {
      var dtAttr = new QbNullAttribute();
      dtAttr.Payload = "unused payload";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.IsNull(res);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetRow_MissingComboBox_Error() {
      var dtAttr = new QbNullAttribute();
      var dt = GetDt();
      var res = dtAttr.GetRow(dt.Rows[0]); // NullReferenceException
    }

    private static DataTable GetDt() {
      var dt = new DataTable();
      dt.Clear();

      dt.Columns.Add("header1");
      dt.Columns.Add("header2");
      dt.Columns.Add("header3");
      dt.Rows.Add(new object[] { "0,0", "0,1", "0,2" });
      dt.Rows.Add(new object[] { "1,0", "1,1", "1,2" });

      return dt;
    }
  }
}
