//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class intake_form_medical_history
    {
        public int medical_history_id { get; set; }
        public Nullable<int> master_form_id { get; set; }
        public Nullable<System.DateTime> Date_Entered { get; set; }
        public Nullable<bool> alcohol_drug_patient_active_YN { get; set; }
        public string alcohol_drug_patient_past_dates { get; set; }
        public Nullable<bool> alcohol_drug_mother_YN { get; set; }
        public string alcohol_drug_mother_age_began { get; set; }
        public Nullable<bool> alcohol_drug_father_YN { get; set; }
        public string alcohol_drug_father_age_began { get; set; }
        public Nullable<bool> alcohol_drug_grandparent_YN { get; set; }
        public string alcohol_drug_grandparent_age_began { get; set; }
        public Nullable<bool> alcohol_drug_sibling_YN { get; set; }
        public string alcohol_drug_sibling_age_began { get; set; }
        public Nullable<bool> cancer_patient_active_YN { get; set; }
        public string cancer_patient_type { get; set; }
        public string cancer_patient_past_dates { get; set; }
        public Nullable<bool> cancer_mother_YN { get; set; }
        public string cancer_mother_age_began { get; set; }
        public Nullable<bool> cancer_father_YN { get; set; }
        public string cancer_father_age_began { get; set; }
        public Nullable<bool> cancer_grandparent_YN { get; set; }
        public string cancer_grandparent_age_began { get; set; }
        public Nullable<bool> cancer_sibling_YN { get; set; }
        public string cancer_sibling_age_began { get; set; }
        public Nullable<bool> depression_patient_active_YN { get; set; }
        public string depression_patient_past_dates { get; set; }
        public Nullable<bool> depression_mother_YN { get; set; }
        public string depression_mother_age_began { get; set; }
        public Nullable<bool> depression_father_YN { get; set; }
        public string depression_father_age_began { get; set; }
        public Nullable<bool> depression_grandparent_YN { get; set; }
        public string depression_grandparent_age_began { get; set; }
        public Nullable<bool> depression_sibling_YN { get; set; }
        public string depression_sibling_age_began { get; set; }
        public Nullable<bool> dementia_patient_active_YN { get; set; }
        public string dementia_patient_past_dates { get; set; }
        public Nullable<bool> dementia_mother_YN { get; set; }
        public string dementia_mother_age_began { get; set; }
        public Nullable<bool> dementia_father_YN { get; set; }
        public string dementia_father_age_began { get; set; }
        public Nullable<bool> dementia_grandparent_YN { get; set; }
        public string dementia_grandparent_age_began { get; set; }
        public Nullable<bool> dementia_sibling_YN { get; set; }
        public string dementia_sibling_age_began { get; set; }
        public Nullable<bool> diabetes_patient_active_YN { get; set; }
        public string diabetes_patient_type { get; set; }
        public string diabetes_patient_past_dates { get; set; }
        public Nullable<bool> diabetes_mother_YN { get; set; }
        public string diabetes_mother_age_began { get; set; }
        public Nullable<bool> diabetes_father_YN { get; set; }
        public string diabetes_father_age_began { get; set; }
        public Nullable<bool> diabetes_grandparent_YN { get; set; }
        public string diabetes_grandparent_age_began { get; set; }
        public Nullable<bool> diabetes_sibling_YN { get; set; }
        public string diabetes_sibling_age_began { get; set; }
        public Nullable<bool> heart_disease_attack_patient_active_YN { get; set; }
        public string heart_disease_attack_patient_past_dates { get; set; }
        public Nullable<bool> heart_disease_attack_mother_YN { get; set; }
        public string heart_disease_attack_mother_age_began { get; set; }
        public Nullable<bool> heart_disease_attack_father_YN { get; set; }
        public string heart_disease_attack_father_age_began { get; set; }
        public Nullable<bool> heart_disease_attack_grandparent_YN { get; set; }
        public string heart_disease_attack_grandparent_age_began { get; set; }
        public Nullable<bool> heart_disease_attack_sibling_YN { get; set; }
        public string heart_disease_attack_sibling_age_began { get; set; }
        public Nullable<bool> high_cholesterol_patient_active_YN { get; set; }
        public string high_cholesterol_patient_past_dates { get; set; }
        public Nullable<bool> high_cholesterol_mother_YN { get; set; }
        public string high_cholesterol_mother_age_began { get; set; }
        public Nullable<bool> high_cholesterol_father_YN { get; set; }
        public string high_cholesterol_father_age_began { get; set; }
        public Nullable<bool> high_cholesterol_grandparent_YN { get; set; }
        public string high_cholesterol_grandparent_age_began { get; set; }
        public Nullable<bool> high_cholesterol_sibling_YN { get; set; }
        public string high_cholesterol_sibling_age_began { get; set; }
        public Nullable<bool> hypertension_patient_active_YN { get; set; }
        public string hypertension_patient_past_dates { get; set; }
        public Nullable<bool> hypertension_mother_YN { get; set; }
        public string hypertension_mother_age_began { get; set; }
        public Nullable<bool> hypertension_father_YN { get; set; }
        public string hypertension_father_age_began { get; set; }
        public Nullable<bool> hypertension_grandparent_YN { get; set; }
        public string hypertension_grandparent_age_began { get; set; }
        public Nullable<bool> hypertension_sibling_YN { get; set; }
        public string hypertension_sibling_age_began { get; set; }
        public Nullable<bool> osteoporosis_patient_active_YN { get; set; }
        public string osteoporosis_patient_past_dates { get; set; }
        public Nullable<bool> osteoporosis_mother_YN { get; set; }
        public string osteoporosis_mother_age_began { get; set; }
        public Nullable<bool> osteoporosis_father_YN { get; set; }
        public string osteoporosis_father_age_began { get; set; }
        public Nullable<bool> osteoporosis_grandparent_YN { get; set; }
        public string osteoporosis_grandparent_age_began { get; set; }
        public Nullable<bool> osteoporosis_sibling_YN { get; set; }
        public string osteoporosis_sibling_age_began { get; set; }
        public Nullable<bool> stroke_patient_active_YN { get; set; }
        public string stroke_patient_past_dates { get; set; }
        public Nullable<bool> stroke_mother_YN { get; set; }
        public string stroke_mother_age_began { get; set; }
        public Nullable<bool> stroke_father_YN { get; set; }
        public string stroke_father_age_began { get; set; }
        public Nullable<bool> stroke_grandparent_YN { get; set; }
        public string stroke_grandparent_age_began { get; set; }
        public Nullable<bool> stroke_sibling_YN { get; set; }
        public string stroke_sibling_age_began { get; set; }
        public Nullable<bool> thyroid_patient_active_YN { get; set; }
        public string thyroid_patient_past_dates { get; set; }
        public Nullable<bool> thyroid_mother_YN { get; set; }
        public string thyroid_mother_age_began { get; set; }
        public Nullable<bool> thyroid_father_YN { get; set; }
        public string thyroid_father_age_began { get; set; }
        public Nullable<bool> thyroid_grandparent_YN { get; set; }
        public string thyroid_grandparent_age_began { get; set; }
        public Nullable<bool> thyroid_sibling_YN { get; set; }
        public string thyroid_sibling_age_began { get; set; }
        public Nullable<bool> aids_hiv_YN { get; set; }
        public Nullable<bool> allergies_YN { get; set; }
        public Nullable<bool> anemia_YN { get; set; }
        public Nullable<bool> anorexia_bulemia_YN { get; set; }
        public Nullable<bool> arthritis_YN { get; set; }
        public Nullable<bool> atrial_fibrillation_YN { get; set; }
        public Nullable<bool> anxiety_YN { get; set; }
        public Nullable<bool> back_pain_YN { get; set; }
        public Nullable<bool> bleeding_disorder_YN { get; set; }
        public Nullable<bool> candida_yeast_YN { get; set; }
        public Nullable<bool> chronic_fatigue_YN { get; set; }
        public Nullable<bool> crohns_disease_YN { get; set; }
        public Nullable<bool> colitis_YN { get; set; }
        public Nullable<bool> depression_YN { get; set; }
        public Nullable<bool> emphysema_YN { get; set; }
        public Nullable<bool> epilepsy_YN { get; set; }
        public Nullable<bool> fibromyalgia_YN { get; set; }
        public Nullable<bool> glaucoma_YN { get; set; }
        public Nullable<bool> goiter_YN { get; set; }
        public Nullable<bool> gout_YN { get; set; }
        public Nullable<bool> hiata_hernia_reflux_YN { get; set; }
        public Nullable<bool> irritable_bowel_YN { get; set; }
        public Nullable<bool> jaundice_YN { get; set; }
        public Nullable<bool> kidney_disorder_YN { get; set; }
        public Nullable<bool> kidney_stones_YN { get; set; }
        public Nullable<bool> liver_disease_YN { get; set; }
        public Nullable<bool> hepatitis_YN { get; set; }
        public Nullable<bool> migraines_YN { get; set; }
        public Nullable<bool> multiple_sclerosis_YN { get; set; }
        public Nullable<bool> pancreatitis_YN { get; set; }
        public Nullable<bool> parasites_YN { get; set; }
        public Nullable<bool> parkinsons_YN { get; set; }
        public Nullable<bool> pelvic_infl_disease_YN { get; set; }
        public Nullable<bool> pneumonia_YN { get; set; }
        public Nullable<bool> polio_YN { get; set; }
        public Nullable<bool> prostate_problem_YN { get; set; }
        public Nullable<bool> rheumatic_fever_YN { get; set; }
        public Nullable<bool> root_canal_YN { get; set; }
        public Nullable<bool> sinusitis_YN { get; set; }
        public Nullable<bool> suicide_attempt_YN { get; set; }
        public Nullable<bool> tmj_YN { get; set; }
        public Nullable<bool> tooth_abscess_YN { get; set; }
        public Nullable<bool> tuberculosis_YN { get; set; }
        public Nullable<bool> ulcers_YN { get; set; }
        public Nullable<bool> urinary_infection_YN { get; set; }
        public string other_text { get; set; }
        public Nullable<int> patient_id { get; set; }
    }
}
