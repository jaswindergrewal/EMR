using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ManageProspectViewModel
    {


    }

    public class MarketingActivityViewModel
    {
        public int MarketingActivityID { get; set; }
        public int SourceID { get; set; }
        public string SourceType { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public int CapmpaignID { get; set; }
        public Nullable<decimal> Money_Spent { get; set; }
    }

    public class ClinicsViewModel
    {
        public int ClinicID { get; set; }
        public string ClinicName { get; set; }

    }

    public class AttendViewModel
    {
        public int ProspectID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MainPhone { get; set; }
        public string Email { get; set; }
        public string Appointment { get; set; }
        public string Attendee { get; set; }

    }

    public class ManageGrdProspectViewModel
    {
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
        public string StaffName { get; set; }
        public string MarketingSources { get; set; }
        public int AppointmentID { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }

    }

    public class ManageCampaignViewModel
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
    }

    public class CRM_Campaigns_ViewModel
    {

        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public Nullable<decimal> MarketingBudget { get; set; }
        public string CampaignType { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string MarketingSources { get; set; }        
        public int CampaignTypeID { get; set; }
        public bool Enabled { get; set; }
        
    }

    public class CRM_Status_ViewModel
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public bool Active_YN { get; set; }
    }
    public class CRM_MarketingSources_ViewModel
    {
        public int MarketingSourceID { get; set; }
        public string MarketingSourceName { get; set; }
        public bool Active_YN { get; set; }
    }

    public class CRM_CampaignType_ViewModel
    {
        public int CampaignID { get; set; }
        public string CampaignType { get; set; }
        public bool IsActive{ get; set; }
    }
}
