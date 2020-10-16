using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AptFollowUpsViewModel
    {
        public int FollowUp_ID { get; set; }
        public Nullable<int> Apt_ID { get; set; }
        public Nullable<int> AptAssigned { get; set; }
        public Nullable<int> FollowUp_Cat { get; set; }
        public string FollowUp_Body { get; set; }
        public string FollowUp_Subject { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> Entered_By { get; set; }
        public Nullable<int> PatientID { get; set; }
        public bool FollowUp_Completed_YN { get; set; }
        public bool FollowUp_Assigned_YN { get; set; }
        public Nullable<System.DateTime> Range_Start { get; set; }
        public Nullable<System.DateTime> Range_End { get; set; }
        public Nullable<int> Severity { get; set; }
        public Nullable<int> Assigned { get; set; }
        public Nullable<int> DepartmentAssign { get; set; }
        public bool Private { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool FirstCall { get; set; }
        public bool SecondCall { get; set; }
        public bool FinalCall { get; set; }
        public bool Letter { get; set; }
        public string FirstCallNote { get; set; }
        public string SeconCallNote { get; set; }
        public string FinalCallNote { get; set; }
        public string LetterNote { get; set; }
    }
}
