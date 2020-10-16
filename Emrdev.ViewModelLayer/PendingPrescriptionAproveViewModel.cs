using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PendingPrescriptionAproveViewModel
    {
        public int PrescriptionID { get; set; }
        public Nullable<System.DateTime> Approved_Date { get; set; }
        public Nullable<bool> Approved_YN { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<System.DateTime> Drug_DatePrescibed { get; set; }
        public string Drug_Dispenses { get; set; }
        public string Drug_Dose { get; set; }
        public Nullable<System.DateTime> Drug_EndDate { get; set; }
        public string Drug_NumbRefills { get; set; }
        public string DrugName { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<bool> viewable_yn { get; set; }
        public string AccessLevel { get; set; }
        public Nullable<bool> ThirdParty_YN { get; set; }

    }
}
