using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class CalendarBAL
    {
        CalendarDAL objDAL = new CalendarDAL();

        /// <summary>
        /// get data to fill the Dropdown  from the database 
        /// </summary>
        public List<CalendarViewModel> GetCalendarDetails()
        {
            return objDAL.GetCalendarDetails();
        }

        /// <summary>
        /// Get the combined schedule for the Providers
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public CombinedScheduleViewModel GetCombinedScheduled(int EventID)
        {
            return objDAL.GetCombinedScheduled(EventID);
        }

        /// <summary>
        /// Get the list of all pending followups
        /// </summary>
        /// <returns></returns>
        public List<CalendarFollowupViewModel> GetCalendarFollowups()
        {
            return objDAL.GetCalendarFollowups();
        }

        
        //update the provider if we change the provider in the appointment details
        public void UpdateProvider(int PatientID, int ProviderID)
        {
            objDAL.UpdateProvider(PatientID, ProviderID);
        }

        /// <summary>
        /// Update the calendar appointment in the databse
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="ProviderID"></param>
        /// <param name="LMCPhys"></param>
        /// <param name="patient"></param>
        /// <param name="ApptTypeID"></param>
        /// <returns></returns>
        public int UpdateCalAppointments(int PatientID, int ProviderID, string LMCPhys, string patient, int ApptTypeID)
        {
            return objDAL.UpdateCalAppointments(PatientID, ProviderID, LMCPhys, patient, ApptTypeID);
        }

        /// <summary>
        /// get the list of all apointments by patientid
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<CalAppointmentViewModel> GetAppointment(int PatientID)
        {
            return objDAL.GetAppointment(PatientID);
        }

        public List<CalStatusViewModel> GetCalStatus()
        {
            return objDAL.GetCalStatus().ToList();
        }

        public void InsertUpdateCalStatus(CalStatusViewModel CalStatus)
        {
            objDAL.InsertUpdateCalStatus(CalStatus);
        }

        public void InsertUpdateStatusLog(CalStatusViewModel CalStatus)
        {
            objDAL.InsertUpdateStatusLog(CalStatus);
        }

        public void RemoveStatusLog(CalStatusViewModel CalStatus)
        {
            objDAL.RemoveStatusLog(CalStatus);
        }
        public List<CalStatusViewModel> GetCalStatusLog(int PatientId)
        {
            return objDAL.GetCalStatusLog(PatientId).ToList();
        }

    }
}
