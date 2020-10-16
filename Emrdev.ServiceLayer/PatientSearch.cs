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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PatientSearch" in both code and config file together.
    public class PatientSearchList : IPatientSearch
    {
        PatientBAL objPatientBAL = new PatientBAL();
        public void DoWork()
        {
        }

        public List<PatientViewModel> SearchPatientDetails(string FirstName, string LastName, string MiddleName, string Phone)
        {
            List<PatientViewModel> objLst = objPatientBAL.SearchPatientDetails(FirstName, LastName, MiddleName, Phone);
            return objLst;
        }
    }
}
