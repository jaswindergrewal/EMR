using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminLabImportDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<AdminImportViewModel> GetAdminImportTestList(DateTime StartDate,DateTime EndDate)
        {
            var objResult = ObjectEntity1.ssp_AdminImportTestList(StartDate, EndDate).ToList();
            var objIList = new List<AdminImportViewModel>();
            Mapper.CreateMap<ssp_AdminImportTestList_Result, AdminImportViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AdminLabReportsTestViewModel> GetAdminLabReportList()
        {
            var objResult = ObjectEntity1.ssp_GetAdminLabReport().ToList();
            var objIList = new List<AdminLabReportsTestViewModel>();
            Mapper.CreateMap<ssp_GetAdminLabReport_Result, AdminLabReportsTestViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<LabReportViewModel> GetLabReport_PanelsGroupsTestsList()
        {
            var objResult = ObjectEntity1.ssp_GetLabReport_PanelsGroupsTests().ToList();
            var objIList = new List<LabReportViewModel>();
            Mapper.CreateMap<ssp_GetLabReport_PanelsGroupsTests_Result, LabReportViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<LabReportViewModel> GetGroupsTriggersByPanelID(int panelID)
        {
            var objResult = ObjectEntity1.ssp_GetGroupsTriggerByPanelID(panelID).ToList();
            var objIList = new List<LabReportViewModel>();
            Mapper.CreateMap<ssp_GetGroupsTriggerByPanelID_Result, LabReportViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        public List<LabReportTestModel> GetTestByGroupID(int groupID)
        {
            var objResult = ObjectEntity1.ssp_GetTestByGroupID(groupID).ToList();
            var objIList = new List<LabReportTestModel>();
            Mapper.CreateMap<ssp_GetTestByGroupID_Result, LabReportTestModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        //public List<LabReportTriggerModel> GetTriggersByPanelID(int panelID)
        //{
        //    var objResult = ObjectEntity1.ssp_GetTriggersByPanelID(panelID).ToList();
        //    var objIList = new List<LabReportTriggerModel>();
        //    Mapper.CreateMap<ssp_GetTriggersByPanelID_Result, LabReportTriggerModel>();
        //    objIList = Mapper.Map(objResult, objIList);
        //    return objIList;
        //}

        public int AddPanels(string panelname, string paneldescrip, Nullable<int> SortOrder)
        {
            ObjectParameter resultout = new ObjectParameter("resultout", typeof(global::System.Int32));
            resultout.Value = DBNull.Value;
            //code review remove int result
            ObjectEntity1.ssp_InsertLabReportPanels(panelname, paneldescrip,SortOrder, resultout);
            return Convert.ToInt16(resultout.Value);
        }


        public int AddGroups(string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
            ObjectEntityPart2.ssp_InsertLabReportGroups(GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }


        public void DeleteGroups(int ID, string Name)
        {
            ObjectEntity1.ssp_DeleteLabManageData(ID, Name);
        }


        public void DeletePanel(int ID, string Name)
        {
            ObjectEntity1.ssp_DeleteLabManageData(ID, Name);
        }

      
        public int UpdatePanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip, int PanelID)
        {
            ObjectParameter resultout = new ObjectParameter("resultout", typeof(global::System.Int32));
            resultout.Value = DBNull.Value;
            //code review remove int result
            ObjectEntity1.ssp_UpdatePanel(PanelName, PanelDescrip,SortOrder,PanelID, resultout);
            return Convert.ToInt16(resultout.Value);
           
        }

        public int UpdateGroups(int ID, string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
            ObjectEntityPart2.ssp_UpdateGroups(ID, GroupName, GroupTitle, SortOrder, ShowGraph, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }

        public int AddTriggers(string triggerName, string triggerDesc, int panelID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
           ObjectEntity1.ssp_InsertLabReportTrigger(triggerName, triggerDesc, panelID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }

        public int UpdateTriggers(int ID, string triggerName, string triggerDesc, int panelID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
           ObjectEntity1.ssp_UpdateTriggers(ID, triggerName, triggerDesc, panelID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }

        public List<LabReportTestModel> InsertTestDetailsForGroup(int GroupID, string TestID, int Hide)
        {
            var objResult = ObjectEntity1.ssp_InsertLabReportTestForGroup(GroupID, TestID, Hide).ToList();
            var objIList = new List<LabReportTestModel>();
            Mapper.CreateMap<ssp_InsertLabReportTestForGroup_Result, LabReportTestModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public int AddCondition(string ConditionName, string ConditionDescrip, string Sex, int TriggerID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
            ObjectEntity1.ssp_InsertLabReportCondition(ConditionName, ConditionDescrip, Sex, TriggerID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }

        public int UpdateConditions(int ID, string conditionName, string conditionDesc, string sex, int triggerID)
        {
            ObjectParameter resultOut = new ObjectParameter("resultOut", typeof(global::System.Int32));
            resultOut.Value = DBNull.Value;
            //code review remove int result
            ObjectEntity1.ssp_UpdateConditions(ID, conditionName, conditionDesc, sex, triggerID, resultOut);
            return Convert.ToInt16(resultOut.Value);
        }

        public List<LabReportConditionModel> GetConditionBytriggerID(int trigger)
        {
            var objResult = ObjectEntity1.ssp_GetConditionBytriggerID(trigger).ToList();
            var objIList = new List<LabReportConditionModel>();
            Mapper.CreateMap<ssp_GetConditionBytriggerID_Result, LabReportConditionModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
