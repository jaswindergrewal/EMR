using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public partial class RecurringAppointmentViewModel
    {
        public string Provider { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ApptType { get; set; }
        public int AppointmentID { get; set; }
    }

    public partial class Apt_Rec_ViewModel
    {
        public int apt_id { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public Nullable<System.DateTime> Arrived_time { get; set; }
        public Nullable<System.DateTime> Seen_nurse_time { get; set; }
        public Nullable<System.DateTime> Seen_dr_time { get; set; }
        public Nullable<System.DateTime> closed_time { get; set; }
        public Nullable<System.DateTime> prescriptprint_time { get; set; }
        public Nullable<System.DateTime> followUp_time { get; set; }
        public Nullable<bool> closed_yn { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public Nullable<System.DateTime> ApptEnd { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> AppointmentTypeID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<bool> AllDay { get; set; }
        public Nullable<bool> EmailOnChange { get; set; }
        public Nullable<int> Results { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string ActionNeeded { get; set; }
        public int SaleMade_yn { get; set; }
        public string Note { get; set; }
        public bool LabsCheckedIn { get; set; }
    }
}