using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProblemListService" in both code and config file together.
    [ServiceContract]
    public interface IProblemListService
    {
        [OperationContract]
        ProblemSymptEditViewModel GetProblemSymtomsList(int ProbSymptID);

        [OperationContract]
        void UpdateProblemSymtomsList(int ProbSymptID, decimal Priority, decimal Severity,string Trend);

        [OperationContract]
        List<ProblemSymptListViewModel> GetProblemSymtomsListByPatientID(int PatientId);

        [OperationContract]
        List<MisDiagnosisListViewModel> GetMiscDiagListByPatientID(int PatientId);

        [OperationContract]
        List<DiagnosisListViewModel> GetProblemSDiagnosisListByPatientID(int PatientId, int AptID);

        [OperationContract]
        void DeleteProblemListElements(int ElementListID, int ElementID);

        [OperationContract]
        List<SymptomProblemListViewModel> GetSymptomList();

        [OperationContract]
        List<SymptomProblemListViewModel> GetDiagnosisList();

        [OperationContract]
        int InsertProblemDiagnosisElements(int ElementListID, MisDiagnosisListViewModel ViewModelInsert,int AptID);

        [OperationContract]
        int InsertProblemSymptoms(ProblemSymptInsertListViewModel ViewModelInsert);

        [OperationContract]
        void UpdateProblemListStatus(int ElementListID, int ElementID,int Status);

        [OperationContract]
        void UpdateProblemListAddress(int ElementListID, int ElementID);

        [OperationContract]
        DiagnosisListViewModel GetProblemDiagnosisList(int ProbDiagID);

        [OperationContract]
        void UpdateProblemDiagList(int ProbDiagID, decimal Priority, decimal Severity);

        [OperationContract]
        DiagnosisListViewModel GetProblemMiscDiagnosisList(int ProbDiagID);

        [OperationContract]
        void UpdateProblemMiscDiagList(int ProbDiagID, decimal Priority, decimal Severity);

        [OperationContract]
        void InsertProblemAppointment(int PatientID, int DiagnosisID, int AptID);

        [OperationContract]
        void DeleteProblemAppointment(int PatientID, int DiagnosisID, int AptID);

        [OperationContract]
        List<DiagnosistblViewModel> GetDiagnosisPropemApt(int PatientID, int ApptID);
      
    }
}
