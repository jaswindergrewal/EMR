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
    
    public partial class intake_form_habits
    {
        public int habit_id { get; set; }
        public Nullable<int> master_form_id { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public string alcohol_type { get; set; }
        public string alcohol_amount { get; set; }
        public string alcohol_frequency { get; set; }
        public string drugs_type { get; set; }
        public string drugs_amount { get; set; }
        public string drugs_frequency { get; set; }
        public Nullable<int> cig_packs_per_day { get; set; }
        public Nullable<int> cig_years { get; set; }
        public Nullable<int> chew_amount_per_day { get; set; }
        public Nullable<int> chew_years { get; set; }
        public Nullable<bool> i_want_to_quit_YN { get; set; }
        public Nullable<int> caffeine_serv_per_day { get; set; }
        public Nullable<int> nutrasweet_serv_per_day { get; set; }
        public Nullable<int> saccharin_serv_per_day { get; set; }
        public Nullable<int> msg_serv_per_day { get; set; }
        public Nullable<int> patient_id { get; set; }
    }
}
