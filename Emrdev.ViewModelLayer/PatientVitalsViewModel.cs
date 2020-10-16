using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class PatientVitalsViewModel
    {
        public int Vital_ID { get; set; }
        public string BloodPres { get; set; }
        public Nullable<decimal> Height { get; set; }
        public string Hip_Circm { get; set; }
        public string grip_r_lbs { get; set; }
        public string grip_l_lbs { get; set; }
        public Nullable<decimal> Wgt { get; set; }
        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string Waist_Circm { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string Respirations { get; set; }
        public string Perc_Body_fat { get; set; }
        public string bicep_r_cir { get; set; }
        public string bicep_l_cir { get; set; }
        public string thigh_r_cir { get; set; }
        public string thigh_l_cir { get; set; }
        public string knee_r_cir { get; set; }
        public string knee_l_cir { get; set; }
        public string neck_cir { get; set; }
        public string chest_cir { get; set; }
        public string midriff_cir { get; set; }
        public string ma_Notes { get; set; }
        public string PatientName { get; set; }
        public  Nullable<decimal>BMI{get; set;}
        public Nullable<decimal> WaistHipRatio { get; set; }
        public string Date_Entered { get; set; }
        public bool active { get; set; }
        
    }

    public class PatientProfile
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Exception { get; set; }
    }
}
