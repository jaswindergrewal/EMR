using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AdminLabImportBAL
    {
        AdminLabImportDAL objAdminLabImportDAL = new AdminLabImportDAL();

        public List<AdminImportViewModel> GetAdminImportTestList(DateTime StartDate,DateTime EndDate)
        {
            List<AdminImportViewModel> lstObject = objAdminLabImportDAL.GetAdminImportTestList(StartDate, EndDate);
            return lstObject;
        }

        public List<AdminLabReportsTestViewModel> GetAdminLabReportList()
        {
            List<AdminLabReportsTestViewModel> lstObj = objAdminLabImportDAL.GetAdminLabReportList();
            return lstObj;
        }

        public void InsertLabReportTest(AdminLabReportsTestViewModel clsAdminLabReport)
        {
            LabReports_Tests cls = new LabReports_Tests();
            AutoMapper.Mapper.CreateMap<AdminLabReportsTestViewModel, LabReports_Tests>();
            cls = AutoMapper.Mapper.Map(clsAdminLabReport, cls);
            objAdminLabImportDAL.Create(cls);
        }

        public void InsertLabReportTest1(int GroupID, bool Hidden, DateTime LastUsed, string TestName)
        {
            AdminLabReportsTestViewModel ViewAdminLabReport = new AdminLabReportsTestViewModel();
            ViewAdminLabReport.GroupID = GroupID;
            ViewAdminLabReport.Hidden = Hidden;
            ViewAdminLabReport.LastUsed = LastUsed;
            ViewAdminLabReport.TestName = TestName;

            LabReports_Tests cls = new LabReports_Tests();
            AutoMapper.Mapper.CreateMap<AdminLabReportsTestViewModel, LabReports_Tests>();
            cls = AutoMapper.Mapper.Map(ViewAdminLabReport, cls);
            objAdminLabImportDAL.Create(cls);
        }


        public List<LabReportViewModel> GetLabReport_PanelsGroupsTestsList()
        {
            List<LabReportViewModel> lstObj = objAdminLabImportDAL.GetLabReport_PanelsGroupsTestsList();
            return lstObj;
        }

        public List<LabReportViewModel> GetGroupsTriggersByPanelID(int panelID)
        {
            List<LabReportViewModel> lstObject = objAdminLabImportDAL.GetGroupsTriggersByPanelID(panelID);
            return lstObject;
        }        

        public List<LabReportTestModel> GetTestByGroupID(int groupID)
        {
            List<LabReportTestModel> lstObject = objAdminLabImportDAL.GetTestByGroupID(groupID);
            return lstObject;
        }

        //public List<LabReportTriggerModel> GetTriggersByPanelID(int panelID)
        //{
        //    List<LabReportTriggerModel> lstObject = objAdminLabImportDAL.GetTriggersByPanelID(panelID);
        //    return lstObject;
        //}

        public int AddPanels(string PanelName, string PanelDescrip, Nullable<int> SortOrder)
        {
            int result = objAdminLabImportDAL.AddPanels(PanelName, PanelDescrip, SortOrder);
            return result;
        }

        public LabReportsPanelViewModel GetPanelDetails(int PanelID)
        {
            var _objpanelList = new LabReportsPanelViewModel();
            var PnanelEntity = new LabReports_Panels();
            PnanelEntity = objAdminLabImportDAL.Get<LabReports_Panels>(o => o.PanelID == PanelID);
            Mapper.CreateMap<LabReports_Panels, LabReportsPanelViewModel>();
            _objpanelList = Mapper.Map(PnanelEntity, _objpanelList);
            return _objpanelList;
        }
        public int AddGroups(string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            int result = objAdminLabImportDAL.AddGroups(GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);
            return result;
        }
      

        public void DeletePanel(int ID, string Name)
        {
            objAdminLabImportDAL.DeletePanel(ID, Name);
        }


        public LabReportGroupViewModel GetGroupByID(int ID)
        {
            var _objgroupList = new LabReportGroupViewModel();
            var groupEntity = new LabReports_Groups();
            groupEntity = objAdminLabImportDAL.Get<LabReports_Groups>(o => o.GroupID == ID);
            Mapper.CreateMap<LabReports_Groups, LabReportGroupViewModel>();
            _objgroupList = Mapper.Map(groupEntity, _objgroupList);
            return _objgroupList;
        }

      
        public int UpdateGroups(int ID, string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            int result = objAdminLabImportDAL.UpdateGroups(ID, GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);
            return result;
        }

        public int AddTriggers(string triggerName, string triggerDesc, int panelID)
        {
            int result = objAdminLabImportDAL.AddTriggers(triggerName, triggerDesc, panelID);
            return result;
        }

        public LabReportTriggerModel GetTriggerByID(int ID)
        {
            var _objtriggerList = new LabReportTriggerModel();
            var triggerEntity = new LabReports_Triggers();
            triggerEntity = objAdminLabImportDAL.Get<LabReports_Triggers>(o => o.TriggerID == ID);
            Mapper.CreateMap<LabReports_Triggers, LabReportTriggerModel>();
            _objtriggerList = Mapper.Map(triggerEntity, _objtriggerList);
            return _objtriggerList;
        }

        public int UpdateTriggers(int ID, string triggerName, string triggerDesc, int panelID)
        {
            int result = objAdminLabImportDAL.UpdateTriggers(ID, triggerName, triggerDesc, panelID);
            return result;
        }

        public int UpdatePanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip, int PanelID)
        {
            int result = objAdminLabImportDAL.UpdatePanels(PanelName, SortOrder, PanelDescrip, PanelID);
            return result;
        }

        public List<LabReportTestModeltest> GetTestNOTINGroups()
        {
            var _objTestList = new List<LabReportTestModeltest>();
            var TestEntity = new List<LabReports_Tests>();
            TestEntity = objAdminLabImportDAL.GetAll<LabReports_Tests>(o => o.GroupID ==0 && o.Hidden==false).ToList();
            Mapper.CreateMap<LabReports_Tests, LabReportTestModeltest>();
            _objTestList = Mapper.Map(TestEntity, _objTestList);
            return _objTestList;
        }

        public List<LabReportTestModel> InsertTestDetailsForGroup(int GroupID, string TestID, int Hide)
        {
            List<LabReportTestModel> objLst = objAdminLabImportDAL.InsertTestDetailsForGroup(GroupID, TestID, Hide);
            return objLst;
        }

        public int AddCondition(string ConditionName, string ConditionDescrip, string Sex, int TriggerID)
        {
            int result = objAdminLabImportDAL.AddCondition(ConditionName, ConditionDescrip, Sex, TriggerID);
            return result;
        }

        public LabReportConditionModel GetConditionByID(int ID)
        {
            var _objConditionList = new LabReportConditionModel();
            var ConditionEntity = new LabReports_Conditions();
            ConditionEntity = objAdminLabImportDAL.Get<LabReports_Conditions>(o => o.ConditionID == ID);
            Mapper.CreateMap<LabReports_Conditions, LabReportConditionModel>();
            _objConditionList = Mapper.Map(ConditionEntity, _objConditionList);
            return _objConditionList;
        }

        public int UpdateConditions(int ID, string conditionName, string conditionDesc, string sex, int triggerID)
        {
            int result = objAdminLabImportDAL.UpdateConditions(ID, conditionName, conditionDesc, sex, triggerID);
            return result;
        }

        public List<LabReportConditionModel> GetConditionBytriggerID(int triggerID)
        {
            List<LabReportConditionModel> objLst = objAdminLabImportDAL.GetConditionBytriggerID(triggerID);
            return objLst;
        }
    }
}
