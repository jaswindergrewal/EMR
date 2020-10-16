using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabScehduleGroupsViewModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string Descrip { get; set; }
        public string SearchText { get; set; }
        public string FullTestName { get; set; }
        public Nullable<int> Male { get; set; }
        public Nullable<int> Female { get; set; }
        public Nullable<bool> InRange { get; set; }
        public Nullable<System.DateTime> LastComplete { get; set; }
        public Nullable<int> Days { get; set; }
    }

    public class LabScheduleTestsViewModel
    {
        public int? TestID { get; set; }
        public int? GroupID { get; set; }
        public string TestName { get; set; }        
    }
}
