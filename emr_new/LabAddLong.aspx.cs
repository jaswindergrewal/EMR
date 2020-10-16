using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;


public partial class LabAddLong : LMCBase
{
    #region Variables
    protected string PatientID = "";
    protected string ApptID = "";
    ILabAddService objService = null;
    #endregion

    #region Events


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //Jaswinder 16th aug 2013 as date is not saving so make textbox readonly from codebehind
            txtRangeEnd.Attributes.Add("readonly", "readonly");
            txtRangeStart.Attributes.Add("readonly", "readonly");

            if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
            inpPatientID.Value = PatientID;
            if (Request.QueryString["aptid"] != null)
            {
                ApptID = Request.QueryString["aptid"];
                btnAptConsol.Enabled = true;
            }
            else
            {
                btnAptConsol.Enabled = false;
            }
            if (!IsPostBack)
            {
                IAppointmentConsole _objAptConsoleService = new AppointmentConsole();
                List<Patient_Details_ViewModel> lstPatinet = _objAptConsoleService.GetPatientDetailListNew(int.Parse(PatientID));
                var pat = lstPatinet.First();
                lblPatientName.Text = pat.FirstName + " " + pat.LastName;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string content = ed.Content;
            RadioButtonList rdoFasting = (RadioButtonList)Utilities.FindControlRecursive(Master, "rdoFasting");
            content = "Fasting required: " + rdoFasting.SelectedValue + "\r\n<br/>" + content;
            apt_FollowUpsViewModel fup = new apt_FollowUpsViewModel();
            fup.FollowUp_Body = content;
            if (txtRangeStart.Text != "")
                fup.Range_Start = DateTime.Parse(txtRangeStart.Text);
            if (txtRangeEnd.Text != "")
                fup.Range_End = DateTime.Parse(txtRangeStart.Text);
            fup.Entered_By = (int)Session["StaffID"];
            try
            {
                fup.Apt_ID = int.Parse(ApptID);
            }
            catch { }
            fup.PatientID = int.Parse(PatientID);
            fup.FollowUp_Cat = 4;
            fup.FollowUp_Completed_YN = false;
            fup.FirstCall = false;
            fup.FirstCallNote = "";
            fup.SecondCall = false;
            fup.SeconCallNote = "";
            fup.FinalCall = false;
            fup.FinalCallNote = "";
            fup.Letter = false;
            fup.DateEntered = DateTime.Now;
            fup.LetterNote = "";
            objService = new LabAddService();
            int RetID = objService.InsertintoAptFollowup(fup, Convert.ToInt16(PatientID.ToString()));
            if (RetID > 0)
                Response.Redirect("apt_console.aspx?aptid=" + RetID.ToString(), false);
            else
                Response.Redirect("Manage.aspx?PendingFollowup=True&PatientID=" + PatientID, false); 
                //Response.Redirect("PendingFollowUps.aspx?PatientID=" + PatientID + "&MasterPage=~/site.master", false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }

    protected void btnAptConsol_Click(object sender, EventArgs e)
    {
        Response.Redirect("apt_console.aspx?aptid=" + ApptID, true);
    }

    #endregion

    #region Method
    private string CleanHtml(string html)
    {
        // start by completely removing all unwanted tags     
        html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
        // then run another pass over the html (twice), removing unwanted attributes     
        html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
        html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
        return html;
    }
    #endregion

}