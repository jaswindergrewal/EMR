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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PrescriptionService" in both code and config file together.
    public class PrescriptionService : IPrescriptionService
    {
        PrescriptionBAL objPrescriptionBAL = new PrescriptionBAL();
        public void DoWork()
        {
        }

        public List<PrescriptionDrugViewModel> GetPrescriptionDrugList(int PatientId)
        {
            List<PrescriptionDrugViewModel> lstPrescriptionDrug = objPrescriptionBAL.GetPrescriptionDrugList(PatientId).ToList();
            return lstPrescriptionDrug;
        }


        public List<DrugViewModel> GetDrugList()
        {
            List<DrugViewModel> lstDrug = objPrescriptionBAL.GetDrugList();
            return lstDrug;
        }


        public List<PrescripDrugStaffViewModel> GetPrescriptionDrugStaffDetails(int PatientId)
        {
            List<PrescripDrugStaffViewModel> lstDetails = objPrescriptionBAL.GetPrescriptionDrugStaffDetails(PatientId);
            return lstDetails;
        }


        public List<PrescriptionSupplierViewModel> GetSupplementsDetails(int PatientId)
        {
            List<PrescriptionSupplierViewModel> lstDetails = objPrescriptionBAL.GetSupplementsDetails(PatientId);
            return lstDetails;
        }


        public List<AutoshipProductsViewModel> GetNewSupplementDetails(string PatientId)
        {
            List<AutoshipProductsViewModel> lstDetails = objPrescriptionBAL.GetNewSupplementDetails(PatientId);
            return lstDetails;
        }


        public List<PresCripSuppAutoshipProductStaffViewModel> GetClosedSupplementsDetails(int PatientId)
        {
            List<PresCripSuppAutoshipProductStaffViewModel> lstDetails = objPrescriptionBAL.GetClosedSupplementsDetails(PatientId);
            return lstDetails;
        }


        public List<ModifiedPrescribedAutoshipViewModel> GetModifiedPrescribedAutoshipList(int PatientId)
        {
            List<ModifiedPrescribedAutoshipViewModel> lstDetails = objPrescriptionBAL.GetModifiedPrescribedAutoshipList(PatientId);
            return lstDetails;
        }


        public List<PendingPrescriptionViewModel> GetPendingPrescriptionList()
        {
            List<PendingPrescriptionViewModel> lstObj = objPrescriptionBAL.GetPendingPrescriptionList();
            return lstObj;
        }

        public List<PendingSupplementViewModel> GetPendingSupplementList()
        {
            List<PendingSupplementViewModel> lstObj = objPrescriptionBAL.GetPendingSupplementList();
            return lstObj;
        }


        public List<PendingConsultRequestViewModel> GetPendingConsultList(string ClinicName)
        {
            List<PendingConsultRequestViewModel> lstObj = objPrescriptionBAL.GetPendingConsultList(ClinicName);
            return lstObj;
        }

        public List<PendingConsultRequestViewModel> GetPendingConsults(string ClinicName)
        {
            List<PendingConsultRequestViewModel> lstObj = objPrescriptionBAL.GetPendingConsults(ClinicName);
            return lstObj;
        }


        public List<PendingBloodDrawsViewModel> GetPendingBloodDrawsList()
        {
            List<PendingBloodDrawsViewModel> objLst = objPrescriptionBAL.GetPendingBloodDrawsList();
            return objLst;
        }

        public List<PendingBloodDrawsByClinicViewModel> GetPendingBloodDrawsListByClinic(string ClinicName)
        {
            List<PendingBloodDrawsByClinicViewModel> objLst = objPrescriptionBAL.GetPendingBloodDrawsListByClinic(ClinicName);
            return objLst;
        }

        public int InsertPrescriptionDrug(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, bool chkThirdPartyAdd)
        {
            return objPrescriptionBAL.InsertPrescriptionDrug(txtDrugNameLocal, txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, chkThirdPartyAdd);

        }

        public int InsertPrescriptionRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            return objPrescriptionBAL.InsertPrescriptionRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, DrugID, PrescriptionID);
        }

        public void ClosePrescription(int PrescriptionID, int ElementId)
        {
            objPrescriptionBAL.ClosePrescription(PrescriptionID, ElementId);
        }

        public void DeletePrescription(int PrescriptionID,int ElementId)
        {
            objPrescriptionBAL.DeletePrescription(PrescriptionID, ElementId);
        }
        public int InsertPrescriptionSupp(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID)
        {
            return objPrescriptionBAL.InsertPrescriptionSupp(txtDrugNameLocal, txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID);
        }

        public int InsertPrescriptionSuppRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            return objPrescriptionBAL.InsertPrescriptionSuppRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, DrugID, PrescriptionID);
        }

        public List<prescriptionHistoryViewModel> GetPrescriptionHistory(int PatientID)
        {
            return objPrescriptionBAL.GetPrescriptionHistory(PatientID);
        }

        public int AutoshipFollowupPrescription(string Data)
        {
            return objPrescriptionBAL.AutoshipFollowupPrescription(Data);
        }

        public List<prescriptionHistoryViewModel> GetSupplementHistory(int PatientID)
        {
            return objPrescriptionBAL.GetSupplementHistory(PatientID);
        }

        public int UpdateFollowUpBloodDraw(string Data)
        {
            return objPrescriptionBAL.UpdateFollowUpBloodDraw(Data);
        }
    }
}
