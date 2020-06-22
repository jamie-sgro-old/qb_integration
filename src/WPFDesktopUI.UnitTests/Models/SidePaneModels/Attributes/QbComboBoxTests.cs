using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopUI.Models.SidePaneModels.Attributes;

namespace WPFDesktopUI.UnitTests.Models.SidePaneModels.Attributes {
  [TestClass]
  public class QbComboBoxTests {
    [TestMethod]
    public void ItemsSource_Init_IsEmpty() {
      var cb = new QbComboBox();

      var res = cb.ItemsSource;

      CollectionAssert.AreEqual(new List<string>(), res);
    }

    [TestMethod]
    public void ItemsSource_Init_Count0() {
      var cb = new QbComboBox();

      var res = cb.ItemsSource.Count;

      Assert.AreEqual(0, res);
    }

    [TestMethod]
    public void ItemsSource_OnChange_NewList() {
      var cb = new QbComboBox();
      cb.ItemsSource.Add("a");
      cb.ItemsSource.Add("b");
      cb.ItemsSource.Add("c");

      var res = cb.ItemsSource;

      CollectionAssert.AreEqual(new List<string>() {"a","b","c"}, res);
    }

    [TestMethod]
    public void SelectedItem_Init_Empty() {
      var cb = new QbComboBox();

      var res = cb.SelectedItem;

      Assert.AreEqual("", res);
    }

    [TestMethod]
    public void SelectedItem_Init_IsNotNull() {
      var cb = new QbComboBox();

      var res = cb.SelectedItem;
      
      Assert.IsNotNull(res);
    }

    [TestMethod]
    public void SelectedItem_Init_IsEmpty() {
      var cb = new QbComboBox();

      var res = cb.SelectedItem;

      Assert.IsTrue(string.IsNullOrEmpty(res));
    }

    [TestMethod]
    public void SelectedItem_OnChange_IsString() {
      var cb = new QbComboBox();
      cb.SelectedItem = "item";

      var res = cb.SelectedItem;

      Assert.AreEqual("item", res);
    }

    [TestMethod]
    public void RequiresCsv_Init_True() {
      var cb = new QbComboBox();

      var res = cb.RequiresCsv;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void RequiresCsv_OnChange_False() {
      var cb = new QbComboBox();
      cb.RequiresCsv = false;

      var res = cb.RequiresCsv;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsEnabled_Init_False() {
      var cb = new QbComboBox();

      var res = cb.IsEnabled;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsEnabled_OnChange_True() {
      var cb = new QbComboBox();
      cb.IsEnabled = true;

      var res = cb.IsEnabled;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void IsBlank_Init_True() {
      var cb = new QbComboBox();

      var res = cb.IsBlank;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void IsBlank_OnChange_True() {
      var cb = new QbComboBox();
      cb.SelectedItem = "not blank";

      var res = cb.IsBlank;

      Assert.IsFalse(res);
    }

    [TestMethod]
    public void IsBlank_OnEmptyChange_True() {
      var cb = new QbComboBox();
      cb.SelectedItem = "";

      var res = cb.IsBlank;

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void IsBlank_OnChange_False() {
      var cb = new QbComboBox();
      cb.SelectedItem = null;

      var res = cb.IsBlank;

      Assert.IsTrue(res);
    }
  }
}
