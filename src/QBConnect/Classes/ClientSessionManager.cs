using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBConnect.Classes.Interfaces;
using QBFC13Lib;

namespace QBConnect.Classes {
  internal class ClientSessionManager : IClientSessionManager {
    public ClientSessionManager() {
      _qbSessionManagerImplementation = new QBSessionManager();
    }

    private QBSessionManager _qbSessionManagerImplementation;
    public bool ConnectionOpen { get; set; } = false;
    public bool SessionBegun { get; set; } = false;

    public void OpenConnection(string AppID, string AppName) {
      _qbSessionManagerImplementation.OpenConnection(AppID, AppName);
      ConnectionOpen = true;
    }

    public void OpenConnection2(string AppID, string AppName, ENConnectionType connType) {
      _qbSessionManagerImplementation.OpenConnection2(AppID, AppName, connType);
      ConnectionOpen = true;
    }

    public void BeginSession(string qbFile, ENOpenMode openMode) {
      _qbSessionManagerImplementation.BeginSession(qbFile, openMode);
      SessionBegun = true;
    }

    public void EndSession() {
      _qbSessionManagerImplementation.EndSession();
      SessionBegun = false;
    }

    public void CloseConnection() {
      _qbSessionManagerImplementation.CloseConnection();
      ConnectionOpen = false;
    }

    public string GetCurrentCompanyFileName() {
      return _qbSessionManagerImplementation.GetCurrentCompanyFileName();
    }

    public IMsgSetRequest CreateMsgSetRequest(string Country, short qbXMLMajorVersion, short qbXMLMinorVersion) {
      return _qbSessionManagerImplementation.CreateMsgSetRequest(Country, qbXMLMajorVersion, qbXMLMinorVersion);
    }

    public IMsgSetResponse ToMsgSetResponse(string qbXMLResponse, string Country, short qbXMLMajorVersion,
      short qbXMLMinorVersion) {
      return _qbSessionManagerImplementation.ToMsgSetResponse(qbXMLResponse, Country, qbXMLMajorVersion, qbXMLMinorVersion);
    }

    public IMsgSetResponse DoRequests(IMsgSetRequest request) {
      return _qbSessionManagerImplementation.DoRequests(request);
    }

    public void GetVersion(out short MajorVersion, out short MinorVersion, out ENReleaseLevel releaseLevel,
      out short releaseNumber) {
      _qbSessionManagerImplementation.GetVersion(out MajorVersion, out MinorVersion, out releaseLevel, out releaseNumber);
    }

    public IMsgSetResponse DoRequestsFromXMLString(string qbXMLRequest) {
      return _qbSessionManagerImplementation.DoRequestsFromXMLString(qbXMLRequest);
    }

    public ISubscriptionMsgSetRequest CreateSubscriptionMsgSetRequest(short qbXMLMajorVersion, short qbXMLMinorVersion) {
      return _qbSessionManagerImplementation.CreateSubscriptionMsgSetRequest(qbXMLMajorVersion, qbXMLMinorVersion);
    }

    public ISubscriptionMsgSetResponse ToSubscriptionMsgSetResponse(string qbXMLSubscriptionResponse, short qbXMLMajorVersion,
      short qbXMLMinorVersion) {
      return _qbSessionManagerImplementation.ToSubscriptionMsgSetResponse(qbXMLSubscriptionResponse, qbXMLMajorVersion, qbXMLMinorVersion);
    }

    public ISubscriptionMsgSetResponse DoSubscriptionRequests(ISubscriptionMsgSetRequest request) {
      return _qbSessionManagerImplementation.DoSubscriptionRequests(request);
    }

    public ISubscriptionMsgSetResponse DoSubscriptionRequestsFromXMLString(string qbXMLSubscriptionRequest) {
      return _qbSessionManagerImplementation.DoSubscriptionRequestsFromXMLString(qbXMLSubscriptionRequest);
    }

    public IEventsMsgSet ToEventsMsgSet(string qbXMLEventsResponse, short qbXMLMajorVersion, short qbXMLMinorVersion) {
      return _qbSessionManagerImplementation.ToEventsMsgSet(qbXMLEventsResponse, qbXMLMajorVersion, qbXMLMinorVersion);
    }

    public IMsgSetRequest ToMsgSetRequest(string qbXMLRequest) {
      return _qbSessionManagerImplementation.ToMsgSetRequest(qbXMLRequest);
    }

    public bool IsErrorRecoveryInfo() {
      return _qbSessionManagerImplementation.IsErrorRecoveryInfo();
    }

    public void ClearErrorRecovery() {
      _qbSessionManagerImplementation.ClearErrorRecovery();
    }

    public IMsgSetResponse GetErrorRecoveryStatus() {
      return _qbSessionManagerImplementation.GetErrorRecoveryStatus();
    }

    public IMsgSetRequest GetSavedMsgSetRequest() {
      return _qbSessionManagerImplementation.GetSavedMsgSetRequest();
    }

    public void CommunicateOutOfProcess(bool useOutOfProc) {
      _qbSessionManagerImplementation.CommunicateOutOfProcess(useOutOfProc);
    }

    public void CommunicateOutOfProcessEx(bool useOutOfProc, string outOfProcCLSID) {
      _qbSessionManagerImplementation.CommunicateOutOfProcessEx(useOutOfProc, outOfProcCLSID);
    }

    public Array QBXMLVersionsForSession => _qbSessionManagerImplementation.QBXMLVersionsForSession;

    public bool EnableErrorRecovery {
      get => _qbSessionManagerImplementation.EnableErrorRecovery;
      set => _qbSessionManagerImplementation.EnableErrorRecovery = value;
    }

    public bool SaveAllMsgSetRequestInfo {
      get => _qbSessionManagerImplementation.SaveAllMsgSetRequestInfo;
      set => _qbSessionManagerImplementation.SaveAllMsgSetRequestInfo = value;
    }

    public IQBGUIDType ErrorRecoveryID => _qbSessionManagerImplementation.ErrorRecoveryID;

    public ENConnectionType ConnectionType => _qbSessionManagerImplementation.ConnectionType;

    public IQBAuthPreferences QBAuthPreferences => _qbSessionManagerImplementation.QBAuthPreferences;

    public Array QBXMLVersionsForSubscription => _qbSessionManagerImplementation.QBXMLVersionsForSubscription;
  }
}
