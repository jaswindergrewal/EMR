using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting;
using System.Configuration;
using System.Net;
using Microsoft.Reporting.WebForms;

public partial class LabManageReport : System.Web.UI.Page
{
    /// <summary>
    /// Show the summary report for the patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //ReportParameter param = null;
            //if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
            //    param = new ReportParameter("PatientName", "3122");
            //else
            //    param = new ReportParameter("PatientName", Request.QueryString["PatientID"]);

            //Report server username & password

            string reportUserName = ConfigurationManager.AppSettings["reportUserName"].ToString();
            string reportPassword = ConfigurationManager.AppSettings["reportPassword"].ToString();
            string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
            string ReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString();

            IReportServerCredentials irsc = new CustomReportCredentials(reportUserName, reportPassword, ReportPath);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            ReportViewer1.ServerReport.ReportPath = "/EMR Reports/LabManageDataReport";
            ReportViewer1.ServerReport.Refresh();

            //Set username and password for reports
            //DataSourceCredentials dsCredentials = new DataSourceCredentials();
            //dsCredentials.Name = "lmc";
            //dsCredentials.UserId = "emrdev";
            //dsCredentials.Password = "emrdev";

            ReportViewer1.ShowCredentialPrompts = false;

            //ReportViewer1.ServerReport.SetDataSourceCredentials(new DataSourceCredentials[1] { dsCredentials });
            //ReportViewer1.ServerReport..SetParameters(param);
        }
    }
}