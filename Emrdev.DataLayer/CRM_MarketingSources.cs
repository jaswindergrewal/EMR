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
    
    public partial class CRM_MarketingSources
    {
        public CRM_MarketingSources()
        {
            this.CRM_MarketingActivity = new HashSet<CRM_MarketingActivity>();
            this.CRM_MarketingSource_Campaigns = new HashSet<CRM_MarketingSource_Campaigns>();
            this.CRM_MarketingSource_Prospects = new HashSet<CRM_MarketingSource_Prospects>();
        }
    
        public int MarketingSourceID { get; set; }
        public string MarketingSourceName { get; set; }
        public bool Active_YN { get; set; }
    
        public virtual ICollection<CRM_MarketingActivity> CRM_MarketingActivity { get; set; }
        public virtual ICollection<CRM_MarketingSource_Campaigns> CRM_MarketingSource_Campaigns { get; set; }
        public virtual ICollection<CRM_MarketingSource_Prospects> CRM_MarketingSource_Prospects { get; set; }
    }
}
