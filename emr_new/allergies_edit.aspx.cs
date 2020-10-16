using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class allergies_edit : LMCBase
{
    #region Variable
    protected int PatientID = 0;
    IAllergieService objService = null;
    #endregion
    #region Event
    /// <summary>
    /// get the details of allergies of patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            PatientID = int.Parse(Request.QueryString["PatientID"]);
            if (!IsPostBack)
            {
                objService = new AllergieService();
                PatientViewModel pat = objService.GetPatientByID(PatientID);
                txtAllergies.Text = pat.Allergies;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
	}
    /// <summary>
    /// update the allergies of patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void btnAllergies_Click(object sender, EventArgs e)
	{
        try
        {
            objService = new AllergieService();
            PatientViewModel pat = objService.GetPatientByID(PatientID);
            pat.Allergies = txtAllergies.Text;
            objService = null;
            objService = new AllergieService();
            objService.UpdateAllergies(pat);

            // code for refresh the parent page.
            string script = "this.window.parent.location=this.window.parent.location;";
            if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
                ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);
            //Response.Redirect("PatientInfo.aspx?patientID=" + PatientID.ToString(),false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }

    //redirect to patient info page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?patientID=" + PatientID.ToString(), false);
    }
    #endregion
   
}