using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PendingMedRecordsViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Range_Start { get; set; }
        public DateTime? Range_End { get; set; }
        public string Clinic { get; set; }
        public string followup_type_desc { get; set; }
        public int? apt_id { get; set; }
        public int PatientID { get; set; }
        public int followup_id { get; set; }
        public DateTime? dateentered { get; set; }
    }
}
