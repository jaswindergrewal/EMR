using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ContacttblViewModel
    {
        public int ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public Nullable<int> AptType { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<bool> ReqsFollow_YN { get; set; }
        public string FollowUpBody { get; set; }
        public string MessageBody { get; set; }
        public Nullable<int> FollowUp_Staff { get; set; }
        public Nullable<System.DateTime> FollowUp_Date { get; set; }
        public Nullable<int> FollowUp_Category { get; set; }
        public Nullable<System.DateTime> FollowUp_ActualDate { get; set; }
        public Nullable<bool> FollowUP_Completed { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<int> Apt_ID { get; set; }
        public Nullable<int> FollowUp_ID { get; set; }
    }

    public class Contact_TypeVieweModel
    {
        public int AptTypeID { get; set; }
        public string AptTypeDesc { get; set; }
        public Nullable<bool> Viewable_yn { get; set; }
    }
}
