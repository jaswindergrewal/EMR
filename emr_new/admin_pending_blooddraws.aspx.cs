using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class admin_pending_blooddraws : System.Web.UI.Page
{
    #region "Variable"

    IPrescriptionService objServicePrescription = null;
    IPatientService objIPatientService = null;
    #endregion
    #region "Event"

    //Bind the grid for pending followups on page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                BindClinic();
                BindGrid("ALL");
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }

        }
    }
    /// <summary>
    /// get the list of pending blood draws according to clinic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid(ddlClinic.SelectedItem.Text);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    /// <summary>
    ///grid rebind on reload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBlood_Rebind(object sender, EventArgs e)
    {
        try
        {
            BindGrid(ddlClinic.SelectedItem.Text);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    #endregion
    #region "Method"
    /// <summary>
    /// methos to get the list of pending blood draws and bind it to grid
    /// </summary>
    private void BindGrid(string Clinic)
    {
        List<PendingBloodDrawsByClinicViewModel> viewModel = null;
        try
        {
            objServicePrescription = new PrescriptionService();
            viewModel = new List<PendingBloodDrawsByClinicViewModel>();
            viewModel = objServicePrescription.GetPendingBloodDrawsListByClinic(Clinic);
            grdBlood.DataSource = viewModel;
            grdBlood.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServicePrescription = null;
            viewModel = null;
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
            ddlClinic.Items.Insert(0, new ListItem("ALL"));
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
    /// <summary>
    /// Method to update followup data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int UpdateFollowup(string data)
    {
        int result = 0;
        try
        {

            IPrescriptionService objServicePrescription = null;
          
            objServicePrescription = new PrescriptionService();
            result = objServicePrescription.UpdateFollowUpBloodDraw(data);
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
           
        }
       
        return result;
    }

    #endregion

}