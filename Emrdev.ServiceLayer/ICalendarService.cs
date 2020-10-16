using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalendarService" in both code and config file together.
    [ServiceContract]
    public interface ICalendarService
    {
        [OperationContract]
        List<CalendarViewModel> GetCalendarDetails();

        [OperationContract]
        CombinedScheduleViewModel GetCombinedScheduled(int EventID);

        [OperationContract]
        List<CalendarFollowupViewModel> GetCalendarFollowups();

        [OperationContract]
        void UpdateProvider(int PatientID, int ProviderID);

        [OperationContract]
        int UpdateCalAppointments(int PatientID, int ProviderID, string LMCPhys, string patient, int ApptTypeID);

        [OperationContract]
        List<CalAppointmentViewModel> GetAppointment(int PatientID);

        [OperationContract]
        List<CalStatusViewModel> GetCalStatus();

        [OperationContract]
        void InsertUpdateCalStatus(CalStatusViewModel CalStatus);

        [OperationContract]
        List<CalStatusViewModel> GetCalStatusLog(int PatientId);

        [OperationContract]
        void InsertUpdateStatusLog(CalStatusViewModel CalStatus);

        [OperationContract]
        void RemoveStatusLog(CalStatusViewModel CalStatus);
    }
}
