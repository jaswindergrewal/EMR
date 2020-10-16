using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ProtocolViewModel
    {
        public DateTime DateEntered { get; set; }
        public string EmployeeName { get; set; }
        public int Protocol_ID { get; set; }
        public string Protocol_Title { get; set; }
        public int RecordCount { get; set; }
        public string Protocol_Desc { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<bool> Viewable_YN { get; set; }
        public DateTime Lastupdated { get; set; }
        public string Date_Entered { get; set; }
    }
}
