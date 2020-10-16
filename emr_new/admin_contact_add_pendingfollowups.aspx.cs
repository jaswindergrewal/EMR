using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class admin_contact_add_pendingfollowups : LMCBase
{
    #region Variables
    protected int PatientID = 0;
	protected int FollowUp_ID = 0;
    IPendingFollowupService objService = null;
    IAppointmentConsole objIAppointmentConsole = null;
    protected PendingFollowupViewModel PendingRequest = new PendingFollowupViewModel();
	protected string CloseLink = "";
    #endregion
    #region Events

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/site.master";

    }

    /// <summary>
    /// getting pending follow up list corresponding to pateint and follow up id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (Request.QueryString["patientid"] != null)
            {
                FollowUp_ID = int.Parse(Request.QueryString["followUp_id"]);
                PatientID = int.Parse(Request.QueryString["patientid"]);
                objService = new PendingFollowupService();
                PendingRequest = objService.GetPendingFollowUpDetail(FollowUp_ID, PatientID);
                if (!IsPostBack)
                {
                    rptAptTypes.DataTextField = "AptTypeDesc";
                    rptAptTypes.DataValueField = "AptTypeID";
                    rptAptTypes.DataSource = objService.GetContactTypeList();
                    rptAptTypes.DataBind();


                    rptContacts.DataSource = objService.GetContactList(FollowUp_ID);
                    rptContacts.DataBind();
                }
            }
            else
            {
                Response.Redirect("LandingPage.aspx", false); 
            }

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

	/// <summary>
	/// Method to insert the contact followups
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
	{
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            string content = ed.Content;
            objIAppointmentConsole.AddContactFollowpRecords(int.Parse(rptAptTypes.SelectedValue), PatientID, content, (int)Session["StaffID"], FollowUp_ID);

            objService = new PendingFollowupService();
            rptContacts.DataSource = objService.GetContactList(FollowUp_ID);
            rptContacts.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally {
            objService = null;
            objIAppointmentConsole = null;
        }
    }
    #endregion
    protected void btnManage_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null && Request.QueryString["MasterPage"] == "~/sub.master")
            Response.Redirect("patientinfo.aspx?patientid=" + PatientID.ToString());
        else
            Response.Redirect("Manage.aspx?patientid=" + PatientID.ToString());
    }
}
