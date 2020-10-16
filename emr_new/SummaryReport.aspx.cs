using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class SummaryReport : System.Web.UI.Page
{
    EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Uri myUri = new Uri("http://10.0.2.89/reportserver");
            ReportViewer1.ServerReport.ReportPath = "/LabReports/Summary";
            ReportViewer1.ServerReport.ReportServerUrl=myUri;
            ReportParameter param = null;
            if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
                param = new ReportParameter("PatientName", "3122");
            else
                param = new ReportParameter("PatientName", Request.QueryString["PatientID"]);
            ReportViewer1.ServerReport.SetParameters(param);

        }
    }

}