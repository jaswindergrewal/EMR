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
    
    public partial class ssp_GetPatient_MiscDiag_Result
    {
        public int ProbDiagID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<bool> BeingAddressed_YN { get; set; }
        public string Diag_Title { get; set; }
        public string ICD9_Code { get; set; }
    }
}
