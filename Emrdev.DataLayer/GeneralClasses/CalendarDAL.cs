using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

using AutoMapper;
using Emrdev.ViewModelLayer;

using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class CalendarDAL : ObjectEntity,IRepositary
    {
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            //throw new NotImplementedException();
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            //throw new NotImplementedException();
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            //throw new NotImplementedException();
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {

            throw new NotImplementedException();
            // _ObjectEntity.Set<T>();
        }
        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        #region IRepositary Members


        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion



        #region IRepositary Members


        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// get data to fill the Dropdown  from the database 
        /// </summary>

        public List<CalendarViewModel> GetCalendarDetails()
        {
            var objResult = ObjectEntity1.Admin_Get().ToList();
            var objIList = new List<CalendarViewModel>();
            Mapper.CreateMap<Admin_Get_Result, CalendarViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get the combined schedule for the Providers
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public CombinedScheduleViewModel GetCombinedScheduled(int EventID)
        {
            var objResult = ObjectEntity1.Appointment_GetByID(EventID);
            CombinedScheduleViewModel objIList = new CombinedScheduleViewModel();
           

            if (objResult != null)
            {
                foreach (var entity in objResult.ToList())
                {
                    objIList.ApptStart = Convert.ToDateTime(entity.ApptStart);
                    objIList.Notes = entity.Notes;
                    objIList.Patient = entity.Patient;
                    objIList.ResultName = entity.ResultName;
                    objIList.StatusName = entity.StatusName;
                    objIList.TypeName = entity.TypeName;
           

                }
            }
            return objIList;
        }

        public List<CalendarFollowupViewModel> GetCalendarFollowups()
        {
            var objResult = ObjectEntity1.ssp_Appointment_FollowUp().ToList();
            var objIList = new List<CalendarFollowupViewModel>();
            Mapper.CreateMap<ssp_Appointment_FollowUp_Result, CalendarFollowupViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void UpdateProvider(int PatientID, int ProviderID)
        {
            ObjectEntity1.ssp_UpdateProvider(PatientID, ProviderID);
        }

        public int UpdateCalAppointments(int PatientID, int ProviderID, string LMCPhys, string patient, int ApptTypeID)
        {
            ObjectParameter providerIdOUT = new ObjectParameter("providerIdOUT", typeof(global::System.Int32));
            providerIdOUT.Value = DBNull.Value;
            ObjectEntity1.ssp_UpdateCalAppointments(PatientID, ProviderID, LMCPhys, patient, ApptTypeID, providerIdOUT);
            return (int)providerIdOUT.Value;
        }

        public List<CalAppointmentViewModel> GetAppointment(int PatientID)
        {
            var objResult = ObjectEntity1.Appointment_GetByPatientID(PatientID).ToList();
            var objIList = new List<CalAppointmentViewModel>();
            Mapper.CreateMap<Appointment_GetByPatientID_Result, CalAppointmentViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<CalStatusViewModel> GetCalStatus()
        {
            var objResult = ObjectEntity1.ssp_GetCalStatusList().ToList();
            var objIList = new List<CalStatusViewModel>();
            Mapper.CreateMap<ssp_GetCalStatusList_Result, CalStatusViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<CalStatusViewModel> GetCalStatusLog(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetCalStatusLogByPatient(PatientId).ToList();
            var objIList = new List<CalStatusViewModel>();
            Mapper.CreateMap<ssp_GetCalStatusLogByPatient_Result, CalStatusViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertUpdateCalStatus(CalStatusViewModel CalStatus)
        {
            StatusProvider provider = null;
            if (CalStatus.StatusId == 0)
            {
                provider = new StatusProvider();
                provider.Active = true;
            }
            else
            {
                provider = Get<StatusProvider>(o => o.StatusId == CalStatus.StatusId);
                provider.Active = CalStatus.Active;
            }


            provider.StatusName = CalStatus.StatusName;
            provider.Ticket = CalStatus.Ticket;
            provider.TicketText = CalStatus.TicketText;
            provider.CalledDays = CalStatus.CalledDays;
            if (provider.StatusId > 0)
            {
                Edit<StatusProvider>(provider);
            }
            else { Create<StatusProvider>(provider); }
        }

        public void InsertUpdateStatusLog(CalStatusViewModel CalStatus)
        {
            ObjectEntity1.ssp_insertPatientLog(CalStatus.PatientId,CalStatus.StatusLogId,CalStatus.StatusId,CalStatus.IsTicketSet,CalStatus.StaffId);
        }
        public void RemoveStatusLog(CalStatusViewModel CalStatus)
        {
            ObjectEntity1.ssp_RemovePatientLog(CalStatus.PatientId,CalStatus.StatusLogId,CalStatus.StatusId,CalStatus.IsTicketSet,CalStatus.StaffId);
        }

    }
}
