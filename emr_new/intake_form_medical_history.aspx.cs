using Emrdev.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
public partial class intake_form_medical_history : System.Web.UI.Page
{
    #region Global Variable/objects
    IntakeService objService = null;
    #endregion

    #region Page_Load
    /// <summary>
    /// this is page load event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }
    #endregion

    #region btnNextPage_Click
    /// <summary>
    /// this event used for insert data in database intake_form_medical_history table.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        try
        {
            InsertIntakeFormMedicalHistory();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    #endregion

    #region InsertIntakeFormMedicalHistory
    /// <summary>
    /// this function used for insert data in database intake_form_medical_history table.
    /// </summary>
    ///Created By : Rakeh Kumar
    ///Created Date : 22-Aug-2013
    private void InsertIntakeFormMedicalHistory()
    {
        IntakeFormMedicalHistoryViewModel objIntakeFormMedicalHistoryViewModel = new IntakeFormMedicalHistoryViewModel();
        try
        {
            objService = new IntakeService();
            objIntakeFormMedicalHistoryViewModel.master_form_id = Convert.ToInt32(Request.QueryString["form_id"].ToString());
            objIntakeFormMedicalHistoryViewModel.Date_Entered = DateTime.Now;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_patient_active_YN = chk_alcohol_drug_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_patient_past_dates = txt_alcohol_drug_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_mother_YN = chk_alcohol_drug_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_mother_age_began = txt_alcohol_drug_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_father_YN = chk_alcohol_drug_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_father_age_began = txt_alcohol_drug_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_grandparent_YN = chk_alcohol_drug_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_grandparent_age_began = txt_alcohol_drug_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_sibling_YN = chk_alcohol_drug_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.alcohol_drug_sibling_age_began = txt_alcohol_drug_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_patient_active_YN = chk_cancer_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.cancer_patient_type = txt_cancer_patient_type.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_patient_past_dates = txt_cancer_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_mother_YN = chk_cancer_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.cancer_mother_age_began = txt_cancer_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_father_YN = chk_cancer_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.cancer_father_age_began = txt_cancer_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_grandparent_YN = chk_cancer_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.cancer_grandparent_age_began = txt_cancer_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.cancer_sibling_YN = chk_cancer_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.cancer_sibling_age_began = txt_cancer_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.depression_patient_active_YN = chk_depression_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_patient_past_dates = txt_depression_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.depression_mother_YN = chk_depression_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_mother_age_began = txt_depression_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.depression_father_YN = chk_depression_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_father_age_began = txt_depression_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.depression_grandparent_YN = chk_depression_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_grandparent_age_began = txt_depression_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.depression_sibling_YN = chk_depression_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_sibling_age_began = txt_depression_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.dementia_patient_active_YN = chk_dementia_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.dementia_patient_past_dates = txt_dementia_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.dementia_mother_YN = chk_dementia_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.dementia_mother_age_began = txt_dementia_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.dementia_father_YN = chk_dementia_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.dementia_father_age_began = txt_dementia_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.dementia_grandparent_YN = chk_dementia_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.dementia_grandparent_age_began = txt_dementia_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.dementia_sibling_YN = chk_dementia_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.dementia_sibling_age_began = txt_dementia_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.diabetes_patient_active_YN = chk_diabetes_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.diabetes_patient_type = ddl_diabetes_patient_type.SelectedValue.ToString();
            objIntakeFormMedicalHistoryViewModel.diabetes_patient_past_dates = txt_diabetes_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.diabetes_mother_YN = chk_diabetes_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.diabetes_mother_age_began = txt_diabetes_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.diabetes_father_YN = chk_diabetes_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.diabetes_father_age_began = txt_diabetes_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.diabetes_grandparent_YN = chk_diabetes_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.diabetes_grandparent_age_began = txt_diabetes_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.diabetes_sibling_YN = chk_diabetes_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.diabetes_sibling_age_began = txt_diabetes_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_patient_active_YN = chk_heart_disease_attack_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_patient_past_dates = txt_heart_disease_attack_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_mother_YN = chk_heart_disease_attack_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_mother_age_began = txt_heart_disease_attack_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_father_YN = chk_heart_disease_attack_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_father_age_began = txt_heart_disease_attack_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_grandparent_YN = chk_heart_disease_attack_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_grandparent_age_began = txt_heart_disease_attack_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_sibling_YN = chk_heart_disease_attack_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.heart_disease_attack_sibling_age_began = txt_heart_disease_attack_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_patient_active_YN = chk_high_cholesterol_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_patient_past_dates = txt_high_cholesterol_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_mother_YN = chk_high_cholesterol_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_mother_age_began = txt_high_cholesterol_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_father_YN = chk_high_cholesterol_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_father_age_began = txt_high_cholesterol_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_grandparent_YN = chk_high_cholesterol_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_grandparent_age_began = txt_high_cholesterol_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_sibling_YN = chk_high_cholesterol_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.high_cholesterol_sibling_age_began = txt_high_cholesterol_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.hypertension_patient_active_YN = chk_hypertension_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hypertension_patient_past_dates = txt_hypertension_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.hypertension_mother_YN = chk_hypertension_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hypertension_mother_age_began = txt_hypertension_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.hypertension_father_YN = chk_hypertension_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hypertension_father_age_began = txt_hypertension_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.hypertension_grandparent_YN = chk_hypertension_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hypertension_grandparent_age_began = txt_hypertension_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.hypertension_sibling_YN = chk_hypertension_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hypertension_sibling_age_began = txt_hypertension_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_patient_active_YN = chk_osteoporosis_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_patient_past_dates = txt_osteoporosis_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_mother_YN = chk_osteoporosis_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_mother_age_began = txt_osteoporosis_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_father_YN = chk_osteoporosis_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_father_age_began = txt_osteoporosis_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_grandparent_YN = chk_osteoporosis_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_grandparent_age_began = txt_osteoporosis_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_sibling_YN = chk_osteoporosis_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.osteoporosis_sibling_age_began = txt_osteoporosis_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.stroke_patient_active_YN = chk_stroke_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.stroke_patient_past_dates = txt_stroke_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.stroke_mother_YN = chk_stroke_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.stroke_mother_age_began = txt_stroke_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.stroke_father_YN = chk_stroke_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.stroke_father_age_began = txt_stroke_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.stroke_grandparent_YN = chk_stroke_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.stroke_grandparent_age_began = txt_stroke_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.stroke_sibling_YN = chk_stroke_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.stroke_sibling_age_began = txt_stroke_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.thyroid_patient_active_YN = chk_thyroid_patient_active_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.thyroid_patient_past_dates = txt_thyroid_patient_past_dates.Text;
            objIntakeFormMedicalHistoryViewModel.thyroid_mother_YN = chk_thyroid_mother_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.thyroid_mother_age_began = txt_thyroid_mother_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.thyroid_father_YN = chk_thyroid_father_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.thyroid_father_age_began = txt_thyroid_father_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.thyroid_grandparent_YN = chk_thyroid_grandparent_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.thyroid_grandparent_age_began = txt_thyroid_grandparent_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.thyroid_sibling_YN = chk_thyroid_sibling_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.thyroid_sibling_age_began = txt_thyroid_sibling_age_began.Text;
            objIntakeFormMedicalHistoryViewModel.aids_hiv_YN = chk_aids_hiv_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.allergies_YN = chk_allergies_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.anemia_YN = chk_anemia_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.anorexia_bulemia_YN = chk_anorexia_bulemia_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.arthritis_YN = chk_arthritis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.atrial_fibrillation_YN = chk_atrial_fibrillation_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.anxiety_YN = chk_anxiety_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.back_pain_YN = chk_back_pain_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.bleeding_disorder_YN = chk_bleeding_disorder_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.candida_yeast_YN = chk_candida_yeast_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.chronic_fatigue_YN = chk_chronic_fatigue_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.crohns_disease_YN = chk_crohns_disease_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.colitis_YN = chk_colitis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.depression_YN = chk_depression_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.emphysema_YN = chk_emphysema_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.epilepsy_YN = chk_epilepsy_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.fibromyalgia_YN = chk_fibromyalgia_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.glaucoma_YN = chk_glaucoma_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.goiter_YN = chk_goiter_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.gout_YN = chk_gout_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hiata_hernia_reflux_YN = chk_hiata_hernia_reflux_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.irritable_bowel_YN = chk_irritable_bowel_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.jaundice_YN = chk_jaundice_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.kidney_disorder_YN = chk_kidney_disorder_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.kidney_stones_YN = chk_kidney_stones_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.liver_disease_YN = chk_liver_disease_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.hepatitis_YN = chk_hepatitis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.migraines_YN = chk_migraines_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.multiple_sclerosis_YN = chk_multiple_sclerosis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.pancreatitis_YN = chk_pancreatitis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.parasites_YN = chk_parasites_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.parkinsons_YN = chk_parkinsons_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.pelvic_infl_disease_YN = chk_pelvic_infl_disease_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.pneumonia_YN = chk_pneumonia_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.polio_YN = chk_polio_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.prostate_problem_YN = chk_prostate_problem_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.rheumatic_fever_YN = chk_rheumatic_fever_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.root_canal_YN = chk_root_canal_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.sinusitis_YN = chk_sinusitis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.suicide_attempt_YN = chk_suicide_attempt_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.tmj_YN = chk_tmj_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.tooth_abscess_YN = chk_tooth_abscess_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.tuberculosis_YN = chk_tuberculosis_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.ulcers_YN = chk_ulcers_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.urinary_infection_YN = chk_urinary_infection_YN.Checked;
            objIntakeFormMedicalHistoryViewModel.other_text = txt_other_text.Text;
            objIntakeFormMedicalHistoryViewModel.patient_id = Convert.ToInt32(Request.QueryString["patientid"].ToString());

            objService.InsertIntakeFormMedicalHistory(objIntakeFormMedicalHistoryViewModel);
            int MasterId;
            if (Request.QueryString["form_id"] == null)
            {
                MasterId = 0;
            }
            else
            {
                MasterId = int.Parse(Request.QueryString["form_id"]);
            }

            Response.Redirect("intake_form_symptoms.aspx?patientid=" + Request.QueryString["patientid"] + "&form_id=" + MasterId,false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
            objIntakeFormMedicalHistoryViewModel = null;
        }
    }
    #endregion
}