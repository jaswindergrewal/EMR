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
    
    public partial class ssp_GetCriticalTask_Result
    {
        public bool Received { get; set; }
        public bool Requested { get; set; }
        public bool Reviewed { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public System.DateTime RequestedDate { get; set; }
        public System.DateTime ReviewedDate { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Upload_Title { get; set; }
        public string Upload_Path { get; set; }
    }
}
