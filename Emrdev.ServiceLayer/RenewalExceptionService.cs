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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RenewalExceptionService" in both code and config file together.
    public class RenewalExceptionService : IRenewalExceptionService
    {

        RenewalExceptionBAL objBAL = new RenewalExceptionBAL();
        public PatientViewModel GEtPatientByID(int PatientID)
        {
            return objBAL.GEtPatientByID(PatientID);
        }


        public StaffViewModel GetStaffByStaffID(int StaffID)
        {
            return objBAL.GetStaffByStaffID(StaffID);
        }


        public void UpdatePatientRenewalException(PatientViewModel pat)
        {
            objBAL.UpdatePatientRenewalException(pat);
        }
    }
}
