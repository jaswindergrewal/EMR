using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPrescriptionService" in both code and config file together.
    [ServiceContract]
    public interface IPrescriptionService
    {
        [OperationContract]
        List<PrescriptionDrugViewModel> GetPrescriptionDrugList(int PatientId);

        [OperationContract]
        List<DrugViewModel> GetDrugList();

        [OperationContract]
        List<PrescripDrugStaffViewModel> GetPrescriptionDrugStaffDetails(int PatientId);

        [OperationContract]
        List<PrescriptionSupplierViewModel> GetSupplementsDetails(int PatientId);

        [OperationContract]
        List<AutoshipProductsViewModel> GetNewSupplementDetails(string PatientId);

        [OperationContract]
        List<PresCripSuppAutoshipProductStaffViewModel> GetClosedSupplementsDetails(int PatientId);

        [OperationContract]
        List<ModifiedPrescribedAutoshipViewModel> GetModifiedPrescribedAutoshipList(int PatientId);

        [OperationContract]
        List<PendingPrescriptionViewModel> GetPendingPrescriptionList();

        [OperationContract]
        List<PendingSupplementViewModel> GetPendingSupplementList();

        [OperationContract]
        List<PendingConsultRequestViewModel> GetPendingConsultList(string ClinicName);

        [OperationContract]
        List<PendingConsultRequestViewModel> GetPendingConsults(string ClinicName);

        [OperationContract]
        List<PendingBloodDrawsViewModel> GetPendingBloodDrawsList();

        [OperationContract]
        List<PendingBloodDrawsByClinicViewModel> GetPendingBloodDrawsListByClinic(string ClinicName);

        [OperationContract]
        int InsertPrescriptionDrug(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, bool chkThirdPartyAdd);

        [OperationContract]
        int InsertPrescriptionRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID);

        [OperationContract]
        void ClosePrescription(int PrescriptionID,int ElementId);

        [OperationContract]
        void DeletePrescription(int PrescriptionID, int ElementId);

        [OperationContract]
        int InsertPrescriptionSupp(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID);

        [OperationContract]
        int InsertPrescriptionSuppRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID);

        [OperationContract]
        List<prescriptionHistoryViewModel> GetPrescriptionHistory(int PatientID);

        [OperationContract]
        int AutoshipFollowupPrescription(string Data);

        [OperationContract]
        List<prescriptionHistoryViewModel> GetSupplementHistory(int PatientID);

        [OperationContract]
        int UpdateFollowUpBloodDraw(string Data);

        [OperationContract]
        void DoWork();
    }
}
