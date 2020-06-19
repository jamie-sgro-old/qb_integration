using System;
using System.Collections.Generic;
using System.Linq;
using MCBusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBConnect.Models;

namespace MCBusinessLogic.UnitTests.Models {
  [TestClass]
  public class ClientInvoiceHeaderModelTests {
    [TestMethod]
    public void AllProperties_CanBeFoundIn_InvoiceHeaderModel_ForEach() {
      var clientHead = new ClientInvoiceHeaderModel();
      var qb = new InvoiceHeaderModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientHead);

      foreach (var str in client) {
        Assert.IsTrue(qbPropStr.Contains(str));
      }
    }

    [TestMethod]
    public void AllProperties_CanBeFoundIn_InvoiceHeaderModel_Linq() {
      var clientHead = new ClientInvoiceHeaderModel();
      var qb = new InvoiceHeaderModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientHead);

      var res = client.All(qbPropStr.Contains);

      Assert.IsTrue(res);
    }

    [TestMethod]
    public void NewProperties_CanNotBeFoundIn_InvoiceHeaderModel_Linq() {
      var clientHead = new MockInvoiceHeaderModel();
      var qb = new InvoiceHeaderModel();
      var qbPropStr = PropStr.GetList(qb);
      var client = PropStr.GetList(clientHead);

      var res = client.All(qbPropStr.Contains);

      Assert.IsFalse(res);
    }

    internal class MockInvoiceHeaderModel : ClientInvoiceHeaderModel {
      public string Mock { get; set; }
    }
  }
}
