using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using QBConnect.Classes;

namespace QBConnect {
  public class InvoiceImporter : IDisposable {
    public InvoiceImporter(string qbwFilePath) {
      // sessionManager is the connection between code and QB
      SessionManager = new ClientSessionManager();
      
      SessionManager.OpenConnection2("", "Sangwa Solutions QuickBooks Importer", ENConnectionType.ctLocalQBD);
      SessionManager.BeginSession(qbwFilePath, ENOpenMode.omDontCare);
    }



    private IClientSessionManager SessionManager { get; }



    public void Import(IInvoiceHeaderModel headerData, List<IInvoiceLineItemModel> lineItems) {

      #region Fail Fast

      if (!SessionManager.ConnectionOpen) throw new ArgumentException("Connection is not open");
      if (!SessionManager.SessionBegun) throw new ArgumentException("Session has not begun");

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
      
      var responseMsgSet = SessionManager.DoRequests(msgSetRequest);
      if (responseMsgSet.ResponseList.GetAt(0).StatusMessage != "Status OK") {
        throw new Exception(responseMsgSet.ResponseList.GetAt(0).StatusMessage);
      }
    }



    private static void BuildRequest(IMsgSetRequest requestMsgSet, IInvoiceHeaderModel headerData, List<IInvoiceLineItemModel> lineItems) {
      // Init invoice variable
      IInvoiceAdd header = requestMsgSet.AppendInvoiceAddRq();

      HeaderItem.SetHeader(header, headerData);

      // Create variable for adding new lines to the invoice
      foreach (IInvoiceLineItemModel line in lineItems) {
        new LineItem(header).AddLine(line);
      }

      // Add SubTotal line item
      IORInvoiceLineAdd subTotal = header.ORInvoiceLineAddList.Append();

      subTotal.InvoiceLineAdd.ItemRef.FullName.SetValue("Sub- Totals");
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
