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
    
    public partial class lab_AddressDetails
    {
        public int ID { get; set; }
        public int OrderSegmentId { get; set; }
        public string LabName { get; set; }
        public string LabAddress { get; set; }
        public string LabCity { get; set; }
        public string LabState { get; set; }
        public string LabZipCode { get; set; }
        public string LabDirector { get; set; }
        public string LabPhone { get; set; }
        public string LabCode { get; set; }
    
        public virtual lab_OrderSegmentDetails lab_OrderSegmentDetails { get; set; }
    }
}
