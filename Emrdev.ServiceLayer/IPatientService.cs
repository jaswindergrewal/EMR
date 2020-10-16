using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPatientService" in both code and config file together.
    [ServiceContract]
    public interface IPatientService
    {
        [OperationContract]
        int InsertPatientDetails(PatientViewModel viewModelPatient);

        [OperationContract]
        bool GetPatientAffiliate(int PatientID);

        [OperationContract]
        void UpdatePatientDetails(PatientViewModel viewModelPatient);

        [OperationContract]
        List<PatientViewModel> GetSouthPatientList(int PageIndex, int PageSize);

        [OperationContract]
        PatientViewModel GetPatientData(int PatientId);

        [OperationContract]
        bool CheckDuplicatePatientByEmailId(int PatientId, string EmailId);

        [OperationContract]
        string CheckMatch(int PatientID,int StaffID);

       

        [OperationContract]
        long QBMatchCount(int PatientID);

        [OperationContract]
        List<ClinicsViewModel> GetClinics();

        [OperationContract]
        PatientViewModel GetPatientDataByPatientId(int PatientId);

        [OperationContract]
        List<PatientViewModel> GetPatientList();
       
        [OperationContract]
        void DoWork();

        [OperationContract]
        int GetPatientIdByOrderId(int orderId);

        [OperationContract]
        string GetPatientFullName(int patientId);

        [OperationContract]
        PatientViewModel GetPatientDataById(int PatientId);
        
    }
}
