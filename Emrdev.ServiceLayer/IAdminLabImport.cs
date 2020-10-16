using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminLabImport" in both code and config file together.
    [ServiceContract]
    public interface IAdminLabImport
    {
        [OperationContract]
        List<AdminImportViewModel> GetAdminImportTestList(DateTime StartDate, DateTime EndDate);

        [OperationContract]
        List<AdminLabReportsTestViewModel> GetAdminLabReportList();

        [OperationContract]
        void InsertLabReportTest(AdminLabReportsTestViewModel clsAdminLabReport);

        [OperationContract]
        void InsertLabReportTest1(int GroupID, bool Hidden, DateTime LastUsed, string TestName);       

        [OperationContract]
        List<LabReportViewModel> GetLabReport_PanelsGroupsTestsList();
 
        [OperationContract]
        List<LabReportViewModel> GetGroupsTriggersByPanelID(int panelID);
                        
        [OperationContract]
        List<LabReportTestModel> GetTestByGroupID(int groupID);

        
        //[OperationContract]
        //List<LabReportTriggerModel> GetTriggersByPanelID(int panelID);

        [OperationContract]

        int AddPanels(string PanelName, string PanelDescrip, Nullable<int> SortOrder);

        [OperationContract]

        LabReportsPanelViewModel GetPanelDetails(int PanelID);

        [OperationContract]
        int AddGroups(string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID);
              
        
        [OperationContract]
        void DeletePanel(int ID, string Name);

        [OperationContract]
        LabReportGroupViewModel GetGroupByID(int ID);


        [OperationContract]
        int UpdateGroups(int ID, string GroupName, string GroupTitle, int SortOrder, bool ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID);

        [OperationContract]
        int UpdatePanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip, int PanelID);

        [OperationContract]
        int AddTriggers(string triggerName, string triggerDesc, int panelID);

        [OperationContract]
        LabReportTriggerModel GetTriggerByID(int ID);

        [OperationContract]
        int UpdateTriggers(int ID, string triggerName, string triggerDesc, int panelID);

        [OperationContract]
        List<LabReportTestModeltest> GetTestNOTINGroups();

        [OperationContract]
        List<LabReportTestModel> InsertTestDetailsForGroup(int GroupID, string TestID, int Hide);

        [OperationContract]
        int AddCondition(string ConditionName, string ConditionDescrip, string Sex, int TriggerID);

        [OperationContract]
        LabReportConditionModel GetConditionByID(int ID);

        [OperationContract]
        int UpdateConditions(int ID, string conditionName, string conditionDesc, string sex, int triggerID);

        [OperationContract]
        List<LabReportConditionModel> GetConditionBytriggerID(int triggerID);
        
        [OperationContract]

        void DoWork();
    }
}
