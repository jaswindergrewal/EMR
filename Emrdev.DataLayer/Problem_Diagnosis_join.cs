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
    using System.Collections.Generic;
    
    public partial class Problem_Diagnosis_join
    {
        public int ProbDiagID { get; set; }
        public Nullable<int> DiagnosisID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<bool> BeingAddressed_YN { get; set; }
    }
}
