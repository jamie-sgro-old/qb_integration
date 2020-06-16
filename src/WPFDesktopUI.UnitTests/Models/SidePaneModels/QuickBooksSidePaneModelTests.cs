using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models;
using WPFDesktopUI.Models.SidePaneModels.Attributes.Interfaces;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels {
  [TestClass]
  public class QuickBooksSidePaneModelTests {
    [TestMethod]
    public void AttrAdd_Count_1() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");

      var res = qbspModel.Attr.Count;

      Assert.AreEqual(1, res);
    }

    [TestMethod]
    public void AttrAdd_StringName_Null() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.Name;

      Assert.AreEqual("name1", res);
    }

    [TestMethod]
    public void AttrAdd_StringPayload_Null() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.Payload;

      Assert.IsNull(res);
    }

    [TestMethod]
    public void AttrAdd_StringPayloadChange_Null() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];
      attr.Payload = "name2";

      var res = attr.Payload;

      Assert.AreEqual("name2", res);
    }

    [TestMethod]
    public void AttrAdd_StringIsMandatory_False() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.IsMandatory;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_NotNull() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.ComboBox;

      Assert.IsNotNull(res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_ItemsSource_EmptyList() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.ComboBox.ItemsSource;

      CollectionAssert.AreEqual(new List<string>(), res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_ItemsSourceChange_NotEmpty() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];
      attr.ComboBox.ItemsSource.Add("a");
      attr.ComboBox.ItemsSource.Add("b");
      attr.ComboBox.ItemsSource.Add("c");

      var res = attr.ComboBox.ItemsSource;

      CollectionAssert.AreEqual(new List<string>() {"a","b","c"}, res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_SelectedItem_Empty() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.ComboBox.SelectedItem;

      Assert.AreEqual("", res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_SelectedItemChange_String() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];
      attr.ComboBox.SelectedItem = "selected item";

      var res = attr.ComboBox.SelectedItem;

      Assert.AreEqual("selected item", res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_RequiresCsv_True() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.ComboBox.RequiresCsv;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_RequiresCsvChange_False() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];
      attr.ComboBox.RequiresCsv = false;


      var res = attr.ComboBox.RequiresCsv;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_IsEnabled_False() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];

      var res = attr.ComboBox.IsEnabled;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void AttrAdd_ComboBox_IsEnabledChange_True() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = qbspModel.Attr["key1"];
      attr.ComboBox.IsEnabled = true;

      var res = attr.ComboBox.IsEnabled;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void AttrAdd_StringHasStringPayload_True() {
      var qbspModel = new QuickBooksSidePaneModel(Factory.CreateQbComboBox);
      qbspModel.AttrAdd(Factory.CreateQbStringAttribute(), "key1", "name1");
      var attr = (IQbStringAttribute)qbspModel.Attr["key1"];

      var res = attr.HasStringPayload;

      Assert.IsTrue(res);
    }
  }
}
