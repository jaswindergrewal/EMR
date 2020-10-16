using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public partial class LabReportViewModel
    {
        public int GroupID { get; set; }
        public Nullable<int> PanelID { get; set; }
        public string GroupName { get; set; }       
        public string PanelName { get; set; }
        public int TriggerID { get; set; }
        public string TriggerName { get; set; }

    }

    public partial class LabReportGroupViewModel
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

    public partial class LabReportsPanelViewModel
    {
        public int PanelID { get; set; }
        public string PanelName { get; set; }
        public string PanelDescrip { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class LabReportGroupTestViewModel
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

        public int TestID { get; set; }
        public string TestName { get; set; }
        public DateTime LastUsed { get; set; }

    }

    public partial class LabReportTestModel
    {
        public int TestID { get; set; }
        public string TestName { get; set; }       
    
    }

    [Serializable]
    public class LabReportTestModeltest
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
    }


    public partial class LabReportTriggerModel
    {
        public int TriggerID { get; set; }
        public string TriggerName { get; set; }
        public string TriggerDescription { get; set; }
    }

    public partial class LabReportConditionModel
    {
        public int ConditionID { get; set; }
        public string ConditionName { get; set; }
        public string ConditionDescrip { get; set; }
        public string Sex { get; set; }
        public int TriggerID { get; set; }
    }

    public partial class ReportTypeViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string ActualName { get; set; }
        
    }

    public partial class ReportListViewModel
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public bool IsActive { get; set; }
        public int ReportTypeId { get; set; }

        public string ActualName { get; set; }
    }
}
