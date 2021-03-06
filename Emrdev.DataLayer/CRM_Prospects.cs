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
    
    public partial class CRM_Prospects
    {
        public CRM_Prospects()
        {
            this.CRM_Attendees = new HashSet<CRM_Attendees>();
            this.CRM_Log = new HashSet<CRM_Log>();
            this.CRM_MarketingSource_Prospects = new HashSet<CRM_MarketingSource_Prospects>();
            this.CRM_Registrants = new HashSet<CRM_Registrants>();
        }
    
        public int ProspectID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MainPhone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public string ContactMethod { get; set; }
        public int StatusID { get; set; }
        public string Notes { get; set; }
        public bool Flagged { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string MarketingSources { get; set; }
        public int AppointmentID { get; set; }
        public bool ActiveYN { get; set; }
    
        public virtual ICollection<CRM_Attendees> CRM_Attendees { get; set; }
        public virtual ICollection<CRM_Log> CRM_Log { get; set; }
        public virtual ICollection<CRM_MarketingSource_Prospects> CRM_MarketingSource_Prospects { get; set; }
        public virtual CRM_Status CRM_Status { get; set; }
        public virtual ICollection<CRM_Registrants> CRM_Registrants { get; set; }
    }
}
