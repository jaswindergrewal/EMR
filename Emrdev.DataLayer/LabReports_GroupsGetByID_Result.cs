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
    
    public partial class LabReports_GroupsGetByID_Result
    {
        public int GroupID { get; set; }
        public Nullable<int> PanelID { get; set; }
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }
        public string HighText { get; set; }
        public string NormalText { get; set; }
        public string LowText { get; set; }
        public string HighFemale { get; set; }
        public string LowFemale { get; set; }
        public string NormalFemale { get; set; }
        public decimal LongevityHighValue { get; set; }
        public decimal LongevityLowValue { get; set; }
        public Nullable<decimal> AcceptableHighValue { get; set; }
        public Nullable<decimal> AcceptableLowValue { get; set; }
        public Nullable<decimal> FemaleLongevityHigh { get; set; }
        public Nullable<decimal> FemaleLongevityLow { get; set; }
        public Nullable<decimal> FemaleAcceptableHigh { get; set; }
        public Nullable<decimal> FemaleAcceptableLow { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int SortOrder { get; set; }
        public bool ShowGraph { get; set; }
        public decimal ChartBottom { get; set; }
    }
}
