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
    
    public partial class Patients_CriticalTasksGetOne_Result
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public bool Requested { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public bool Received { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public bool Reviewed { get; set; }
        public Nullable<System.DateTime> ReviewedDate { get; set; }
    }
}