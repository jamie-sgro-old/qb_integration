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
  public class InvoiceImporter {
    public InvoiceImporter(string qbwFilePath) {
      SessionManager = null;
      SessionBegun = false;
      ConnectionOpen = false;

      try {
        // sessionManager is the connection between code and QB
        SessionManager = new QBSessionManager();

        // Create the message set request object to hold our request
        RequestMsgSet = SessionManager.CreateMsgSetRequest("US", 13, 0);
        RequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

        SessionManager.OpenConnection2("", "Sangwa Solutions QuickBooks Importer", ENConnectionType.ctLocalQBD);
        ConnectionOpen = true;
        SessionManager.BeginSession(qbwFilePath, ENOpenMode.omDontCare);
        SessionBegun = true;
      }
      catch (Exception e) {
        Console.WriteLine(e.GetType());
        Console.WriteLine(e.Message);
        EndAndCloseConnection();
        throw;
      }
    }



    private QBSessionManager SessionManager { get; }
    private bool ConnectionOpen { get; set; }
    private bool SessionBegun { get; set; }
    private IMsgSetRequest RequestMsgSet { get; }



    public void Import(InvoiceHeaderModel headerData, List<InvoiceLineItemModel> lineItems) {

      if (!ConnectionOpen) throw new ArgumentException("Connection is not open");
      if (!SessionBegun) throw new ArgumentException("Session has not begun");

      // Fail fast: Template can't be null
      var userTemplateName = GetUserTemplateName(headerData);

      // Fail fast: LineItems need some data
      if (lineItems.Count < 1) {
        throw new ArgumentOutOfRangeException(
          paramName: nameof(lineItems),
          message: "No Invoice lineItems were supplied. " +
                   "The Importer was expecting at least 1.");
      }


      try {

        bool isValidTemplate = IsValidTemplate(userTemplateName);
        
        if (!isValidTemplate) {
          throw new ArgumentException(paramName: nameof(headerData.TemplateRefFullName),
            message: "Could not find '" + userTemplateName + "' in QuickBooks template list");
        }

        BuildRequest(RequestMsgSet, headerData, lineItems);
        
        var responseMsgSet = SessionManager.DoRequests(RequestMsgSet);

      } catch (Exception e) {
        Console.WriteLine(e.GetType());
        Console.WriteLine(e.Message);
        throw;
      } finally {
        EndAndCloseConnection();
      }
    }



    private void EndAndCloseConnection() {
      if (SessionBegun) {
        SessionManager.EndSession();
        SessionBegun = false;
      }
      if (ConnectionOpen) {
        SessionManager.CloseConnection();
        ConnectionOpen = false;
      }
    }



    private static void BuildRequest(IMsgSetRequest requestMsgSet, InvoiceHeaderModel headerData, List<InvoiceLineItemModel> lineItems) {
      // Init invoice variable
      IInvoiceAdd header = requestMsgSet.AppendInvoiceAddRq();

      HeaderItem.SetHeader(header, headerData);

      // Create variable for adding new lines to the invoice
      foreach (InvoiceLineItemModel line in lineItems) {
        new Classes.LineItem(header).AddLine(line);
      }

      // Add SubTotal line item
      IORInvoiceLineAdd subTotal = header.ORInvoiceLineAddList.Append();

      subTotal.InvoiceLineAdd.ItemRef.FullName.SetValue("Sub- Totals");
    }
  
    private bool IsValidTemplate(string userTemplateName) {
      var templateQuery = new TemplateQuery(RequestMsgSet, SessionManager);
      List<string> templateNamesList = templateQuery.GetList<ITemplateRetList>();
      return templateNamesList.Contains(userTemplateName);
    }



    /// <summary>
    /// Decide which of the two possible template variables should be used
    /// in future processes. Priority goes to IDs over strings if both are
    /// provided
    /// </summary>
    /// <param name="headerData">Data model containing both TemplateRefListID &
    /// TemplateRefFullName</param>
    /// <returns>Template ID if possible, else Template fullname, else exception</returns>
    private static string GetUserTemplateName(InvoiceHeaderModel headerData) {
      if (headerData.TemplateRefListID != null) {
        return headerData.TemplateRefListID;
      }

      if (headerData.TemplateRefFullName != null) {
        return headerData.TemplateRefFullName;
      }

      throw new ArgumentNullException(paramName: nameof(headerData.TemplateRefListID),
        message: "No QuickBooks invoice template has been specified.");
    }
  }
}
