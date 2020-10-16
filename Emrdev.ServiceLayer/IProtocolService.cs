using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProtocolService" in both code and config file together.
    [ServiceContract]
    public interface IProtocolService
    {
        [OperationContract]
        List<ProtocolViewModel> GetProtocolList(int PageIndex, int PageSize);

        [OperationContract]
        ProtocolViewModel GetProtocolByID(int ProtocolId);

        [OperationContract]
        List<SymptomViewModel> GetProtocolSymptoms(int ProtocolId);

        [OperationContract]
        List<DiagnosistblViewModel> GetProtocolDiagnosis(int ProtocolId);

        [OperationContract]
        void DeleteProtocolDiagnosis(int ProtocolId,int DiagnosisId);

        [OperationContract]
        void DeleteProtocolSymptoms(int ProtocolId, int SymptomId);

        [OperationContract]
        int InsertProtocolSymptoms(int ProtocolId, int SymptomId);

        [OperationContract]
        int InsertProtocolDiagnosis(int ProtocolId, int SymptomId);

        [OperationContract]
        int InsertSymptoms(string SymptomText);

        [OperationContract]
        List<SymptomViewModel> BindSymptomList(int PageIndex, int PAGE_SIZE);

        [OperationContract]
        int InsertDiagnosis(string ICDCode, string Diagnosis, bool Viewable);

        [OperationContract]
        List<DiagnosistblViewModel> BindDiagnosisList(int PageIndex, int PageSize);

        [OperationContract]
        DiagnosistblViewModel GetDiagnosisByID(int DiagnosisId);

        [OperationContract]
        int UpdateDiagnosis(string ICDCode, string Diagnosis, bool Viewable,int DiagnosisId);

        [OperationContract]
        void AddProtocolDetails(ProtocolViewModel ProtocolView);

        [OperationContract]
        void UpdateProtocolDetails(int ProtocolId,string Content, string Title);

        [OperationContract]
        void DeleteProtocol(int ProtocolId);

        [OperationContract]
        List<ICD10CodesViewmodel> GetIcd10Codes();

        [OperationContract]
        void InserUpdateIcd10Codes(ICD10CodesViewmodel IcdCode );

        [OperationContract]
        List<ICD10CodesViewmodel> GetPatientIcd10Codes(int AptID, int PatientID);

        [OperationContract]
        List<ICD10CodesViewmodel> GetPatientIcd10CodesSuppliments(int AptID, int PatientID);

        [OperationContract]
        void InsertPatientIcdCodes(int PatientID, int AptID, string IcdCode);

        [OperationContract]
        List<ICD10CodesViewmodel> GetPatientAssessIcd10Codes(int AptID, int PatientID);
    }
}
