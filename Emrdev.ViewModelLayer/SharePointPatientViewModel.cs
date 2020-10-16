using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class SharePointPatientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public string ClinicName { get; set; }
        public string ProviderName { get; set; }
        public int Id { get; set; }
        public Nullable<DateTime> StartRange { get; set; }
        public Nullable<DateTime> EndRange { get; set; }
        public int ClinicId { get; set; }
        public int ProviderId { get; set; }
        public Nullable<int> ApptDuration { get; set; }
    }
}
