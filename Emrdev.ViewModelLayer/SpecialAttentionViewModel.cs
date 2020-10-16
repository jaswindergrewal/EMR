using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class SpecialAttentionViewModel
    {
        public int SpecialAttentionID { get; set; }
        public string FlagNotes { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> PatientID { get; set; }
      
        public Nullable<bool> FlagYN { get; set; }
        public string EmployeeName { get; set; }
    }
}
