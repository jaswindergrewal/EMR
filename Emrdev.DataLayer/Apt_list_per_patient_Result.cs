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
    
    public partial class Apt_list_per_patient_Result
    {
        public string TypeName { get; set; }
        public string ResultName { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public string ProviderName { get; set; }
        public int apt_id { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public Nullable<bool> closed_yn { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<bool> OVU { get; set; }
    }
}
