using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class admin_pending_medrecs : LMCBase
{
    #region Variable
    IPendingMedRecordsService objService = null;
    #endregion

    #region Events
    /// <summary>
    /// bind grid with  all pending medical record list 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
           
                IPatientService objIPatientService = new PatientService();
                ddlClinic.DataSource = objIPatientService.GetClinics();
                ddlClinic.DataTextField = "ClinicName";
                ddlClinic.DataValueField = "ClinicName";
                ddlClinic.DataBind();
                ddlClinic.Items.Insert(0, new ListItem("All"));
               
            BindData("All");
		}
	}
    /// <summary>
    /// selecting a clinic to get the list of pending medical records correspnding to the clinic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindData(ddlClinic.SelectedValue);
	}
    #endregion

    #region Methods
    /// <summary>
    /// getting the list of pending medical records
    /// </summary>
    /// <param name="Clinic"></param>
    private void BindData(string Clinic)
	{
        try
        {
            objService = new PendingMedRecordsService();
            rptConsults.DataSource = objService.GetPendingMedRegords(Clinic);
            rptConsults.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    #endregion
}

