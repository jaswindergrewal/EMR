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
    
    public partial class ssp_GetPatientListForAppointmentDate_Result
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPreference { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public Nullable<bool> Work_CB_Only { get; set; }
        public Nullable<bool> Work_Nomessage { get; set; }
        public Nullable<bool> Cell_CB_Only { get; set; }
        public Nullable<bool> Cell_NoMessage { get; set; }
        public Nullable<bool> Home_CB_Only { get; set; }
        public Nullable<bool> Home_NoMessage { get; set; }
        public string ApptStart { get; set; }
        public string ApptEnd { get; set; }
    }
}
