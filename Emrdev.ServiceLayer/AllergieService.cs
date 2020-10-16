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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AllergieService" in both code and config file together.
    public class AllergieService : IAllergieService
    {
        AllergeBAL objBAL = new AllergeBAL();
        public PatientViewModel GetPatientByID(int PatientId)
        {
            return objBAL.GetPatientByID(PatientId);
        }

        public void UpdateAllergies(PatientViewModel pat)
        {
             objBAL.UpdateAllergies(pat);
        }

        public List<AestheticNotesViewModel> GetAestheticNotes(int PatientID)
        {
            return objBAL.GetAestheticNotes(PatientID);
        }

        public List<AnestheticFollowupViewModel> GetAestheticFollowups(int PatientID)
        {
            return objBAL.GetAestheticFollowups(PatientID);
        }

        public List<AestheticNotesViewModel> GetAestheticNotesALL(int PatientID)
        {
            return objBAL.GetAestheticNotesALL(PatientID);
        }
    }
}
