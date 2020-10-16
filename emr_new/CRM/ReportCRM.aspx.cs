using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Linq;

public partial class CRM_ReportCRM : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportParameter param = null;

            if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "1")
            {
                ReportViewer1.ServerReport.ReportPath = "/Marketing/Registrations By Source2.0";
            }
            else if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "2")
            {

                ReportViewer1.ServerReport.ReportPath = "/Marketing/Scheduled by Event2.0";
            }
            else if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "3")
            {


                ReportViewer1.ServerReport.ReportPath = "/Marketing/ProspectReports";
                param = new ReportParameter("rptID", Request.QueryString["rptID"]);
                ReportViewer1.ServerReport.SetParameters(param);
            }
            else if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "4")
            {


                ReportViewer1.ServerReport.ReportPath = "/Marketing/ProspectReports";
                param = new ReportParameter("rptID", Request.QueryString["rptID"]);
                ReportViewer1.ServerReport.SetParameters(param);
            }
            else if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "5")
            {
                ReportViewer1.ServerReport.ReportPath = "/Marketing/ProspectReports";
                param = new ReportParameter("rptID", Request.QueryString["rptID"]);
                ReportViewer1.ServerReport.SetParameters(param);

            }
            else if (Request.QueryString["rptID"] != null && Request.QueryString["rptID"] == "6")
            {
                ReportViewer1.ServerReport.ReportPath = "/Marketing/ProspectFollowup";


            }
            else {
                Response.Redirect("CRM_Dashboard.aspx");
            }
        }
    }
}