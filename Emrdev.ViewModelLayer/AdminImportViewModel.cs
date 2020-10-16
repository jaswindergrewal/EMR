using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminImportViewModel
    {
        public string TestName { get; set; }
        public DateTime? LastUsed { get; set; }
        public string CleanName { get; set; }
    }

    public class AdminLabReportsTestViewModel 
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public System.DateTime LastUsed { get; set; }
        public bool Hidden { get; set; }
        public int GroupID { get; set; }
    }
}
