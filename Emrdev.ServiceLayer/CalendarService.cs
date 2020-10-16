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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalendarService" in both code and config file together.
    public class CalendarService : ICalendarService
    {
        CalendarBAL objBAL = new CalendarBAL();
        public List<CalendarViewModel >GetCalendarDetails()
        {
            return objBAL.GetCalendarDetails();
        }

        public CombinedScheduleViewModel GetCombinedScheduled(int EventID)
        {
            return objBAL.GetCombinedScheduled(EventID);
        }

        public List<CalendarFollowupViewModel>GetCalendarFollowups()
        {
            return objBAL.GetCalendarFollowups();
        }

        public void UpdateProvider(int PatientID, int ProviderID)
        {
            objBAL.UpdateProvider(PatientID, ProviderID);
        }

        public int UpdateCalAppointments(int PatientID, int ProviderID, string LMCPhys, string patient, int ApptTypeID)
        {
            return objBAL.UpdateCalAppointments(PatientID, ProviderID, LMCPhys, patient, ApptTypeID);
        }

        public List<CalAppointmentViewModel> GetAppointment(int PatientID)
        {
            return objBAL.GetAppointment(PatientID);
        }

        public List<CalStatusViewModel> GetCalStatus()
        {
            return objBAL.GetCalStatus().ToList();
        }
        public void InsertUpdateCalStatus(CalStatusViewModel CalStatus)
        {
            objBAL.InsertUpdateCalStatus(CalStatus);
        }

        public void InsertUpdateStatusLog(CalStatusViewModel CalStatus)
        {
            objBAL.InsertUpdateStatusLog(CalStatus);
        }

        public void RemoveStatusLog(CalStatusViewModel CalStatus)
        {
            objBAL.RemoveStatusLog(CalStatus);
        }

        public List<CalStatusViewModel> GetCalStatusLog(int PatientId)
        {
            return objBAL.GetCalStatusLog(PatientId).ToList();
        }

    }
}
