using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PendingPrescriptionAproveService" in both code and config file together.
    public class PendingPrescriptionAproveService : IPendingPrescriptionAproveService
    {
        PendingPrescriptionAproveBAL objBAL = new PendingPrescriptionAproveBAL();
        public PendingPrescriptionAproveViewModel GetPescriptionDetail(int StaffId, int PrescriptionID)
        {
            return objBAL.GetPescriptionDetail(StaffId, PrescriptionID);
        }

        public int UpdatePrescription(int StaffID, int PrescriptionID, string Sig, string Dispenses, string NumbRefills, DateTime DateEntered, string PreNotes)
        {
            return objBAL.UpdatePrescription(StaffID,PrescriptionID,Sig, Dispenses, NumbRefills,DateEntered, PreNotes);
        }

        public int UpdateSuppliments(int StaffID, int PrescriptionID, string Sig, string Dispenses, string NumbRefills, DateTime DateEntered, string PreNotes)
        {
            return objBAL.UpdateSuppliments(StaffID, PrescriptionID, Sig, Dispenses, NumbRefills, DateEntered, PreNotes);
        }

        public PendingPrescriptionAproveViewModel GetPescriptionSupDetail(int StaffId, int PrescriptionID)
        {
            return objBAL.GetPescriptionSupDetail(StaffId, PrescriptionID);
        }

    }
}
