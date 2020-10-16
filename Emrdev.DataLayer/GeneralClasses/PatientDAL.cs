using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PatientDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).Count();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<PatientViewModel> SearchPatientDetails(string FirstName, string LastName, string MiddleName, string Phone)
        {
            var objResult = ObjectEntity1.ssp_SearchPatientDetails(FirstName, LastName, MiddleName, Phone).ToList();
            var objIList = new List<PatientViewModel>();
            Mapper.CreateMap<ssp_SearchPatientDetails_Result, PatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public bool GetPatientAffiliate(int PatientID)
        {
            bool isAffiliate = (from p in ObjectEntity1.Patients where p.PatientID == PatientID select p.IsAffiliate).FirstOrDefault();
            return isAffiliate;
        }


        /// <summary>
        /// Call the procedure amd map the fields to get the south clinic patients data
        /// Jaswinder aug 9 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<PatientViewModel> GetSouthPatientList(int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_SouthPatientList(PageIndex, PageSize).ToList();
            var objIList = new List<PatientViewModel>();
            Mapper.CreateMap<ssp_SouthPatientList_Result, PatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public PatientViewModel GetPatientData(int PatientId)
        {
            var objResult = ObjectEntity1.GetPatientById(PatientId);
            var objIList = new PatientViewModel();
            Mapper.CreateMap<GetPatientById_Result, PatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        public string CheckMatch(int PatientID, int StaffID)
        {
            int? PatientId = PatientID;
            int? StaffId = StaffID;
            ObjectParameter matchOUT = new ObjectParameter("matchOUT", typeof(global::System.Int32));
            matchOUT.Value = DBNull.Value;
            ObjectEntity1.ssp_CheckMatch(PatientId, StaffId, matchOUT);
            int result = Convert.ToInt32(matchOUT.Value);
            if (result == 0)
                return "";
            else
                return "No Match";
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
            var objResult = ObjectEntity1.GetPatientById(PatientId).FirstOrDefault();
            var objIList = new PatientViewModel();
            if (objResult != null)
            {
                objIList.FirstName = objResult.FirstName.ToString();
                objIList.LastName = objResult.LastName.ToString();
            }
            return objIList;

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
            var objResult = ObjectEntity1.Patient_List();
            var objIList = new List<PatientViewModel>();

            foreach (var patient in objResult)
            {
                PatientViewModel objPatientViewModel = new PatientViewModel();
                if (patient.Inactive == true)
                {
                    objPatientViewModel.FirstName = patient.FirstName;
                    objPatientViewModel.LastName = patient.LastName;
                    objPatientViewModel.PatientID = patient.PatientID;
                    objPatientViewModel.Age = patient.AGE.ToString();
                    objPatientViewModel.Inactive = patient.Inactive;
                    objPatientViewModel.FullName = patient.FullName;
                    objPatientViewModel.PatientFullNameWithInActiveStatus = patient.FirstName + ", " + patient.LastName + " -(inactive)";
                }
                else
                {
                    objPatientViewModel.FirstName = patient.FirstName;
                    objPatientViewModel.LastName = patient.LastName;
                    objPatientViewModel.PatientID = patient.PatientID;
                    objPatientViewModel.Age = patient.AGE.ToString();
                    objPatientViewModel.Inactive = patient.Inactive;
                    objPatientViewModel.FullName = patient.FullName;
                    objPatientViewModel.PatientFullNameWithInActiveStatus = patient.FirstName + ", " + patient.LastName;
                }
                objIList.Add(objPatientViewModel);
            }
            return objIList;
        }

        /// <summary>
        /// Get Patient Id By Order Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Patient Id</returns>
        public int GetPatientIdByOrderId(int orderId)
        {
            return ObjectEntity1.Orders.SingleOrDefault(i => i.OrderID == orderId).PatientID;
        }


        #region Get Patient FullName

        public string GetPatientFullName(int patientId)
        {
            return ObjectEntity1.Patients.Select(i => new { PatientId = i.PatientID, FullName = i.FirstName + " " + i.LastName }).SingleOrDefault(i => i.PatientId == patientId).FullName;
        }

        #endregion

        public PatientViewModel GetPatientDataById(int PatientId)
        {
            var objResult = ObjectEntityPart2.ssp_PatientInfoByPatientId(PatientId).FirstOrDefault();
         
            var objIList = new PatientViewModel();
            Mapper.CreateMap<ssp_PatientInfoByPatientId_Result, PatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Updating reasonid for the deleted autoship
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ReasonId"></param>
        public void UpdatePatientAfterDeleingAutoship(int PatientId, int ReasonId)
        {

            Patient objPatient = new Patient();
            //
            //Getting Patient record
            objPatient = ObjectEntity1.Patients.SingleOrDefault(i => i.PatientID == PatientId);
            objPatient.AutoshipCancelReasonID = ReasonId;

            //
            //Update the record
            ObjectEntity1.SaveChanges();


        }
    }
}
