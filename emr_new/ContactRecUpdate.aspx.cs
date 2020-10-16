using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class ContactRecUpdate : LMCBase
{
    protected string PatientID = "";
    protected string ContactID = "";    
    IAppointmentConsole objIAppointmentConsole = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["contactID"] != null) ContactID = Request.QueryString["contactID"];

        objIAppointmentConsole = new AppointmentConsole();

        ContactStaffPatientTypeViewModel Contact = objIAppointmentConsole.GetContactFromMultipleTableByContactId(int.Parse(ContactID));        

        PatientID = Contact.PatientID.ToString();
        if (!IsPostBack)
        {
            lblAptTypeDesc.Text = Contact.AptTypeDesc;
            lblContactDateEntered.Text = ((DateTime)Contact.ContactDateEntered).ToShortDateString();
            lblEnteredBy.Text = Contact.EnteredBy;
            ed.Content = Contact.MessageBody;

            List<Patient_Details_ViewModel> lstPatient = objIAppointmentConsole.GetPatientDetailListNew(int.Parse(PatientID));
            var varPatient = lstPatient.FirstOrDefault();
            lblPatientName.Text = varPatient.FirstName + " " + varPatient.LastName;
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
        try
        {
            string content = ed.Content;
            content = content.Replace("'", "''");

            objIAppointmentConsole = new AppointmentConsole();
            objIAppointmentConsole.UpdateMedicalNote(content, int.Parse(ContactID));
        }
        catch (System.Exception)
        {
            throw;
        }
        Response.Redirect("contact_record_close.aspx?ContactID=" + ContactID);
    }

}