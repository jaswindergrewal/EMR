using System;
using System.Collections.Generic;
using System.Linq;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using AutoMapper;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class PatientBAL
    {
        PatientDAL objPatientDAL = new PatientDAL();

        public List<PatientViewModel> SearchPatientDetails(string FirstName, string LastName, string MiddleName, string Phone)
        {
            List<PatientViewModel> objLst = objPatientDAL.SearchPatientDetails(FirstName, LastName, MiddleName, Phone);
            return objLst;
        }

        public int InsertPatientDetails(PatientViewModel viewModelPatient)
        {

            Patient cls = new Patient();
            AutoMapper.Mapper.CreateMap<PatientViewModel, Patient>();
            cls = AutoMapper.Mapper.Map(viewModelPatient, cls);
            RenewalPackagesDAL objDal = new RenewalPackagesDAL();
            objDal.Create1(cls);
            if (viewModelPatient.PatientPackage > 0 && cls.PatientID > 0)
            {
               
                objDal.InsertPatientPackages(cls.PatientID, viewModelPatient.PatientPackage, true);
            }

            if(viewModelPatient.PatientManagementProgramId>0)
            {
                objDal.InsertPatientProgramManagement(cls.PatientID, viewModelPatient.PatientManagementProgramId);
            }
            return cls.PatientID;
        }

        public void UpdatePatientDetails(PatientViewModel viewModelPatient)
        {
            Patient cls = new Patient();
            RenewalPackagesDAL objDal = new RenewalPackagesDAL();
            AutoMapper.Mapper.CreateMap<PatientViewModel, Patient>();
            cls = AutoMapper.Mapper.Map(viewModelPatient, cls);
            objDal.Edit1(cls);


            objDal.InsertPatientProgramManagement(viewModelPatient.PatientID, viewModelPatient.PatientManagementProgramId);
            

            //RenewalPackagesDAL objDal = new RenewalPackagesDAL();
            //objDal.UpdatePatientChina(viewModelPatient.PatientID, viewModelPatient.ChinaPatientId, viewModelPatient.BillingCountry, viewModelPatient.ShippingCountry);

            //if (viewModelPatient.PatientPackage > 0 && viewModelPatient.PatientID > 0)
            //{
            //    RenewalPackagesDAL objDal = new RenewalPackagesDAL();
            //    objDal.InsertPatientPackages(viewModelPatient.PatientID, viewModelPatient.PatientPackage,false);
            //}
        }

        public bool GetPatientAffiliate(int PatientID)
        {
            return objPatientDAL.GetPatientAffiliate(PatientID);
        }

        /// <summary>
        /// Get the South Clinic Patients Details
        /// Jaswinder 9th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<PatientViewModel> GetSouthPatientList(int PageIndex, int PageSize)
        {
            return objPatientDAL.GetSouthPatientList(PageIndex, PageSize);
        }

        public PatientViewModel GetPatientData(int PatientId)
        {
            return objPatientDAL.GetPatientData(PatientId);

        }

        public bool CheckDuplicatePatientByEmailId(int PatientId, string EmailId)
        {
            string strStatus = string.Empty;
            bool isExist = false;
            if (PatientId == 0)
            {
                var objfirst = objPatientDAL.Get<Emrdev.DataLayer.Patient>(o => o.Email == EmailId);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objPatientDAL.Get<Emrdev.DataLayer.Patient>(o => o.Email == EmailId && o.PatientID != PatientId);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// Get Patient Id By Order Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Patient Id</returns>
        public int GetPatientIdByOrderId(int orderId)
        {
            objPatientDAL = new PatientDAL();
            return objPatientDAL.GetPatientIdByOrderId(orderId);
        }

        /// <summary>
        /// Added by jaswinder to match the patient for quickbooks
        /// 28 aug 2013
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public string CheckMatch(int PatientID, int StaffID)
        {
            string QBMatch=objPatientDAL.CheckMatch(PatientID, StaffID);
            long CountLabreviewappointment;
            CountLabreviewappointment=objPatientDAL.Count<apt_rec>(o => o.patient_id == PatientID);
            if (CountLabreviewappointment > 0)
            {
                DateTime  Apptstart=DateTime.Now.AddYears(-1);
                CountLabreviewappointment = objPatientDAL.Count<apt_rec>(o => o.patient_id == PatientID && o.AppointmentTypeID != 4 && (o.ApptStart >= Apptstart && o.ApptStart <= DateTime.Now));
                QBMatch = QBMatch + ',' + CountLabreviewappointment.ToString();
            }
            return QBMatch;
        }


        /// <summary>
        /// Jaswinder : To get the count for QB Matches
        /// 29 aug 2013
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public long QBMatchCount(int PatientID)
        {
            long QBMatchCount = objPatientDAL.Count<QB_Match>(o => o.PatientID == PatientID);
            return QBMatchCount;
        }

        public List<ClinicsViewModel> GetClinics()
        {
            var _objClinicList = new List<ClinicsViewModel>();
            var ClinicEntity = new List<Clinic>();
            ClinicEntity = objPatientDAL.GetAll<Clinic>(o => o.IsActive == true).ToList();

            Mapper.CreateMap<Clinic, ClinicsViewModel>();
            _objClinicList = Mapper.Map(ClinicEntity, _objClinicList);
            return _objClinicList;
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
            return objPatientDAL.GetPatientDataByPatientId(PatientId);
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
            return objPatientDAL.GetPatientList();
        }


        /// <summary>
        /// Concate Patient First Name and Last Name 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public string GetPatientFullName(int patientId)
        {
           return objPatientDAL.GetPatientFullName(patientId);
        }

        /// <summary>
        /// Get the patient records for the patient info page
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientViewModel GetPatientDataById(int PatientId)
        {
            return objPatientDAL.GetPatientDataById(PatientId);
        }

        /// <summary>
        /// Updating reasonid for the deleted autoship
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ReasonId"></param>
        public void UpdatePatientAfterDeleingAutoship(int PatientId, int ReasonId)
        {
            objPatientDAL.UpdatePatientAfterDeleingAutoship(PatientId, ReasonId);
        }
    }
}
