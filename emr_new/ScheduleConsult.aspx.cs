using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class ScheduleConsult : LMCBase, IDisposable
{
    protected string PatientID = "";
    protected string ApptID = "";
    IAppointmentConsole _objAptConsoleService = null;

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

    protected void Page_Load(object sender, EventArgs e)
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
            
            try
            {
                _objAptConsoleService = new AppointmentConsole();
                List<Patient_Details_ViewModel> lstPatinet = _objAptConsoleService.GetPatientDetailListNew(int.Parse(PatientID));
                var pat = lstPatinet.First();
                lblPatientName.Text = pat.FirstName + " " + pat.LastName;

                var fups = _objAptConsoleService.GetFollowUpTypesList();
                AptType.DataSource = fups;
                AptType.DataTextField = "FollowUp_Type_Desc";
                AptType.DataValueField = "FollowUp_Type_ID";
                AptType.DataBind();
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }
            finally
            {
                _objAptConsoleService = null;
            }

        }
    }


    private string CleanHtml(string html)
    {
        // start by completely removing all unwanted tags     
        html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
        // then run another pass over the html (twice), removing unwanted attributes     
        html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
        html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
        return html;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      
        string content = ed.Content;
        DateTime? startDate;
        DateTime? endDate;
        int? AptId;
        if (txtRangeStart.Text != "" && txtRangeStart.Text != string.Empty)
            startDate = DateTime.Parse(txtRangeStart.Text);
        else
            startDate = null;

        if (txtRangeEnd.Text != "" && txtRangeEnd.Text != string.Empty)
            endDate = DateTime.Parse(txtRangeEnd.Text);
        else
            endDate = null;

        if (ApptID != "")
            AptId = int.Parse(ApptID);
        else
            AptId = null;

        try
        {
            _objAptConsoleService = new AppointmentConsole();
            _objAptConsoleService.InsertAptFollowUp(content, startDate, endDate, int.Parse(AptType.SelectedValue), (int)Session["StaffID"], AptId, int.Parse(PatientID));
            if (ApptID != "")
                Response.Redirect("apt_console.aspx?aptid=" + ApptID, false);
            else
                Response.Redirect("PendingFollowUps.aspx?PatientID=" + PatientID, false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }

    }


    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }
    protected void btnAptConsol_Click(object sender, EventArgs e)
    {
        Response.Redirect("apt_console.aspx?aptid=" + ApptID, true);
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] == null)
            Response.Redirect("patientinfo.aspx?patientid=" + PatientID.ToString());
        else
            Response.Redirect("Manage.aspx?patientid=" + PatientID.ToString());
    }
}