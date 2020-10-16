using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class MedicalRecords : LMCBase
{
    #region "variables"
    protected string PatientID = "";
	protected string ApptID = "";
    IQBCustMatchPatientService objPatient = null;
    ILabAddService objService = null;

    #endregion

    /// <summary>
    /// get patient details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
		inpPatientID.Value = PatientID;
		if (Request.QueryString["aptid"] != null) ApptID = Request.QueryString["aptid"];
		if (!IsPostBack)
		{
            objPatient = new QBCustMatchPatientService();
            PatientViewModel PatientView = objPatient.GetPatientDetailById(int.Parse(PatientID));
            if (PatientView != null)
            {
                lblPatientName.Text = PatientView.FirstName + " " + PatientView.LastName;
            }
		}
	}


  

	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		string content = ed.Content;
		
		content = content.Replace("'", "''");
        apt_FollowUpsViewModel fup = new apt_FollowUpsViewModel();
		fup.FollowUp_Body = content;
		fup.Range_Start = txtRangeStart.Text == "" ? (DateTime?)null : DateTime.Parse(txtRangeStart.Text);
		if (txtRangeEnd.Text != "") fup.Range_End = DateTime.Parse(txtRangeEnd.Text);
		fup.Entered_By = (int)Session["StaffID"];
		try
		{
			fup.Apt_ID = int.Parse(ApptID);
		}
		catch { }
		fup.PatientID = int.Parse(PatientID);
		fup.FollowUp_Cat = 12;
        fup.FollowUp_Completed_YN = false;
        fup.FirstCall = false;
        fup.FirstCallNote = "";
        fup.SecondCall = false;
        fup.SeconCallNote = "";
        fup.FinalCall = false;
        fup.FinalCallNote = "";
        fup.Letter = false;
        fup.LetterNote = "";
        objService=new LabAddService();
        int result = objService.InsertAptFollowup(fup);
       
		if (ApptID != "")
			Response.Redirect("apt_console.aspx?aptid=" + ApptID);
		else
			Response.Redirect("PendingFollowUps.aspx?PatientID=" + PatientID);
	}

    
}