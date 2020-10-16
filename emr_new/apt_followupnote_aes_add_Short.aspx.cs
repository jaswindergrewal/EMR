using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class apt_followupnote_aes_add_Short : System.Web.UI.Page
{
    #region "Variables"
    IPatientService objPatientService = null;
    IAptFollowUpsService objAptFollowUpsService = null;
    AptFollowUpsViewModel objAptFollowUpsViewModel = null;
    IAppointmentConsole _objAptConsoleService = new AppointmentConsole();
    #endregion


    #region Events
    #region Page_Load
    /// <summary>
    /// this is oage load event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["aptid"] != null && Request.QueryString["aptid"].ToString() != string.Empty)
            {
                btnBack.Text = "Back to Apt Console";
                btnBack.PostBackUrl = "~/apt_console.aspx?aptid=" + Request.QueryString["aptid"].ToString();
            }
            else
            {
                if (Request.QueryString["PatientID"] != null && Request.QueryString["PatientID"].ToString() != string.Empty)
                {
                    btnBack.Text = "Back";
                    btnBack.PostBackUrl = "followup_confirmation_Aes.aspx?MasterPage=~/sub.master&patientid=" + Request.QueryString["PatientID"].ToString();
                }
                else
                {
                    btnBack.Visible = false;
                }
            }
            if (Request.QueryString["PatientID"] != null && Request.QueryString["PatientID"].ToString() != string.Empty)
            {
                if (btnBack.Visible == true)
                {
                    btnPatientDetails.PostBackUrl = "patientinfo.aspx?MasterPage=~/sub.master&patientid=" + Request.QueryString["PatientID"].ToString();
                }
                else
                {
                    btnPatientDetails.PostBackUrl = "manage.aspx?patientid=" + Request.QueryString["PatientID"].ToString();
                }
               
                 
                List<Patient_Details_ViewModel> lstPatinet = _objAptConsoleService.GetPatientDetailListNew(Convert.ToInt32(Request.QueryString["PatientID"].ToString()));
                if (lstPatinet != null)
                {
                    var pat = lstPatinet.First();
                   lblFirstName.Text = pat.FirstName.ToString();
                   lblLastName.Text = pat.LastName.ToString();
                }
                
          
                //PatientViewModel patient = objPatientService.GetPatientDataByPatientId(Convert.ToInt32(Request.QueryString["PatientID"].ToString()));
                //if (patient != null)
                //{
                //    lblFirstName.Text = patient.FirstName.ToString();
                //    lblLastName.Text = patient.LastName.ToString();
                //}
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPatientService = null;
        }
    }
    #endregion

    #region btnSubmit_Click
    /// <summary>
    /// this event used for submit form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            InsertAptFollowUps();
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objAptFollowUpsViewModel = null;
        }
    }
    #endregion
    #endregion


    #region Methods
    #region InsertAptFollowUps
    /// <summary>
    /// this function used for insert aptfollowups details in to database apt_FollowUps table
    /// </summary>
    /// Created By : Rakesh Kumar
    /// Created Date : 3-Sep-2013
    public void InsertAptFollowUps()
    {
        objAptFollowUpsViewModel = new AptFollowUpsViewModel();
        objAptFollowUpsViewModel.Apt_ID = Request.QueryString["aptid"] != null && Request.QueryString["aptid"].ToString() != string.Empty ? Convert.ToInt32(Request.QueryString["aptid"].ToString()) : 0;
        objAptFollowUpsViewModel.FollowUp_Cat = 5;
        objAptFollowUpsViewModel.FollowUp_Body = ed.Content.ToString();
        objAptFollowUpsViewModel.FollowUp_Subject = ed.PlainText.ToString();
        objAptFollowUpsViewModel.DateEntered = DateTime.Now;
        objAptFollowUpsViewModel.Entered_By = Session["UserID"] != null && Session["UserID"].ToString() != string.Empty ? Convert.ToInt32(Session["UserID"].ToString()) : 0;
        objAptFollowUpsViewModel.PatientID = Request.QueryString["PatientID"] != null && Request.QueryString["PatientID"].ToString() != string.Empty ? Convert.ToInt32(Request.QueryString["PatientID"].ToString()) : 0;
        objAptFollowUpsViewModel.FollowUp_Completed_YN = false;
        objAptFollowUpsViewModel.FollowUp_Assigned_YN = false;
        objAptFollowUpsViewModel.Range_Start = Convert.ToDateTime(txtRangeStart.Text);
        objAptFollowUpsViewModel.Range_End = Convert.ToDateTime(txtRangeEnd.Text);
        objAptFollowUpsViewModel.Private = false;
        objAptFollowUpsViewModel.DueDate = DateTime.Now;
        objAptFollowUpsViewModel.FinalCall = false;
        objAptFollowUpsViewModel.SecondCall = false;
        objAptFollowUpsViewModel.FinalCall = false;
        objAptFollowUpsViewModel.Letter = false;
        objAptFollowUpsViewModel.FirstCallNote = "";
        objAptFollowUpsViewModel.SeconCallNote = "";
        objAptFollowUpsViewModel.FinalCallNote = "";
        objAptFollowUpsViewModel.LetterNote = "";
        objAptFollowUpsService = new AptFollowUpsService();
        objAptFollowUpsService.InsertAptFollowUps(objAptFollowUpsViewModel);
        txtRangeStart.Text = string.Empty;
        txtRangeEnd.Text = string.Empty;
        ed.Content = string.Empty;
        Page.ClientScript.RegisterStartupScript(typeof(Page), "Success", "alert('You have successfully added the record.');", true);
        Response.Redirect("AestheticFollowUps.aspx?patientid=" + Request.QueryString["PatientID"].ToString());
    }
    #endregion
    #endregion

}