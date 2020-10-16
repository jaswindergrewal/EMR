using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IntakeService" in both code and config file together.
    public class IntakeService : IIntakeService
    {
        public void DoWork()
        {
        }

        IntakeBAL objIntakeBAL = null;
        public void AddIntakeFormHabits(int master_form_id, DateTime Date_Entered, string alcohol_type, string alcohol_amount, string alcohol_frequency, string drugs_type, string drugs_amount, string drugs_frequency, int cig_packs_per_day, int cig_years, int chew_amount_per_day, int chew_years, bool i_want_to_quit_YN, int caffeine_serv_per_day, int nutrasweet_serv_per_day, int saccharin_serv_per_day, int msg_serv_per_day, int patient_id)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.AddIntakeFormHabits(master_form_id, Date_Entered, alcohol_type, alcohol_amount, alcohol_frequency, drugs_type, drugs_amount, drugs_frequency, cig_packs_per_day, cig_years, chew_amount_per_day, chew_years, i_want_to_quit_YN, caffeine_serv_per_day, nutrasweet_serv_per_day, saccharin_serv_per_day, msg_serv_per_day, patient_id);
        }

        public int InsertMasterFormIntake(int PatientID)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.InsertMasterFormIntake(PatientID);
        }

        public void InsertIntakePersonalInfo(int PatientId, int MasterId, string marital_status, string Education, string current_occupation, bool occupation_enjoy_YN, bool occupation_stress_YN, bool occupation_fulfill_YN,
            bool occupation_hazardous_YN, bool retired_YN, string retired_occupation, DateTime ? retired_date_of, bool retired_happy_YN)
        { 
         objIntakeBAL = new IntakeBAL();
         objIntakeBAL.InsertIntakePersonalInfo(PatientId, MasterId, marital_status, Education, current_occupation, occupation_enjoy_YN, occupation_stress_YN, occupation_fulfill_YN,
          occupation_hazardous_YN, retired_YN, retired_occupation, retired_date_of, retired_happy_YN);
        }

        public void InsertIntakeAllergy(int PatientId, int MasterId, string drug_or_med_allergy, string food_allergy, bool drug_or_med_allergy_YN,
                                            bool food_allergy_YN)
        {
         objIntakeBAL = new IntakeBAL();
         objIntakeBAL.InsertIntakeAllergy(PatientId, MasterId, drug_or_med_allergy, food_allergy, drug_or_med_allergy_YN,
                                           food_allergy_YN);
        }

        public void InsertIntakeFormMedicalHistory(IntakeFormMedicalHistoryViewModel objIntakeFormMedicalHistoryViewModel)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.InsertIntakeFormMedicalHistory(objIntakeFormMedicalHistoryViewModel);
        }

        public void AddIntakeFormGoalsReview(int master_form_id, int patient_id, DateTime Date_Entered, bool balance_hormones_YN, bool start_hormones_YN, bool improve_energy_YN, bool feel_better_YN, bool feel_stronger_YN, bool improve_sleep_YN, bool eliminate_hot_flashes_YN, bool eliminate_prescriptions_YN, bool weight_loss_YN, bool stabalize_PMS_YN, bool stop_hair_loss_YN, bool sense_of_well_being_YN, bool enhance_immune_sys_YN, bool less_pain_YN, string less_pain_where, bool improve_libido_YN, bool improve_sex_life_YN, bool improve_muscle_YN, bool improve_memory_YN, bool bladder_control_YN, bool improve_skin_YN, bool better_stamina_YN, bool general_wellness_YN, bool reduce_stress_YN, bool improve_metabolism_YN, string other)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.AddIntakeFormGoalsReview(master_form_id, patient_id, Date_Entered, balance_hormones_YN, start_hormones_YN, improve_energy_YN, feel_better_YN, feel_stronger_YN, improve_sleep_YN, eliminate_hot_flashes_YN, eliminate_prescriptions_YN, weight_loss_YN, stabalize_PMS_YN, stop_hair_loss_YN, sense_of_well_being_YN, enhance_immune_sys_YN, less_pain_YN, less_pain_where, improve_libido_YN, improve_sex_life_YN, improve_muscle_YN, improve_memory_YN, bladder_control_YN, improve_skin_YN, better_stamina_YN, general_wellness_YN, reduce_stress_YN, improve_metabolism_YN, other);
        }

        public void InsertIntakeSymptoms(int PatientId, int MasterId, string YrStr, string OtherSymptoms)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.InsertIntakeSymptoms(PatientId, MasterId, YrStr, OtherSymptoms);
        }

        public void InsertIntakeGoals(int PatientId, int MasterId, string ChkGoalStr, string LessPain, string OtherSymptoms)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.InsertIntakeGoals(PatientId, MasterId, ChkGoalStr, LessPain, OtherSymptoms);
        }

        public void InsertIntakeSuppliments(int PatientId, int MasterId, string ChkStr, string Other)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.InsertIntakeSuppliments(PatientId, MasterId, ChkStr, Other);
        }

       

        public List<IntakeTestViewModel> GetPatientRecentTestByPatientId(int PatientID)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.GetPatientRecentTestByPatientId(PatientID);
        }

        public int InsertTestDetails(IntakeTestViewModel TestViewModel)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.InsertTestDetails(TestViewModel);
        }

        public List<IntakeSurgeryViewModel> GetPatientRecentsurgeries(int PatientID)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.GetPatientRecentsurgeries(PatientID);
        }

        public int InsertSurgeryDetails(IntakeSurgeryViewModel SurgeryViewModel)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.InsertSurgeryDetails(SurgeryViewModel);
        }

        public List<DrugViewModel> GetDrugList()
        {
            objIntakeBAL = new IntakeBAL();
            List<DrugViewModel> lstDrug = objIntakeBAL.GetDrugList();
            return lstDrug;
        }


        public List<IntakePrescriptionViewModel> GetPatientprescription(int PatientID)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.GetPatientprescription(PatientID);
        }

        public int InsertPrescriptionDetails(IntakePrescriptionViewModel PrescriptionViewModel)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.InsertPrescriptionDetails(PrescriptionViewModel);
        }

        public void UpdateIntakeForm(Emrdev.ViewModelLayer.IntakeServiceViewModel objModel)
        {
            objIntakeBAL = new IntakeBAL();
            objIntakeBAL.UpdateIntakeForm(objModel);
        }

        public Emrdev.ViewModelLayer.IntakeServiceViewModel GetByGoalId(int goalId)
        {
            objIntakeBAL = new IntakeBAL();
            return objIntakeBAL.GetByGoalId(goalId);
        }
    }
}
