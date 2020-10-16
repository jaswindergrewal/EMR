using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class ContactPrint : LMCBase
{
	protected string ContactMessage;
    IContactPrintService objService = null;
    protected ContactPrintViewModel Contact;
	
    /// <summary>
    /// To show the contact records for patient on the basis of contactid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            int ContactID = 0;
            if (Request.QueryString["ContactID"] != null) ContactID = int.Parse(Request.QueryString["ContactID"]);

            objService = new ContactPrintService();
            Contact = objService.GetContactPrintDetails(ContactID);
            ContactMessage = Contact.MessageBody;
            if (!IsPostBack)
            {
                lblAptTypeDesc.Text = Contact.AptTypeDesc;
                lblContactDateEntered.Text = ((DateTime)Contact.ContactDateEntered).ToShortDateString();
                lblEnteredBy.Text = Contact.username;
                lblPatientName.Text = Contact.FirstName + " " + Contact.LastName;
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
}