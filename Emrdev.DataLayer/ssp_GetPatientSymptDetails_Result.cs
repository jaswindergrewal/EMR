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
    
    public partial class ssp_GetPatientSymptDetails_Result
    {
        public int ProbSymptID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string SymptomName { get; set; }
        public Nullable<int> Active_YN { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public Nullable<System.DateTime> inactive_date { get; set; }
        public Nullable<int> beingaddressed_YN { get; set; }
        public string Dir { get; set; }
    }
}
