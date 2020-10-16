using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabPatientsViewModel
    {
        public int ID { get; set; }
        public System.DateTime LastChanged { get; set; }
        public Nullable<int> CorrespondingPatientID { get; set; }
        public int MessageID { get; set; }
        public Nullable<int> SetID { get; set; }
        public string PatientExternalID { get; set; }
        public string PatientInternalID { get; set; }
        public string AlternatePatientID { get; set; }
        public string PatientNameLastName { get; set; }
        public string PatientNameFirstName { get; set; }
        public string PatientNameMiddleName { get; set; }
        public string PatientNameSuffix { get; set; }
        public string PatientNamePrefix { get; set; }
        public string PatientNameDegree { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string PatientAddressStreetAddress { get; set; }
        public string PatientAddressOtherDesignation { get; set; }
        public string PatientAddressCity { get; set; }
        public string PatientAddressState { get; set; }
        public string PatientAddressZipCode { get; set; }
        public string PatientAddressCountry { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PatientAccountNumber { get; set; }
        public string PatientSSN { get; set; }
        public int PatientId { get; set; }
    }
}
