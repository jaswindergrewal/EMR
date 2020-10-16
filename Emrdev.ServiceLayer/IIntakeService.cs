using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIntakeService" in both code and config file together.
    [ServiceContract]
    public interface IIntakeService
    {
        [OperationContract]
        void AddIntakeFormHabits(int master_form_id, DateTime Date_Entered, string alcohol_type, string alcohol_amount,
           string alcohol_frequency, string drugs_type, string drugs_amount, string drugs_frequency, int cig_packs_per_day, int cig_years,
           int chew_amount_per_day, int chew_years, bool i_want_to_quit_YN, int caffeine_serv_per_day, int nutrasweet_serv_per_day,
           int saccharin_serv_per_day, int msg_serv_per_day, int patient_id);

        [OperationContract]
        int InsertMasterFormIntake(int PatientID);

        [OperationContract]
        void InsertIntakePersonalInfo(int PatientId, int MasterId, string marital_status, string Education, string current_occupation, bool occupation_enjoy_YN, bool occupation_stress_YN, bool occupation_fulfill_YN,
          bool occupation_hazardous_YN, bool retired_YN, string retired_occupation, DateTime ? retired_date_of, bool retired_happy_YN);

        [OperationContract]
         void InsertIntakeAllergy(int PatientId, int MasterId, string drug_or_med_allergy, string food_allergy, bool drug_or_med_allergy_YN,
                                            bool food_allergy_YN);

        [OperationContract]
        void AddIntakeFormGoalsReview(int master_form_id, int patient_id, DateTime Date_Entered, bool balance_hormones_YN, bool start_hormones_YN, bool improve_energy_YN,
           bool feel_better_YN, bool feel_stronger_YN, bool improve_sleep_YN, bool eliminate_hot_flashes_YN, bool eliminate_prescriptions_YN, bool weight_loss_YN,
           bool stabalize_PMS_YN, bool stop_hair_loss_YN, bool sense_of_well_being_YN, bool enhance_immune_sys_YN, bool less_pain_YN,
           string less_pain_where, bool improve_libido_YN, bool improve_sex_life_YN, bool improve_muscle_YN,
           bool improve_memory_YN, bool bladder_control_YN, bool improve_skin_YN, bool better_stamina_YN, bool general_wellness_YN,
           bool reduce_stress_YN, bool improve_metabolism_YN, string other);

        
        [OperationContract]
        void InsertIntakeSymptoms(int PatientId, int MasterId, string YrStr, string OtherSymptoms);

        [OperationContract]
        void InsertIntakeGoals(int PatientId, int MasterId, string ChkGoalStr, string LessPain, string OtherSymptoms);

        [OperationContract]
        void InsertIntakeSuppliments(int PatientId, int MasterId, string ChkStr, string Other);

        [OperationContract]
        void InsertIntakeFormMedicalHistory(IntakeFormMedicalHistoryViewModel objIntakeFormMedicalHistoryViewModel);

        [OperationContract]
        List<IntakeTestViewModel>GetPatientRecentTestByPatientId(int PatientID);

        [OperationContract]
        int InsertTestDetails(IntakeTestViewModel TestViewModel);

        [OperationContract]
        List<IntakeSurgeryViewModel> GetPatientRecentsurgeries(int PatientID);

        [OperationContract]
        int InsertSurgeryDetails(IntakeSurgeryViewModel SurgeryViewModel);

        [OperationContract]
        List<DrugViewModel> GetDrugList();

        [OperationContract]
        List<IntakePrescriptionViewModel> GetPatientprescription(int PatientID);

        [OperationContract]
        int InsertPrescriptionDetails(IntakePrescriptionViewModel PrescriptionViewModel);

        [OperationContract]
        void DoWork();

        [OperationContract]
        void UpdateIntakeForm(Emrdev.ViewModelLayer.IntakeServiceViewModel objModel);

        [OperationContract]
        IntakeServiceViewModel GetByGoalId(int goalId);
         
    }
}
