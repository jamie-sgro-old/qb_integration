using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Models;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal static class HeaderItem {
    internal static void SetHeader(IInvoiceAdd Header, InvoiceHeaderModel headerData) {
      // Accounts Receivable Ref
      if (headerData.ARAccountRefFullName != null) {
        Header.ARAccountRef.FullName.SetValue(headerData.ARAccountRefFullName);
      }

      if (headerData.ARAccountRefListID != null) {
        Header.ARAccountRef.ListID.SetValue(headerData.ARAccountRefListID);
      }

      // CLASS Header Box
      if (headerData.ClassRefFullName != null) {
        Header.ClassRef.FullName.SetValue(headerData.ClassRefFullName);
      }

      if (headerData.ClassRefListID != null) {
        Header.ClassRef.ListID.SetValue(headerData.ClassRefListID);
      }

      // CUSTOMER MESSAGE Footer Box
      if (headerData.CustomerMsgRefFullName != null) {
        Header.CustomerMsgRef.FullName.SetValue(headerData.CustomerMsgRefFullName);
      }

      if (headerData.CustomerMsgRefListID != null) {
        Header.CustomerMsgRef.ListID.SetValue(headerData.CustomerMsgRefListID);
      }

      // CUSTOMER:JOB Header Box
      if (headerData.CustomerRefFullName != null) {
        Header.CustomerRef.FullName.SetValue(headerData.CustomerRefFullName);
      }

      if (headerData.CustomerRefListID != null) {
        Header.CustomerRef.ListID.SetValue(headerData.CustomerRefListID);
      }

      // Transaction Date
      if (headerData.TxnDate != null) {
        Header.TxnDate.SetValue((DateTime)headerData.TxnDate);
      }

      // Due Date
      if (headerData.DueDate != null) {
        Header.DueDate.SetValue((DateTime)headerData.DueDate);
      }

      // Sales Tax Code
      if (headerData.CustomerSalesTaxCodeRefFullName != null) {
        Header.CustomerSalesTaxCodeRef.FullName
            .SetValue(headerData.CustomerSalesTaxCodeRefFullName);
      }

      if (headerData.CustomerSalesTaxCodeRefListID != null) {
        Header.CustomerSalesTaxCodeRef.ListID
            .SetValue(headerData.CustomerSalesTaxCodeRefListID);
      }

      // P.O. NO. Header Box
      if (headerData.PONumber != null) {
        Header.PONumber.SetValue(headerData.PONumber);
      }

      // Is Tax Included
      if (headerData.IsTaxIncluded != null) {
        Header.IsTaxIncluded.SetValue((bool)headerData.IsTaxIncluded);
      }

      // Email Later Checkbox
      if (headerData.IsToBeEmailed != null) {
        Header.IsToBeEmailed.SetValue((bool)headerData.IsToBeEmailed);
      }

      // Print Later Checkbox
      if (headerData.IsToBePrinted != null) {
        Header.IsToBePrinted.SetValue((bool)headerData.IsToBePrinted);
      }

      // TEMPLATE Header Box
      if (headerData.TemplateRefFullName != null) {
        Header.TemplateRef.FullName.SetValue(headerData.TemplateRefFullName);
      }

      if (headerData.TemplateRefListID != null) {
        Header.TemplateRef.ListID.SetValue(headerData.TemplateRefListID);
      }

      // TERMS Header Box
      if (headerData.TermsRefFullName != null) {
        Header.TermsRef.FullName.SetValue(headerData.TermsRefFullName);
      }

      if (headerData.TermsRefListID != null) {
        Header.TermsRef.ListID.SetValue(headerData.TermsRefListID);
      }

      // Header Other
      if (headerData.Other != null) {
        Header.Other.SetValue(headerData.Other);
      }
    }
  }
}
