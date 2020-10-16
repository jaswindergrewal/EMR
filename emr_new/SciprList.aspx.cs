using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class SciprList : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			ReportParameter PatientID = null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				PatientID = new ReportParameter("PatientID", "3122");
			else
				PatientID = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(PatientID);
		}
	}

	protected void toScrps(object semder, EventArgs e)
	{
		Response.Redirect("PresrcriptionList.aspx?PatientID=" + Request.QueryString["PatientID"] + "&MasterPage=~/sub.master");
	}
}