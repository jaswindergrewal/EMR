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
    
    public partial class MedicalHistory
    {
        public int HistoryID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<bool> Viewable_YN { get; set; }
        public string HistoryDesc { get; set; }
    }
}
