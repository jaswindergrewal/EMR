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
    
    public partial class ssp_GetPatient_ProblemSymptoms_Result
    {
        public int SymptomID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string SymptomName { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public string Dir { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> Active_YN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<int> BeingAddressed_YN { get; set; }
    }
}