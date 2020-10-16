using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class DictationConsole_WeightChart : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			ReportParameter param = null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["AptID"] == "")
				param = new ReportParameter("PatientID", "2397");
			else
				param = new ReportParameter("PatientID", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(param);

		}
	}
}