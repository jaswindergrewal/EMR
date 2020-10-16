using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AddProspectViewModel
    {
    }

    public class MarketingSourceViewModel
    {
        public string MarketingSourceName { get; set; }
        public int MarketingSourceID { get; set; }

    }

    public class CRM_Events_ViewModel
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public System.DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string Notes { get; set; }
        public int CampaignID { get; set; }
        public string Location { get; set; }
        public string EventLength { get; set; }
        public string Walkins { get; set; }
        public Nullable<int> Appointments { get; set; }
        public Nullable<int> Callbacks { get; set; }
        public Nullable<int> OverallPerformance { get; set; }
        public Nullable<int> FacilityInteriorExterior { get; set; }
        public Nullable<int> VenueQuality { get; set; }
        public Nullable<int> Parking { get; set; }
        public Nullable<int> AudienceReaction { get; set; }
        public Nullable<int> AudienceQuality { get; set; }
        public bool Enabled { get; set; }
    }

    public class AcuityAppointment
    {

        public Nullable<int> id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public string endTime { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime datetimeCreated { get; set; }
        public DateTime datetime { get; set; }
        public decimal price { get; set; }
        public string Location { get; set; }
        public int calendarId { get; set; }
        public string type { get; set; }

    }

}
