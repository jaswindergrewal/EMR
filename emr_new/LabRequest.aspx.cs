using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.ListBox;

public partial class LabRequest : LMCBase
{
    protected string PatientID = "";
    protected string ApptID = "";
    private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/site.master";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
        inpPatientID.Value = PatientID;
        if (Request.QueryString["aptid"] != null) ApptID = Request.QueryString["aptid"];
        if (!IsPostBack)
        {
            var pat = ctx.Patient_Details(int.Parse(PatientID)).First();

            lblPatientName.Text = pat.FirstName + " " + pat.LastName;

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string content = "";
		RadioButtonList rdoFasting = (RadioButtonList)Utilities.FindControlRecursive(Master, "rdoFasting");
		LabRequests_Request req = new LabRequests_Request();
		if (txtRangeEnd.Text != "") req.EndRange = DateTime.Parse(txtRangeEnd.Text);
		if (txtRangeStart.Text != "") req.EndRange = DateTime.Parse(txtRangeStart.Text);
		req.Fasting = rdoFasting.SelectedValue == "Yes" ? true : false;
		req.PatientID = int.Parse(PatientID);
		ctx.LabRequests_Requests.InsertOnSubmit(req);
		ctx.SubmitChanges();
		foreach (ListItem panel in lstPanels.Items)
        {
            if (panel.Selected)
            {
				LabRequests_RequestPanel reqp = new LabRequests_RequestPanel();
				reqp.PanelID = int.Parse(panel.Value);
				reqp.RequesitID = req.LabRequests_RequestID;
				ctx.LabRequests_RequestPanels.InsertOnSubmit(reqp);
                content += panel.Text + "\r\n";
            }
        }
        foreach (ListItem test in lstTests.Items)
        {
            if (test.Selected)
            {
				LabRequests_RequestTest reqt = new LabRequests_RequestTest();
				reqt.TestID = int.Parse(test.Value);
				reqt.RequestID = req.LabRequests_RequestID;
				ctx.LabRequests_RequestTests.InsertOnSubmit(reqt);
				content += test.Text + "\r\n";
            }
        }
        content = "Fasting required: " + rdoFasting.SelectedValue + "\r\n<br/>" + content;
        apt_FollowUp fup = new apt_FollowUp();
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
        ctx.apt_FollowUps.InsertOnSubmit(fup);
        ctx.SubmitChanges();
        AssignAppts(int.Parse(PatientID), fup.FollowUp_ID, true);
        if (ApptID != "")
            Response.Redirect("apt_console.aspx?aptid=" + ApptID);
        else
            Response.Redirect("PendingFollowUps.aspx?PatientID=" + PatientID);
    }

    private void AssignAppts(int PatientID, int FolloupID, bool SaveChanges)
    {
        //get follow up
        int? retID = null;
        List<apt_rec> allApts = (from a in ctx.apt_recs
                                 where (a.AppointmentTypeID == 7 || a.AppointmentTypeID == 27)
                                 && a.ApptStart > DateTime.Now
                                 select a).ToList();

        apt_FollowUp fup = (from f in ctx.apt_FollowUps
                            where f.FollowUp_ID == FolloupID
                            select f).First();

        bool fsting = fup.FollowUp_Body.Contains("Fasting required: Yes");

        // get all apts for patient of blood test

        apt_rec apt = (from a in allApts
                       where a.patient_id == PatientID
                       && a.ApptStart >= fup.Range_Start
                       orderby a.ApptStart
                       select a).FirstOrDefault();

        // if found,match to most recent
        if (apt != null)
        {
            if (SaveChanges)
            {
                fup.AptAssigned = apt.apt_id;
                fup.FollowUp_Completed_YN = true;
                fup.FollowUp_Assigned_YN = true;
                ctx.SubmitChanges();
            }
            retID = apt.apt_id;
        }
    }

}