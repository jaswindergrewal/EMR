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
    
    public partial class intake_form_recent_tests
    {
        public int recent_tests_id { get; set; }
        public Nullable<int> master_form_id { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public Nullable<System.DateTime> chest_xray_date { get; set; }
        public string chest_xray_reason { get; set; }
        public string chest_xray_result { get; set; }
        public Nullable<System.DateTime> ekg_date { get; set; }
        public string ekg_reason { get; set; }
        public string ekg_result { get; set; }
        public Nullable<System.DateTime> egd_date { get; set; }
        public string egd_reason { get; set; }
        public string egd_result { get; set; }
        public Nullable<System.DateTime> colonoscopy_date { get; set; }
        public string colonoscopy_reason { get; set; }
        public string colonoscopy_result { get; set; }
        public Nullable<System.DateTime> ultrasound_date { get; set; }
        public string ultrasound_reason { get; set; }
        public string ultrasound_result { get; set; }
        public Nullable<System.DateTime> cat_scan_date { get; set; }
        public string cat_scan_reason { get; set; }
        public string cat_scan_result { get; set; }
        public Nullable<System.DateTime> mri_scan_date { get; set; }
        public string mri_scan_reason { get; set; }
        public string mri_scan_result { get; set; }
        public Nullable<System.DateTime> bone_density_date { get; set; }
        public string bone_density_reason { get; set; }
        public string bone_density_result { get; set; }
        public string other_test { get; set; }
        public Nullable<System.DateTime> other_date { get; set; }
        public string other_reason { get; set; }
        public string other_result { get; set; }
        public Nullable<int> patient_id { get; set; }
    }
}
