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
    
    public partial class Symptom_Diagn_join
    {
        public int Sympt_Diag_ID { get; set; }
        public Nullable<int> Symptom_ID { get; set; }
        public Nullable<int> Diagnosis_ID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> Viewable_YN { get; set; }
    }
}