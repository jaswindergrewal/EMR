using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminDictationService" in both code and config file together.
    public class AdminDictationService : IAdminDictationService
    {
        AdminDictationBAL objBAL = new AdminDictationBAL();
        public List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosis(string DiagnosisText)
        {
            List<Dictation_Diagnosis_ViewModel> _objDictationDiagnosisBAL = objBAL.GetDictationDiagnosis(DiagnosisText);
            return _objDictationDiagnosisBAL;
        }

        public List<Dictation_Plan_ViewModel> GetDictationPlans(string PlanText)
        {
            List<Dictation_Plan_ViewModel> _objDictationPlanBAL = objBAL.GetDictationPlans(PlanText);
            return _objDictationPlanBAL;
        }

        public List<Dictation_Plan_ViewModel> GetDictationDiagnosisPlans(int DiagnosisId)
        {
            List<Dictation_Plan_ViewModel> _objDictationPlanBAL = objBAL.GetDictationDiagnosisPlans(DiagnosisId);
            return _objDictationPlanBAL;
        }

        public void InsertDictationDiagnosis(Dictation_Diagnosis_ViewModel Diag)
        {
            objBAL.InsertDictationDiagnosis(Diag);
        }

        public void UpdateDictationDiagnosis(int DiagnosisID, string DiagDesrcip, string DiagName, string Code, string KeyWords)
        {
            objBAL.UpdateDictationDiagnosis(DiagnosisID, DiagDesrcip, DiagName, Code, KeyWords);
        }

        public void InsertDictationPlans(Dictation_Plan_ViewModel Plans, int DiagnosisID)
        {
            objBAL.InsertDictationPlans(Plans, DiagnosisID);
        }

        public void DeleteDictationDiagnosis(int DiagnosisID)
        {
            objBAL.DeleteDictationDiagnosis(DiagnosisID);
        }

        public List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosisByID(int DiagnosisID)
        {
            return objBAL.GetDictationDiagnosisByID(DiagnosisID);
        }

        public void UpdateDictationPlans(int PlanID, string rdoEditPlanCategory, string PlanDescrip, string PlanName, string KeyWords, int DiagnosisID)
        {
            objBAL.UpdateDictationPlans(PlanID, rdoEditPlanCategory, PlanDescrip, PlanName, KeyWords, DiagnosisID);
        }

        public void DeleteDictationPlan(int DiagnosisID, int PlanID, bool IsDiagnosis)
        {
            objBAL.DeleteDictationPlan(DiagnosisID, PlanID, IsDiagnosis);
        }

        public List<Dictation_Plan_ViewModel> GetDictationPlanByID(int PlanID)
        {
            return objBAL.GetDictationPlanByID(PlanID);
        }

        public long GetCountForPlanDiagnosis(int DiagnosisID, int PlanID)
        {
            return objBAL.GetCountForPlanDiagnosis(DiagnosisID, PlanID);
        }

        public void InsertDictaionDiagnosiPlan(Dictation_PlanDiagnosis_ViewModel PlnDiag)
        {
            objBAL.InsertDictaionDiagnosiPlan(PlnDiag);
        }


        public string InsertDictation_Diagnosis(string DiagnosisDescrip, string DiagnosisName, string ICDCode, string KeyWords, bool Viewable_YN)
        {
            return objBAL.InsertDictation_Diagnosis(DiagnosisDescrip, DiagnosisName, ICDCode, KeyWords, Viewable_YN).ToString();
        }


        public string InsertDictation_Plan(string Category, string PlanDesc, string PlanName, string KeyWords, bool Viewable_YN)
        {
            return objBAL.InsertDictation_Plan(Category, PlanDesc, PlanName, KeyWords, Viewable_YN).ToString();
        }


        public string CheckExistDictation_Plan(string Category, string PlanName, int PlanId)
        {
            return objBAL.CheckExistDictation_Plan(Category, PlanName, PlanId).ToString();
        }


        public string ValidateAndUpdateDictation_Diagnosis(int DiagnosisID, string DiagnosisName, string ICDCode)
        {
            return objBAL.ValidateAndUpdateDictation_Diagnosis(DiagnosisID, DiagnosisName, ICDCode).ToString();
        }
    }
}
