using System;
using System.Linq;
using MCBusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBConnect.Models;

namespace MCBusinessLogic.UnitTests.Models {
  [TestClass]
  public class CsvModelTests {
    [TestMethod]
    public void AllProperties_CanBeFoundIn_LineAndHeader_ForEach() {
      var csv = new CsvModel();
      var csvPropStr = PropStr.GetList(csv);
      var line = new ClientInvoiceLineItemModel();
      var joinedModel = PropStr.GetList(line);
      var header = new ClientInvoiceHeaderModel();
      var headerPropStr = PropStr.GetList(header);
      joinedModel.AddRange(headerPropStr);

      foreach (var str in csvPropStr) {
        Assert.IsTrue(joinedModel.Contains(str));
      }
    }

    [TestMethod]
    public void LineAndHeader_CanBeFoundIn_Properties_ForEach() {
      var csv = new CsvModel();
      var csvPropStr = PropStr.GetList(csv);
      var line = new ClientInvoiceLineItemModel();
      var joinedModel = PropStr.GetList(line);
      var header = new ClientInvoiceHeaderModel();
      var headerPropStr = PropStr.GetList(header);
      joinedModel.AddRange(headerPropStr);

      foreach (var str in joinedModel) {
        Assert.IsTrue(csvPropStr.Contains(str));
      }
    }

    [TestMethod]
    public void AllProperties_CanBeFoundIn_LineAndHeader_Linq() {
      var csv = new CsvModel();
      var csvPropStr = PropStr.GetList(csv);
      var line = new ClientInvoiceLineItemModel();
      var joinedModel = PropStr.GetList(line);
      var header = new ClientInvoiceHeaderModel();
      var headerPropStr = PropStr.GetList(header);
      joinedModel.AddRange(headerPropStr);

      var res = csvPropStr.All(joinedModel.Contains);

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void LineAndHeader_CanBeFoundIn_Properties_Linq() {
      var csv = new CsvModel();
      var csvPropStr = PropStr.GetList(csv);
      var line = new ClientInvoiceLineItemModel();
      var joinedModel = PropStr.GetList(line);
      var header = new ClientInvoiceHeaderModel();
      var headerPropStr = PropStr.GetList(header);
      joinedModel.AddRange(headerPropStr);

      var res = joinedModel.All(csvPropStr.Contains);

      Assert.IsTrue(res);
    }
  }
}
