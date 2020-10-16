using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class DictationConsole_OVUForm : LMCBase
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
    {

		if (!IsPostBack)
		{
			ReportParameter param = null;
            //Report server username & password

            //string reportUserName = ConfigurationManager.AppSettings["reportUserName"].ToString();
            //string reportPassword = ConfigurationManager.AppSettings["reportPassword"].ToString();
            //string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
            //string ReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString();

            //IReportServerCredentials irsc = new CustomReportCredentials(reportUserName, reportPassword, ReportPath);
            //ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            //ReportViewer1.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            //ReportViewer1.ServerReport.ReportPath = "/EMR/OVUForm New";
           

			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				param = new ReportParameter("PatientID", "3122");
			else
				param = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(param);
			ReportViewer1.BackColor = System.Drawing.Color.White;

		}
	}
}