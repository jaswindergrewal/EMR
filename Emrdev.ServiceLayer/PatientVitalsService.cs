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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PatientVitalsService" in both code and config file together.
    public class PatientVitalsService : IPatientVitalsService
    {
        PatientVitalsBAL objBAL = new PatientVitalsBAL();
        public PatientVitalsViewModel GetPatientVitalsByVitalId(int VitalID)
        {
            return objBAL.GetPatientVitalsByVitalId(VitalID);
        }

        public int UpdatePatientVitals(PatientVitalsViewModel theVital, int VitalID)
        {
            return objBAL.UpdatePatientVitals(theVital, VitalID);
        }

        public List<PatientVitalsViewModel> GetPatientVitalsByPatientId(int PatientID)
        {
            return objBAL.GetPatientVitalsByPatientId(PatientID);
        }

        public int InsertPatientVitalDetails(PatientVitalsViewModel vitalViewModel)
        {
            return objBAL.InsertPatientVitalDetails(vitalViewModel);
        }

        public void DeleteVitalsByID(int ID)
        {
            objBAL.DeleteVitalsByID(ID);
        }
    }
}
