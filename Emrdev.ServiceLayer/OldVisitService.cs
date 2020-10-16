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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OldVisitService" in both code and config file together.
    public class OldVisitService : IOldVisitService
    {
        OldVisitBAL objBAL = new OldVisitBAL();
        public List<OldVisitViewModel> GetOldVisits(int PatientID)
        {
            return objBAL.GetOldVisits(PatientID);
        }

        public List<CallbacksoldViewModel> GetOldNotes(int PatientID)
        {
           return objBAL.GetOldNotes(PatientID);
        }

    }
}
