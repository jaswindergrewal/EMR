//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class lab_Patients
    {
        public lab_Patients()
        {
            this.lab_CommonOrderSegments = new HashSet<lab_CommonOrderSegments>();
            this.lab_GuarantorSegments = new HashSet<lab_GuarantorSegments>();
            this.lab_InsuranceSegments = new HashSet<lab_InsuranceSegments>();
        }
    
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
    
        public virtual ICollection<lab_CommonOrderSegments> lab_CommonOrderSegments { get; set; }
        public virtual ICollection<lab_GuarantorSegments> lab_GuarantorSegments { get; set; }
        public virtual ICollection<lab_InsuranceSegments> lab_InsuranceSegments { get; set; }
        public virtual lab_Messages lab_Messages { get; set; }
    }
}