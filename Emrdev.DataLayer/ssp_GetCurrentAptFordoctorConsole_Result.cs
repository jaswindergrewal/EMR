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
    
    public partial class ssp_GetCurrentAptFordoctorConsole_Result
    {
        public int apt_Id { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public Nullable<System.DateTime> ApptEnd { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string StatusName { get; set; }
    }
}
