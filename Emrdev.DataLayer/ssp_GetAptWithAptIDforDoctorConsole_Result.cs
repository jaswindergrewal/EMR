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
    
    public partial class ssp_GetAptWithAptIDforDoctorConsole_Result
    {
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<System.DateTime> ApptStart { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<decimal> InvoiceDue { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<System.DateTime> InvoiceDueDate { get; set; }
        public Nullable<bool> InvoicePaid { get; set; }
        public Nullable<System.DateTime> RenewalExcExpire { get; set; }
        public string RenewalException { get; set; }
        public string RenewalMonth { get; set; }
        public Nullable<int> TermsInMonths { get; set; }
        public Nullable<int> BloodWorkCount { get; set; }
    }
}
