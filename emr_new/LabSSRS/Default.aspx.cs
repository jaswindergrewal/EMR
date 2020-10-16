using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class _Default : LMCBase
{
    /// <summary>
    /// To show the lab Report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ReportParameter param=null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				param = new ReportParameter("PatientName", "2397");
			else
				param = new ReportParameter("PatientName", Request.QueryString["PatientID"]);

            ReportViewer1.ServerReport.SetParameters(param);
            ReportViewer1.ShowRefreshButton = false;

		}
		
    }
}
