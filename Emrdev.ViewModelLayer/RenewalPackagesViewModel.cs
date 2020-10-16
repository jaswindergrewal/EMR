using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class RenewalPackagesViewModel
    {
        public int RenewalID { get; set; }
        public string PackageName { get; set; }
        public int Duration { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }   

    }

    public class PatientPackagesViewModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int RenewalID { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int Duration { get; set; }
    }

    public class MailChimpCampaignViewModel
    {
        public string MailChimpCampaignId { get; set; }
        public string MailChimpCampaignName { get; set; }
        public string MailChimpCampaignListId { get; set; }
    }
}
