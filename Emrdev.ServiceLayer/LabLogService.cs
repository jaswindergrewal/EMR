using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LabLogService" in both code and config file together.
    public class LabLogService : ILabLogService
    {
        LabLogBAL objBAL = new LabLogBAL();
        public List<LabLogViewModel> GetLabLogList()
        {

            return objBAL.GetLabLogList();
        }

        public List<LabRequestPanelViewModel> GetLabPanelList()
        {
            return objBAL.GetLabPanelList();
        }

        public List<LabRequestTestViewModel> GetLabTestList()
        {
            return objBAL.GetLabTestList();
        }

        public void InsertUpdateLabRequests(string gridName, int RequestID, bool Active, string RequestLabName)
        {
            objBAL.InsertUpdateLabRequests(gridName, RequestID, Active, RequestLabName);
        }


        public bool CheckDuplicatePanel(int PanelID, string PanelName)
        {
            return objBAL.CheckDuplicatePanel(PanelID, PanelName);
        }


        public bool CheckDuplicateTest(int testID, string testName)
        {
            return objBAL.CheckDuplicateTest(testID, testName);
        }


        public void DeleteLabRequestPanels(int Id)
        {
            objBAL.DeleteLabRequestPanels(Id);
        }

        public void DeleteLabRequestTests(int Id)
        {
            objBAL.DeleteLabRequestTests(Id);
        }
    }
}
