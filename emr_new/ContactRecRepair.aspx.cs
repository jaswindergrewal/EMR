using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;

public partial class ContactRecUpdate : LMCBase
{
	protected string PatientID = "";
	protected string ContactID = "";
	protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["contactID"] != null) ContactID = Request.QueryString["contactID"];

		var Contact = (from c in ctx.Contact_tbls
					   join s in ctx.Staffs on c.EnteredBy equals s.EmployeeID
					   join p in ctx.Patients on c.PatientID equals p.PatientID
					   join t in ctx.Contact_Type_tbls on c.AptType equals t.AptTypeID
					   where c.ContactID == int.Parse(ContactID)
					   select new
					   {
						   FirstName = p.FirstName,
						   LastName = p.LastName,
						   PatientID = c.PatientID,
						   ContactID = c.ContactID,
						   ContactDateEntered = c.ContactDateEntered,
						   EnteredBy = s.username,
						   MessageBody = c.MessageBody,
						   Followupbody = c.FollowUpBody,
						   AptTypeDesc = t.AptTypeDesc,
					   }).First();

		PatientID = Contact.PatientID.ToString();
		if (!IsPostBack)
		{
			lblAptTypeDesc.Text = Contact.AptTypeDesc;
			lblContactDateEntered.Text = ((DateTime)Contact.ContactDateEntered).ToShortDateString();
			lblEnteredBy.Text = Contact.EnteredBy;
			ed.Content = Contact.MessageBody;

			var pat = ctx.Patient_Details(int.Parse(PatientID)).First();
			lblPatientName.Text = pat.FirstName + " " + pat.LastName;


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
		content = content.Replace("'", "''");

		Contact_tbl cont = (from c in ctx.Contact_tbls
							where c.ContactID == int.Parse(ContactID)
							select c).First();

		cont.MessageBody = content;
		ctx.SubmitChanges();
		Response.Redirect("ContactsRepair.aspx?patientID=" + PatientID);
	}

}