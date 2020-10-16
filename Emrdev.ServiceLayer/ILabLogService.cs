using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILabLogService" in both code and config file together.
    [ServiceContract]
    public interface ILabLogService
    {
        [OperationContract]
        List<LabLogViewModel> GetLabLogList();

        [OperationContract]
        List<LabRequestPanelViewModel> GetLabPanelList();

        [OperationContract]
        List<LabRequestTestViewModel> GetLabTestList();

        [OperationContract]
        void InsertUpdateLabRequests(string gridName,int RequestID, bool Active,string RequestLabName);

        [OperationContract]
        bool CheckDuplicatePanel(int PanelID, string PanelName);

        [OperationContract]
        bool CheckDuplicateTest(int testID, string testName);

        [OperationContract]
        void DeleteLabRequestPanels(int Id);

        [OperationContract]
        void DeleteLabRequestTests(int Id);
    }
}
