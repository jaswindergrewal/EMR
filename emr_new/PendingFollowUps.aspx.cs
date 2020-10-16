using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class PendingFollowUps : LMCBase
{
    #region "variables"
    protected string PatientID = "";
    IFollowUpTypeService objIFollowUpTypeService = null;
    #endregion

    #region "events"

    /// <summary>
    /// Set the master page on the bases of querystring
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/sub.master";

    }

    /// <summary>
    /// bind the grid on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString != null)
        {
            PatientID = Request.QueryString["PatientID"];

            BindPendingFollowUp();
        }
        else
        {
            Response.Redirect("landingpage.aspx");
        }
        
    }

    /// <summary>
    /// bind the pending follow up list details
    /// </summary>
    private void BindPendingFollowUp()
    {
        List<PendingFollowUpsViewModel> lstViewModel = null;
        try
        {
            objIFollowUpTypeService = new FollowUpTypeService();
            lstViewModel = new List<PendingFollowUpsViewModel>();

            if (rdoPending.SelectedValue == "0")
                lstViewModel = objIFollowUpTypeService.GetPendingFollowUpList(int.Parse(Request.QueryString["PatientID"]));
            else
                lstViewModel = objIFollowUpTypeService.GetPendingFollowUpListByPatient(int.Parse(Request.QueryString["PatientID"]));

            rptFollow.DataSource = lstViewModel;
            rptFollow.DataBind();

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIFollowUpTypeService = null;
            lstViewModel = null;
        }
    }
    #endregion
}
