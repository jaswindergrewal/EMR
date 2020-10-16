using System;
using Microsoft.Reporting.WebForms;

public partial class PatientAppointments : LMCBase
{
	
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ReportParameter param = null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				param = new ReportParameter("PatientID", "3122");
			else
				param = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(param);
			

		}
    }
}