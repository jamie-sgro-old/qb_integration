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
      var qbi = new QbImportController("specified in the constructor parameters", null, null);

      var res = qbi.QbFilePath;

      Assert.AreEqual("specified in the constructor parameters", res);
    }

    [TestMethod]
    public void QbFilePath_OnChange_NewString() {
      var qbi = new QbImportController("specified in the constructor parameters", null, null);
      qbi.QbFilePath = "new QbFilePath";

      var res = qbi.QbFilePath;

      Assert.AreEqual("new QbFilePath", res);
    }

    #region Import 2

    [TestMethod]
    public void Import_2SameData() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      //csvModels[1].ItemRef = "item2";
      //csvModels[1].Other1 = "other2";

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines =  new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);
      
      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 1);
      Assert.IsTrue(saveLines.Count == 1);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item1",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][1].Other1);
    }

    [TestMethod]
    public void Import_2DifferentLines() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ItemRef = "item2";
      csvModels[1].Other1 = "other2";

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);
      
      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 1);
      Assert.IsTrue(saveLines.Count == 1);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item2",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other2",    saveLines[0][1].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_Class() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].ClassRefFullName = "class2";

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);
      
      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 2);
      Assert.IsTrue(saveLines.Count == 2);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class2",    saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item1",     saveLines[1][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[1][0].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_Cx() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].CustomerRefFullName = "cx2";

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 2);
      Assert.IsTrue(saveLines.Count == 2);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class1",    saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx2",       saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item1",     saveLines[1][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[1][0].Other1);
    }

    [TestMethod]
    public void Import_2DifferentHeader_CxClass() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels[1].CustomerRefFullName = "cx2";
      csvModels[1].ClassRefFullName = "class2";

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 2);
      Assert.IsTrue(saveLines.Count == 2);
      Assert.AreEqual("class1", saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1", saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class2", saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx2", saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("item1", saveLines[0][0].ItemRef);
      Assert.AreEqual("other1", saveLines[0][0].Other1);
      Assert.AreEqual("item1", saveLines[1][0].ItemRef);
      Assert.AreEqual("other1", saveLines[1][0].Other1);
    }

    #endregion Import 2


    #region Import 3

    [TestMethod]
    public void Import_3SameData() {

      var csvModels = new List<ICsvModel>();
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());
      csvModels.Add(BaseCsvModel());

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 1);
      Assert.IsTrue(saveLines.Count == 1);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item1",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][1].Other1);
      Assert.AreEqual("item1",     saveLines[0][2].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][2].Other1);
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

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 1);
      Assert.IsTrue(saveLines.Count == 1);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item2",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other2",    saveLines[0][1].Other1);
      Assert.AreEqual("item3",     saveLines[0][2].ItemRef);
      Assert.AreEqual("other3",    saveLines[0][2].Other1);
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

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 2);
      Assert.IsTrue(saveLines.Count == 2);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class2",    saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item2",     saveLines[1][0].ItemRef);
      Assert.AreEqual("other2",    saveLines[1][0].Other1);
      Assert.AreEqual("item3",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other3",    saveLines[0][1].Other1);
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

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      Assert.IsTrue(saveHeaderModel.Count == 2);
      Assert.IsTrue(saveLines.Count == 2);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class1",    saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx2",       saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item2",     saveLines[0][1].ItemRef);
      Assert.AreEqual("other2",    saveLines[0][1].Other1);
      Assert.AreEqual("item3",     saveLines[1][0].ItemRef);
      Assert.AreEqual("other3",    saveLines[1][0].Other1);
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

      // Each item is a unique header+lines pairing that constitutes a single invoice
      var saveHeaderModel = new List<IInvoiceHeaderModel>();
      var saveLines = new List<List<IInvoiceLineItemModel>>();

      // Set up mocking
      var mockImporter = new Mock<IInvoiceImporter>();
      mockImporter.Setup(x => x.Import(It.IsAny<IInvoiceHeaderModel>(), It.IsAny<List<IInvoiceLineItemModel>>()))
        .Callback<IInvoiceHeaderModel, List<IInvoiceLineItemModel>>((head, lines) => {
          saveHeaderModel.Add(head);
          saveLines.Add(lines);
        });
      IInvoiceImporter CreateInvoiceImporter(string path) {
        return mockImporter.Object;
      }
      var qbi = new QbImportController("QbFilePath", McFactory.CreateClientInvoiceHeaderModel, CreateInvoiceImporter);

      //Act
      qbi.Import(csvModels);

      //Assert
      // Order changes on this one
      Assert.IsTrue(saveHeaderModel.Count == 3);
      Assert.IsTrue(saveLines.Count == 3);
      Assert.AreEqual("class1",    saveHeaderModel[0].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[0].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[0].TemplateRefFullName);
      Assert.AreEqual("class1",    saveHeaderModel[1].ClassRefFullName);
      Assert.AreEqual("cx2",       saveHeaderModel[1].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[1].TemplateRefFullName);
      Assert.AreEqual("class2",    saveHeaderModel[2].ClassRefFullName);
      Assert.AreEqual("cx1",       saveHeaderModel[2].CustomerRefFullName);
      Assert.AreEqual("template1", saveHeaderModel[2].TemplateRefFullName);
      Assert.AreEqual("item1",     saveLines[0][0].ItemRef);
      Assert.AreEqual("other1",    saveLines[0][0].Other1);
      Assert.AreEqual("item3",     saveLines[1][0].ItemRef);
      Assert.AreEqual("other3",    saveLines[1][0].Other1);
      Assert.AreEqual("item2",     saveLines[2][0].ItemRef);
      Assert.AreEqual("other2",    saveLines[2][0].Other1);
    }

    #endregion Import 3


    #region MapHeader

    [TestMethod]
    public void MapHeader_SingleData() {
      var qbi = new QbImportController("QbFilePath", null, null);
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
      var qbi = new QbImportController("QbFilePath", null, null);
      var csvModel = new MockInvoiceHeaderModel {
        ClassRefFullName = "class1",
        Mock = "mock1"
      };

      var res = qbi.MapHeader(csvModel);

      Assert.AreEqual("class1", res.ClassRefFullName);
    }

    #endregion MapHeader

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

  internal class MockInvoiceHeaderModel : IMockInvoiceHeaderModel {
    public string ClassRefFullName { get; set; }
    public string CustomerRefFullName { get; set; }
    public string TemplateRefFullName { get; set; }
    public string TermsRefFullName { get; set; }
    public DateTime? TxnDate { get; set; }
    public string BillAddress { get; set; }
    public string ShipAddress { get; set; }
    public string PONumber { get; set; }
    public string Other { get; set; }
    public string Mock { get; set; }
  }

  internal interface IMockInvoiceHeaderModel : IClientInvoiceHeaderModel {
    string Mock { get; set; }
  }
}
