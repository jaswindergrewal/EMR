using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ContactRecordCloseViewModel
    {
        public int ContactID { get; set; }
        public string AptTypeDesc { get; set; }
        public int ? PatientID { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime ? ContactDateEntered { get; set; }
        public bool ? FollowUP_Completed { get; set; }
        public DateTime ? FollowUp_ActualDate { get; set; }
        public DateTime ? FollowUp_Date { get; set; }
        public string MessageBody { get; set; }
        public bool ? ReqsFollow_YN { get; set; }
        public string FollowUpBody { get; set; }
        public string Cat_Desc { get; set; }
     }
}
