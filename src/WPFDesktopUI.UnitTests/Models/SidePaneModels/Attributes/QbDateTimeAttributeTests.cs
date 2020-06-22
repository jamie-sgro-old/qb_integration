using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.SidePaneModels.Attributes;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels.Attributes {
  [TestClass]
  public class QbDateTimeAttributeTests {

    [TestMethod]
    public void Name_Init_IsNull() {
      var dtAttr = new QbDateTimeAttribute();

      var res = dtAttr.Name;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void Name_OnChange_IsString() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Name = "John Smith";

      var res = dtAttr.Name;

      Assert.AreEqual("John Smith", res);
    }

    [TestMethod]
    public void Payload_Init_IsNow() {
      var dtAttr = new QbDateTimeAttribute();
      
      var res = dtAttr.Payload;
      
      Assert.AreEqual(DateTime.Now.ToString(), res);
    }

    [TestMethod]
    public void Payload_OnChange_IsJan1() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = new DateTime(2000, 01, 01).ToString();

      var res = dtAttr.Payload;

      Assert.AreEqual(new DateTime(2000, 01, 01).ToString(), res);
    }

    [TestMethod]
    public void IsMandatory_Init_False() {
      var dtAttr = new QbDateTimeAttribute();

      var res = dtAttr.IsMandatory;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsMandatory_OnChange_True() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.IsMandatory = true;

      var res = dtAttr.IsMandatory;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void HasDateTimePayload_Init_True() {
      var dtAttr = new QbDateTimeAttribute();

      var res = dtAttr.HasDateTimePayload;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void GetRow_BothValid_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = new DateTime(2012, 12, 12).ToString();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);
      
      Assert.AreEqual(new DateTime(2000, 01, 01), res);
    }

    [TestMethod]
    public void GetRow_BothValidRow1_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = new DateTime(2012, 12, 12).ToString();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(new DateTime(2000, 02, 01), res);
    }

    [TestMethod]
    public void GetRow_BothValidCol1_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = new DateTime(2012, 12, 12).ToString();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(new DateTime(2000, 01, 02), res);
    }

    [TestMethod]
    public void GetRow_MissingPayload_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(new DateTime(2000, 01, 01), res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadRow1_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header1";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[1]);

      Assert.AreEqual(new DateTime(2000, 02, 01), res);
    }

    [TestMethod]
    public void GetRow_MissingPayloadCol1_DataRow() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      dtAttr.ComboBox.SelectedItem = "header2";
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(new DateTime(2000, 01, 02), res);
    }

    [TestMethod]
    public void GetRow_MissingSelectedItem_Payload() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = new DateTime(2012, 12, 12).ToString();
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]);

      Assert.AreEqual(new DateTime(2012, 12, 12), res);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetRow_MissingComboBox_Error() {
      var dtAttr = new QbDateTimeAttribute();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]); // NullReferenceException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRow_BadPayload_Error() {
      var dtAttr = new QbDateTimeAttribute();
      dtAttr.Payload = "bad datetime string";
      dtAttr.ComboBox = Factory.CreateQbComboBox();
      var dt = GetDt();

      var res = dtAttr.GetRow(dt.Rows[0]); // FormatException
    }

    private static DataTable GetDt() {
      var dt = new DataTable();
      dt.Clear();

      dt.Columns.Add("header1");
      dt.Columns.Add("header2");
      dt.Columns.Add("header3");
      dt.Rows.Add(new object[] { new DateTime(2000, 01, 01), new DateTime(2000, 01, 02), new DateTime(2000, 01, 03) });
      dt.Rows.Add(new object[] { new DateTime(2000, 02, 01), new DateTime(2000, 02, 02), new DateTime(2000, 02, 03) });

      return dt;
    }
  }
}
