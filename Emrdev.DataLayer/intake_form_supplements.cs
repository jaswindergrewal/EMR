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
    
    public partial class intake_form_supplements
    {
        public int supplement_id { get; set; }
        public Nullable<int> master_form_id { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public Nullable<bool> multi_vitamin_YN { get; set; }
        public Nullable<bool> multi_vitamin_oneaday_YN { get; set; }
        public Nullable<bool> cal_mag_combo_YN { get; set; }
        public Nullable<bool> vitamin_a_YN { get; set; }
        public Nullable<bool> vitamin_b_YN { get; set; }
        public Nullable<bool> vitamin_c_YN { get; set; }
        public Nullable<bool> vitamin_e_YN { get; set; }
        public Nullable<bool> chon_gluc_combo_YN { get; set; }
        public Nullable<bool> fish_oil_YN { get; set; }
        public Nullable<bool> other_oil_YN { get; set; }
        public Nullable<bool> co_q_10_YN { get; set; }
        public Nullable<bool> herbal_blend_YN { get; set; }
        public string other_supplement_text { get; set; }
        public Nullable<int> patient_id { get; set; }
    }
}
