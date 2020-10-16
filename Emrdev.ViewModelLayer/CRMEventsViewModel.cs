using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   public partial class CRMEventsViewModel
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

   public partial class ProspectViewmodel
   {

       public int ProspectID { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Phone { get; set; }
       public string Email { get; set; }
       public string Seminar { get; set; }
       public string HowHear { get; set; }
       public Nullable<System.DateTime> TimeStamp { get; set; }
   }

   public partial class AppointmentTimemodel
   {
       public string TimeValue { get; set; }
   }

   public partial class AppointmentTypeModel
   {
       public string TypeName { get; set; }
       public int ID { get; set; }
   }

   public partial class PlottGraphViewModel
   {
       public int Total { get; set; }
       public string MarketSourceName { get; set; }
       public int PatientConverted { get; set; }

   }


   public partial class CRMStatisticViewModel
   {
       public int Total { get; set; }
       public int ProspectAttended { get; set; }
       public int PatientConverted { get; set; }
       public int MedStartPatient { get; set; }
       public string MarketSourceName { get; set; }
   }
}
