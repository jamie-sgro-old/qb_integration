using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using QBConnect.Classes;
using QBConnect.Classes.Query;

namespace QBConnect {
  public class InvoiceImporter : IInvoiceImporter {
    public InvoiceImporter(string qbwFilePath) {
      // sessionManager is the connection between code and QB
      SessionManager = new ClientSessionManager();
      
      SessionManager.OpenConnection2("", "Sangwa Solutions QuickBooks Importer", ENConnectionType.ctLocalQBD);
      SessionManager.BeginSession(qbwFilePath, ENOpenMode.omDontCare);
    }



    private IClientSessionManager SessionManager { get; }

    // After a req is processed with DoRequest(), append it's TxnId to this list
    // Necessary for rollbacks
    private List<string> _finishedTxnIds { get; set; } = new List<string>();


    public void Import(IInvoiceHeaderModel headerData, List<IInvoiceLineItemModel> lineItems) {

      #region Fail Fast

      if (!SessionManager.ConnectionOpen) throw new ArgumentException("Could not Import. Connection is not open");
      if (!SessionManager.SessionBegun) throw new ArgumentException("Could not Import. Session has not begun");

      // LineItems need some data
      if (lineItems.Count < 1) {
        throw new ArgumentOutOfRangeException(
          paramName: nameof(lineItems),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }

      // Template can't be null
      var userTemplateName = GetUserTemplateName(headerData);

      // Template string can't be missing from QB template list
      bool isValidTemplate = GetTemplateNamesList().Contains(userTemplateName);
      if (!isValidTemplate) {
        throw new ArgumentException(paramName: nameof(headerData.TemplateRefFullName),
          message: "Could not find '" + userTemplateName + "' in QuickBooks template list");
      }

      #endregion Fail Fast

      // Create the message set request object to hold our request
      IMsgSetRequest msgSetRequest = SessionManager.CreateMsgSetRequest("US", 13, 0);
      msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;


      BuildRequest(msgSetRequest, headerData, lineItems);

      // CRITICAL: Only do one BuildRequest() and DoRequests() per method-call since 
      // it's vital we cast and retrieve the right TxnId in case of a roll-back
      if (msgSetRequest.RequestList.Count > 1) {
        throw new ArgumentOutOfRangeException(
          paramName: nameof(msgSetRequest.RequestList),
          message: msgSetRequest.RequestList.Count + " elements found is responseList. " +
                   "The Response Model was expecting 1.");
      }

      // Needs to be a req type where the response can later upcast to acquire the TxnID
      // i.e. an IInvoiceAdd request converts into an IInvoiceRet response
      var reqType = msgSetRequest.RequestList.GetAt(0).Detail;
      if (!(reqType is IInvoiceAdd)) {
        throw new ArgumentException(
          message: "Message request is not of type 'IInvoiceAdd'",
          paramName: nameof(reqType));
      }


      // Do Request
      var responseMsgSet = SessionManager.DoRequests(msgSetRequest);
      

      // Check all response statuses for potential error (should only have count == 1)
      for (int i = 0; i < responseMsgSet.ResponseList.Count; i++) {
        if (responseMsgSet.ResponseList.GetAt(i).StatusMessage != "Status OK") {
          Rollback();
          throw new Exception(responseMsgSet.ResponseList.GetAt(i).StatusMessage);
        }
      }

      // Get TxnId (asserting only 1 req)
      var singleResponse = new Response(responseMsgSet?.ResponseList);
      if (!singleResponse.IsValid(ENResponseType.rtInvoiceAddRs)) {
        throw new ArgumentException(
          message: "Message response is not of type 'rtInvoiceAddRs'. " +
                   "Invoices may have been produced that could not be rolled back. " +
                   "Please ensure all recently created invoices in QuickBooks " +
                   "are correct. If this problem persists, notify your system administrator.",
          paramName: nameof(singleResponse));
      }

      // Cast is type-safe from IsValid check above
      IInvoiceRet invoiceRet = (IInvoiceRet)singleResponse.Payload.Detail;
      _finishedTxnIds.Add(invoiceRet.TxnID.GetValue());
    }



    private void BuildRequest(IMsgSetRequest requestMsgSet, IInvoiceHeaderModel headerData, List<IInvoiceLineItemModel> lineItems) {
      // Init invoice variable
      IInvoiceAdd header = requestMsgSet.AppendInvoiceAddRq();

      HeaderItem.SetHeader(header, headerData);

      // Create variable for adding new lines to the invoice
      foreach (IInvoiceLineItemModel line in lineItems) {
        new LineItem(header).AddLine(line);
      }
    }


    public void Rollback() {
      if (!SessionManager.ConnectionOpen) throw new ArgumentException("Could not Rollback. Connection is not open");
      if (!SessionManager.SessionBegun) throw new ArgumentException("Could not Rollback. Session has not begun");

      var rollback = Factory.CreateRollback(SessionManager);
      var res = rollback.Invoice(_finishedTxnIds);

      // clear TxnId list if they've all been processed
      if (res.Item1) {
        _finishedTxnIds = new List<string>();
      } else {
        throw new Exception(res.Item2);
      }
    }



    /// <summary>
    /// Check if template name provided is included in the list of QB template names
    /// </summary>
    /// <param name="userTemplateName">The name of the template to be used</param>
    /// <returns></returns>
    public List<string> GetTemplateNamesList() {
      var templateQuery = new TemplateQuery(SessionManager);
      List<string> templateNamesList = templateQuery.GetList<ITemplateRetList>();
      return templateNamesList;
    }

    public List<string> GetItemNamesList() {
      var itemQuery = new ItemQuery(SessionManager);
      List<string> itemNamesList = itemQuery.GetList<IORItemRetList>();
      return itemNamesList;
    }

    public List<string> GetInvoiceIdList() {
      var invoiceQuery = new InvoiceQuery(SessionManager);
      List<string> templateNamesList = invoiceQuery.GetList<IInvoiceRetList>();
      return templateNamesList;
    }

    public List<string> GetTermsNamesList() {
      var termsQuery = new TermsQuery(SessionManager);
      List<string> termsNamesList = termsQuery.GetList<IORTermsRetList>();
      return termsNamesList;
    }

    public List<string> GetCustomerNamesList() {
      var customerQuery = new CustomerQuery(SessionManager);
      List<string> customerNamesList = customerQuery.GetList<ICustomerRetList>();
      return customerNamesList;
    }



    /// <summary>
    /// Decide which of the two possible template variables should be used
    /// in future processes. Priority goes to IDs over strings if both are
    /// provided
    /// </summary>
    /// <param name="headerData">Data model containing both TemplateRefListID &
    /// TemplateRefFullName</param>
    /// <returns>Template ID if possible, else Template fullname, else exception</returns>
    private static string GetUserTemplateName(IInvoiceHeaderModel headerData) {
      if (headerData.TemplateRefListID != null) {
        return headerData.TemplateRefListID;
      }

      if (headerData.TemplateRefFullName != null) {
        return headerData.TemplateRefFullName;
      }

      throw new ArgumentNullException(paramName: nameof(headerData.TemplateRefListID),
        message: "No QuickBooks invoice template has been specified.");
    }

    public void Dispose(bool canAlsoFreeManagedObjects) {
      // Free all unmanaged resources here:
      // pass

      if (!canAlsoFreeManagedObjects) return;
      if (SessionManager.SessionBegun) {
        SessionManager.EndSession();
      }
      if (SessionManager.ConnectionOpen) {
        SessionManager.CloseConnection();
      }
    }

    public void Dispose() {
      Dispose(true);
    }

    // C# syntax for C++ override of 'Finalize' in IDisposable
    ~InvoiceImporter() {
      Dispose(false);
    }
  }
}
