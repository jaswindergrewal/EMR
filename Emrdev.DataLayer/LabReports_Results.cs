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
    
    public partial class LabReports_Results
    {
        public string ObservationIdentifier { get; set; }
        public string ObservationValue { get; set; }
        public string Units { get; set; }
        public Nullable<System.DateTime> ObservationDateTime { get; set; }
        public Nullable<System.DateTime> ReceivedDateTime { get; set; }
        public Nullable<System.DateTime> ReportedDateTime { get; set; }
        public int PatientID { get; set; }
        public int TestID { get; set; }
        public string TestName { get; set; }
        public int GroupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sex { get; set; }
    }
}
