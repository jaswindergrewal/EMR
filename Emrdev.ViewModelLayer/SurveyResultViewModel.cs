using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class SurveyResultViewModel
    {
        public int Sat_ID { get; set; }
        public string Patient_ID { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public Nullable<int> Sat_Overall { get; set; }
        public Nullable<int> Sat_CustServ { get; set; }
        public Nullable<int> Sat_Progress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RecordCount { get; set; }
    
    }

    public class TodaysContactViewModel
    {
        public string PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RecordCount { get; set; }
    }
}
