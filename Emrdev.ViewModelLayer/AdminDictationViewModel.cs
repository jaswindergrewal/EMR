using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminDictationViewModel
    {
    }

    public class Dictation_Diagnosis_ViewModel
    {
        public int DiagnosisID { get; set; }
        public string DiagnosisName { get; set; }
        public string DiagnosisDescrip { get; set; }
        public string ICDCode { get; set; }
        public string KeyWords { get; set; }
        public bool Viewable_YN { get; set; }
    }

    public class Dictation_Plan_ViewModel
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public string PlanDescrip { get; set; }
        public string Category { get; set; }
        public string KeyWords { get; set; }
        public bool Viewable_YN { get; set; }
        public Nullable<int> DiagnosisID { get; set; }
    }

    public class Dictation_PlanDiagnosis_ViewModel
    {

        public int PlanDiagID { get; set; }
        public int PlanID { get; set; }
        public int DiagnosisID { get; set; }
    }
}
