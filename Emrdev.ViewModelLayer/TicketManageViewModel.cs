using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   public class TicketManageViewModel
    {
       public int ProcessID { get; set; }
       public string ProcessName { get; set; }
       public int Interval { get; set; }
       public bool Enabled { get; set; }
       public string Note { get; set; }
    }

   public class TicketPatientViewModel
   {
       public Nullable<System.DateTime> DateEntered { get; set; }
       public Nullable<int> Severity { get; set; }
       public string FollowUp_Subject { get; set; }
       public int FollowUp_ID { get; set; }
       public string EnteredBy { get; set; }
       public string FollowUp_Type_Desc { get; set; }
       public string Assigned { get; set; }
   }
}
