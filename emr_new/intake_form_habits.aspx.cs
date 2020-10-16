using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class intake_form_habits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// code for insert the intake form details.
    /// created by: Deepak Thakur[21.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        IIntakeService objIIntakeService = null;
        try
        {
            int PatientId = 0;
            int MasterFormId = 0;
            bool isSelected = false;
            if (Request.QueryString["patientid"] != null)
                PatientId = int.Parse(Request.QueryString["patientid"].ToString());
            if (Request.QueryString["form_id"] != null)
                MasterFormId = int.Parse(Request.QueryString["form_id"].ToString());

            if (rdbQuitYn.SelectedValue == "0")
                isSelected = true;

            objIIntakeService = new IntakeService();
            objIIntakeService.AddIntakeFormHabits(MasterFormId, System.DateTime.Now, txtAlcohol_type.Text, txtAlcohol_amount.Text, txtAlcohol_frequency.Text, txtDrugs_type.Text,
                txtDrugs_amount.Text, txtDrugs_frequency.Text,
                txtCig_packs_per_day.Text != string.Empty ? int.Parse(txtCig_packs_per_day.Text) : 0,
                txtCig_years.Text != string.Empty ? int.Parse(txtCig_years.Text) : 0,
                txtChew_amount_per_day.Text != string.Empty ? int.Parse(txtChew_amount_per_day.Text) : 0,
                txtChew_years.Text != string.Empty ? int.Parse(txtChew_years.Text) : 0, isSelected,
                txtCaffeine_serv_per_day.Text != string.Empty ? int.Parse(txtCaffeine_serv_per_day.Text) : 0,
                txtNutrasweet_serv_per_day.Text != string.Empty ? int.Parse(txtNutrasweet_serv_per_day.Text) : 0,
                txtSaccharin_serv_per_day.Text != string.Empty ? int.Parse(txtSaccharin_serv_per_day.Text) : 0,
                txtMsg_serv_per_day.Text != string.Empty ? int.Parse(txtMsg_serv_per_day.Text) : 0,
                PatientId);
            Response.Redirect("intake_form_hormone.aspx?patientid=" + PatientId + "&form_id=" + MasterFormId, false);
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objIIntakeService = null;
        }
    }
}