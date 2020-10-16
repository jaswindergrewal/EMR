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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminLabImport" in both code and config file together.
    public class AdminLabImport : IAdminLabImport
    {
        AdminLabImportBAL objAdminLabImportBAL = new AdminLabImportBAL();
        public void DoWork()
        {
        }

        public List<AdminImportViewModel> GetAdminImportTestList(DateTime StartDate, DateTime EndDate)
        {
            List<AdminImportViewModel> objLst = objAdminLabImportBAL.GetAdminImportTestList(StartDate, EndDate);
            return objLst;
        }


        public List<AdminLabReportsTestViewModel> GetAdminLabReportList()
        {
            List<AdminLabReportsTestViewModel> objLst = objAdminLabImportBAL.GetAdminLabReportList();
            return objLst;
        }


        public void InsertLabReportTest(AdminLabReportsTestViewModel clsAdminLabReport)
        {
            objAdminLabImportBAL.InsertLabReportTest(clsAdminLabReport);
        }


        public void InsertLabReportTest1(int GroupID, bool Hidden, DateTime LastUsed, string TestName)
        {
            objAdminLabImportBAL.InsertLabReportTest1(GroupID, Hidden, LastUsed, TestName);
        }

        public List<LabReportViewModel> GetLabReport_PanelsGroupsTestsList()
        {
            List<LabReportViewModel> objLst = objAdminLabImportBAL.GetLabReport_PanelsGroupsTestsList();
            return objLst;
        }

        public List<LabReportViewModel> GetGroupsTriggersByPanelID(int panelID)
        {
            List<LabReportViewModel> objLst = objAdminLabImportBAL.GetGroupsTriggersByPanelID(panelID);
            return objLst;
        }


        public List<LabReportTestModel> GetTestByGroupID(int groupID)
        {
            List<LabReportTestModel> objLst = objAdminLabImportBAL.GetTestByGroupID(groupID);
            return objLst;
        }

        //public List<LabReportTriggerModel> GetTriggersByPanelID(int panelID)
        //{
        //    List<LabReportTriggerModel> objLst = objAdminLabImportBAL.GetTriggersByPanelID(panelID);
        //    return objLst;
        //}





        public int AddPanels(string PanelName, string PanelDescrip, Nullable<int> SortOrder)
        {
            int result = objAdminLabImportBAL.AddPanels(PanelName, PanelDescrip, SortOrder);
            return result;
        }


        public LabReportsPanelViewModel GetPanelDetails(int PanelID)
        {
            LabReportsPanelViewModel objPanelModel = objAdminLabImportBAL.GetPanelDetails(PanelID);
            return objPanelModel;
        }

        public int AddGroups(string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            int result = objAdminLabImportBAL.AddGroups(GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);
            return result;
        }

  
        public void DeletePanel(int ID, string Name)
        {
            objAdminLabImportBAL.DeletePanel(ID, Name);
        }
        public LabReportGroupViewModel GetGroupByID(int ID)
        {
            LabReportGroupViewModel objGroupModel = objAdminLabImportBAL.GetGroupByID(ID);
            return objGroupModel;
        }



        public int UpdateGroups(int ID, string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            int result = objAdminLabImportBAL.UpdateGroups(ID, GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);
            return result;
        }

        public int AddTriggers(string triggerName, string triggerDesc, int panelID)
        {
            int result = objAdminLabImportBAL.AddTriggers(triggerName, triggerDesc, panelID);
            return result;
        }

        public LabReportTriggerModel GetTriggerByID(int ID)
        {
            LabReportTriggerModel objTriggerModel = objAdminLabImportBAL.GetTriggerByID(ID);
            return objTriggerModel;
        }

        public int UpdateTriggers(int ID, string triggerName, string triggerDesc, int panelID)
        {
            int result = objAdminLabImportBAL.UpdateTriggers(ID, triggerName, triggerDesc, panelID);
            return result;
        }

        public int UpdatePanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip, int PanelID)
        {
            int result = objAdminLabImportBAL.UpdatePanels(PanelName, SortOrder, PanelDescrip, PanelID);
            return result;
        }

        public List<LabReportTestModeltest> GetTestNOTINGroups()
        {
            List<LabReportTestModeltest> objLst = objAdminLabImportBAL.GetTestNOTINGroups();
            return objLst;
        }

        public List<LabReportTestModel> InsertTestDetailsForGroup(int GroupID, string TestID, int Hide)
        {
            List<LabReportTestModel> objLst = objAdminLabImportBAL.InsertTestDetailsForGroup(GroupID, TestID, Hide);
            return objLst;
        }

        public int AddCondition(string ConditionName, string ConditionDescrip, string Sex, int TriggerID)
        {
            int result = objAdminLabImportBAL.AddCondition(ConditionName, ConditionDescrip, Sex, TriggerID);
            return result;
        }

        public LabReportConditionModel GetConditionByID(int ID)
        {
            LabReportConditionModel objConditionModel = objAdminLabImportBAL.GetConditionByID(ID);
            return objConditionModel;
        }

        public int UpdateConditions(int ID, string conditionName, string conditionDesc, string sex, int triggerID)
        {
            int result = objAdminLabImportBAL.UpdateConditions(ID, conditionName, conditionDesc, sex, triggerID);
            return result;
        }

        public List<LabReportConditionModel> GetConditionBytriggerID(int triggerID)
        {
            List<LabReportConditionModel> objLst = objAdminLabImportBAL.GetConditionBytriggerID(triggerID);
            return objLst;
        }
    }
}
