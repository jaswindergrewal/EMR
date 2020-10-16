using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class EmailTemplateViewModel
    {
        public int TemplateID { get; set; }
        public string TemplateDesc { get; set; }
        public int AppointmentId { get; set; }
        public int Id { get; set; }
    }

    public class CRMEmailTemplateViewModel
    {
        public int Id { get; set; }
        public string WufooFormLink { get; set; }
        public string EmailDescription { get; set; }
        public bool IsActive { get; set; }
    }

    public class MergedPatientViewModel
    {
        public int MergedPatientID { get; set; }
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string WorkPhone { get; set; }
        public string email { get; set; }
        public string sex { get; set; }
        public string clinic { get; set; }
        public int actualPatientID { get; set; }
        public string CreatedBY { get; set; }
        public string ActualPatient { get; set; }
        public int RecordCount { get; set; }
        public int Patientid { get; set; }
    }

    public class SalesAccountCodeViewModel
    {
        public int Id { get; set; }
        public int salesAccountCode { get; set; }
    }
}
