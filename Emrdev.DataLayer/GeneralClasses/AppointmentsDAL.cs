using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AppointmentsDAL : ObjectEntity, IRepositary
    {


        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<AppointmentViewModel> GetAppointsbyPatientID(int PatientId)
        {
            var objResult = ObjectEntity1.Apt_list_per_patient(PatientId).ToList();
            var objIList = new List<AppointmentViewModel>();
            Mapper.CreateMap<Apt_list_per_patient_Result, AppointmentViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        public List<OpenAppointmentViewModel> GetAppointmentsByClinic(string Clinic, int PageIndex, int PageSize)
        {//emr2017
            //var objResult = ObjectEntity1.ssp_GetOpenAptPageWise(PageIndex, PageSize, Clinic).ToList();
            var objIList = new List<OpenAppointmentViewModel>();
            //Mapper.CreateMap<ssp_GetOpenAptPageWise_Result, OpenAppointmentViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<OpenAppointmentViewModel> GetAppointmentsGap(string SearchType, int PageIndex, int PageSize)
        {//emr2017
            //var objResult = ObjectEntity1.ssp_Patient_Apt_Gap(SearchType, PageIndex, PageSize).ToList();
            var objIList = new List<OpenAppointmentViewModel>();
            //Mapper.CreateMap<ssp_Patient_Apt_Gap_Result, OpenAppointmentViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingFollowupClinicViewModel> GetPendingFollowupsByClinic(string Clinic, string OrderBy, int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_PendingFollowup_ByClinic(OrderBy, Clinic, PageIndex, PageSize).ToList();
            var objIList = new List<PendingFollowupClinicViewModel>();
            Mapper.CreateMap<ssp_PendingFollowup_ByClinic_Result, PendingFollowupClinicViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PrescriptionFor5Days> GetLastFiveDayPrescription(int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_GetLast5DaysPrescriptions(PageIndex, PageSize).ToList();
            var objIList = new List<PrescriptionFor5Days>();
            Mapper.CreateMap<ssp_GetLast5DaysPrescriptions_Result, PrescriptionFor5Days>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get the special AppointmentList
        /// jaswinder 14th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<SpecialAptViewModel> GetSpecialAppointment(int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_GetSpecial99Apt(PageIndex, PageSize).ToList();
            var objIList = new List<SpecialAptViewModel>();
            Mapper.CreateMap<ssp_GetSpecial99Apt_Result, SpecialAptViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}


