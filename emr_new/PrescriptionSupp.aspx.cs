using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;


public partial class PrescriptionSupp : LMCBase
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{

		if (!IsPostBack)
		{
            //Report server username & password

            //string reportUserName = ConfigurationManager.AppSettings["reportUserName"].ToString();
            //string reportPassword = ConfigurationManager.AppSettings["reportPassword"].ToString();
            //string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
            //string ReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString();

            //IReportServerCredentials irsc = new CustomReportCredentials(reportUserName, reportPassword, ReportPath);
            //ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            //ReportViewer1.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            //ReportViewer1.ServerReport.ReportPath = "/LabReports/PrescriptionSupp";
           
            
            ReportParameter PatientID = null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				PatientID = new ReportParameter("PatientID", "3122");
			else
				PatientID = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(PatientID);

			ReportParameter scripIds = null;
			if (Request.QueryString["scripIds"] == null || Request.QueryString["scripIds"] == "")
				scripIds = new ReportParameter("PrescriptionIDs", "");
			else
				scripIds = new ReportParameter("PrescriptionIDs", Request.QueryString["scripIds"]);
			ReportViewer1.ServerReport.SetParameters(scripIds);

			ReportParameter clinic = null;
			if (Request.QueryString["clinic"] == null || Request.QueryString["clinic"] == "")
				clinic = new ReportParameter("Clinic", "");
			else
				clinic = new ReportParameter("Clinic", Request.QueryString["clinic"]);
			ReportViewer1.ServerReport.SetParameters(clinic);
           

		}
	}

	protected void btnList_Click(object sender, EventArgs e)
	{
		Response.Redirect("admin_pending_prescriptions.aspx", "_top", "");
	}

}