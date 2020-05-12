using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect {
  public class BasicImporter {
    private const string _qbwFilePath = "C:\\Users\\Jamie\\Nextcloud\\Sangwa\\Clients\\NX - Nexim Healthcare\\01 - Invoicing\\QB Mock\\NX Mock.qbw";

    public static void Import(InvoiceLineItemModel staff) {
      QBSessionManager sessionManager = null;
      bool _sessionBegun = false;
      bool _connectionOpen = false;

      try {
        // Create the session Manager object
        sessionManager = new QBSessionManager();

        // Create the message set request object to hold our request
        IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
        requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

        //Connect to QuickBooks and begin a session
        sessionManager.OpenConnection2("", "Sangwa Solutions Customer Export", ENConnectionType.ctLocalQBD);
        _connectionOpen = true;
        sessionManager.BeginSession(_qbwFilePath, ENOpenMode.omDontCare);
        _sessionBegun = true;

        // Put main process here:
        BuildInvoiceAddRq(requestMsgSet, staff);

        IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        //End the session and close the connection to QuickBooks
        sessionManager.EndSession();
        sessionManager.CloseConnection();
      } catch (Exception e) {

        Console.WriteLine(e.Message);
        throw;
      } finally {
        if (_sessionBegun) {
          sessionManager.EndSession();
        }
        if (_connectionOpen) {
          sessionManager.CloseConnection();
        }
      }
    }

    static void BuildInvoiceAddRq(IMsgSetRequest requestMsgSet, InvoiceLineItemModel lineItem) {
      // Init invoice variable
      IInvoiceAdd InvoiceAddRq = requestMsgSet.AppendInvoiceAddRq();


      #region Header

      // Fill CUSTOMER:JOB input box by string value
      InvoiceAddRq.CustomerRef.FullName.SetValue("CLASS");

      // Fill CLASS input box by string value
      InvoiceAddRq.ClassRef.FullName.SetValue("Barrie Area:Barrie Corporate");

      // Fill TEMPLATE input box by string value
      InvoiceAddRq.TemplateRef.FullName.SetValue("NEXIM's Invoice with credits &");

      #endregion Header


      #region LineItems


      // Create variable for adding new lines to the invoice
      IORInvoiceLineAdd LineItem = InvoiceAddRq.ORInvoiceLineAddList.Append();

      if (lineItem.Amount != null) {
        double amount = Convert.ToDouble(lineItem.Amount);
        LineItem.InvoiceLineAdd.Amount.SetValue(amount);
      }

      // CLASS
      if (lineItem.ClassRef != null) {
        LineItem.InvoiceLineAdd.ClassRef.FullName.SetValue(lineItem.ClassRef);
      }

      // DESCRIPTION
      if (lineItem.Desc != null) {
        LineItem.InvoiceLineAdd.Desc.SetValue(lineItem.Desc);
      }

      if (lineItem.InventorySiteLocationRef != null) {
        LineItem.InvoiceLineAdd.InventorySiteLocationRef.FullName
          .SetValue(lineItem.InventorySiteLocationRef); 
      }

      if (lineItem.InventorySiteRef != null) {
        LineItem.InvoiceLineAdd.InventorySiteRef.FullName
            .SetValue(lineItem.InventorySiteRef); 
      }

      if (lineItem.IsTaxable != null) {
        bool isTaxable = Convert.ToBoolean(lineItem.IsTaxable);
        LineItem.InvoiceLineAdd.IsTaxable.SetValue(isTaxable);
      }

      // ITEM
      if (lineItem.ItemRef != null) {
        LineItem.InvoiceLineAdd.ItemRef.FullName.SetValue(lineItem.ItemRef);
      }


      if (lineItem.LinkToTxnTxnID != null) {
        LineItem.InvoiceLineAdd.LinkToTxn.TxnID.SetValue(lineItem.LinkToTxnTxnID); 
      }

      if (lineItem.LinkToTxnTxnLineID != null) {
        LineItem.InvoiceLineAdd.LinkToTxn.TxnLineID.SetValue(lineItem.LinkToTxnTxnLineID);
      }

      if (lineItem.ORRatePriceLevelRate != null) {
        double orRatePriceLevelRate = Convert.ToDouble(lineItem.ORRatePriceLevelRate);
        LineItem.InvoiceLineAdd.ORRatePriceLevel.Rate.SetValue(orRatePriceLevelRate); 
      }

      if (lineItem.ORRatePriceLevelRatePercent != null) {
        double orRatePriceLevelRatePercent = Convert
          .ToDouble(lineItem.ORRatePriceLevelRatePercent);

        LineItem.InvoiceLineAdd.ORRatePriceLevel.RatePercent
          .SetValue(orRatePriceLevelRatePercent);
      }

      if (lineItem.ORRatePriceLevelPriceLevelRef != null) {
        LineItem.InvoiceLineAdd.ORRatePriceLevel.PriceLevelRef
          .FullName.SetValue(lineItem.ORRatePriceLevelPriceLevelRef);
      }

      // SERIAL NUMBER
      if (lineItem.ORSerialLotNumberSerialNumber != null) {
        LineItem.InvoiceLineAdd.ORSerialLotNumber.SerialNumber
          .SetValue(lineItem.ORSerialLotNumberSerialNumber); 
      }

      // LOT NUMBER
      if (lineItem.ORSerialLotNumberLotNumber != null) {
        LineItem.InvoiceLineAdd.ORSerialLotNumber.LotNumber
          .SetValue(lineItem.ORSerialLotNumberLotNumber);
      }

      // OTHER1
      if (lineItem.Other1 != null) {
        LineItem.InvoiceLineAdd.Other1.SetValue(lineItem.Other1); 
      }

      // OTHER2
      if (lineItem.Other2 != null) {
        LineItem.InvoiceLineAdd.Other2.SetValue(lineItem.Other2); 
      }

      if (lineItem.OverrideItemAccountRef != null) {
        LineItem.InvoiceLineAdd.OverrideItemAccountRef.FullName
      .SetValue(lineItem.OverrideItemAccountRef); 
      }

      // QTY
      if (lineItem.Quantity != null) {
        double quantity = Convert.ToDouble(lineItem.Quantity);
        LineItem.InvoiceLineAdd.Quantity.SetValue(quantity);
      }

      if (lineItem.SalesTaxCodeRef != null) {
        LineItem.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue(lineItem.SalesTaxCodeRef); 
      }

      // DATE
      if (lineItem.ServiceDate != null) {
        DateTime serviceDate = Convert.ToDateTime(lineItem.ServiceDate);
        LineItem.InvoiceLineAdd.ServiceDate.SetValue(serviceDate); 
      }

      // TAX AMOUNT
      if (lineItem.TaxAmount != null) {
        double taxAmount = Convert.ToDouble(lineItem.TaxAmount);
        LineItem.InvoiceLineAdd.TaxAmount.SetValue(taxAmount); 
      }

      // UNIT OF MEASURE
      if (lineItem.UnitOfMeasure != null) {
        LineItem.InvoiceLineAdd.UnitOfMeasure.SetValue(lineItem.UnitOfMeasure); 
      }




      // Add SubTotal line item
      IORInvoiceLineAdd SubTotal = InvoiceAddRq.ORInvoiceLineAddList.Append();

      SubTotal.InvoiceLineAdd.ItemRef.FullName.SetValue("Sub- Totals");

      #endregion LineItems
    }
  }
}
