using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminResellerViewModel
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
        public bool Active_YN { get; set; }
        public Nullable<bool> AttendedDinner { get; set; }
        public int EventID { get; set; }
        public Nullable<System.DateTime> DateEnrolled { get; set; }
        public bool CoManageAgreement { get; set; }
        public Nullable<System.DateTime> CoManageDate { get; set; }
        public bool ContractSigned { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public string LeadStatus { get; set; }
        public string SalesRepName { get; set; }
        public string Status { get; set; }
        public Nullable<int> ResellerMarketingSourceID { get; set; }
        public Nullable<int> ResellerNumber { get; set; }
        public string ContactString { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }

    }

    public class AdminResellerContactViewModel
    {
        public int ResellerContactID { get; set; }
        public int ResellerID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string MessageBody { get; set; }
        public int EnteredBy { get; set; }
    
    }

    public class StatusViewModel
    {
        public string Status { get; set; }
        public int StatusID{ get; set; }
        public bool Active_YN { get; set; } 
    }

    public class EventViewModel
    {
        public string EventName { get; set; }
        public int EventID { get; set; }
        public bool Active_YN { get; set; } 
    }

    public class SaleRepViewModel
    {
        public string EmployeeName { get; set; }
        public int SalesRep { get; set; }
    }

    public class ResellerMarketingSourceViewModel
    {
        public string SourceName { get; set; }
        public int ResellerMarketingSourceID { get; set; }
        public bool Active_YN { get; set; } 
    }
}
