using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels.Attributes {
  [TestClass]
  public class QbDoubleAttributeTests {
    [TestMethod]
    public void Name_Init_IsNull() {
      var dtAttr = new QbDoubleAttribute();

      var res = dtAttr.Name;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Name_OnChange_IsString() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Name = "1234";

      var res = dtAttr.Name;

      Assert.AreEqual("1234", res);
    }

    [TestMethod]
    public void Payload_Init_IsNull() {
      var dtAttr = new QbDoubleAttribute();

      var res = dtAttr.Payload;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Payload_OnChange_IsString() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Payload = "payload";

      var res = dtAttr.Payload;

      Assert.AreEqual("payload", res);
    }

    [TestMethod]
    public void IsMandatory_Init_False() {
      var dtAttr = new QbDoubleAttribute();

      var res = dtAttr.IsMandatory;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsMandatory_OnChange_True() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.IsMandatory = true;

      var res = dtAttr.IsMandatory;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void HasStringPayload_Init_True() {
      var dtAttr = new QbDoubleAttribute();

      var res = dtAttr.HasStringPayload;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void GetRow_BothValid_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Payload = "1234";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(00, res);
    }

    [TestMethod]
    public void GetRow_BothValidRow1_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Payload = "1234";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(10, res);
    }

    [TestMethod]
    public void GetRow_BothValidCol1_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Payload = "1234";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(01, res);
    }

    [TestMethod]
    public void GetRow_MissingPayload_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(00, res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadRow1_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(10, res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadCol1_DataRow() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(01, res);
    }

    [TestMethod]
    public void GetRow_MissingSelectedItem_Payload() {
      var dtAttr = new QbDoubleAttribute();
      dtAttr.Payload = "1234";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(1234, res);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetRow_MissingComboBox_Error() {
      var dtAttr = new QbDoubleAttribute();
      var dt = GetDt();
      var res = dtAttr.GetRow(dt.Rows[0]); // NullReferenceException
    }

    private static DataTable GetDt() {
      var dt = new DataTable();
      dt.Clear();

      dt.Columns.Add("header1");
      dt.Columns.Add("header2");
      dt.Columns.Add("header3");
      dt.Rows.Add(new object[] { 00, 01, 02 });
      dt.Rows.Add(new object[] { 10, 11, 12 });

      return dt;
    }
  }
}
