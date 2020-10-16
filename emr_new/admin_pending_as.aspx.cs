using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class admin_pending_as : LMCBase
{
    #region Variable
    protected List<PendingConsultRequestViewModel> PendingConsults;
    IPrescriptionService objService = null;
    #endregion
    #region Events
    /// <summary>
    /// binding list of all the pending auto ship requests
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
            IPatientService objService = new PatientService();
            ddlClinic.DataSource = objService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("All"));
            BindData("All");
		}
	}
    /// <summary>
    /// selecting clinic from dropdown to bind list of all the pending auto ship requests
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindData(ddlClinic.SelectedValue);
	}
#endregion
    #region Method
    /// <summary>
    /// get the list of all the Pending Auto Ship Requests
    /// </summary>
    /// <param name="Clinic"></param>
    private void BindData(string Clinic)
	{
        try
        {
            objService = new PrescriptionService();
            rptConsults.DataSource = objService.GetPendingConsults(Clinic);
            rptConsults.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion

}

