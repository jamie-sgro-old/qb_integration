using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect {
  public class BasicImporter {
    private const string _qbwFilePath = "C:\\Users\\Jamie\\Nextcloud\\Sangwa\\Clients\\NX - Nexim Healthcare\\01 - Invoicing\\QB Mock\\NX Mock.qbw";

    public static void Import(string desc) {
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
        BuildInvoiceAddRq(requestMsgSet, desc);

        IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        //End the session and close the connection to QuickBooks
        sessionManager.EndSession();
        sessionManager.CloseConnection();
      } catch (Exception e) {
        Console.WriteLine(e.Message);
        if (_sessionBegun) {
          sessionManager.EndSession();
        }
        if (_connectionOpen) {
          sessionManager.CloseConnection();
        }
      }
    }

    static void BuildInvoiceAddRq(IMsgSetRequest requestMsgSet, string desc) {
      // Init invoice variable
      IInvoiceAdd InvoiceAddRq = requestMsgSet.AppendInvoiceAddRq();

      // Fill CUSTOMER:JOB box by string value
      InvoiceAddRq.CustomerRef.FullName.SetValue("CLASS");

      // Create variable for adding new lines to the invoice
      IORInvoiceLineAdd ORInvoiceLineAddListElement11156 = InvoiceAddRq.ORInvoiceLineAddList.Append();
      ORInvoiceLineAddListElement11156.InvoiceLineAdd.ItemRef.FullName.SetValue("CLASS - DSW1");
      ORInvoiceLineAddListElement11156.InvoiceLineAdd.Desc.SetValue(desc);
    }
  }
}
