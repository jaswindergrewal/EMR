using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class Patients_CriticalTasks : LMCBase
{
    #region Variables
    protected int PatientID = 0;
    ICriticalTaskService objService = null;
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
    /// binds grid with critical task list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{

		if (!IsPostBack)
		{
            if (Request.QueryString["patientid"] != null || Request.QueryString["patientid"] != "")
            {
                BindTaskData();
            }
		}
	}
    /// <summary>
    /// updates patients critical tasks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Requested_CheckedChanged(object sender, EventArgs e)
    {
        //assign date
        try
        {
            CheckBox cboRequest = (CheckBox)sender;
            GridViewRow selRow = (GridViewRow)cboRequest.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            int taskID = int.Parse(taskLabel.Text);
            objService = new CriticalTaskService();
            objService.UpdatePatientsCriticalTasks(taskID, cboRequest.Checked);
            BindTaskData();
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
    /// binds the grid with patients uploaded docs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Received_CheckedChanged(object sender, EventArgs e)
    {
        //asign doc and date
        try
        {
            CheckBox cboReceived = (CheckBox)sender;
            Session["cboReceived"] = cboReceived;
            GridViewRow selRow = (GridViewRow)cboReceived.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            Session["TaskID"] = int.Parse(taskLabel.Text);
            if (cboReceived.Checked)
            {
                objService = new CriticalTaskService();
                PatientID = int.Parse(Request.QueryString["PatientID"].ToString());
                grdDocs.DataSource = objService.GetUploadDocsByPatientID(PatientID);
                grdDocs.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('On the next screen, open the document, determine the actual date, and then enter it.');", true);
                modReceived.Show();
            }
            else
            {
                objService = new CriticalTaskService();
                DateTime? ReceivedDate = null;
                int? UploadId = null;
                objService.UpdatePatientsCriticalTasksUploads((int)Session["TaskID"], cboReceived.Checked, ReceivedDate, UploadId);

                BindTaskData();
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

    protected void Reviewed_CheckedChanged(object sender, EventArgs e)
    {
        //assign date
        try
        {
            CheckBox cboRequest = (CheckBox)sender;
            GridViewRow selRow = (GridViewRow)cboRequest.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            int taskID = int.Parse(taskLabel.Text);

            objService = new CriticalTaskService();
            objService.UpdatePatientsCriticalTasks(taskID, cboRequest.Checked);

            BindTaskData();
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
    /// binds critical tasks list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDocs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objService = new CriticalTaskService();
            DateTime ? ReceivedDate = DateTime.Now;
            objService.UpdatePatientsCriticalTasksUploads((int)Session["TaskID"], true, ReceivedDate, (int)grdDocs.SelectedDataKey["ID"]);

            BindTaskData();
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
    /// cancel the document uploads by patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelDoc_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox cboReceived = (CheckBox)Session["cboReceived"];
            cboReceived.Checked = false;
            BindTaskData();
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

    #region Method
    /// <summary>
    /// get the list of patients critical task
    /// </summary>
    private void BindTaskData()
	{
        try
        {
            objService = new CriticalTaskService();
            PatientID = int.Parse(Request.QueryString["PatientID"].ToString());
            grdTasks.DataSource = objService.GetCriticalTaskListByPatientID(PatientID);
            grdTasks.DataBind();
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