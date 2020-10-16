using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminDictationService" in both code and config file together.
    [ServiceContract]
    public interface IAdminDictationService
    {
        [OperationContract]
        List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosis(string DiagnosisText);

        [OperationContract]
        List<Dictation_Plan_ViewModel> GetDictationPlans(string PlanText);

        [OperationContract]
        List<Dictation_Plan_ViewModel> GetDictationDiagnosisPlans(int DiagnosisId);

        [OperationContract]
        void InsertDictationDiagnosis(Dictation_Diagnosis_ViewModel Diag);

        [OperationContract]
        void UpdateDictationDiagnosis(int DiagnosisID, string DiagDesrcip, string DiagName, string Code, string KeyWords);

        [OperationContract]
        void InsertDictationPlans(Dictation_Plan_ViewModel Plans, int DiagnosisID);

        [OperationContract]
        void DeleteDictationDiagnosis(int DiagnosisID);

        [OperationContract]
        List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosisByID(int DiagnosisID);

        [OperationContract]
        void UpdateDictationPlans(int PlanID, string rdoEditPlanCategory, string PlanDescrip, string PlanName, string KeyWords, int DiagnosisID);

        [OperationContract]
        void DeleteDictationPlan(int DiagnosisID, int PlanID, bool IsDiagnosis);

        [OperationContract]
        List<Dictation_Plan_ViewModel> GetDictationPlanByID(int PlanID);

        [OperationContract]
        long GetCountForPlanDiagnosis(int DiagnosisID, int PlanID);

        [OperationContract]
        void InsertDictaionDiagnosiPlan(Dictation_PlanDiagnosis_ViewModel PlnDiag);

        [OperationContract]
        string InsertDictation_Diagnosis(string DiagnosisDescrip, string DiagnosisName, string ICDCode, string KeyWords, bool Viewable_YN);

        [OperationContract]
        string InsertDictation_Plan(string Category, string PlanDesc, string PlanName, string KeyWords, bool Viewable_YN);

        [OperationContract]
        string CheckExistDictation_Plan(string Category, string PlanName, int PlanId);

        [OperationContract]
        string ValidateAndUpdateDictation_Diagnosis(int DiagnosisID, string DiagnosisName, string ICDCode);


    }
}
