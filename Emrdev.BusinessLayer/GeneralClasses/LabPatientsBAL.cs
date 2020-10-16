using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.GeneralClasses
{
    public class LabPatientsBAL
    {
        #region Global Variables/Objects
        LabPatientsDAL objLabPatientsDAL = new LabPatientsDAL();
        #endregion

        /// <summary>
        ///this function used for get unassigned clinic patient details using Labs_UnmatchedLabs store procedure.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns>PatientViewModel</returns>
        /// Created By : Rakesh Pareek
        /// Cteated Date : 4-sep-2013
        public List<LabPatientsViewModel> GetUnmatchedLabsPatientData()
        {
            return objLabPatientsDAL.GetUnmatchedLabsPatientDataBy();
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
            return objLabPatientsDAL.LabQuestPatientMatchByLabID(LabID);
        }

       
        /// <summary>
        /// this function used for update the lab_patients details in the database lab_patients table.
        /// </summary>
        /// <returns>id</returns>
        /// Created By : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public int UpdaterLabPatientDetails(LabPatientsViewModel objLabPatientsViewModel)
        {
            int id = 0;
            lab_Patients labPatient = new lab_Patients();

            labPatient = objLabPatientsDAL.Get<lab_Patients>(o => o.ID == objLabPatientsViewModel.ID);

         
            if (labPatient != null)
            {
                labPatient.CorrespondingPatientID = objLabPatientsViewModel.PatientId;
                objLabPatientsDAL.Edit(labPatient);
                id = objLabPatientsViewModel.ID;
            }
          
        
            return id;
            
            //return objLabPatientsDAL.UpdaterLabPatientDetails(objLabPatientsViewModel);
        }
       
    }
}
