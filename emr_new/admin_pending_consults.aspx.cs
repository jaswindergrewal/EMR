using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class admin_pending_consults : LMCBase
{
    #region "Variable"
    IPrescriptionService objServicePrescription = null;
    IPatientService objIPatientService = null;
    #endregion
    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
            BindClinic();
            BindData("All");
		}
	}
    /// <summary>
    /// calling BindingData method on seleting value in dropdownlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
	{
        try
        {
            BindData(ddlClinic.SelectedValue);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
	}
    #endregion
    #region "Methods"
    /// <summary>
    /// binding data in repeater
    /// </summary>
    /// <param name="Clinic"></param>
    private void BindData(string Clinic)
    {
        List<PendingConsultRequestViewModel> lstPendingConsult = null;
        try
        {
            objServicePrescription = new PrescriptionService();
            lstPendingConsult = new List<PendingConsultRequestViewModel>();
            lstPendingConsult = objServicePrescription.GetPendingConsultList(Clinic);
            rptConsults.DataSource = lstPendingConsult;
            rptConsults.DataBind();
        }
        catch (System.Exception ex) 
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServicePrescription = null;
            lstPendingConsult = null;
        }
    }
    /// <summary>
    /// Added by jaswinder to bind the clinic from database
    /// 24th sept 2013
    /// </summary>
    public void BindClinic()
    {
        try
        {
            objIPatientService = new PatientService();
            ddlClinic.DataSource = objIPatientService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("All"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIPatientService = null;
        }
    }


    #endregion

}

