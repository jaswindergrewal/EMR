using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   public class IntakeServiceViewModel
    {
        public int GoalId { get; set; }
        public Nullable<int> MasterFormId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<bool> BalanceHormones { get; set; }
        public Nullable<bool> StartHormones { get; set; }
        public Nullable<bool> ImproveEnergy { get; set; }
        public Nullable<bool> FeelBetter { get; set; }
        public Nullable<bool> FeelStronger { get; set; }
        public Nullable<bool> ImproveSleep { get; set; }
        public Nullable<bool> EliminateHotFlashes { get; set; }
        public Nullable<bool> EliminatePrescriptions { get; set; }
        public Nullable<bool> WeightLoss { get; set; }
        public Nullable<bool> StabalizePMS { get; set; }
        public Nullable<bool> StopHairLoss { get; set; }
        public Nullable<bool> SenseOfWellBeing { get; set; }
        public Nullable<bool> EnhanceImmuneSys { get; set; }
        public Nullable<bool> LessPain { get; set; }
        public string LessPainWhere { get; set; }
        public Nullable<bool> ImproveLibido { get; set; }
        public Nullable<bool> ImproveSexLife_YN { get; set; }
        public Nullable<bool> ImproveMuscle { get; set; }
        public Nullable<bool> ImproveMemory { get; set; }
        public Nullable<bool> BladderControl { get; set; }
        public Nullable<bool> ImproveSkin { get; set; }
        public Nullable<bool> BetterStamina { get; set; }
        public Nullable<bool> GeneralWellness { get; set; }
        public Nullable<bool> ReduceStress { get; set; }
        public Nullable<bool> ImproveMetabolism { get; set; }
        public string other { get; set; }
    }

   public class IntakeTestViewModel
   {
       public int recent_tests_id { get; set; }
       public Nullable<int> master_form_id { get; set; }
       public Nullable<System.DateTime> Date_Entered { get; set; }
       public Nullable<System.DateTime> chest_xray_date { get; set; }
       public string chest_xray_reason { get; set; }
       public string chest_xray_result { get; set; }
       public Nullable<System.DateTime> ekg_date { get; set; }
       public string ekg_reason { get; set; }
       public string ekg_result { get; set; }
       public Nullable<System.DateTime> egd_date { get; set; }
       public string egd_reason { get; set; }
       public string egd_result { get; set; }
       public Nullable<System.DateTime> colonoscopy_date { get; set; }
       public string colonoscopy_reason { get; set; }
       public string colonoscopy_result { get; set; }
       public Nullable<System.DateTime> ultrasound_date { get; set; }
       public string ultrasound_reason { get; set; }
       public string ultrasound_result { get; set; }
       public Nullable<System.DateTime> cat_scan_date { get; set; }
       public string cat_scan_reason { get; set; }
       public string cat_scan_result { get; set; }
       public Nullable<System.DateTime> mri_scan_date { get; set; }
       public string mri_scan_reason { get; set; }
       public string mri_scan_result { get; set; }
       public Nullable<System.DateTime> bone_density_date { get; set; }
       public string bone_density_reason { get; set; }
       public string bone_density_result { get; set; }
       public string other_test { get; set; }
       public Nullable<System.DateTime> other_date { get; set; }
       public string other_reason { get; set; }
       public string other_result { get; set; }
       public Nullable<int> patient_id { get; set; }
   }


   public class IntakeSurgeryViewModel
   {
       public int surgeries_id { get; set; }
       public Nullable<int> master_form_id { get; set; }
       public Nullable<System.DateTime> Date_Entered { get; set; }
       public Nullable<bool> hysterectomy_YN { get; set; }
       public Nullable<System.DateTime> hysterectomy_date { get; set; }
       public string hysterectomy_reason { get; set; }
       public Nullable<bool> appendix_YN { get; set; }
       public Nullable<System.DateTime> appendix_date { get; set; }
       public Nullable<bool> tonsils_YN { get; set; }
       public Nullable<System.DateTime> tonsils_date { get; set; }
       public Nullable<bool> gall_bladder_YN { get; set; }
       public Nullable<System.DateTime> gall_bladder_date { get; set; }
       public Nullable<bool> colon_YN { get; set; }
       public Nullable<System.DateTime> colon_date { get; set; }
       public string colon_reason { get; set; }
       public Nullable<bool> prostate_YN { get; set; }
       public Nullable<System.DateTime> prostate_date { get; set; }
       public string prostate_reason { get; set; }
       public Nullable<bool> orthopedic_YN { get; set; }
       public Nullable<System.DateTime> orthopedic_date { get; set; }
       public string orthopedic_reason { get; set; }
       public Nullable<bool> heart_YN { get; set; }
       public Nullable<System.DateTime> heart_date { get; set; }
       public string heart_reason { get; set; }
       public Nullable<bool> thyroid_YN { get; set; }
       public Nullable<System.DateTime> thyroid_date { get; set; }
       public string thyroid_reason { get; set; }
       public Nullable<bool> breast_YN { get; set; }
       public Nullable<System.DateTime> breast_date { get; set; }
       public string breast_reason { get; set; }
       public Nullable<bool> stomach_YN { get; set; }
       public Nullable<System.DateTime> stomach_date { get; set; }
       public string stomach_reason { get; set; }
       public string other_surgeries { get; set; }
       public Nullable<System.DateTime> other_date { get; set; }
       public string other_reason { get; set; }
       public string other_result { get; set; }
       public Nullable<int> patient_id { get; set; }
   }

   public class IntakePrescriptionViewModel
   {
       public int prescription_id { get; set; }
       public Nullable<int> patient_id { get; set; }
       public Nullable<int> drug_id { get; set; }
       public Nullable<System.DateTime> Date_Entered { get; set; }
       public string dosage { get; set; }
       public string medication { get; set; }
       public string Display { get; set; }
       public string drugtype{get; set;}
   }
}
