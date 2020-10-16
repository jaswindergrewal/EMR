using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using AjaxControlToolkit;

public partial class DiabetesCallBack : LMCBase
{
	CallBackSettings sets = new CallBackSettings();
	const int CallBackID = 1;
	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
		int TicketID = 0;
		if (Request.QueryString["TicketID"] != null) TicketID = int.Parse(Request.QueryString["TicketID"]);
		sets = (from f in ctx.apt_FollowUps
				join a in ctx.apt_recs on f.Apt_ID equals a.apt_id
				join pr in ctx.Providers on a.ProviderID equals pr.id
				join p in ctx.Patients on a.patient_id equals p.PatientID
				where f.FollowUp_ID == TicketID
				select new CallBackSettings
				{
					PatientID = p.PatientID,
					ProviderID = pr.id,
					TicketID = f.FollowUp_ID,
					AptID = a.apt_id,
					PatientName = p.FirstName + " " + p.LastName,
					ProviderName = pr.ProviderName,
					HomePhone = p.HomePhone + ((bool)p.Home_detailed_info ? " Deatiled info" : ""),
					WorkPhone = p.WorkPhone + ((bool)p.Work_Detailed_info ? " Deatiled info" : ""),
					CellPhone = p.CellPhone + ((bool)p.Cell_Detailed_info ? " Deatiled info" : ""),
					ApppointmentDate = ((DateTime)a.ApptStart).ToShortDateString(),
					ProviderStaffID = pr.EmployeeID == null ? -1 : (int)pr.EmployeeID,
				}).FirstOrDefault();
		if (!IsPostBack)
		{
            if (sets != null)
            {
                lblCell.Text = sets.CellPhone;
                lblHome.Text = sets.HomePhone;
                lblName.Text = sets.PatientName;
                lblProvider.Text = sets.ProviderName;
                lblwork.Text = sets.WorkPhone;
                lblApptDate.Text = sets.ApppointmentDate;

                var Responses = from r in ctx.CallBackResponses where r.AptID == sets.AptID select r;

                if (Responses.Count() == 0)
                {

                    btnSubmit.Visible = true;
                }
                else
                {
                    rdoQ1.SelectedValue = (from r in Responses where r.CallBackQuestionID == 1 select r.Response).First();
                    txtQ2.Text = (from r in Responses where r.CallBackQuestionID == 2 select r.Response).First();
                    txtQ3.Text = (from r in Responses where r.CallBackQuestionID == 3 select r.Response).First();
                    txtQ4.Text = (from r in Responses where r.CallBackQuestionID == 4 select r.Response).First();
                    txtQ8.Text = (from r in Responses where r.CallBackQuestionID == 8 select r.Response).First();
                    rdoQ5.SelectedValue = (from r in Responses where r.CallBackQuestionID == 5 select r.Response).First();
                    rdoQ6.SelectedValue = (from r in Responses where r.CallBackQuestionID == 6 select r.Response).First();
                    rdoQ7.SelectedValue = (from r in Responses where r.CallBackQuestionID == 7 select r.Response).First();
                    rdoQ9.SelectedValue = (from r in Responses where r.CallBackQuestionID == 9 select r.Response).First();
                    rdoQ10.SelectedValue = (from r in Responses where r.CallBackQuestionID == 10 select r.Response).First();
                    rdoQ11.SelectedValue = (from r in Responses where r.CallBackQuestionID == 11 select r.Response).First();
                    btnSubmit.Visible = false;
                }
            }
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		CallBackResponse q1 = new CallBackResponse();
		q1.AptID = sets.AptID;
		q1.CallBackID = CallBackID;
		q1.CallBackQuestionID = 1;
		q1.DateEntered = DateTime.Now;
		q1.EnteredBy = (int)Session["StaffID"];
		q1.PatientID = sets.PatientID;
		q1.Response = rdoQ1.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q1);

		CallBackResponse q2 = new CallBackResponse();
		q2.AptID = sets.AptID;
		q2.CallBackID = CallBackID;
		q2.CallBackQuestionID = 2;
		q2.DateEntered = DateTime.Now;
		q2.EnteredBy = (int)Session["StaffID"];
		q2.PatientID = sets.PatientID;
		q2.Response = txtQ2.Text;
		ctx.CallBackResponses.InsertOnSubmit(q2);

		CallBackResponse q3 = new CallBackResponse();
		q3.AptID = sets.AptID;
		q3.CallBackID = CallBackID;
		q3.CallBackQuestionID = 3;
		q3.DateEntered = DateTime.Now;
		q3.EnteredBy = (int)Session["StaffID"];
		q3.PatientID = sets.PatientID;
		q3.Response = txtQ3.Text;
		ctx.CallBackResponses.InsertOnSubmit(q3);

		CallBackResponse q4 = new CallBackResponse();
		q4.AptID = sets.AptID;
		q4.CallBackID = CallBackID;
		q4.CallBackQuestionID = 4;
		q4.DateEntered = DateTime.Now;
		q4.EnteredBy = (int)Session["StaffID"];
		q4.PatientID = sets.PatientID;
		q4.Response = txtQ4.Text;
		ctx.CallBackResponses.InsertOnSubmit(q4);

		CallBackResponse q5 = new CallBackResponse();
		q5.AptID = sets.AptID;
		q5.CallBackID = CallBackID;
		q5.CallBackQuestionID = 5;
		q5.DateEntered = DateTime.Now;
		q5.EnteredBy = (int)Session["StaffID"];
		q5.PatientID = sets.PatientID;
		q5.Response = rdoQ5.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q5);

		CallBackResponse q6 = new CallBackResponse();
		q6.AptID = sets.AptID;
		q6.CallBackID = CallBackID;
		q6.CallBackQuestionID = 6;
		q6.DateEntered = DateTime.Now;
		q6.EnteredBy = (int)Session["StaffID"];
		q6.PatientID = sets.PatientID;
		q6.Response = rdoQ6.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q6);

		CallBackResponse q7 = new CallBackResponse();
		q7.AptID = sets.AptID;
		q7.CallBackID = CallBackID;
		q7.CallBackQuestionID = 7;
		q7.DateEntered = DateTime.Now;
		q7.EnteredBy = (int)Session["StaffID"];
		q7.PatientID = sets.PatientID;
		q7.Response = rdoQ7.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q7);

		CallBackResponse q8 = new CallBackResponse();
		q8.AptID = sets.AptID;
		q8.CallBackID = CallBackID;
		q8.CallBackQuestionID = 8;
		q8.DateEntered = DateTime.Now;
		q8.EnteredBy = (int)Session["StaffID"];
		q8.PatientID = sets.PatientID;
		q8.Response = txtQ8.Text;
		ctx.CallBackResponses.InsertOnSubmit(q8);

		CallBackResponse q9 = new CallBackResponse();
		q9.AptID = sets.AptID;
		q9.CallBackID = CallBackID;
		q9.CallBackQuestionID = 9;
		q9.DateEntered = DateTime.Now;
		q9.EnteredBy = (int)Session["StaffID"];
		q9.PatientID = sets.PatientID;
		q9.Response = rdoQ9.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q9);

		CallBackResponse q10 = new CallBackResponse();
		q10.AptID = sets.AptID;
		q10.CallBackID = CallBackID;
		q10.CallBackQuestionID = 10;
		q10.DateEntered = DateTime.Now;
		q10.EnteredBy = (int)Session["StaffID"];
		q10.PatientID = sets.PatientID;
		q10.Response = rdoQ10.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q10);

		CallBackResponse q11 = new CallBackResponse();
		q11.AptID = sets.AptID;
		q11.CallBackID = CallBackID;
		q11.CallBackQuestionID = 11;
		q11.DateEntered = DateTime.Now;
		q11.EnteredBy = (int)Session["StaffID"];
		q11.PatientID = sets.PatientID;
		q11.Response = rdoQ11.SelectedValue;
		ctx.CallBackResponses.InsertOnSubmit(q11);

		apt_FollowUp fup = (from f in ctx.apt_FollowUps where f.FollowUp_ID == sets.TicketID select f).First();
		fup.FollowUp_Completed_YN = true;

		ctx.contact_tbl_Ticket_Insert(sets.PatientID, "Ticket " + ((int)Session["ActiveTicket"]).ToString() + " closed.\r\nClosed by " + Session["MM_Username"], (int)Session["StaffID"], (int)Session["ActiveTicket"]);

		ctx.SubmitChanges();
		btnSubmit.Enabled = false;
	}

	protected void btnEscalate_Click(object sender, EventArgs e)
	{
		string MsgBody = "";
		MsgBody += "Rate how well you have stuck to your diet since the last time we checked in with you. " + rdoQ1.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "What is your current weight? " + txtQ2.Text + "\r\n<br/>";
		MsgBody += "How many times per week have you exercised? " + txtQ3.Text + "\r\n<br/>";
		MsgBody += "How long? " + txtQ4.Text + "\r\n<br/>";
		MsgBody += "Rate how well you have done over all in your exercise. " + rdoQ5.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Are you taking all of the supplements and medications your doctor prescribed? " + rdoQ6.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Is there anything I can escalate for you - any medical issues or concerns - any supplement or medicaation issues? " + rdoQ7.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Are we meeting your expectations? " + rdoQ9.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Do you have any questions regarding your Longevity program? " + rdoQ10.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Is there anything else I can do for you? " + rdoQ11.SelectedItem.Text + "\r\n<br/>";
		MsgBody += "Comments " + txtQ8.Text + "\r\n<br/>";

		TicketUtils.MakeTicket((int)Session["StaffID"], MsgBody, 15, sets.PatientID, 2, "i", 72, "Patient Escalation.");
		ctx.contact_tbl_Ticket_Insert(sets.PatientID, "Esaclated Ticket\r\n<br/>" + MsgBody, (int)Session["StaffID"], sets.TicketID);
		
	}

	protected void btnVoiceMail_Click(object sender, EventArgs e)
	{
		apt_FollowUp fup = (from a in ctx.apt_FollowUps
							where a.FollowUp_ID == sets.TicketID
							select a).First();

		DateTime CurrDue = (DateTime)fup.DueDate;
		fup.DueDate = CurrDue.AddDays(3);
		ctx.SubmitChanges();
			ctx.contact_tbl_Ticket_Insert(sets.PatientID, "Left Voice Mail.", (int)Session["StaffID"], sets.TicketID);
	}
}

class CallBackSettings
{
	public int PatientID { get; set; }
	public int ProviderID { get; set; }
	public int TicketID { get; set; }
	public int AptID { get; set; }
	public string PatientName { get; set; }
	public string ProviderName { get; set; }
	public string HomePhone { get; set; }
	public string WorkPhone { get; set; }
	public string CellPhone { get; set; }
	public string ApppointmentDate { get; set; }
	public int ProviderStaffID { get; set; }
}