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
    
    public partial class apt_PendingConsults_Result
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> Range_Start { get; set; }
        public Nullable<System.DateTime> Range_End { get; set; }
        public string Clinic { get; set; }
        public string followup_type_desc { get; set; }
        public Nullable<int> apt_id { get; set; }
        public int patientid { get; set; }
        public int followup_id { get; set; }
        public Nullable<System.DateTime> dateentered { get; set; }
    }
}
