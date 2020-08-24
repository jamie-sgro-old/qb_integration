using System;
using System.Collections.Generic;
using MCBusinessLogic.Controllers;
using MCBusinessLogic.Controllers.Interfaces;
using MCBusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QBConnect;
using QBConnect.Models;

namespace MCBusinessLogic.UnitTests.Controllers {
  [TestClass]
  public class QbImportControllerTests {
    [TestMethod]
    public void QbFilePath_Init_Constructed() {
      var qbi = new QbImportController("specified in the constructor parameters", null);

      var res = qbi.QbFilePath;

      Assert.AreEqual("specified in the constructor parameters", res);
    }

    [TestMethod]
    public void QbFilePath_OnChange_NewString() {
      var qbi = new QbImportController("specified in the constructor parameters", null);
      qbi.QbFilePath = "new QbFilePath";

      var res = qbi.QbFilePath;

      Assert.AreEqual("new QbFilePath", res);
    }

    #region MapHeader

    [TestMethod]
    public void MapHeader_SingleData() {
      var qbi = new QbImportController("QbFilePath", null);
      var csvModel = BaseCsvModel();

      var res = qbi.MapHeader(csvModel);

      Assert.AreEqual("class1", res.ClassRefFullName);
      Assert.AreEqual("cx1", res.CustomerRefFullName);
      Assert.AreEqual("template1", res.TemplateRefFullName);
      Assert.AreEqual(null, res.TxnDate);
      Assert.AreEqual(null, res.TermsRefFullName);
    }

    [TestMethod]
    public void MapHeader_ExtendedInterface_MockExtensionProps_NotIncluded() {
      var qbi = new QbImportController("QbFilePath", null);
      // Reflection off of this property instead of the interface would
      // fail since Mock isn't a property in IInvoiceHeaderModel
      var csvModel = new MockInvoiceHeaderModel {
        ClassRefFullName = "class1",
        Mock = "mock1" // This prop should be ignored
      };

      var res = qbi.MapHeader(csvModel);

      Assert.AreEqual("class1", res.ClassRefFullName);
    }

    #endregion MapHeader


    #region MapLineItems

    [TestMethod]
    public void MapLineItems_SingleData() {
      var qbi = new QbImportController("QbFilePath", null);
      var csvModels = new List<ICsvModel> {
        BaseCsvModel()
      };

      var res = qbi.MapLineItems(csvModels);

      Assert.IsTrue(res.Count == 1);
      Assert.AreEqual("item1", res[0].ItemRef);
      Assert.AreEqual("other1", res[0].Other1);
      Assert.AreEqual(null, res[0].Quantity);
      Assert.AreEqual(null, res[0].Amount);
    }

    [TestMethod]
    public void MapLineItems_2SameData() {
      var qbi = new QbImportController("QbFilePath", null);
      var csvModels = new List<ICsvModel> {
        BaseCsvModel(),
        BaseCsvModel()
      };

      var res = qbi.MapLineItems(csvModels);

      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("item1",  res[0].ItemRef);
      Assert.AreEqual("other1", res[0].Other1);
      Assert.AreEqual(null,     res[0].Quantity);
      Assert.AreEqual(null,     res[0].Amount);
      Assert.AreEqual("item1",  res[1].ItemRef);
      Assert.AreEqual("other1", res[1].Other1);
      Assert.AreEqual(null,     res[1].Quantity);
      Assert.AreEqual(null,     res[1].Amount);

    }

    [TestMethod]
    public void MapLineItems_2DifferentData() {
      var qbi = new QbImportController("QbFilePath", null);
      var csvModels = new List<ICsvModel> {
        BaseCsvModel(),
        BaseCsvModel()
      };
      csvModels[1].PONumber = "po2"; // Ignored since not in IClientInvoiceLineItemModel
      csvModels[1].Quantity = 2;

      var res = qbi.MapLineItems(csvModels);

      Assert.IsTrue(res.Count == 2);
      Assert.AreEqual("item1", res[0].ItemRef);
      Assert.AreEqual("other1", res[0].Other1);
      Assert.AreEqual(null, res[0].Quantity);
      Assert.AreEqual(null, res[0].Amount);
      Assert.AreEqual("item1", res[1].ItemRef);
      Assert.AreEqual("other1", res[1].Other1);
      Assert.AreEqual(2, res[1].Quantity);
      Assert.AreEqual(null, res[1].Amount);

    }

    #endregion MapLineItems

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


  internal interface IMockInvoiceHeaderModel : IClientInvoiceHeaderModel {
    string Mock { get; set; }
  }

  internal class MockInvoiceHeaderModel : IMockInvoiceHeaderModel {
    public string ClassRefFullName { get; set; }
    public string CustomerRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public string TermsRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string BillAddress { get; set; }
    public string ShipAddress { get; set; }
    public string PONumber { get; set; }
    public string FOB { get; set; }
    public string Other { get; set; }
    public string Mock { get; set; }
    public object Clone() {
      throw new NotImplementedException();
    }
  }

}
