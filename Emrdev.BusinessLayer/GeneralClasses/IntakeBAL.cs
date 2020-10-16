using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{

    public class IntakeBAL
    {
        IntakeDAL objIntakeDAL = null;

        public void AddIntakeFormHabits(int master_form_id, DateTime Date_Entered, string alcohol_type, string alcohol_amount,
            string alcohol_frequency, string drugs_type, string drugs_amount, string drugs_frequency, int cig_packs_per_day, int cig_years,
            int chew_amount_per_day, int chew_years, bool i_want_to_quit_YN, int caffeine_serv_per_day, int nutrasweet_serv_per_day,
            int saccharin_serv_per_day, int msg_serv_per_day, int patient_id)
        {
            objIntakeDAL = new IntakeDAL();
            intake_form_habits objEntity = new intake_form_habits();
            objEntity.master_form_id = master_form_id;
            objEntity.Date_Entered = Date_Entered;
            objEntity.alcohol_type = alcohol_type;
            objEntity.alcohol_amount = alcohol_amount;
            objEntity.alcohol_frequency = alcohol_frequency;
            objEntity.drugs_type = drugs_type;
            objEntity.drugs_amount = drugs_amount;
            objEntity.drugs_frequency = drugs_frequency;
            objEntity.cig_packs_per_day = cig_packs_per_day;
            objEntity.cig_years = cig_years;
            objEntity.chew_amount_per_day = chew_amount_per_day;
            objEntity.chew_years = chew_years;
            objEntity.i_want_to_quit_YN = i_want_to_quit_YN;
            objEntity.caffeine_serv_per_day = caffeine_serv_per_day;
            objEntity.nutrasweet_serv_per_day = nutrasweet_serv_per_day;
            objEntity.saccharin_serv_per_day = saccharin_serv_per_day;
            objEntity.msg_serv_per_day = msg_serv_per_day;
            objEntity.patient_id = patient_id;

            objIntakeDAL.Create(objEntity);

        }

       /// <summary>
       /// Added by jaswinder on 21st aug 2013
       /// </summary>
       /// <param name="PatientID"></param>
       /// <returns></returns>
        public int InsertMasterFormIntake(int PatientID)
        {
            objIntakeDAL = new IntakeDAL();
            intake_form_master_index objEntity = new intake_form_master_index();
            objEntity.patient_id = PatientID;
            objEntity.CompleteYN = false;
            objEntity.date_entered = DateTime.Now;
            objIntakeDAL.Create(objEntity);
            return objEntity.master_form_id;

        }

        /// <summary>
        /// Added by jaswinder on 22st aug 2013
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="MasterId"></param>
        /// <param name="marital_status"></param>
        /// <param name="Education"></param>
        /// <param name="current_occupation"></param>
        /// <param name="occupation_enjoy_YN"></param>
        /// <param name="occupation_stress_YN"></param>
        /// <param name="occupation_fulfill_YN"></param>
        /// <param name="occupation_hazardous_YN"></param>
        /// <param name="retired_YN"></param>
        /// <param name="retired_occupation"></param>
        /// <param name="retired_date_of"></param>
        /// <param name="retired_happy_YN"></param>
        public void InsertIntakePersonalInfo(int PatientId, int MasterId, string marital_status, string Education, string current_occupation, bool occupation_enjoy_YN, bool occupation_stress_YN, bool occupation_fulfill_YN,
            bool occupation_hazardous_YN, bool retired_YN, string retired_occupation, DateTime ? retired_date_of, bool retired_happy_YN)
        {
            objIntakeDAL = new IntakeDAL();
            intake_form_personal_info objEntity = new intake_form_personal_info();

            objEntity.patient_id = PatientId;
            objEntity.master_form_id = MasterId;
            objEntity.marital_status = marital_status;
            objEntity.education = Education;
            objEntity.current_occupation = current_occupation;
            objEntity.occupation_enjoy_YN = occupation_enjoy_YN;
            objEntity.occupation_fulfill_YN = occupation_fulfill_YN;
            objEntity.occupation_stress_YN = occupation_stress_YN;
            objEntity.occupation_hazardous_YN = occupation_hazardous_YN;
            objEntity.retired_YN = retired_YN;
            objEntity.retired_occupation = retired_occupation;
            objEntity.retired_date_of=retired_date_of;
            objEntity.DateEntered = DateTime.Now;
            objEntity.retired_happy_YN = retired_happy_YN;
            objIntakeDAL.Create(objEntity);
        }

        /// <summary>
        /// Added by jaswinder on 21st aug 2013
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="MasterId"></param>
        /// <param name="drug_or_med_allergy"></param>
        /// <param name="food_allergy"></param>
        /// <param name="drug_or_med_allergy_YN"></param>
        /// <param name="food_allergy_YN"></param>
        public void InsertIntakeAllergy(int PatientId, int MasterId, string drug_or_med_allergy, string food_allergy, bool drug_or_med_allergy_YN,
                                           bool food_allergy_YN)
        {
            objIntakeDAL = new IntakeDAL();
            intake_form_allergies objEntity = new intake_form_allergies();
            objEntity.Date_Entered = DateTime.Now;
            objEntity.drug_or_med_allergy = drug_or_med_allergy;
            objEntity.drug_or_med_allergy_YN = drug_or_med_allergy_YN;
            objEntity.food_allergy = food_allergy;
            objEntity.food_allergy_YN = food_allergy_YN;
            objEntity.master_form_id = MasterId;
            objEntity.patient_id = PatientId;
            objIntakeDAL.Create(objEntity);

        }

        public void AddIntakeFormGoalsReview(int master_form_id, int patient_id, DateTime Date_Entered, bool balance_hormones_YN, bool start_hormones_YN, bool improve_energy_YN,
            bool feel_better_YN, bool feel_stronger_YN, bool improve_sleep_YN, bool eliminate_hot_flashes_YN, bool eliminate_prescriptions_YN, bool weight_loss_YN,
            bool stabalize_PMS_YN, bool stop_hair_loss_YN, bool sense_of_well_being_YN, bool enhance_immune_sys_YN, bool less_pain_YN,
            string less_pain_where, bool improve_libido_YN, bool improve_sex_life_YN, bool improve_muscle_YN,
            bool improve_memory_YN, bool bladder_control_YN, bool improve_skin_YN, bool better_stamina_YN, bool general_wellness_YN,
            bool reduce_stress_YN, bool improve_metabolism_YN, string other)
        {
            intake_form_goals objEntity = new intake_form_goals();
            objEntity.master_form_id = master_form_id;
            objEntity.patient_id = patient_id;
            objEntity.Date_Entered = Date_Entered;
            objEntity.balance_hormones_YN = balance_hormones_YN;
            objEntity.start_hormones_YN = start_hormones_YN;
            objEntity.improve_energy_YN = improve_energy_YN;
            objEntity.feel_better_YN = feel_better_YN;
            objEntity.feel_stronger_YN = feel_stronger_YN;
            objEntity.improve_sleep_YN = improve_sleep_YN;
            objEntity.eliminate_hot_flashes_YN = eliminate_hot_flashes_YN;
            objEntity.eliminate_prescriptions_YN = eliminate_prescriptions_YN;
            objEntity.weight_loss_YN = weight_loss_YN;
            objEntity.stabalize_PMS_YN = stabalize_PMS_YN;
            objEntity.stop_hair_loss_YN = stop_hair_loss_YN;
            objEntity.sense_of_well_being_YN = sense_of_well_being_YN;
            objEntity.enhance_immune_sys_YN = enhance_immune_sys_YN;
            objEntity.less_pain_YN = less_pain_YN;
            objEntity.less_pain_where = less_pain_where;
            objEntity.improve_libido_YN = improve_libido_YN;
            objEntity.improve_sex_life_YN = improve_sex_life_YN;
            objEntity.improve_muscle_YN = improve_muscle_YN;
            objEntity.bladder_control_YN = bladder_control_YN;
            objEntity.improve_skin_YN = improve_skin_YN;            
            objEntity.better_stamina_YN = better_stamina_YN;
            objEntity.general_wellness_YN = general_wellness_YN;
            objEntity.reduce_stress_YN = reduce_stress_YN;
            objEntity.improve_metabolism_YN = improve_metabolism_YN;
            objEntity.improve_memory_YN = improve_memory_YN;

            objEntity.other = other;

            objIntakeDAL = new IntakeDAL();
            objIntakeDAL.Create(objEntity);
        }

        /// <summary>
        /// Added by jaswinder
        /// 22nd aug 2013
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="MasterId"></param>
        /// <param name="YrStr"></param>
        /// <param name="OtherSymptoms"></param>
        public void InsertIntakeSymptoms(int PatientId, int MasterId, string YrStr, string OtherSymptoms)
        {
            objIntakeDAL = new IntakeDAL();
            objIntakeDAL.InsertIntakeSymptoms(PatientId, MasterId, YrStr, OtherSymptoms);
        }

        /// <summary>
        /// Added by jaswinder
        /// 22nd aug 2013
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="MasterId"></param>
        /// <param name="ChkGoalStr"></param>
        /// <param name="LessPain"></param>
        /// <param name="OtherSymptoms"></param>
        public void InsertIntakeGoals(int PatientId, int MasterId, string ChkGoalStr, string LessPain, string OtherSymptoms)
        {
            objIntakeDAL = new IntakeDAL();
            objIntakeDAL.InsertIntakeGoals(PatientId, MasterId, ChkGoalStr, LessPain, OtherSymptoms);
        }

        /// <summary>
        /// Added by jaswinder on 23 aug 2013
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="MasterId"></param>
        /// <param name="ChkStr"></param>
        /// <param name="Other"></param>
        public void InsertIntakeSuppliments(int PatientId, int MasterId, string ChkStr, string Other)
        {
            objIntakeDAL = new IntakeDAL();
            objIntakeDAL.InsertIntakeSuppliments(PatientId, MasterId, ChkStr, Other);
        }

        public void InsertIntakeFormMedicalHistory(IntakeFormMedicalHistoryViewModel objIntakeFormMedicalHistoryViewModel)
        {
            objIntakeDAL = new IntakeDAL();
            intake_form_medical_history objintake_form_medical_history = new intake_form_medical_history();
            Mapper.CreateMap<IntakeFormMedicalHistoryViewModel, intake_form_medical_history>();
            objintake_form_medical_history = Mapper.Map(objIntakeFormMedicalHistoryViewModel, objintake_form_medical_history);
            objIntakeDAL.Create(objintake_form_medical_history);
        }

        public List<IntakeTestViewModel> GetPatientRecentTestByPatientId(int PatientID)
        {
            objIntakeDAL = new IntakeDAL();
            var _objTestList = new List<IntakeTestViewModel>();
             List<intake_form_recent_tests> RecentTestEntity = objIntakeDAL.GetAll<intake_form_recent_tests>(o => o.patient_id == PatientID).ToList();

            Mapper.CreateMap<intake_form_recent_tests, IntakeTestViewModel>();
            _objTestList = Mapper.Map(RecentTestEntity, _objTestList);
            return _objTestList;
        }

        public int InsertTestDetails(IntakeTestViewModel TestViewModel)
        {
            try
            {
                objIntakeDAL = new IntakeDAL();
                intake_form_recent_tests RecentTestEntity = new intake_form_recent_tests();
                Mapper.CreateMap<IntakeTestViewModel, intake_form_recent_tests>();
                RecentTestEntity = Mapper.Map(TestViewModel, RecentTestEntity);
                objIntakeDAL.Create(RecentTestEntity);
                return 1;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }


        public List<IntakeSurgeryViewModel> GetPatientRecentsurgeries(int PatientID)
        {
            objIntakeDAL = new IntakeDAL();
            var _objTestList = new List<IntakeSurgeryViewModel>();
            List<intake_form_surgeries> RecentSurgeryEntity = objIntakeDAL.GetAll<intake_form_surgeries>(o => o.patient_id == PatientID).ToList();

            Mapper.CreateMap<intake_form_surgeries, IntakeSurgeryViewModel>();
            _objTestList = Mapper.Map(RecentSurgeryEntity, _objTestList);
            return _objTestList;
        }

        public int InsertSurgeryDetails(IntakeSurgeryViewModel SurgeryViewModel)
        {
            try
            {
                objIntakeDAL = new IntakeDAL();
                intake_form_surgeries RecentSurgeryEntity = new intake_form_surgeries();
                Mapper.CreateMap<IntakeSurgeryViewModel, intake_form_surgeries>();
                RecentSurgeryEntity = Mapper.Map(SurgeryViewModel, RecentSurgeryEntity);
                objIntakeDAL.Create(RecentSurgeryEntity);
                return 1;
            }
            catch (System.Exception )
            {
                return 0;
            }
        }

        public List<DrugViewModel> GetDrugList()
        {
            objIntakeDAL = new IntakeDAL();
            List<Drug> drugDetails = objIntakeDAL.GetAll<Drug>(o => o.Viewable_yn==true).ToList();
            var objIList = new List<DrugViewModel>();
            Mapper.CreateMap<Drug, DrugViewModel>();
            objIList = Mapper.Map(drugDetails, objIList);
            return objIList;
        }

        public List<IntakePrescriptionViewModel> GetPatientprescription(int PatientID)
        {
            objIntakeDAL = new IntakeDAL();
            return objIntakeDAL.GetPatientprescription(PatientID);
        
        }

        public int InsertPrescriptionDetails(IntakePrescriptionViewModel PrescriptionViewModel)
        {
            try
            {
                objIntakeDAL = new IntakeDAL();
                intake_form_prescriptions RecentSurgeryEntity = new intake_form_prescriptions();
                Mapper.CreateMap<IntakePrescriptionViewModel, intake_form_prescriptions>();
                RecentSurgeryEntity = Mapper.Map(PrescriptionViewModel, RecentSurgeryEntity);
                objIntakeDAL.Create(RecentSurgeryEntity);
                return 1;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        public void UpdateIntakeForm(Emrdev.ViewModelLayer.IntakeServiceViewModel objModel)
        {
            objIntakeDAL = new IntakeDAL();
            objIntakeDAL.UpdateIntakeForm(objModel);
        }

        public Emrdev.ViewModelLayer.IntakeServiceViewModel GetByGoalId(int goalId)
        {
            objIntakeDAL = new IntakeDAL();
            return objIntakeDAL.GetByGoalId(goalId);
        }
    }
}
