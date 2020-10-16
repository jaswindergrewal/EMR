using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LandingPageViewModel
    {
    
    }

    public class MyTicketsViewModel
    {
        public Nullable<int> PatientID { get; set; }
        public string Name { get; set; }
        public  Nullable<DateTime> CreateDate { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public Nullable<int> FollowUp_ID { get; set; }
        public bool ChangeColor { get; set; }
        public string Assigned { get; set; }
        public string InProgress { get; set; }
        public Nullable<int> DaysOld { get; set; }
        public Nullable<int> RecordCount { get; set; }
        
    }

    public class MyGroupTicketViewModel
    {
        public Nullable<int> PatientID { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public int FollowUp_ID { get; set; }
        public bool ChangeColor { get; set; }
        public string Assigned { get; set; }
        public string InProgress { get; set; }
        public Nullable<int> DaysOld { get; set; }

    }


}
