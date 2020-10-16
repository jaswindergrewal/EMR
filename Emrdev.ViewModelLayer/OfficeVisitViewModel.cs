using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class OfficeVisitViewModel
    {
        public Nullable<int> PatientId { get; set; }
        public int AptID { get; set; }
        public bool Fasting { get; set; }
        public string FastingComment { get; set; }
        public bool Creams { get; set; }
        public string CreamComment { get; set; }
        public bool DHEA { get; set; }
        public string DHEAComment { get; set; }
        public bool Pregenolone { get; set; }
        public string PregenoloneAMPM { get; set; }
        public string PregenoloneComment { get; set; }
        public Nullable<bool> Anastrozole { get; set; }
        public string AnastrozoleComment { get; set; }
        public Nullable<bool> Testosterone { get; set; }
        public string TestosteroneComment { get; set; }
        public string WaterIntake { get; set; }
        public string WaterIntakeComment { get; set; }
        public string IntakeType { get; set; }
        public string WaterSource { get; set; }
        public string WaterSourceComment { get; set; }
        public string ExerciseFreq { get; set; }
        public string ExerciseFreqComment { get; set; }
        public string ExerciseType { get; set; }
        public string ExerciseTypeCommnet { get; set; }
        public string WorkoutLemgth { get; set; }
        public string WorkoutLemgthComment { get; set; }
        public string SleepQuality { get; set; }
        public string SleepQualityComment { get; set; }
        public Nullable<int> SleepHours { get; set; }
        public Nullable<int> Mealtonin { get; set; }
        public string FruitVeggie { get; set; }
        public string FruitVeggieComment { get; set; }
        public string Diet { get; set; }
        public string DietComment { get; set; }
        public string EnergeyLevel { get; set; }
        public string EnergyComment { get; set; }
        public bool NewMedicationsYN { get; set; }
        public string NewMedsNames { get; set; }
        public bool NewIllness { get; set; }
        public string IllnessNames { get; set; }
        public string LastPhysical { get; set; }
        public string LastPap { get; set; }
        public string LastMammo { get; set; }
        public string LastProstate { get; set; }
        public string RealizeGoals { get; set; }
        public string HappyProgram { get; set; }
        public string Other { get; set; }
        public string SubjNote { get; set; }

    }
    public class Sympt
    {
        public string Symptom { get; set; }
        public int SymptomID { get; set; }
        public int RowPosition { get; set; }
        public string dir { get; set; }
        public bool resolved { get; set; }

    }

    public class apt_BloodWork
    {
        public int apt_BloodWorkID { get; set; }
        public int AptID { get; set; }
        public bool Fasting { get; set; }
        public string FastingComment { get; set; }
        public bool Creams { get; set; }
        public string CreamComment { get; set; }
        public bool DHEA { get; set; }
        public string DHEAComment { get; set; }
        public bool Pregenolone { get; set; }
        public string PregenoloneAMPM { get; set; }
        public string PregenoloneComment { get; set; }
        public Nullable<bool> Anastrozole { get; set; }
        public string AnastrozoleComment { get; set; }
        public Nullable<bool> Testosterone { get; set; }
        public string TestosteroneComment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
    }

    public class apt_LisfeStyle
    {
        public int apt_LisfeStyleID { get; set; }
        public int AptID { get; set; }
        public string WaterIntake { get; set; }
        public string WaterIntakeComment { get; set; }
        public string IntakeType { get; set; }
        public string WaterSource { get; set; }
        public string WaterSourceComment { get; set; }
        public string ExerciseFreq { get; set; }
        public string ExerciseFreqComment { get; set; }
        public string ExerciseType { get; set; }
        public string ExerciseTypeCommnet { get; set; }
        public string WorkoutLemgth { get; set; }
        public string WorkoutLemgthComment { get; set; }
        public string SleepQuality { get; set; }
        public string SleepQualityComment { get; set; }
        public Nullable<int> SleepHours { get; set; }
        public Nullable<int> Mealtonin { get; set; }
        public string FruitVeggie { get; set; }
        public string FruitVeggieComment { get; set; }
        public string Diet { get; set; }
        public string DietComment { get; set; }
        public string EnergeyLevel { get; set; }
        public string EnergyComment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
    }

    public class apt_Misc
    {
        public int apt_MiscID { get; set; }
        public int AptID { get; set; }
        public bool NewMedicationsYN { get; set; }
        public string NewMedsNames { get; set; }
        public bool NewIllness { get; set; }
        public string IllnessNames { get; set; }
        public string LastPhysical { get; set; }
        public string LastPap { get; set; }
        public string LastMammo { get; set; }
        public string LastProstate { get; set; }
        public string RealizeGoals { get; set; }
        public string HappyProgram { get; set; }
        public string Other { get; set; }
        public string SubjNote { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
    }

    public class apt_Symtpoms
    {
        public int apt_SymtpomsID { get; set; }
        public int AptID { get; set; }
        public int SymptomID { get; set; }
        public int Priority { get; set; }
        public string dir { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
        public Nullable<bool> Resolved { get; set; }
    }

    public class apt_Goals
    {
        public int apt_GoalID { get; set; }
        public int AptID { get; set; }
        public int GoalItemID { get; set; }
        public int Priority { get; set; }
        public string dir { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
        public Nullable<bool> Resolved { get; set; }
    }
}