using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class MedicalNoteUpdate : LMCBase
{
    #region "Variables"
    protected string PatientID = "";
    protected string ContactID = "";
    protected string ApptID = string.Empty;
    protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    IAppointmentConsole objAppointmentConsole = null;
    #endregion

    #region "Events"
    /// <summary>
    /// getting the firstname and lastname on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Contact_tblViewModel> lstContactTbl = null;
        try
        {
            if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
            if (Request.QueryString["ApptID"] != null) ApptID = Request.QueryString["ApptID"];
            inpPatientID.Value = PatientID;
            if (Request.QueryString["ContactID"] != null) ContactID = Request.QueryString["ContactID"];

            objAppointmentConsole = new AppointmentConsole();
            lstContactTbl = new List<Contact_tblViewModel>();
            lstContactTbl = objAppointmentConsole.GetContactTblDetails(int.Parse(ContactID));
            var Note = lstContactTbl.First();

            if (!IsPostBack) ed.Content = Note.MessageBody;
            PatientID = Note.PatientID.ToString();
            ApptID = Note.Apt_ID.ToString();

            // to get patient details
            List<Patient_Details_ViewModel> lstPatinet = objAppointmentConsole.GetPatientDetailListNew(int.Parse(PatientID));            
            var pat = lstPatinet.First();

            lblPatientName.Text = pat.FirstName + " " + pat.LastName;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objAppointmentConsole = null;
            lstContactTbl = null;
        }
    }

    /// <summary>
    /// updating the medical notes on button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string content = ed.Content;
        try
        {
            objAppointmentConsole = new AppointmentConsole();
            objAppointmentConsole.UpdateMedicalNote(content, int.Parse(ContactID));
            Response.Redirect("apt_console.aspx?aptid=" + ApptID, false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objAppointmentConsole = null;
        }

    }
    #endregion

    #region "Method"
    /// <summary>
    /// removing unwanted tags and attributes
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
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