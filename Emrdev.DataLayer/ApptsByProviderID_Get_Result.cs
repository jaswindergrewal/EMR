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
    
    public partial class ApptsByProviderID_Get_Result
    {
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> ApptEnd { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public int apt_ID { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public int ApptTypeID { get; set; }
        public int StatusID { get; set; }
        public Nullable<bool> AllDay { get; set; }
        public string Email { get; set; }
        public Nullable<bool> EmailOnChange { get; set; }
        public string Notes { get; set; }
        public string Patient { get; set; }
        public string ActionNeeded { get; set; }
    }
}
