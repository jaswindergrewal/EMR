using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AestheticNotesViewModel
    {
        public int ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string MessageBody { get; set; }
        public string AptTypeDesc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EnteredBy { get; set; }
        public string PageTitle { get; set; }

    }

    public class AnestheticFollowupViewModel
    {
        public int FollowUp_ID { get; set; }
        public Nullable<int> Apt_ID { get; set; }
        public Nullable<System.DateTime> Range_Start { get; set; }
        public Nullable<System.DateTime> Range_End { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string FollowUp_Body { get; set; }
        public string EmployeeName { get; set; }
        public string FollowUp_Type_Desc { get; set; }
       
    }
}
