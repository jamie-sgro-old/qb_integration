using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBConnect.Classes;
using QBFC13Lib;

namespace QBConnect.UnitTests {
  [TestClass]
  public class HeaderItemTests {
    [TestMethod]
    public void ParseAddress_CommaSep() {
      var header = GetHeader();
      var addrs = "123 Main St, Brampton, ON N2E 4X2, Canada";

      var res = HeaderItem.ParseAddress(header.BillAddress, addrs);

      Assert.AreEqual("123 Main St", res.Addr1.GetValue());
      Assert.AreEqual(" Brampton", res.Addr2.GetValue());
      Assert.AreEqual(" ON N2E 4X2", res.Addr3.GetValue());
      Assert.AreEqual(" Canada", res.Addr4.GetValue());
    }

    [TestMethod]
    public void ParseAddress_SingleLine() {
      var header = GetHeader();
      var addrs = "3045 Bd Levesque O Laval QC H7V 1C3";

      var res = HeaderItem.ParseAddress(header.BillAddress, addrs);

      Assert.AreEqual("3045 Bd Levesque O Laval QC H7V 1C3", res.Addr1.GetValue());
    }

    [TestMethod]
    public void ParseAddress_Empty_Addr1Empty() {
      var header = GetHeader();
      var addrs = "";

      var res = HeaderItem.ParseAddress(header.BillAddress, addrs);

      Assert.AreEqual("", res.Addr1.GetValue());
    }

    [TestMethod]
    [ExpectedException(typeof(COMException),
      "No Value has been set.")]
    public void ParseAddress_Null_COMException() {
      var header = GetHeader();
      string addrs = null;

      var res = HeaderItem.ParseAddress(header.BillAddress, addrs);

      var exception = res.Addr1.GetValue(); // COMException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException),
      "Could not parse Address. QuickBook's only allows up to five lines per address.")]
    public void ParseAddress_InvalidArrs_OutOfRangeException() {
      var header = GetHeader();
      string addrs = "3072 Brittany Dr, Colwood, BC, V9B 5P7(Victoria ,British Columbia), Canada";

      var res = HeaderItem.ParseAddress(header.BillAddress, addrs); // ArgumentOutOfRangeException
    }

    private IInvoiceAdd GetHeader() {
      var mock = new QBSessionManager();
      var msgReq = mock.CreateMsgSetRequest("US", 13, 0);
      return msgReq.AppendInvoiceAddRq();
    }
  }
}
