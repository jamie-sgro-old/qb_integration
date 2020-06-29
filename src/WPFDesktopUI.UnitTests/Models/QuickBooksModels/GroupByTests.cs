using System;
using System.Collections.Generic;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBConnect;
using QBConnect.Models;
using WPFDesktopUI.Models.QuickBooksModels;

namespace WPFDesktopUI.UnitTests.Models.QuickBooksModels {
  [TestClass]
  public class UnitTest1 {
    
    #region Import 2
    
    [TestMethod]
    public void Import_2SameData() {
      // Arrange
      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item1",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[1].Other1);
    }

    [TestMethod]
    public void Import_2DifferentLines() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item2",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other2",    res[0].Lines[1].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_Class() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ClassRefFullName = "class2";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class2",    res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item1",     res[1].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[1].Lines[0].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_Cx() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].CustomerRefFullName = "cx2";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class1",    res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx2",       res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item1",     res[1].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[1].Lines[0].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_CxClass() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].CustomerRefFullName = "cx2";
      csvModels[1].ClassRefFullName = "class2";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("class1", res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1", res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class2", res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx2", res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("item1", res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1", res[0].Lines[0].Other1);
      Assert.AreEqual("item1", res[1].Lines[0].ItemRef);
      Assert.AreEqual("other1", res[1].Lines[0].Other1);
    }

    #endregion Import 2


    #region Import 3

    [TestMethod]
    public void Import_3SameData() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item1",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[1].Other1);
      Assert.AreEqual("item1",     res[0].Lines[2].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[2].Other1);
    }

    [TestMethod]
    public void Import_3DifferentLines() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";
      csvModels.Add(BaseCsvModel());
      csvModels[2].ItemRef = "item3";
      csvModels[2].Other1 = "other3";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item2",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other2",    res[0].Lines[1].Other1);
      Assert.AreEqual("item3",     res[0].Lines[2].ItemRef);
      Assert.AreEqual("other3",    res[0].Lines[2].Other1);
    }

    [TestMethod]
    public void Import_3DifferentHeader_Class() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ClassRefFullName = "class2";
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";
      csvModels.Add(BaseCsvModel());
      csvModels[2].ItemRef = "item3";
      csvModels[2].Other1 = "other3";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class2",    res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item2",     res[1].Lines[0].ItemRef);
      Assert.AreEqual("other2",    res[1].Lines[0].Other1);
      Assert.AreEqual("item3",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other3",    res[0].Lines[1].Other1);
    }

    [TestMethod]
    public void Import_3DifferentHeader_Cx() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";
      csvModels.Add(BaseCsvModel());
      csvModels[2].CustomerRefFullName = "cx2";
      csvModels[2].ItemRef = "item3";
      csvModels[2].Other1 = "other3";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class1",    res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx2",       res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item2",     res[0].Lines[1].ItemRef);
      Assert.AreEqual("other2",    res[0].Lines[1].Other1);
      Assert.AreEqual("item3",     res[1].Lines[0].ItemRef);
      Assert.AreEqual("other3",    res[1].Lines[0].Other1);
    }

    [TestMethod]
    public void Import_3DifferentHeader_CxClass() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ClassRefFullName = "class2";
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";
      csvModels.Add(BaseCsvModel());
      csvModels[2].CustomerRefFullName = "cx2";
      csvModels[2].ItemRef = "item3";
      csvModels[2].Other1 = "other3";

      //Act
      var res = GroupBy.GroupInvoices(csvModels);

      //Assert
      // Order changes on this one
      Assert.IsTrue(res.Count == 3);
      Assert.AreEqual("class1",    res[0].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[0].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[0].Header.TemplateRefFullName);
      Assert.AreEqual("class1",    res[1].Header.ClassRefFullName);
      Assert.AreEqual("cx2",       res[1].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[1].Header.TemplateRefFullName);
      Assert.AreEqual("class2",    res[2].Header.ClassRefFullName);
      Assert.AreEqual("cx1",       res[2].Header.CustomerRefFullName);
      Assert.AreEqual("template1", res[2].Header.TemplateRefFullName);
      Assert.AreEqual("item1",     res[0].Lines[0].ItemRef);
      Assert.AreEqual("other1",    res[0].Lines[0].Other1);
      Assert.AreEqual("item3",     res[1].Lines[0].ItemRef);
      Assert.AreEqual("other3",    res[1].Lines[0].Other1);
      Assert.AreEqual("item2",     res[2].Lines[0].ItemRef);
      Assert.AreEqual("other2",    res[2].Lines[0].Other1);
    }

    #endregion Import 3









    private CsvModel BaseCsvModel() {
      return new CsvModel {
        ClassRefFullName = "class1",
        CustomerRefFullName = "cx1",
        TemplateRefFullName = "template1",
        ItemRef = "item1",
        Other1 = "other1"
      };
    }
  }
}
