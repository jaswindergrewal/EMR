using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 22nd aug 2013
/// </summary>
public partial class intake_form_personal_info : LMCBase
{
    #region "Variables"
    IAppointmentConsole objPatientService = null;
    PatientViewModel patientDetail = null;
    IntakeService objService = null;

    #endregion

    #region"Events"

    /// <summary>
    /// Get the patient details
    /// Jaswinder 22 aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        txtretired_date_of.Attributes.Add("readonly", "readonly");

        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["patientid"] != null)
                {
                    objPatientService = new AppointmentConsole();
                    patientDetail = objPatientService.GetPatientList(int.Parse(Request.QueryString["patientid"]));
                    if (patientDetail != null)
                    {
                        lblFirstName.Text = patientDetail.FirstName;
                        lblLastName.Text = patientDetail.LastName;
                        lblBirthDay.Text = patientDetail.Birthday.ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
            }
            finally { 
                objPatientService = null;
                patientDetail = null;
            }
        }
    }


    /// <summary>
    /// Enter the patent personal intake data
    /// Jaswinder 22nd aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new IntakeService();
            bool occupation_enjoy_YN = false;
            bool occupation_stress_YN = false;
            bool occupation_fulfill_YN = false;
            bool occupation_hazardous_YN = false;
            bool retired_YN = false;
            bool retired_happy_YN = false;
            DateTime ? RetiredDate;
            int MasterId;

            if (Request.QueryString["form_id"] == null)
            {
                MasterId = 0;
            }
            else
            {
                MasterId = int.Parse(Request.QueryString["form_id"]);
            }

            if (rdloccupation_enjoy_YN.SelectedValue == "1")
            {
                occupation_enjoy_YN = true;
            }

            if (rdloccupation_stress_YN.SelectedValue == "1")
            {
                occupation_stress_YN = true;
            }

            if (rdloccupation_fulfill_YN.SelectedValue == "1")
            {
                occupation_fulfill_YN = true;
            }

            if (rdloccupation_hazardous_YN.SelectedValue == "1")
            {
                occupation_hazardous_YN = true;
            }

            if (rdlretired_YN.SelectedValue == "1")
            {
                retired_YN = true;
            }

            if (rdlretired_happy_YN.SelectedValue == "1")
            {
                retired_happy_YN = true;
            }

            if (txtretired_date_of.Text != "")
            {
                RetiredDate = DateTime.Parse(txtretired_date_of.Text);
            }
            else
            { RetiredDate = null; }
            objService.InsertIntakePersonalInfo(int.Parse(Request.QueryString["patientid"]), MasterId, ddlmarital_status.SelectedValue, ddlEducation.SelectedValue, txtcurrent_occupation.Text,
                                                occupation_enjoy_YN, occupation_stress_YN, occupation_fulfill_YN, occupation_hazardous_YN,
                                                retired_YN, txtretired_occupation.Text, RetiredDate, retired_happy_YN);

            Response.Redirect("intake_form_goals.aspx?patientid=" + Request.QueryString["patientid"] + "&form_id=" + MasterId,false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
           
        }
    }

    #endregion
}