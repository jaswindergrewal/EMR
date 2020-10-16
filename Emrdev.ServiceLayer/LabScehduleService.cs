using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LabScehduleService" in both code and config file together.
    public class LabScehduleService : ILabScehduleService
    {
        LabScehduleGroupsBAL objLabScehduleGroupsBAL = new LabScehduleGroupsBAL();
        public void DoWork()
        {
        }

        public List<LabScehduleGroupsViewModel> GetLabScehduleGroupsDetails()
        {
            List<LabScehduleGroupsViewModel> lstObj = objLabScehduleGroupsBAL.GetLabScehduleGroupsDetails();
            return lstObj;
        }

        public List<LabScheduleTestsViewModel> GetLabScehduleTestDetails()
        {
            List<LabScheduleTestsViewModel> lstObj = objLabScehduleGroupsBAL.GetLabScehduleTestDetails();
            return lstObj;
        }


        public LabScehduleGroupsViewModel GetLabScehduleGroupListByGroupId(int groupId)
        {
            LabScehduleGroupsViewModel clsObj = objLabScehduleGroupsBAL.GetLabScehduleGroupListByGroupId(groupId);
            return clsObj;
        }

        public LabScheduleTestsViewModel GetLabScehduleTestListByTestId(int testId)
        {
            LabScheduleTestsViewModel clsObj = objLabScehduleGroupsBAL.GetLabScehduleTestListByTestId(testId);
            return clsObj;
        }

        public void InsertLabScehduleGroups(LabScehduleGroupsViewModel viewModelQB)
        {
            objLabScehduleGroupsBAL.InsertLabScehduleGroups(viewModelQB);
        }

        public void UpdateLabScehduleGroups(LabScehduleGroupsViewModel viewModelQB)
        {
            objLabScehduleGroupsBAL.UpdateLabScehduleGroups(viewModelQB);
        }

        public void InsertLabScehduleTest(LabScheduleTestsViewModel viewModelQB)
        {
            objLabScehduleGroupsBAL.InsertLabScehduleTest(viewModelQB);
        }

        public void UpdateLabScehduleTest(LabScheduleTestsViewModel viewModelQB)
        {
            objLabScehduleGroupsBAL.UpdateLabScehduleTest(viewModelQB);
        }


        public bool CheckDuplicateRecords(int ID, string Name, string tableName)
        {
            return objLabScehduleGroupsBAL.CheckDuplicateRecords(ID, Name, tableName);
        }


        public void DeleteLabScehduleGroups(int Id)
        {
            objLabScehduleGroupsBAL.DeleteLabScehduleGroups(Id);
        }

        public void DeleteLabScheduleTests(int Id)
        {
            objLabScehduleGroupsBAL.DeleteLabScheduleTests(Id);
        }
    }
}
