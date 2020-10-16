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
    
    public partial class lab_OrderSegmentDetails
    {
        public lab_OrderSegmentDetails()
        {
            this.lab_AddressDetails = new HashSet<lab_AddressDetails>();
            this.lab_DiagnosisSegments = new HashSet<lab_DiagnosisSegments>();
            this.lab_ObservationResultDetailSegments = new HashSet<lab_ObservationResultDetailSegments>();
        }
    
        public int ID { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int CommonOrderSegmentID { get; set; }
        public int SetID { get; set; }
        public string PlacerOrderNumber { get; set; }
        public string FillerOrderNumber { get; set; }
        public string UniversalServiceID { get; set; }
        public Nullable<System.DateTime> ObservationDateTime { get; set; }
        public string SpecimenActionCode { get; set; }
        public Nullable<System.DateTime> SpecimenReceivedDateTime { get; set; }
        public string OrderingProviderIDNumber { get; set; }
        public string OrderingProviderLastName { get; set; }
        public string OrderingProviderFirstName { get; set; }
        public string OrderingProviderMiddleName { get; set; }
        public string OrderingProviderSuffix { get; set; }
        public string OrderingProviderPrefix { get; set; }
        public string OrderingProviderDegree { get; set; }
        public string PlacerField1 { get; set; }
        public string PlacerField2 { get; set; }
        public string FillerField1 { get; set; }
        public string CLIAProviderID { get; set; }
        public string LabName { get; set; }
        public string LabAddress { get; set; }
        public string LabCity { get; set; }
        public string LabState { get; set; }
        public string LabZipCode { get; set; }
        public string LabDirector { get; set; }
        public Nullable<System.DateTime> ResultsReportOrStatusDateTime { get; set; }
        public string ResultStatus { get; set; }
        public string QuantityOrTiming { get; set; }
    
        public virtual ICollection<lab_AddressDetails> lab_AddressDetails { get; set; }
        public virtual lab_CommonOrderSegments lab_CommonOrderSegments { get; set; }
        public virtual ICollection<lab_DiagnosisSegments> lab_DiagnosisSegments { get; set; }
        public virtual ICollection<lab_ObservationResultDetailSegments> lab_ObservationResultDetailSegments { get; set; }
    }
}
