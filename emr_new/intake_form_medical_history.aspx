<%@ Page Title="intake_form_medical_history" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_medical_history.aspx.cs" Inherits="intake_form_medical_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span class="style1">Medical History</span>
        <br>
    </p>
    <table width="965" border="0" cellpadding="10" cellspacing="0" bgcolor="#FFFFFF" class="border">
        <tr class="border">
            <td width="461"><span class="style1">Personal History</span></td>
            <td width="461"><span class="style1">Family History</span></td>
        </tr>
        <tr>
            <td width="461">Indicate current problems by using the checkbox. Indicate problems that are not active with the years that the problem persisted. (eg. 1993 - 1995) </td>
            <td width="461">Indicate who had the problem and how old the were when that problem began by putting the age in the blank provided </td>
        </tr>
    </table>

    <p>
        <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
            <tr>
                <td>
                    <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <div align="center">You</div>
                            </td>
                            <td colspan="2">
                                <div align="center">Mother</div>
                            </td>
                            <td colspan="2">
                                <div align="center">Father</div>
                            </td>
                            <td colspan="2">
                                <div align="center">Grandparent</div>
                            </td>
                            <td colspan="2">
                                <div align="center">Sibling</div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <div align="center">Y</div>
                            </td>
                            <td>
                                <div align="left">Years persisted </div>
                            </td>
                            <td>
                                <div align="center">Y</div>
                            </td>
                            <td>
                                <div align="left">Age Began </div>
                            </td>
                            <td>
                                <div align="center">Y</div>
                            </td>
                            <td>
                                <div align="left">Age Began </div>
                            </td>
                            <td>
                                <div align="center">Y</div>
                            </td>
                            <td>
                                <div align="left">Age Began </div>
                            </td>
                            <td>
                                <div align="center">Y</div>
                            </td>
                            <td>
                                <div align="left">Age Began </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Alcohol / Drug Problem </td>
                            <td width="25">
                                <div align="center">

                                    <asp:CheckBox ID="chk_alcohol_drug_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_alcohol_drug_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="25">
                                <div align="center">
                                    <asp:CheckBox ID="chk_alcohol_drug_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_alcohol_drug_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="25">
                                <div align="center">
                                    <asp:CheckBox ID="chk_alcohol_drug_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_alcohol_drug_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="25">
                                <div align="center">
                                    <asp:CheckBox ID="chk_alcohol_drug_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_alcohol_drug_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="25">
                                <div align="center">
                                    <asp:CheckBox ID="chk_alcohol_drug_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_alcohol_drug_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Cancer - specify 
                                <asp:TextBox ID="txt_cancer_patient_type" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_cancer_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cancer_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_cancer_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cancer_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_cancer_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cancer_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_cancer_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cancer_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_cancer_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cancer_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Depression / Other Mental Illness</td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_depression_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_depression_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_depression_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_depression_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_depression_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_depression_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_depression_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_depression_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_depression_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_depression_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Dementia</td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_dementia_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_dementia_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_dementia_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_dementia_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_dementia_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_dementia_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_dementia_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_dementia_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_dementia_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_dementia_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Diabetes - Type 
            
                                <asp:DropDownList ID="ddl_diabetes_patient_type" runat="server" CssClass="border">
                                    <asp:ListItem Value="NA" Text="- Select -"></asp:ListItem>
                                    <asp:ListItem Value="Type 1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="Type 2" Text="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_diabetes_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_diabetes_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_diabetes_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_diabetes_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">
                                    <asp:CheckBox ID="chk_diabetes_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_diabetes_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_diabetes_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_diabetes_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_diabetes_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_diabetes_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Heart Disease / Heart Attack </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_heart_disease_attack_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_heart_disease_attack_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_heart_disease_attack_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_heart_disease_attack_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_heart_disease_attack_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_heart_disease_attack_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_heart_disease_attack_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_heart_disease_attack_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_heart_disease_attack_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_heart_disease_attack_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>High Cholesterol</td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_high_cholesterol_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_high_cholesterol_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_high_cholesterol_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_high_cholesterol_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_high_cholesterol_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_high_cholesterol_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_high_cholesterol_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_high_cholesterol_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_high_cholesterol_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_high_cholesterol_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Hypertension / High Blood Pressure </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_hypertension_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_hypertension_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_hypertension_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_hypertension_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_hypertension_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_hypertension_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_hypertension_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_hypertension_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_hypertension_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_hypertension_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Osteoperosis</td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_osteoporosis_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_osteoporosis_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_osteoporosis_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_osteoporosis_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_osteoporosis_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_osteoporosis_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_osteoporosis_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_osteoporosis_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_osteoporosis_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_osteoporosis_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Stroke</td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_stroke_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_stroke_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_stroke_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_stroke_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_stroke_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_stroke_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_stroke_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_stroke_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_stroke_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_stroke_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Thyroid Problem </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_thyroid_patient_active_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_thyroid_patient_past_dates" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_thyroid_mother_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_thyroid_mother_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_thyroid_father_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_thyroid_father_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_thyroid_grandparent_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_thyroid_grandparent_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <div align="center">

                                    <asp:CheckBox ID="chk_thyroid_sibling_YN" runat="server" />
                                </div>
                            </td>
                            <td>

                                <asp:TextBox ID="txt_thyroid_sibling_age_began" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="942" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
                        <tr class="border">
                            <td>&nbsp; Indicate current problems by using the checkbox.</td>
                        </tr>
                    </table>
                    <table width="942" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
                        <tr>
                            <td width="25">

                                <asp:CheckBox ID="chk_aids_hiv_YN" runat="server" />
                            </td>
                            <td>AIDS / HIV+ </td>
                            <td width="25">

                                <asp:CheckBox ID="chk_candida_yeast_YN" runat="server" />
                            </td>
                            <td>Candida / Yeast </td>
                            <td width="25">

                                <asp:CheckBox ID="chk_goiter_YN" runat="server" />
                            </td>
                            <td>Goiter</td>
                            <td width="25">
                                <asp:CheckBox ID="chk_migraines_YN" runat="server" />
                            </td>
                            <td>Migraines</td>
                            <td width="25">
                                <asp:CheckBox ID="chk_rheumatic_fever_YN" runat="server" />
                            </td>
                            <td>Rheumatic Fever</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_allergies_YN" runat="server" />
                            </td>
                            <td>Allergies / Asthma </td>
                            <td>
                                <asp:CheckBox ID="chk_chronic_fatigue_YN" runat="server" />
                            </td>
                            <td>Chronic Fatigue</td>
                            <td>
                                <asp:CheckBox ID="chk_gout_YN" runat="server" />
                            </td>
                            <td>Gout</td>
                            <td>
                                <asp:CheckBox ID="chk_multiple_sclerosis_YN" runat="server" />
                            </td>
                            <td>Multiple Sclerosis</td>
                            <td>
                                <asp:CheckBox ID="chk_root_canal_YN" runat="server" />
                            </td>
                            <td>Root Canal</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_anemia_YN" runat="server" />
                            </td>
                            <td>Anemia</td>
                            <td>
                                <asp:CheckBox ID="chk_crohns_disease_YN" runat="server" />
                            </td>
                            <td>Crohn's Disease</td>
                            <td>
                                <asp:CheckBox ID="chk_hiata_hernia_reflux_YN" runat="server" />
                            </td>
                            <td>Haital Hernia / Reflux </td>
                            <td>
                                <asp:CheckBox ID="chk_pancreatitis_YN" runat="server" />
                            </td>
                            <td>Pancreatitis</td>
                            <td>
                                <asp:CheckBox ID="chk_sinusitis_YN" runat="server" />
                            </td>
                            <td>Sinusitis</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_anorexia_bulemia_YN" runat="server" />
                            </td>
                            <td>Anorexia / Bulemia </td>
                            <td>
                                <asp:CheckBox ID="chk_colitis_YN" runat="server" />
                            </td>
                            <td>Colitis</td>
                            <td>
                                <asp:CheckBox ID="chk_irritable_bowel_YN" runat="server" />
                            </td>
                            <td>Irritable Bowel </td>
                            <td>
                                <asp:CheckBox ID="chk_parasites_YN" runat="server" />
                            </td>
                            <td>Parasites</td>
                            <td>
                                <asp:CheckBox ID="chk_suicide_attempt_YN" runat="server" />
                            </td>
                            <td>Suicide Attempt </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_arthritis_YN" runat="server" />
                            </td>
                            <td>Arthritis</td>
                            <td>
                                <asp:CheckBox ID="chk_depression_YN" runat="server" />
                            </td>
                            <td>Depression</td>
                            <td>
                                <asp:CheckBox ID="chk_jaundice_YN" runat="server" />
                            </td>
                            <td>Jaundice</td>
                            <td>
                                <asp:CheckBox ID="chk_parkinsons_YN" runat="server" />
                            </td>
                            <td>Parkinson's</td>
                            <td>
                                <asp:CheckBox ID="chk_tmj_YN" runat="server" />
                            </td>
                            <td>TMJ</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_atrial_fibrillation_YN" runat="server" />
                            </td>
                            <td>Atrial Fibrillation </td>
                            <td>
                                <asp:CheckBox ID="chk_emphysema_YN" runat="server" />
                            </td>
                            <td>Emphysema</td>
                            <td>
                                <asp:CheckBox ID="chk_kidney_disorder_YN" runat="server" />
                            </td>
                            <td>Kidney Disorder</td>
                            <td>
                                <asp:CheckBox ID="chk_pelvic_infl_disease_YN" runat="server" />
                            </td>
                            <td>Pelvic Infl. Disease</td>
                            <td>
                                <asp:CheckBox ID="chk_tooth_abscess_YN" runat="server" />
                            </td>
                            <td>Tooth Abscess </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_anxiety_YN" runat="server" />
                            </td>
                            <td>Anxiety / Panic Disorder </td>
                            <td>
                                <asp:CheckBox ID="chk_epilepsy_YN" runat="server" />
                            </td>
                            <td>Epilepsy / Seizures</td>
                            <td>
                                <asp:CheckBox ID="chk_kidney_stones_YN" runat="server" />
                            </td>
                            <td>Kidney Stones </td>
                            <td>
                                <asp:CheckBox ID="chk_pneumonia_YN" runat="server" />
                            </td>
                            <td>Pneumonia</td>
                            <td>
                                <asp:CheckBox ID="chk_tuberculosis_YN" runat="server" />
                            </td>
                            <td>Tuberculosis</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_back_pain_YN" runat="server" />
                            </td>
                            <td>Back Pain</td>
                            <td>
                                <asp:CheckBox ID="chk_fibromyalgia_YN" runat="server" />
                            </td>
                            <td>Fibromyalgia</td>
                            <td>
                                <asp:CheckBox ID="chk_liver_disease_YN" runat="server" />
                            </td>
                            <td>Liver Disease </td>
                            <td>
                                <asp:CheckBox ID="chk_polio_YN" runat="server" />
                            </td>
                            <td>Polio</td>
                            <td>
                                <asp:CheckBox ID="chk_ulcers_YN" runat="server" />
                            </td>
                            <td>Ulcers</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chk_bleeding_disorder_YN" runat="server" />
                            </td>
                            <td>Bleeding Disorder </td>
                            <td>
                                <asp:CheckBox ID="chk_glaucoma_YN" runat="server" />
                            </td>
                            <td>Glaucoma</td>
                            <td>
                                <asp:CheckBox ID="chk_hepatitis_YN" runat="server" />
                            </td>
                            <td>Hepatitis</td>
                            <td>
                                <asp:CheckBox ID="chk_prostate_problem_YN" runat="server" />
                            </td>
                            <td>Prostate Problem </td>
                            <td>
                                <asp:CheckBox ID="chk_urinary_infection_YN" runat="server" />
                            </td>
                            <td>Urinary Infection </td>
                        </tr>
                    </table>
                    <table width="942" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF" class="border">
                        <tr class="border">
                            <td width="50">other</td>
                            <td>
                                <asp:TextBox ID="txt_other_text" runat="server" CssClass="FormField" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="942" border="0" cellpadding="10" cellspacing="0">
            <tr>
                <td>
                    <div align="center">
                        <asp:Button ID="btnNextPage" runat="server" CssClass="button" Text="Next Page" OnClick="btnNextPage_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </p>

    <p>&nbsp;</p>
    <p>&nbsp;</p>
</asp:Content>

