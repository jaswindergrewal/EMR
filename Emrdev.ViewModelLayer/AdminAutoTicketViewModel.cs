using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminAutoTicketViewModel
    {
        public int AutoTicketID { get; set; }
        public string AutoticketName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int FollowUp_Type_ID { get; set; }
        public int Assigned { get; set; }
        public int DeptAssign { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> LastSent { get; set; }
        public int CreatedID { get; set; }
        public int Frequency { get; set; }
        public string FrequencyType { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public string CreatedBY { get; set; }
        public string followup_type_desc { get; set; }
    
    }
}
