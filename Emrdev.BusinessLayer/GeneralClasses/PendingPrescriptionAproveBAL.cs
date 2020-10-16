using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class PendingPrescriptionAproveBAL
    {
        PendingPrescriptionAproveDAL objDAL = new PendingPrescriptionAproveDAL();
        public PendingPrescriptionAproveViewModel GetPescriptionDetail(int StaffId, int PrescriptionID)
        {
            return objDAL.GetPescriptionDetail(StaffId, PrescriptionID);
        }

        public int UpdatePrescription(int StaffID, int PrescriptionID, string Sig, string Dispenses, string NumbRefills, DateTime DateEntered, string PreNotes)
        {

            Prescription myScrip = objDAL.Get<Prescription>(o => o.PrescriptionID == PrescriptionID);
            myScrip.Approved_YN = true;
            myScrip.Approved_Date = DateTime.Now;
            myScrip.Writer = StaffID;
            myScrip.Drug_Dose = Sig;
            myScrip.Drug_Dispenses = Dispenses;
            myScrip.Drug_NumbRefills = NumbRefills;
            myScrip.DateEntered = DateEntered;
            myScrip.Notes = PreNotes;
            objDAL.Edit(myScrip);
            int PatientId = 0;
            if(myScrip.PatientID!=null)
            {
             PatientId=(int)myScrip.PatientID;
            }
            return PatientId;

        }

        public int UpdateSuppliments(int StaffID, int PrescriptionID, string Sig, string Dispenses, string NumbRefills, DateTime DateEntered, string PreNotes)
        {
            PresscriptionSupp myScrip = objDAL.Get<PresscriptionSupp>(o => o.PresscriptionSuppID == PrescriptionID);
            myScrip.Approved_YN = true;
            myScrip.Approved_Date = DateTime.Now;
            myScrip.Writer = StaffID;


            myScrip.SuppDose = Sig;
            myScrip.SuppDispenses = Dispenses;
            myScrip.SuppNumbRefills = NumbRefills;
            myScrip.SuppDatePrescibed = DateEntered;
            myScrip.Notes = PreNotes;
           
            objDAL.Edit(myScrip);
            int PatientId = 0;
            if (myScrip.PatientID != null)
            {
                PatientId = (int)myScrip.PatientID;
            }
            return PatientId;
        }

        public PendingPrescriptionAproveViewModel GetPescriptionSupDetail(int StaffId, int PrescriptionID)
        {
            return objDAL.GetPescriptionSupDetail(StaffId, PrescriptionID);
        }
    }
}
