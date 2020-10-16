using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ProblemListViewModel
    {
        
    }

    public class ProblemSymptEditViewModel
    {
        public string SymptomName { get; set; }
        public int Severity_num { get; set; }
        public int Priority_num { get; set; }
        public string Dir { get; set; }
    }

    public class ProblemSymptListViewModel
    {
        public string SymptomName { get; set; }
        public Nullable<int> SymptomID { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public string Dir { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> Active_YN { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<int> BeingAddressed_YN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> PatientID { get; set; }


    }

    public class ProblemSymptInsertListViewModel
    {
        public string SymptomName { get; set; }
        public Nullable<int> SymptomID { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public string Dir { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<bool> BeingAddressed_YN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> PatientID { get; set; }



    }

    public class MisDiagnosisListViewModel
    {
        public int ProbDiagID { get; set; }
        public Nullable<int> SymptomID { get; set; }
        public Nullable<int> DiagnosisID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> Active_YN { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<bool> BeingAddressed_YN { get; set; }
        public string Diag_Title{ get; set; }
        public string ICD9_Code { get; set; }
        public string LineColor { get; set; }
        public string BeingAddressedLink { get; set; }
    }

    public class DiagnosisListViewModel
    {
        public int ProbDiagID { get; set; }
        public Nullable<int> SymptomID { get; set; }
        public Nullable<int> DiagnosisID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> Active_YN { get; set; }
        public Nullable<decimal> Severity_num { get; set; }
        public Nullable<decimal> Priority_num { get; set; }
        public Nullable<System.DateTime> Inactive_Date { get; set; }
        public Nullable<int> BeingAddressed_YN { get; set; }
        public string Diag_Title { get; set; }
        public string ICD9_Code { get; set; }
        public string LineColor { get; set; }
        public string BeingAddressedLink { get; set; }
        public int apt { get; set; }
        public int Diagnosis_ID { get; set; }
    }


    public class SymptomProblemListViewModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }


}
