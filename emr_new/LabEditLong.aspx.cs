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

public partial class LabEditLong : LMCBase
{
    #region Variables
    protected string PatientID = "";
    protected string ApptID = "";
    private string OrigContent = "";
    ILabAddService objService = null;
    protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    #endregion

    #region Events
    /// <summary>
    /// getting the follow up details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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

                objService = new LabAddService();
                apt_FollowUpsViewModel fup = objService.GetFollowupDetails(int.Parse(Request.QueryString["LabID"]));
                ed.Content = fup.FollowUp_Body;
                txtRangeStart.Text = fup.Range_Start == null ? "" : ((DateTime)fup.Range_Start).ToShortDateString();
                txtRangeEnd.Text = fup.Range_End == null ? "" : ((DateTime)fup.Range_End).ToShortDateString();


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


    /// <summary>
    /// update lab
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string content = ed.Content;
            RadioButtonList rdoFasting = (RadioButtonList)Utilities.FindControlRecursive(Master, "rdoFasting");
            content = "Fasting required: " + rdoFasting.SelectedValue + "\r\n<br/>" + content;
            objService = new LabAddService();
            objService.UpdateLabAdd(int.Parse(Request.QueryString["LabID"]), DateTime.Parse(txtRangeStart.Text), DateTime.Parse(txtRangeEnd.Text), (int)Session["StaffID"], ed.Content);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }

        if (ApptID != "")
            Response.Redirect("apt_console.aspx?aptid=" + ApptID);
        else
            Response.Redirect("PendingFollowUps.aspx?PatientID=" + PatientID);
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