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

    public static void Import(InvoiceHeaderModel header, InvoiceLineItemModel lineItem) {
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
        BuildInvoiceAddRq(requestMsgSet, header, lineItem);
        BuildCustomerQuery(requestMsgSet);

        IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        // Temp
        Console.WriteLine(responseMsgSet.ToXMLString());

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

    static void BuildInvoiceAddRq(IMsgSetRequest requestMsgSet, InvoiceHeaderModel header, InvoiceLineItemModel lineItem) {
      // Init invoice variable
      IInvoiceAdd Header = requestMsgSet.AppendInvoiceAddRq();


      #region Header

      // Accounts Recievable Ref
      if (header.ARAccountRefFullName != null) {
        Header.ARAccountRef.FullName.SetValue(header.ARAccountRefFullName); 
      }

      if (header.ARAccountRefListID != null) {
        Header.ARAccountRef.ListID.SetValue(header.ARAccountRefListID); 
      }

      // CLASS Header Box
      if (header.ClassRefFullName != null) {
        Header.ClassRef.FullName.SetValue(header.ClassRefFullName);
      }

      if (header.ClassRefListID != null) {
        Header.ClassRef.ListID.SetValue(header.ClassRefListID); 
      }

      // CUSTOMER MESSAGE Footer Box
      if (header.CustomerMsgRefFullName != null) {
        Header.CustomerMsgRef.FullName.SetValue(header.CustomerMsgRefFullName); 
      }

      if (header.CustomerMsgRefListID != null) {
        Header.CustomerMsgRef.ListID.SetValue(header.CustomerMsgRefListID); 
      }

      // CUSTOMER:JOB Header Box
      if (header.CustomerRefFullName != null) {
        Header.CustomerRef.FullName.SetValue(header.CustomerRefFullName); 
      }

      if (header.CustomerRefListID != null) {
        Header.CustomerRef.ListID.SetValue(header.CustomerRefListID); 
      }

      // Due Date
      if (header.DueDate != null) {
        DateTime dueDate = Convert.ToDateTime(header.DueDate);
        Header.DueDate.SetValue(dueDate);
      }

      // Sales Tax Code
      if (header.CustomerSalesTaxCodeRefFullName != null) {
        Header.CustomerSalesTaxCodeRef.FullName
            .SetValue(header.CustomerSalesTaxCodeRefFullName); 
      }

      if (header.CustomerSalesTaxCodeRefListID != null) {
        Header.CustomerSalesTaxCodeRef.ListID
            .SetValue(header.CustomerSalesTaxCodeRefListID);
      }

      // P.O. NO. Header Box
      if (header.PONumber != null) {
        Header.PONumber.SetValue(header.PONumber);
      }

      // Is Tax Included
      if (header.IsTaxIncluded != null) {
        bool isTaxIncluded = Convert.ToBoolean(header.IsTaxIncluded);
        Header.IsTaxIncluded.SetValue(isTaxIncluded); 
      }

      // Email Later Checkbox
      if (header.IsToBeEmailed != null) {
        bool isToBeEmailed = Convert.ToBoolean(header.IsToBeEmailed);
        Header.IsToBeEmailed.SetValue(isToBeEmailed);
      }

      // Print Later Checkbox
      if (header.IsToBePrinted != null) {
        bool isToBePrinted = Convert.ToBoolean(header.IsToBePrinted);
        Header.IsToBePrinted.SetValue(isToBePrinted);
      }

      // TEMPLATE Header Box
      if (header.TemplateRefFullName != null) {
        Header.TemplateRef.FullName.SetValue(header.TemplateRefFullName); 
      }

      if (header.TemplateRefListID != null) {
        Header.TemplateRef.ListID.SetValue(header.TemplateRefListID); 
      }

      // TERMS Header Box
      if (header.TermsRefFullName != null) {
        Header.TermsRef.FullName.SetValue(header.TermsRefFullName); 
      }

      if (header.TermsRefListID != null) {
        Header.TermsRef.ListID.SetValue(header.TermsRefListID); 
      }



      #endregion Header


      #region LineItems


      // Create variable for adding new lines to the invoice
      IORInvoiceLineAdd LineItem = Header.ORInvoiceLineAddList.Append();

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
      IORInvoiceLineAdd SubTotal = Header.ORInvoiceLineAddList.Append();

      SubTotal.InvoiceLineAdd.ItemRef.FullName.SetValue("Sub- Totals");

      #endregion LineItems
    }
  
    static void BuildCustomerQuery(IMsgSetRequest requestMsgSet) {
      ICustomerQuery CustomerQueryRq = requestMsgSet.AppendCustomerQueryRq();
      CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.TotalBalanceFilter.Operator.SetValue(ENOperator.oGreaterThanEqual);
      CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.TotalBalanceFilter.Amount.SetValue(0);
    }
  }
}
