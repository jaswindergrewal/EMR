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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PatientService" in both code and config file together.
    public class PatientService : IPatientService
    {
        PatientBAL objPatientBAL = new PatientBAL();
        public void DoWork()
        {
        }

        public int InsertPatientDetails(PatientViewModel viewModelPatient)
        {
            return objPatientBAL.InsertPatientDetails(viewModelPatient);
        }


        public bool GetPatientAffiliate(int PatientID)
        {
            return objPatientBAL.GetPatientAffiliate(PatientID);
        }


        public void UpdatePatientDetails(PatientViewModel viewModelPatient)
        {
            objPatientBAL.UpdatePatientDetails(viewModelPatient);
        }

        public List<PatientViewModel> GetSouthPatientList(int PageIndex, int PageSize)
        {
            return objPatientBAL.GetSouthPatientList(PageIndex, PageSize);
        }

        public PatientViewModel GetPatientData(int PatientId)
        {
            return objPatientBAL.GetPatientData(PatientId);

        }

        public bool CheckDuplicatePatientByEmailId(int PatientId, string EmailId)
        {
            return objPatientBAL.CheckDuplicatePatientByEmailId(PatientId, EmailId);
        }


        public int GetPatientIdByOrderId(int orderId)
        {
            objPatientBAL = new PatientBAL();
            return objPatientBAL.GetPatientIdByOrderId(orderId);
        }

        public string CheckMatch(int PatientID, int StaffID)
        {
            return objPatientBAL.CheckMatch(PatientID, StaffID);
        }

       

        public long QBMatchCount(int PatientID)
        {
            return objPatientBAL.QBMatchCount(PatientID);
        }

        public List<ClinicsViewModel> GetClinics()
        {
            return objPatientBAL.GetClinics();
        }

        /// <summary>
        ///this function used for get patient details by patientid using GetPatientById store procedure.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns>PatientViewModel</returns>
        /// Created By : Rakesh Pareek
        /// Cteated Date : 3-sep-2013
        public PatientViewModel GetPatientDataByPatientId(int PatientId)
        {
            return objPatientBAL.GetPatientDataByPatientId(PatientId);
        }

        /// <summary>
        /// this function used for get all patient record to Patients table in database which is not null.
        /// </summary>
        /// <param name="LabID"></param>
        /// <returns>LabPatientsViewModel</returns>
        /// Created Date : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public List<PatientViewModel> GetPatientList()
        {
            return objPatientBAL.GetPatientList();
        }


        public string GetPatientFullName(int patientId)
        {
            return objPatientBAL.GetPatientFullName(patientId);
        }

        public PatientViewModel GetPatientDataById(int PatientId)
        {
            return objPatientBAL.GetPatientDataById(PatientId);
        }

        /// <summary>
        /// Updating reasonid for the deleted autoship
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ReasonId"></param>
        public void UpdatePatientAfterDeleingAutoship(int PatientId, int ReasonId)
        {

            objPatientBAL.UpdatePatientAfterDeleingAutoship(PatientId, ReasonId);
        }

    }

}
