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
    
    public partial class ssp_GetPendingBloodDrawsListByClinic_Result
    {
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<System.DateTime> value { get; set; }
        public string EmployeeName { get; set; }
        public string PatientName { get; set; }
        public bool FirstCall { get; set; }
        public string FirstNotes { get; set; }
        public bool SecondCall { get; set; }
        public string SecondNotes { get; set; }
        public bool FinalCall { get; set; }
        public string FinalNotes { get; set; }
        public bool LetterSent { get; set; }
        public int PatientID { get; set; }
        public string LetterNotes { get; set; }
        public int ID { get; set; }
        public string Clinic { get; set; }
    }
}
