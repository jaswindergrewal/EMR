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
    
    public partial class intake_form_personal_info
    {
        public int personal_info_id { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<int> master_form_id { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public Nullable<int> age { get; set; }
        public string marital_status { get; set; }
        public string education { get; set; }
        public string current_occupation { get; set; }
        public Nullable<bool> occupation_enjoy_YN { get; set; }
        public Nullable<bool> occupation_stress_YN { get; set; }
        public Nullable<bool> occupation_fulfill_YN { get; set; }
        public Nullable<bool> occupation_hazardous_YN { get; set; }
        public Nullable<bool> retired_YN { get; set; }
        public string retired_occupation { get; set; }
        public Nullable<System.DateTime> retired_date_of { get; set; }
        public Nullable<bool> retired_happy_YN { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
    }
}