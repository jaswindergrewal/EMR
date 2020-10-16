using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminLabRemindersViewModel
    {
    
    
    
    }

    public class LabSymptomViewModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public string Assigned { get; set; }
    }

    public class DiagnosistblViewModel
    {
        public int Diagnosis_ID { get; set; }
        public string Diag_Title { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string ICD9_Code { get; set; }
        public Nullable<bool> Viewable_YN { get; set; }
        public int RecordCount { get; set; }
    }
    public class DiagnosisLabViewModel
    {
        public int DiagnosisLabID { get; set; }
        public int GroupID { get; set; }
        public int DiagnosisID { get; set; }
    }

    public class SymptomLabViewModel
    {
        public int SymptomLabID { get; set; }
        public int GroupID { get; set; }
        public int SymptomID { get; set; }
    }

    public class ICD10CodesViewmodel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string ICD10Code { get; set; }

        public bool IsActive { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}
