using System;
using System.Collections.Generic;
using System.Linq;
using MCBusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBConnect.Models;

namespace MCBusinessLogic.UnitTests.Models {
  [TestClass]
  public class ClientInvoiceLineItemModelTests {
    [TestMethod]
    public void AllProperties_CanBeFoundIn_InvoiceLineItemModel_ForEach() {
      var clientLine = new ClientInvoiceLineItemModel();
      var qb = new InvoiceLineItemModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientLine);

      foreach (var str in client) {
        Assert.IsTrue(qbPropStr.Contains(str));
      }
    }

    [TestMethod]
    public void AllProperties_CanBeFoundIn_InvoiceLineItemModel_Linq() {
      var clientLine = new ClientInvoiceLineItemModel();
      var qb = new InvoiceLineItemModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientLine);

      var res = client.All(qbPropStr.Contains);

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void NewProperties_CanNotBeFoundIn_InvoiceLineItemModel_Linq() {
      var clientHead = new MockInvoiceLineItemModel();
      var qb = new InvoiceHeaderModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientHead);

      var res = client.All(qbPropStr.Contains);

      Assert.IsFalse(res);
    }

    internal class MockInvoiceLineItemModel : ClientInvoiceLineItemModel {
      public string Mock { get; set; }
    }
  }
}
