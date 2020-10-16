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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProtocolService" in both code and config file together.
    public class ProtocolService : IProtocolService
    {
        ProtocolBAL objBAL = new ProtocolBAL();
        public List<ProtocolViewModel> GetProtocolList(int PageIndex, int PageSize)
        {
            return objBAL.GetProtocolList(PageIndex, PageSize);
        }

        public ProtocolViewModel GetProtocolByID(int ProtocolId)
        {
            return objBAL.GetProtocolByID(ProtocolId);
        }

        public List<SymptomViewModel> GetProtocolSymptoms(int ProtocolId)
        {
            return objBAL.GetProtocolSymptoms(ProtocolId);
        }

        public List<DiagnosistblViewModel> GetProtocolDiagnosis(int ProtocolId)
        {
            return objBAL.GetProtocolDiagnosis(ProtocolId);
        }

        public void DeleteProtocolDiagnosis(int ProtocolId, int DiagnosisId)
        {
            objBAL.DeleteProtocolDiagnosis(ProtocolId, DiagnosisId);
        }

        public void DeleteProtocolSymptoms(int ProtocolId, int SymptomId)
        {
            objBAL.DeleteProtocolSymptoms(ProtocolId, SymptomId);
        }

        public int InsertProtocolSymptoms(int ProtocolId, int SymptomId)
        {
           return objBAL.InsertProtocolSymptoms(ProtocolId, SymptomId);
        }

        public int InsertProtocolDiagnosis(int ProtocolId, int SymptomId)
        {
            return objBAL.InsertProtocolDiagnosis(ProtocolId, SymptomId);
        }

        public int InsertSymptoms(string SymptomText)
        {
            return objBAL.InsertSymptoms(SymptomText);
        }

        public List<SymptomViewModel> BindSymptomList(int PageIndex, int PAGE_SIZE)
        {
            return objBAL.BindSymptomList(PageIndex, PAGE_SIZE);
        }

        public int InsertDiagnosis(string ICDCode, string Diagnosis, bool Viewable)
        {
            return objBAL.InsertDiagnosis(ICDCode, Diagnosis, Viewable);
        }

        public List<DiagnosistblViewModel> BindDiagnosisList(int PageIndex, int PageSize)
        {
            return objBAL.BindDiagnosisList(PageIndex, PageSize);
        }

        public DiagnosistblViewModel GetDiagnosisByID(int DiagnosisId)
        {
            return objBAL.GetDiagnosisByID(DiagnosisId);
        }

        public int UpdateDiagnosis(string ICDCode, string Diagnosis, bool Viewable,int DiagnosisId)
        {
            return objBAL.UpdateDiagnosis(ICDCode, Diagnosis, Viewable, DiagnosisId);
        }

        public void AddProtocolDetails(ProtocolViewModel ProtocolView)
        {
             objBAL.AddProtocolDetails(ProtocolView);
        }

        public void UpdateProtocolDetails(int ProtocolId, string Content, string Title)
        {
            objBAL.UpdateProtocolDetails(ProtocolId, Content, Title);
        }

        public void DeleteProtocol(int ProtocolId)
        {
            objBAL.DeleteProtocol(ProtocolId);
        }

        public List<ICD10CodesViewmodel> GetIcd10Codes()
        {
            return objBAL.GetIcd10Codes();
        }

        public void InserUpdateIcd10Codes(ICD10CodesViewmodel IcdCode)
        {
             objBAL.InserUpdateIcd10Codes(IcdCode);
        }

        public List<ICD10CodesViewmodel> GetPatientIcd10Codes(int AptID, int PatientID)
        {
            return objBAL.GetPatientIcd10Codes(AptID, PatientID);
        }

        public List<ICD10CodesViewmodel> GetPatientIcd10CodesSuppliments(int AptID, int PatientID)
        {
            return objBAL.GetPatientIcd10CodesSuppliments(AptID, PatientID);
        }
       
        public void InsertPatientIcdCodes(int PatientID, int AptID, string IcdCode)
        {
            objBAL.InsertPatientIcdCodes(PatientID, AptID, IcdCode);
        }

        public List<ICD10CodesViewmodel> GetPatientAssessIcd10Codes(int AptID, int PatientID)
        {
            return objBAL.GetPatientAssessIcd10Codes(AptID, PatientID);
        }
    }
}
