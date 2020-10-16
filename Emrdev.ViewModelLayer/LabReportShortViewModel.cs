using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabReportShortViewModel
    {
        public string ClientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public System.DateTime DOB { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string SSN { get; set; }
        public string RequisitionID { get; set; }
        public string SpecimenID { get; set; }
        public string ProviderLastName { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderMiddleName { get; set; }
        public Nullable<System.DateTime> CollectedDateTime { get; set; }
        public Nullable<System.DateTime> ReceivedDateTime { get; set; }
        public Nullable<System.DateTime> ReportedDateTime { get; set; }
        public string Result { get; set; }
        public int OrderSegmentID { get; set; }
        public int SetID { get; set; }
        public string ServiceID { get; set; }
        public string LabID { get; set; }
        public string LabName { get; set; }
        public string LabAddress { get; set; }
        public string LabCity { get; set; }
        public string LabState { get; set; }
        public string LabZip { get; set; }
        public string LabDirector { get; set; }
        public int ? TotalAge{ get; set; }
    }
}
