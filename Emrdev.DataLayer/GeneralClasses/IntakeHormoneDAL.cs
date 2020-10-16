using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class IntakeHormoneDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion


        #region Save Intake Form Hormone

        public void SaveIntakeHormone(Emrdev.ViewModelLayer.IntakeHormoneViewModel objModel)
        {
            //Save Intake form hormone
            intake_form_hormone_review objEntity=new intake_form_hormone_review();
            
            objEntity.master_form_id = objModel.MasterFormId;
            objEntity.patient_id = objModel.PatientId;
            objEntity.Date_Entered = objModel.DateEntered;
            objEntity.f_date_of_last_period = objModel.DateOfLastPeriod;
            objEntity.f_birth_ctrl_method = objModel.BirthCtrlMethod;
            objEntity.f_pregnant_YN = objModel.Pregnant;
            objEntity.f_date_of_last_pap_test = objModel.DateOfLastPapTest;
            objEntity.f_last_pap_test_result = objModel.LastPapTestResult;
            objEntity.f_date_of_last_mammogram = objModel.DateOfLastMammogram;
            objEntity.f_last_mammogram_result = objModel.LastMammogramResult;
            objEntity.f_date_of_menopause = objModel.DateOfMenopause;
            objEntity.f_abnormal_pap_YN = objModel.AbnormalPap;
            objEntity.f_abnormal_pap_date = objModel.AbnormalPapDate;
            objEntity.f_hot_flashes_YN = objModel.HotFlashes;
            objEntity.f_night_sweats_YN = objModel.NightSweats;
            objEntity.f_leak_urine_YN = objModel.LeakUrine;
            objEntity.f_fibrocystic_breasts_YN = objModel.FibrocysticBreasts;
            objEntity.f_sleep_problems_YN = objModel.SleepProblems;
            objEntity.f_vaginal_dryness_pain_YN = objModel.VaginalDrynessPain;
            objEntity.f_loss_of_interest_in_sex_YN = objModel.LossOfInterestInSex;
            objEntity.f_irregular_periods_YN = objModel.IrregularPeriods;
            objEntity.f_spotting_after_menopause_YN = objModel.SpottingAfterMenopause;
            objEntity.f_mood_swings_YN = objModel.MoodSwings;
            objEntity.f_pms_YN = objModel.Pms;
            objEntity.f_bloating_late_in_cycle_YN = objModel.BloatingLateInCycle;
            objEntity.f_migraines_late_in_cycle_YN = objModel.MigrainesLateInCycle;
            objEntity.f_cravings_sugar_chocalate_YN = objModel.CravingsSugarChocalate;
            objEntity.f_pcos_YN = objModel.Pcos;
            objEntity.f_acne_YN = objModel.Acne;
            objEntity.f_facial_hair_YN = objModel.FacialHair;
            objEntity.f_lack_of_periods_YN = objModel.LackOfPeriods;
            objEntity.f_problems_with_infertility_YN = objModel.ProblemsWithInfertility;
            objEntity.f_ovarian_cysts_YN = objModel.OvarianCysts;
            objEntity.f_uterine_fibroid_YN = objModel.UterineFibroid;
            objEntity.f_cramps_clots_with_period_YN = objModel.CrampsClotsWithPeriod;
            objEntity. f_increased_fat_hips_thighs_YN= objModel.IncreasedFatHipsThighs;
            objEntity.f_endometriosis_YN = objModel.Endometriosis;
            objEntity.f_painful_sex_YN = objModel.PainfulSex;
            objEntity.f_painful_periods_YN = objModel.PainfulPeriods;
            objEntity.f_vaginal_irritation_YN = objModel.VaginalIrritation;
            objEntity. f_unusual_vaginal_discharge_YN= objModel.UnusualVaginalDischarge;

            //===========================For Men================================//
            objEntity.m_date_of_last_prostate_exam = objModel.DateOfLastProstateExam;
            objEntity.m_lowered_interest_in_sex_YN = objModel.LoweredInterestInSex;
            objEntity.m_cannot_maintain_erecetion_YN = objModel.CannotMaintainErecetion;
            objEntity.m_erection_less_firm_YN = objModel.ErectionLessFirm;
            objEntity.m_enlarged_prostate_YN = objModel.EnlargedProstate;
            objEntity.m_slowing_urinary_stream_YN = objModel.SlowingUrinaryStream;
            objEntity.m_night_time_urination_YN = objModel.NightTimeUrination;
            objEntity.m_difficulty_initiating_urine_stream_YN = objModel.DifficultyInitiatingUrineStream;
            objEntity.m_bladder_not_emptying_YN = objModel.BladderNotEmptying;
            objEntity.m_premature_ejaculation_YN = objModel.PrematureEjaculation;

            //========================Thyroid===================================//
            objEntity.dry_hair_YN = objModel.DryHair;
            objEntity.infertility_YN = objModel.Infertility;
            objEntity.migraines_YN = objModel.Migraines;
            objEntity.losing_hair_YN = objModel.LosingHair;
            objEntity.constipation_YN = objModel.Constipation;
            objEntity. fluid_retention_YN= objModel.FluidRetention;
            objEntity. crave_caffeine_YN= objModel.CraveCaffeine;
            objEntity.dry_coarse_skin_YN = objModel.DryCoarseSkin;
            objEntity.diets_dont_work_YN = objModel.DietsDontWork;
            objEntity.cold_hands_feet_YN = objModel.ColdHandsFeet;
            objEntity.elevated_cholesterol_YN = objModel.ElevatedCholesterol;
            objEntity.low_body_temp_YN = objModel.LowBodyTemp;
            objEntity.fatigue_exhaustion_YN = objModel.FatigueExhaustion;
            objEntity.decreased_memory_YN = objModel.DecreasedMemory;
            objEntity.brittle_nails_YN = objModel.BrittleNails;
            objEntity.unable_to_lose_weight_YN = objModel.UnableToLoseWeight;
            objEntity.daytime_drowsiness_YN = objModel.DayTimeDrowsiness;
            objEntity.foggy_mind_YN = objModel.FoggyMind;
            objEntity.depression_anxiety_YN = objModel.DepressionAnxiety;
            objEntity.low_ambition_motivation_YN = objModel.LowAmbitionMotivation;
            objEntity.decreased_concentration_YN = objModel.DecreasedConcentration;
            objEntity.fibromyalgia_chronic_fatigue_YN = objModel.FibromyalgiaChronicFatigue;
            objEntity.feel_cold_YN = objModel.FeelCold;

            //==================Adrenal=======================//

            objEntity.palpitations_YN= objModel.Palpitations;
            objEntity.salt_craving_YN = objModel.SaltCraving;
            objEntity.muscle_tension_YN = objModel.MuscleTension;
            objEntity.easily_frustrated_YN = objModel.EasilyFrustrated;
            objEntity.poor_stress_tolerance_YN = objModel.PoorStressTolerance;
            objEntity.sugar_craving_YN = objModel.SugarCraving;
            objEntity.panic_attacks_YN = objModel.PanicAttacks;
            objEntity.excessive_hungar_YN = objModel.ExcessiveHungar;
            objEntity.prone_to_infection_YN = objModel.ProneToInfection;
            objEntity.low_blood_pressure_YN = objModel.LowBloodPressure;
            objEntity.light_headed_standing_up_YN = objModel.LightHeadedStandingUp;
            objEntity.racing_mind_no_sleep_YN = objModel.RacingMindNoSleep;
            objEntity.sluggish_am_slow_starter_YN = objModel.SluggishAmSlowStarter;
            objEntity.need_sunglasses_in_sunlight_YN = objModel.NeedSunglassesInSunlight;
            objEntity.low_back_pain_with_stress_YN = objModel.LowBackPainWithStress;

            //===================Metabolism====================//
            objEntity.cannot_skip_meals_YN = objModel.CannotSkipMeals;
            objEntity.headache_when_missed_meal_YN = objModel.HeadacheWhenMissedMeal;
            objEntity.crave_sugar_carbs_YN = objModel.CraveSugarCarbs;
            objEntity.low_energy_relieved_with_food_YN = objModel.LowEnergyRelievedWithFood;
            objEntity.shake_weak_YN = objModel.ShakeWeak;
            objEntity.jittery_irratable_YN = objModel.JitteryIrratable;
            objEntity.high_and_low_moods_YN = objModel.HighAndLowMoods;
            objEntity.sluggish_and_high_energy_YN = objModel.SluggishAndHighEnergy;
            objEntity.high_blood_pressure_YN = objModel.HighBloodPressure;
            objEntity.high_cholesterol_YN = objModel.HighCholesterol;
            objEntity.mid_afternoon_drowsiness_YN = objModel.MidAfternoonDrowsiness;
            objEntity.increased_fat_abdomen_YN = objModel.IncreasedFatAbdomen;
            objEntity. inflammation_bursitis_YN= objModel.InflammationBursitis;
            objEntity.fluid_retention_puffy_extremities_YN = objModel.FluidRetentionPuffyExtremities;

            //=======================Cardio-Respiratory=========================//
            objEntity.decreased_stamina_YN = objModel.DecreasedStamina;
            objEntity.decreased_endurance_YN = objModel.DecreasedEndurance;
            objEntity.run_out_of_breath_YN = objModel.RunOutOfBreath;
            objEntity.easily_exhausted_YN = objModel.EasilyExhausted;
            objEntity.decreased_desire_exercise_YN = objModel.DecreasedDesireExercise;
            objEntity.dry_skin_YN = objModel.DrySkin;
            objEntity.thin_lips_YN = objModel.ThinLips;
            objEntity.graying_hair_YN = objModel.GrayingHair;
            objEntity.skin_blemishes_YN = objModel.SkinBlemishes;
            objEntity.tendency_bruising_YN = objModel.TendencyBruising;
            objEntity.thinned_skin_YN = objModel.ThinnedSkin;
            objEntity.thinning_hair_YN = objModel.ThinningHair;
            objEntity.wrinkling_skin_YN = objModel.WrinklingSkin;
            objEntity.sagging_skin_YN = objModel.SaggingSkin;

            //===========================Muscles / Joints ======================//
            objEntity.osteoporosis_YN = objModel.Osteoporosis;
            objEntity.aches_pains_YN = objModel.AchesPains;
            objEntity.loss_of_strength_YN = objModel.LossOfStrength;
            objEntity.body_joints_YN = objModel.BodyJoints;
            objEntity.thinning_muscles_YN = objModel.ThinningMuscles;

            //======================Neuro-cognitive=============================//
            objEntity.loss_of_esteem_YN = objModel.LossOfEsteem;
            objEntity.feeling_hopeless_YN = objModel.FeelingHopeless;
            objEntity.feeling_defeated_YN = objModel.FeelingDefeated;
            objEntity.feeling_apathy_YN = objModel.FeelingApathy;
            objEntity.loss_of_confidence_YN = objModel.LossOfConfidence;
            objEntity.vision_deteriorating_YN = objModel.VisionDeteriorating;
            objEntity. hearing_deteriorating_YN= objModel.HearingDeteriorating;
            objEntity. memory_deteriorating_YN= objModel.MemoryDeteriorating;
            objEntity. balance_deteriorating_YN= objModel.BalanceDeteriorating;
            objEntity.coordination_deteriorating_YN = objModel.CoordinationDeteriorating;
            objEntity.sense_of_powerlessness_YN = objModel.SenseOfPowerLessNess;
            objEntity.decreased_sense_of_well_being_YN = objModel.DecreasedSenseOfWellBeing;

            //======================Gastrointestinal================================//
            objEntity.indigestion_YN = objModel.Indigestion;
            objEntity.feel_full_faster_YN = objModel.FeelFullFaster;
            objEntity.slower_digestion_YN = objModel.SlowerDigestion;
            objEntity.eat_less_meals_YN = objModel.EatLessMeals;
            objEntity.fullness_persists_after_meal_YN = objModel.FullnessPersistsAfterMeal;
            objEntity.burping_belching_after_meal_YN = objModel.BurpingBelchingAfterMeal;
            objEntity.decreased_sense_of_taste_smell_YN = objModel.DecreasedSenseOfTasteSmell;

            //============================Diet=================================//
            objEntity.specific_diet = objModel.SpecificDiet;
            objEntity.diet_is_successful_YN = objModel.DietIsSuccessfull;
            objEntity.past_successful_diets = objModel.PastSuccessfulDiets;

            //===================Stress================================//
            objEntity.stess_level = objModel.StressLevel;
            objEntity.stress_how_long = objModel.StressHowLong;
            objEntity.stress_expect_to_last = objModel.StressExpectToLast;
            objEntity.stress_solution_YN = objModel.StressSolution;
            objEntity.need_help_with_stress_YN = objModel.NeedHelpWithStress;

             //=========================Exercise=============================//
            objEntity.exercise_type = objModel.ExerciseType;
            objEntity.exercise_length_of_session = objModel.ExerciseLengthOfSession;
            objEntity. exercise_frequency= objModel.ExerciseFrequency;
            objEntity.exercise_type_other = objModel.ExerciseTypeOther;

            //===================Sleep================================//
            objEntity.early_morning_waking_YN = objModel.EarlyMorningWaking;
            objEntity.trouble_getting_to_sleep_YN = objModel.TroubleGettingToSleep;
            objEntity.sleep_not_restful_YN = objModel.SleepNotRestful;
            objEntity.sleep_toss_and_turn_YN = objModel.SleepTossAndTurn;
            objEntity.daytime_drowsiness_sleepiness_YN = objModel.DaytimeDrowsinessSleepiness;
            objEntity.sleep_feeling_smothered_choke_YN = objModel.SleepFeelingSmotheredChoke;
            objEntity.heaving_snoring_stop_breathing_YN = objModel.HeavingSnoringStopBreathing;
            objEntity.work_night_shift_YN = objModel.WorkNightHhift;
            objEntity.night_shifts_per_week = objModel.NightShiftsPerWeek;
            Create(objEntity);
        }

        #endregion
    }
}
