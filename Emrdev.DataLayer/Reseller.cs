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
    
    public partial class Reseller
    {
        public int ResellerID { get; set; }
        public string BusinessName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
        public string Description { get; set; }
        public Nullable<int> SalesRep { get; set; }
        public string Notes { get; set; }
        public Nullable<int> StatusID { get; set; }
        public bool Active_YN { get; set; }
        public Nullable<bool> AttendedDinner { get; set; }
        public int EventID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public Nullable<System.DateTime> DateEnrolled { get; set; }
        public bool CoManageAgreement { get; set; }
        public Nullable<System.DateTime> CoManageDate { get; set; }
        public bool ContractSigned { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public Nullable<int> ResellerMarketingSourceID { get; set; }
        public string LeadStatus { get; set; }
        public bool ReminderSent { get; set; }
    }
}
