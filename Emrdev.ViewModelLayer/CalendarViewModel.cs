using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class CalendarViewModel
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public string TypeObject { get; set; }
        public string UpdateMethod { get; set; }
        public string InsertMethod { get; set; }
        public string SelectMethod { get; set; }
        public string SelectSingle { get; set; }
    }

    public class CombinedScheduleViewModel
    {
       
        public string Notes { get; set; }
        public string Patient { get; set; }
        public DateTime ApptStart { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public string ResultName { get; set; }
    }

    public class CalendarFollowupViewModel
    {
        public string Patient { get; set; }
        public string NoteText { get; set; }
        public string AppointmentType { get; set; }
        public string Provider { get; set; }
        public string Result { get; set; }
        public string HomePhone { get; set; }
        public string HomeHIPAA { get; set; }
        public string WorkPhone { get; set; }
        public string WorkHIPAA { get; set; }
        public string CellPhone { get; set; }
        public string CellHIPAA { get; set; }
        public string Notes { get; set; }
        public int ID { get; set; }
        public DateTime apptstart { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
    }

    public class CalAppointmentViewModel
    {
        public string Notes { get; set; }
        public string TypeName { get; set; }
        public string ProviderName { get; set; }
        public DateTime ApptStart { get; set; }

    }

    public class CalStatusViewModel
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool Ticket { get; set; }
        public string TicketText { get; set; }
        public bool Active { get; set; }

        public string EmployeeName { get; set; }
        public string StatusDate { get; set; }

        public bool IsActive { get; set; }
        public bool PatientStatus
        {
            get; set;
        }
        public int StatusLogId { get; set; }
        public bool IsTicketSet { get; set; }

        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public int CalledDays { get; set; }
    }

}
