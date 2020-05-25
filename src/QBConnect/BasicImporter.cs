using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Classes;

namespace QBConnect {
  public class BasicImporter {

    public static void Import(string qbwFilePath, InvoiceHeaderModel header, List<InvoiceLineItemModel> lineItems) {

      // Fail fast: Template can't be null
      GetUserTemplateName(header);

      // Fail fast: LineItems need some data
      if (lineItems.Count < 1) {
        throw new ArgumentOutOfRangeException(
          paramName: nameof(lineItems),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      QBSessionManager sessionManager = null;
      bool sessionBegun = false;
      bool connectionOpen = false;

      try {
        // sessionManager is the connection between code and QB
        sessionManager = new QBSessionManager();

        // Create the message set request object to hold our request
        IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
        requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

        sessionManager.OpenConnection2("", "Sangwa Solutions QuickBooks Importer", ENConnectionType.ctLocalQBD);
        connectionOpen = true;
        sessionManager.BeginSession(qbwFilePath, ENOpenMode.omDontCare);
        sessionBegun = true;

        #region Main Process

        bool isValidTemplate = IsValidTemplate(requestMsgSet, sessionManager, GetUserTemplateName(header));
        
        if (!isValidTemplate) {
          throw new ArgumentException(paramName: nameof(header.TemplateRefFullName),
            message: "Could not find '" + GetUserTemplateName(header) + "' in QuickBooks template list");
        }

        BuildInvoiceAddRq(requestMsgSet, header, lineItems);
        // BuildCustomerQuery(requestMsgSet);
        
        #endregion Main Process

        var responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        // Temp
        // Console.WriteLine(responseMsgSet.ToXMLString());

        sessionManager.EndSession();
        sessionManager.CloseConnection();
      } catch (Exception e) {


        Console.WriteLine(e.GetType());
        Console.WriteLine(e.Message);
        throw;
      } finally {
        if (sessionBegun) {
          sessionManager.EndSession();
        }
        if (connectionOpen) {
          sessionManager.CloseConnection();
        }
      }
    }

    static void BuildInvoiceAddRq(IMsgSetRequest requestMsgSet, InvoiceHeaderModel header, List<InvoiceLineItemModel> lineItems) {
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
      foreach (InvoiceLineItemModel line in lineItems) {
        new Classes.LineItem(Header).AddLine(line);
      }


      //IORInvoiceLineAdd LineItem = Header.ORInvoiceLineAddList.Append();



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

    private static bool IsValidTemplate(IMsgSetRequest requestMsgSet, QBSessionManager sessionManager, string userTemplateName) {
      var templateQuery = new TemplateQuery(requestMsgSet, sessionManager);
      List<string> templateNamesList = templateQuery.GetList<ITemplateRetList>();
      return templateNamesList.Contains(userTemplateName);
    }

    /// <summary>
    /// Decide which of the two possible template variables should be used
    /// in future processes. Priority goes to IDs over strings if both are
    /// provided
    /// </summary>
    /// <param name="header">Data model containing both TemplateRefListID &
    /// TemplateRefFullName</param>
    /// <returns>Template ID if possible, else Template fullname, else exception</returns>
    private static string GetUserTemplateName(InvoiceHeaderModel header) {
      if (header.TemplateRefListID != null) {
        return header.TemplateRefListID;
      }
      if (header.TemplateRefFullName != null) {
        return header.TemplateRefFullName;
      }
      throw new ArgumentNullException(paramName: nameof(header.TemplateRefListID),
        message: "No QuickBooks invoice template has been specified.");
    }
  }
}
