//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    
    public partial class Contact_Record_details_Result
    {
        public int ContactID { get; set; }
        public string AptTypeDesc { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EnteredBy { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public Nullable<bool> FollowUP_Completed { get; set; }
        public Nullable<System.DateTime> FollowUp_ActualDate { get; set; }
        public Nullable<System.DateTime> FollowUp_Date { get; set; }
        public string MessageBody { get; set; }
        public Nullable<bool> ReqsFollow_YN { get; set; }
        public string FollowUpBody { get; set; }
        public string Cat_Desc { get; set; }
    }
}
