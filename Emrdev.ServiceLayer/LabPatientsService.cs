using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.GeneralClasses;
namespace Emrdev.ServiceLayer
{
    public class LabPatientsService : ILabPatientsService
    {
        LabPatientsBAL objLabPatientsBAL = new LabPatientsBAL();
        public void DoWork()
        {
        }

        /// <summary>
        ///this function used for get unassigned clinic patient details using Labs_UnmatchedLabs store procedure.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns>PatientViewModel</returns>
        /// Created By : Rakesh Pareek
        /// Cteated Date : 4-sep-2013
        public List<LabPatientsViewModel> GetUnmatchedLabsPatientData()
        {
            return objLabPatientsBAL.GetUnmatchedLabsPatientData();
        }

        /// <summary>
        /// this function used for get lab_Patients details according the labID
        /// </summary>
        /// <param name="LabID"></param>
        /// <returns>LabPatientsViewModel</returns>
        /// Created Date : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public LabPatientsViewModel LabQuestPatientMatchByLabID(int LabID)
        {
            return objLabPatientsBAL.LabQuestPatientMatchByLabID(LabID);
        }

        /// <summary>
        /// this function used for update the lab_patients details in the database lab_patients table.
        /// </summary>
        /// <returns>id</returns>
        /// Created By : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public int UpdaterLabPatientDetails(LabPatientsViewModel objLabPatientsViewModel)
        {
            return objLabPatientsBAL.UpdaterLabPatientDetails(objLabPatientsViewModel);
        }
    }
}
