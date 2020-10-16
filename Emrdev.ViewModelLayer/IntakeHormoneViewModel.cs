using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class IntakeHormoneViewModel
    {

        //====================Primary & Foriegn Key(s)===================//
        public int HormoneId { get; set; }//hormone_review_id
        public Nullable<int> MasterFormId { get; set; }//master_form_id
        public Nullable<int> PatientId { get; set; }//patient_id
        public Nullable<System.DateTime> DateEntered { get; set; } //Date_Entered

        //====================For Women=========================//
        public Nullable<System.DateTime> DateOfLastPeriod { get; set; }//f_date_of_last_period//
        public string BirthCtrlMethod { get; set; }//f_birth_ctrl_method
        public Nullable<bool> Pregnant { get; set; }//f_pregnant_YN
        public Nullable<System.DateTime> DateOfLastPapTest { get; set; }//f_date_of_last_pap_test
        public string LastPapTestResult { get; set; } //f_last_pap_test_result
        public Nullable<System.DateTime> DateOfLastMammogram { get; set; }//f_date_of_last_mammogram
        public string LastMammogramResult { get; set; }//f_last_mammogram_result
        public Nullable<System.DateTime> DateOfMenopause { get; set; }//f_date_of_menopause
        public Nullable<bool> AbnormalPap { get; set; }//f_abnormal_pap_YN
        public Nullable<System.DateTime> AbnormalPapDate { get; set; }//f_abnormal_pap_date
        public Nullable<bool> HotFlashes { get; set; }//f_hot_flashes_YN
        public Nullable<bool> NightSweats { get; set; }//f_night_sweats_YN
        public Nullable<bool> LeakUrine { get; set; }//f_leak_urine_YN
        public Nullable<bool> FibrocysticBreasts { get; set; }//f_fibrocystic_breasts_YN
        public Nullable<bool> SleepProblems { get; set; }//f_sleep_problems_YN
        public Nullable<bool> VaginalDrynessPain { get; set; }//f_vaginal_dryness_pain_YN
        public Nullable<bool> LossOfInterestInSex { get; set; }//f_loss_of_interest_in_sex_YN
        public Nullable<bool> IrregularPeriods { get; set; }//f_irregular_periods_YN
        public Nullable<bool> SpottingAfterMenopause { get; set; }//f_spotting_after_menopause_YN
        public Nullable<bool> MoodSwings { get; set; }//f_mood_swings_YN
        public Nullable<bool> Pms { get; set; }//f_pms_YN
        public Nullable<bool> BloatingLateInCycle { get; set; }//f_bloating_late_in_cycle_YN
        public Nullable<bool> MigrainesLateInCycle { get; set; }//f_migraines_late_in_cycle_YN
        public Nullable<bool> CravingsSugarChocalate { get; set; }//f_cravings_sugar_chocalate_YN
        public Nullable<bool> Pcos { get; set; }//f_pcos_YN
        public Nullable<bool> Acne { get; set; }//f_acne_YN
        public Nullable<bool> FacialHair { get; set; } //f_facial_hair_YN
        public Nullable<bool> LackOfPeriods { get; set; }//f_lack_of_periods_YN
        public Nullable<bool> ProblemsWithInfertility { get; set; }//f_problems_with_infertility_YN
        public Nullable<bool> OvarianCysts { get; set; }//f_ovarian_cysts_YN
        public Nullable<bool> UterineFibroid { get; set; }//f_uterine_fibroid_YN
        public Nullable<bool> CrampsClotsWithPeriod { get; set; }//f_cramps_clots_with_period_YN
        public Nullable<bool> IncreasedFatHipsThighs { get; set; }//f_increased_fat_hips_thighs_YN
        public Nullable<bool> Endometriosis { get; set; }//f_endometriosis_YN
        public Nullable<bool> PainfulSex { get; set; }//f_painful_sex_YN
        public Nullable<bool> PainfulPeriods { get; set; }//f_painful_periods_YN
        public Nullable<bool> VaginalIrritation { get; set; }//f_vaginal_irritation_YN
        public Nullable<bool> UnusualVaginalDischarge { get; set; }//f_unusual_vaginal_discharge_YN

        //===========================For Men================================//
        public Nullable<System.DateTime> DateOfLastProstateExam { get; set; }//m_date_of_last_prostate_exam
        public Nullable<bool> LoweredInterestInSex { get; set; }//m_lowered_interest_in_sex_YN
        public Nullable<bool> CannotMaintainErecetion { get; set; }//m_cannot_maintain_erecetion_YN
        public Nullable<bool> ErectionLessFirm { get; set; }//m_erection_less_firm_YN
        public Nullable<bool> EnlargedProstate { get; set; }//m_enlarged_prostate_YN
        public Nullable<bool> SlowingUrinaryStream { get; set; }//m_slowing_urinary_stream_YN
        public Nullable<bool> NightTimeUrination { get; set; }//m_night_time_urination_YN
        public Nullable<bool> DifficultyInitiatingUrineStream { get; set; }//m_difficulty_initiating_urine_stream_YN
        public Nullable<bool> BladderNotEmptying { get; set; }//m_bladder_not_emptying_YN
        public Nullable<bool> PrematureEjaculation { get; set; }//m_premature_ejaculation_YN
        

        //========================Thyroid===================================//
        public Nullable<bool> DryHair { get; set; }//dry_hair_YN
        public Nullable<bool> Infertility { get; set; }//infertility_YN
        public Nullable<bool> Migraines { get; set; }//migraines_YN
        public Nullable<bool> LosingHair { get; set; }//losing_hair_YN
        public Nullable<bool> Constipation { get; set; }//constipation_YN
        public Nullable<bool> FluidRetention { get; set; }//fluid_retention_YN
        public Nullable<bool> CraveCaffeine { get; set; }//crave_caffeine_YN
        public Nullable<bool> DryCoarseSkin { get; set; }//dry_coarse_skin_YN
        public Nullable<bool> DietsDontWork { get; set; }//diets_dont_work_YN
        public Nullable<bool> ColdHandsFeet { get; set; }//cold_hands_feet_YN
        public Nullable<bool> ElevatedCholesterol { get; set; }//elevated_cholesterol_YN
        public Nullable<bool> LowBodyTemp { get; set; }//low_body_temp_YN
        public Nullable<bool> FatigueExhaustion { get; set; }//fatigue_exhaustion_YN
        public Nullable<bool> DecreasedMemory { get; set; }//decreased_memory_YN
        public Nullable<bool> BrittleNails { get; set; }//brittle_nails_YN
        public Nullable<bool> UnableToLoseWeight { get; set; }//unable_to_lose_weight_YN
        public Nullable<bool> DayTimeDrowsiness { get; set; }//daytime_drowsiness_YN
        public Nullable<bool> FoggyMind { get; set; }//foggy_mind_YN
        public Nullable<bool> DepressionAnxiety { get; set; }//depression_anxiety_YN
        public Nullable<bool> LowAmbitionMotivation { get; set; }//low_ambition_motivation_YN
        public Nullable<bool> DecreasedConcentration { get; set; }//decreased_concentration_YN
        public Nullable<bool> FibromyalgiaChronicFatigue { get; set; }//fibromyalgia_chronic_fatigue_YN
        public Nullable<bool> FeelCold { get; set; }//feel_cold_YN

        //==================Adrenal=======================//
        public Nullable<bool> Palpitations { get; set; }//palpitations_YN
        public Nullable<bool> SaltCraving { get; set; }//salt_craving_YN
        public Nullable<bool> MuscleTension { get; set; }//muscle_tension_YN
        public Nullable<bool> EasilyFrustrated { get; set; }//easily_frustrated_YN
        public Nullable<bool> PoorStressTolerance { get; set; }//poor_stress_tolerance_YN
        public Nullable<bool> SugarCraving { get; set; }//sugar_craving_YN
        public Nullable<bool> PanicAttacks { get; set; }//panic_attacks_YN
        public Nullable<bool> ExcessiveHungar { get; set; }//excessive_hungar_YN
        public Nullable<bool> ProneToInfection { get; set; }//prone_to_infection_YN
        public Nullable<bool> LowBloodPressure { get; set; }//low_blood_pressure_YN
        public Nullable<bool> LightHeadedStandingUp { get; set; }//light_headed_standing_up_YN
        public Nullable<bool> RacingMindNoSleep { get; set; }//racing_mind_no_sleep_YN
        public Nullable<bool> SluggishAmSlowStarter { get; set; }//sluggish_am_slow_starter_YN
        public Nullable<bool> NeedSunglassesInSunlight { get; set; }//need_sunglasses_in_sunlight_YN
        public Nullable<bool> LowBackPainWithStress { get; set; }//low_back_pain_with_stress_YN

        //===================Metabolism====================//
        public Nullable<bool> CannotSkipMeals { get; set; }//cannot_skip_meals_YN
        public Nullable<bool> HeadacheWhenMissedMeal { get; set; }//headache_when_missed_meal_YN
        public Nullable<bool> CraveSugarCarbs { get; set; }//crave_sugar_carbs_YN
        public Nullable<bool> LowEnergyRelievedWithFood { get; set; }//low_energy_relieved_with_food_YN
        public Nullable<bool> ShakeWeak { get; set; }//shake_weak_YN
        public Nullable<bool> JitteryIrratable { get; set; }//jittery_irratable_YN
        public Nullable<bool> HighAndLowMoods { get; set; }//high_and_low_moods_YN
        public Nullable<bool> SluggishAndHighEnergy { get; set; }//sluggish_and_high_energy_YN
        public Nullable<bool> HighBloodPressure { get; set; }//high_blood_pressure_YN
        public Nullable<bool> HighCholesterol { get; set; }//high_cholesterol_YN
        public Nullable<bool> MidAfternoonDrowsiness { get; set; }//mid_afternoon_drowsiness_YN
        public Nullable<bool> IncreasedFatAbdomen { get; set; }//increased_fat_abdomen_YN
        public Nullable<bool> InflammationBursitis { get; set; }//inflammation_bursitis_YN
        public Nullable<bool> FluidRetentionPuffyExtremities { get; set; }//fluid_retention_puffy_extremities_YN

        //=======================Cardio-Respiratory=========================//
        public Nullable<bool> DecreasedStamina { get; set; }//decreased_stamina_YN
        public Nullable<bool> DecreasedEndurance { get; set; }//decreased_endurance_YN
        public Nullable<bool> RunOutOfBreath { get; set; }//run_out_of_breath_YN
        public Nullable<bool> EasilyExhausted { get; set; }//easily_exhausted_YN
        public Nullable<bool> DecreasedDesireExercise { get; set; }//decreased_desire_exercise_YN
        public Nullable<bool> DrySkin { get; set; }//dry_skin_YN
        public Nullable<bool> ThinLips { get; set; }//thin_lips_YN
        public Nullable<bool> GrayingHair { get; set; }//graying_hair_YN
        public Nullable<bool> SkinBlemishes { get; set; }//skin_blemishes_YN
        public Nullable<bool> TendencyBruising { get; set; }//tendency_bruising_YN
        public Nullable<bool> ThinnedSkin { get; set; }//thinned_skin_YN
        public Nullable<bool> ThinningHair { get; set; }//thinning_hair_YN
        public Nullable<bool> WrinklingSkin { get; set; }//wrinkling_skin_YN
        public Nullable<bool> SaggingSkin { get; set; }//sagging_skin_YN

        //===========================Muscles / Joints ======================//
        public Nullable<bool> Osteoporosis { get; set; }//osteoporosis_YN
        public Nullable<bool> AchesPains { get; set; }//aches_pains_YN
        public Nullable<bool> LossOfStrength { get; set; }//loss_of_strength_YN
        public Nullable<bool> BodyJoints { get; set; }//body_joints_YN
        public Nullable<bool> ThinningMuscles { get; set; }//thinning_muscles_YN


        //======================Neuro-cognitive=============================//
        public Nullable<bool> LossOfEsteem { get; set; }//loss_of_esteem_YN
        public Nullable<bool> FeelingHopeless { get; set; }//feeling_hopeless_YN
        public Nullable<bool> FeelingDefeated { get; set; }//feeling_defeated_YN
        public Nullable<bool> FeelingApathy { get; set; }//feeling_apathy_YN
        public Nullable<bool> LossOfConfidence { get; set; }//loss_of_confidence_YN
        public Nullable<bool> VisionDeteriorating { get; set; }//vision_deteriorating_YN
        public Nullable<bool> HearingDeteriorating { get; set; }//hearing_deteriorating_YN
        public Nullable<bool> MemoryDeteriorating { get; set; }//memory_deteriorating_YN
        public Nullable<bool> BalanceDeteriorating { get; set; }//balance_deteriorating_YN
        public Nullable<bool> CoordinationDeteriorating { get; set; }//coordination_deteriorating_YN
        public Nullable<bool> SenseOfPowerLessNess { get; set; }//sense_of_powerlessness_YN
        public Nullable<bool> DecreasedSenseOfWellBeing { get; set; }//decreased_sense_of_well_being_YN


        //======================Gastrointestinal================================//
        public Nullable<bool> Indigestion { get; set; }//indigestion_YN
        public Nullable<bool> FeelFullFaster { get; set; }//feel_full_faster_YN
        public Nullable<bool> SlowerDigestion { get; set; }//slower_digestion_YN
        public Nullable<bool> EatLessMeals { get; set; }//eat_less_meals_YN
        public Nullable<bool> FullnessPersistsAfterMeal { get; set; }//fullness_persists_after_meal_YN
        public Nullable<bool> BurpingBelchingAfterMeal { get; set; }//burping_belching_after_meal_YN
        public Nullable<bool> DecreasedSenseOfTasteSmell { get; set; }//decreased_sense_of_taste_smell_YN

        //============================Diet=================================//
        public string SpecificDiet { get; set; }//specific_diet
        public Nullable<bool> DietIsSuccessfull { get; set; }//diet_is_successful_YN
        public string PastSuccessfulDiets { get; set; }//past_successful_diets


        //===================Stress================================//
        public string StressLevel { get; set; }//stess_level
        public string StressHowLong { get; set; }//stress_how_long
        public string StressExpectToLast { get; set; }//stress_expect_to_last
        public Nullable<bool> StressSolution { get; set; }//stress_solution_YN
        public Nullable<bool> NeedHelpWithStress { get; set; }//need_help_with_stress_YN

        //=========================Exercise=============================//
        public string ExerciseType { get; set; }//exercise_type
        public string ExerciseLengthOfSession { get; set; }//exercise_length_of_session
        public string ExerciseFrequency { get; set; }//exercise_frequency
        public string ExerciseTypeOther { get; set; }//exercise_type_other

        //===================Sleep================================//
        public Nullable<bool> EarlyMorningWaking { get; set; }//early_morning_waking_YN
        public Nullable<bool> TroubleGettingToSleep { get; set; }//trouble_getting_to_sleep_YN
        public Nullable<bool> SleepNotRestful { get; set; }//sleep_not_restful_YN
        public Nullable<bool> SleepTossAndTurn { get; set; }//sleep_toss_and_turn_YN
        public Nullable<bool> DaytimeDrowsinessSleepiness { get; set; }//daytime_drowsiness_sleepiness_YN
        public Nullable<bool> SleepFeelingSmotheredChoke { get; set; }//sleep_feeling_smothered_choke_YN
        public Nullable<bool> HeavingSnoringStopBreathing { get; set; }//heaving_snoring_stop_breathing_YN
        public Nullable<bool> WorkNightHhift { get; set; }//work_night_shift_YN
        public string NightShiftsPerWeek { get; set; }//night_shifts_per_week
        

    }
}
