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
    
    public partial class CRM_Log
    {
        public int LogID { get; set; }
        public int OldStatus { get; set; }
        public int NewStatus { get; set; }
        public System.DateTime DateEntered { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public int ProspectID { get; set; }
    
        public virtual CRM_Prospects CRM_Prospects { get; set; }
    }
}