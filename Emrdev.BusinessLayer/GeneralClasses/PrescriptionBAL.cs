using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class PrescriptionBAL
    {
        PrescriptionDAL objPrescriptionDAL = new PrescriptionDAL();

        public List<PrescriptionDrugViewModel> GetPrescriptionDrugList(int PatientId)
        {
            List<PrescriptionDrugViewModel> lstPrescriptionDrug = new List<PrescriptionDrugViewModel>();
            lstPrescriptionDrug = objPrescriptionDAL.GetPrescriptionDrugDetails(PatientId).ToList();
            return lstPrescriptionDrug;
        }

        public List<DrugViewModel> GetDrugList()
        {
            List<DrugViewModel> lstDrug = objPrescriptionDAL.GetDrugDetails();
            return lstDrug;
        }

        public List<PrescripDrugStaffViewModel> GetPrescriptionDrugStaffDetails(int PatientId)
        {
            List<PrescripDrugStaffViewModel> lstPrescriptionDrug = new List<PrescripDrugStaffViewModel>();
            lstPrescriptionDrug = objPrescriptionDAL.GetPrescriptionDrugStaffDetails(PatientId).ToList();
            return lstPrescriptionDrug;
        }

        public List<PrescriptionSupplierViewModel> GetSupplementsDetails(int PatientId)
        {
            List<PrescriptionSupplierViewModel> lstDetails = new List<PrescriptionSupplierViewModel>();
            lstDetails = objPrescriptionDAL.GetSupplementsDetails(PatientId).ToList();
            return lstDetails;
        }

        public List<AutoshipProductsViewModel> GetNewSupplementDetails(string PatientId)
        {
            List<AutoshipProductsViewModel> lstDetails = new List<AutoshipProductsViewModel>();
            lstDetails = objPrescriptionDAL.GetNewSupplementDetails(PatientId).ToList();
            return lstDetails;
        }

        public List<PresCripSuppAutoshipProductStaffViewModel> GetClosedSupplementsDetails(int PatientId)
        {
            List<PresCripSuppAutoshipProductStaffViewModel> lstDetails = new List<PresCripSuppAutoshipProductStaffViewModel>();
            lstDetails = objPrescriptionDAL.GetClosedSupplementsDetails(PatientId).ToList();
            return lstDetails;
        }

        public List<ModifiedPrescribedAutoshipViewModel> GetModifiedPrescribedAutoshipList(int PatientId)
        {
            List<ModifiedPrescribedAutoshipViewModel> lstDetails = new List<ModifiedPrescribedAutoshipViewModel>();
            lstDetails = objPrescriptionDAL.GetModifiedPrescribedAutoshipList(PatientId);
            return lstDetails;
        }

        public List<PendingPrescriptionViewModel> GetPendingPrescriptionList()
        {
            List<PendingPrescriptionViewModel> objLst = objPrescriptionDAL.GetPendingPrescriptionList();
            return objLst;
        }

        public List<PendingSupplementViewModel> GetPendingSupplementList()
        {
            List<PendingSupplementViewModel> objLst = objPrescriptionDAL.GetPendingSupplementList();
            return objLst;
        }

        public List<PendingConsultRequestViewModel> GetPendingConsultList(string ClinicName)
        {
            List<PendingConsultRequestViewModel> objLst = objPrescriptionDAL.GetPendingConsultList(ClinicName);
            return objLst;
        }

        public List<PendingConsultRequestViewModel> GetPendingConsults(string ClinicName)
        {
            List<PendingConsultRequestViewModel> objLst = objPrescriptionDAL.GetPendingConsults(ClinicName);
            return objLst;
        }

        public List<PendingBloodDrawsViewModel> GetPendingBloodDrawsList()
        {
            List<PendingBloodDrawsViewModel> objLst = objPrescriptionDAL.GetPendingBloodDrawsList();
            return objLst;
        }

        public List<PendingBloodDrawsByClinicViewModel> GetPendingBloodDrawsListByClinic(string ClinicName)
        {
            List<PendingBloodDrawsByClinicViewModel> objLst = objPrescriptionDAL.GetPendingBloodDrawsListByClinic(ClinicName);
            return objLst;
        }

        public int InsertPrescriptionDrug(string txtDrugNameLocal, string txtSig,string txtDisp,string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, bool chkThirdPartyAdd)
        {
            return objPrescriptionDAL.InsertPrescriptionDrug(txtDrugNameLocal, txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, chkThirdPartyAdd);
        }

        public int InsertPrescriptionRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            return objPrescriptionDAL.InsertPrescriptionRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, DrugID, PrescriptionID);
        }

        public void ClosePrescription(int PrescriptionID ,int ElementId)
        {
            objPrescriptionDAL.ClosePrescription(PrescriptionID, ElementId);
        }

        public void DeletePrescription(int PrescriptionID, int ElementId)
        {
            objPrescriptionDAL.DeletePrescription(PrescriptionID, ElementId);
        }

        public int InsertPrescriptionSupp(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID)
        {
            return objPrescriptionDAL.InsertPrescriptionSupp(txtDrugNameLocal, txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID);
        }

        public int InsertPrescriptionSuppRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            return objPrescriptionDAL.InsertPrescriptionSuppRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, DrugID, PrescriptionID);
        }

        public List<prescriptionHistoryViewModel> GetPrescriptionHistory(int PatientID)
        {
            return objPrescriptionDAL.GetPrescriptionHistory(PatientID);
        }

        public int AutoshipFollowupPrescription(string Data)
        {
            return objPrescriptionDAL.AutoshipFollowupPrescription(Data);
        }


        /// <summary>
        /// fetch supplement history by patient Id
        /// Added by surabhi 8 oct 2013
        /// </summary>
        /// <param name="PatientID"></param>            
        public List<prescriptionHistoryViewModel> GetSupplementHistory(int PatientID)
        {
            return objPrescriptionDAL.GetSupplementHistory(PatientID);
        }

        /// <summary>
        /// Update follow up
        /// Jaswinder Dec 23 2013
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int UpdateFollowUpBloodDraw(string Data)
        {
            try
            {
                string[] dat = Data.Split('|');
                int followupId = int.Parse(dat[0]);
                apt_FollowUps fup = objPrescriptionDAL.Get<apt_FollowUps>(f => f.FollowUp_ID == followupId);
                if (dat[1] == "true") fup.FirstCall = true; else fup.FirstCall = false;
                if (dat[2] == "true") fup.SecondCall = true; else fup.SecondCall = false;
                if (dat[3] == "true") fup.FinalCall = true; else fup.FinalCall = false;
                if (dat[4] == "true") fup.Letter = true; else fup.Letter = false;

                fup.FirstCallNote = dat[5];
                fup.SeconCallNote = dat[6];
                fup.FinalCallNote = dat[7];
                fup.LetterNote = dat[8];

                objPrescriptionDAL.Edit(fup);
                return 1;
            }
            catch {
                return 0;
            }

        }
    }
}
