using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect {
  public class BasicImporter {

    public static void Import(string _qbwFilePath, InvoiceHeaderModel header, InvoiceLineItemModel lineItem) {

      // null TEMPLATE throws error
      bool isNullTemplate = header.TemplateRefFullName == null && header.TemplateRefListID == null;

      if (isNullTemplate) {
        string paramName = nameof(header.TemplateRefFullName);

        // Default to ID if both templates are null
        if (header.TemplateRefListID == null) {
          paramName = nameof(header.TemplateRefListID);
        }

        throw new ArgumentNullException(paramName: paramName,
          message: "No QuickBooks invoice template has been specified.");
      }

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

        #region Main Process

        bool isValidTemplate = IsValidTemplate(requestMsgSet, sessionManager, GetUserTemplateName(header));
        
        if (!isValidTemplate) {
          throw new ArgumentException(paramName: nameof(header.TemplateRefFullName),
            message: "Could not find '" + GetUserTemplateName(header) + "' in QuickBooks template list");
        }

        BuildInvoiceAddRq(requestMsgSet, header, lineItem);
        // BuildCustomerQuery(requestMsgSet);
        
        #endregion Main Process

        IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        // Temp
        // Console.WriteLine(responseMsgSet.ToXMLString());

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
      IInvoiceLineAppender newLine = new Classes.LineItem(Header);
      newLine.AddLine(lineItem);

      new Classes.LineItem(Header).AddLine(lineItem);

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
      List<string> templateNamesList = GetTemplateList(requestMsgSet, sessionManager);
      return templateNamesList.Contains(userTemplateName);
    }

    private static string GetUserTemplateName(InvoiceHeaderModel header) {
      if (header.TemplateRefListID != null) {
        return header.TemplateRefListID;
      }
      if (header.TemplateRefFullName != null) {
        return header.TemplateRefFullName;
      }
      throw new ArgumentNullException(paramName: nameof(header.TemplateRefFullName),
        message: "No QuickBooks invoice template has been specified.");
    }

    private static List<string> GetTemplateList(IMsgSetRequest requestMsgSet, QBSessionManager sessionManager) {
      
      // Generate request for a template query
      // to be executed when .DoRequests() is run
      ITemplateQuery templateQueryRq = requestMsgSet.AppendTemplateQueryRq();
      templateQueryRq.metaData.SetValue(ENmetaData.mdMetaDataAndResponseData);

      IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);
      if (responseMsgSet == null) new List<string>();

      IResponseList responseList = responseMsgSet.ResponseList;
      if (responseList == null) new List<string>();

      // get data from first response from AppendTemplateQueryRq request
      IResponse response = responseList.GetAt(0);

      #region Verify

      //check the status code of the response, 0=ok, >0 is warning
      if (response.StatusCode < 0) new List<string>();

      //the request-specific response is in the details, make sure we have some
      if (response.Detail == null) new List<string>();

      //make sure the response is the type we're expecting
      ENResponseType responseType = (ENResponseType)response.Type.GetValue();
      if (responseType != ENResponseType.rtTemplateQueryRs) new List<string>();
      
      #endregion

      //upcast to more specific type here, this is safe because we checked with response. Type check above
      ITemplateRetList templateRetList = (ITemplateRetList)response.Detail;


      return GetTemplateNames(templateRetList);
    }

    /// <summary>
    /// From a template return list, compile and return a list of all template names
    /// </summary>
    /// <param name="templateRetList">A template return list</param>
    /// <returns>Empty list of strings if null, else a list of template names</returns>
    private static List<string> GetTemplateNames(ITemplateRetList templateRetList) {
      if (templateRetList == null) return new List<string>();

      List<string> names = new List<string>();

      for (int i = 0; i < templateRetList.Count; i++) {
        ITemplateRet template = templateRetList.GetAt(i);
        string name = (string)template.Name.GetValue();

        names.Add(name);
      }

      return names;
    }
  }
}
