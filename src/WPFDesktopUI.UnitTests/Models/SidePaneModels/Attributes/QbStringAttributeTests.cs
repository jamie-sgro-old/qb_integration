using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels.Attributes {
  [TestClass]
  public class QbStringAttributeTests {
    [TestMethod]
    public void Name_Init_IsNull() {
      var dtAttr = new QbStringAttribute();

      var res = dtAttr.Name;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Name_OnChange_IsString() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Name = "John Smith";

      var res = dtAttr.Name;

      Assert.AreEqual("John Smith", res);
    }

    [TestMethod]
    public void Payload_Init_IsNull() {
      var dtAttr = new QbStringAttribute();

      var res = dtAttr.Payload;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Payload_OnChange_IsString() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Payload = "payload";

      var res = dtAttr.Payload;

      Assert.AreEqual("payload", res);
    }

    [TestMethod]
    public void IsMandatory_Init_False() {
      var dtAttr = new QbStringAttribute();

      var res = dtAttr.IsMandatory;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsMandatory_OnChange_True() {
      var dtAttr = new QbStringAttribute();
      dtAttr.IsMandatory = true;

      var res = dtAttr.IsMandatory;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void HasStringPayload_Init_True() {
      var dtAttr = new QbStringAttribute();

      var res = dtAttr.HasStringPayload;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void GetRow_BothValid_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Payload = "unused string";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual("0,0", res);
    }

    [TestMethod]
    public void GetRow_BothValidRow1_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Payload = "unused string";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual("1,0", res);
    }

    [TestMethod]
    public void GetRow_BothValidCol1_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Payload = "unused string";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual("0,1", res);
    }

    [TestMethod]
    public void GetRow_MissingPayload_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual("0,0", res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadRow1_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual("1,0", res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadCol1_DataRow() {
      var dtAttr = new QbStringAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual("0,1", res);
    }

    [TestMethod]
    public void GetRow_MissingSelectedItem_Payload() {
      var dtAttr = new QbStringAttribute();
      dtAttr.Payload = "used string";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual("used string", res);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetRow_MissingComboBox_Error() {
      var dtAttr = new QbStringAttribute();
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
