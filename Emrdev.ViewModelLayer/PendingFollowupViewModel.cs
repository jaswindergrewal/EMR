using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PendingFollowupViewModel
    {
        public string FollowUp_Type_Desc { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? Range_Start { get; set; }
        public DateTime? Range_End { get; set; }
        public bool? FollowUp_Completed_YN { get; set; }
        public string FollowUp_Body { get; set; }
        public string CloseLink { get; set; }
        public string PatientName { get; set; }
        public int? Apt_ID { get; set; }

    }

    public class ContactTypeViewModel
    {
        public int AptTypeID { get; set; }
        public string AptTypeDesc { get; set; }
    }


    public class ContactListViewModel
    {
        public int ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public string MessageBody { get; set; }
        public string AptTypeDesc { get; set; }
    }
}
