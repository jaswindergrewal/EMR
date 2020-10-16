using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class patient_details_printable : LMCBase
{
	/// <summary>
	/// Print a report for patient details
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Report server username & password

            //string reportUserName = ConfigurationManager.AppSettings["reportUserName"].ToString();
            //string reportPassword = ConfigurationManager.AppSettings["reportPassword"].ToString();
            //string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
            //string ReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString();
            //string ReportEMR = ConfigurationManager.AppSettings["ReportEMR"].ToString();


            //IReportServerCredentials irsc = new CustomReportCredentials(reportUserName, reportPassword, ReportPath);
            //ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            //ReportViewer1.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            //ReportViewer1.ServerReport.ReportPath = "/"+ReportEMR + "/OnePatientInfo";

            ReportParameter param = null;
            try
            {
                if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
                    param = new ReportParameter("PatientID", "3122");
                else
                    param = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
                ReportViewer1.ServerReport.SetParameters(param);
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
            }
            finally
            {
                param = null;
                //irsc = null;
            }
        }
	}
}