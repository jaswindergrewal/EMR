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
    
    public partial class lab_ObservationResultDetailSegments
    {
        public int ID { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int OrderSegmentDetailID { get; set; }
        public int SetID { get; set; }
        public string ValueType { get; set; }
        public string ObservationIdentifier { get; set; }
        public string ObservationSubID { get; set; }
        public string ObservationValue { get; set; }
        public string Units { get; set; }
        public string ReferencesRange { get; set; }
        public string AbnormalFlags { get; set; }
        public string ObservationResultStatus { get; set; }
        public Nullable<System.DateTime> ObservationDateTime { get; set; }
        public string ProducerID { get; set; }
    
        public virtual lab_OrderSegmentDetails lab_OrderSegmentDetails { get; set; }
    }
}
