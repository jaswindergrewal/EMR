using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class OVU : LMCBase
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			if ((from a in ctx.apt_BloodWorks where a.AptID == int.Parse(Request.QueryString["AptID"]) select a).Count() > 0)
			{
				ReportParameter param = null;
				if (Request.QueryString["AptID"] == null || Request.QueryString["AptID"] == "")
					param = new ReportParameter("PatientID", "3122");
				else
					param = new ReportParameter("AptID", Request.QueryString["AptID"]);
				ReportViewer1.ServerReport.SetParameters(param);
			}
			else
			{
				ReportViewer1.Visible = false;
				Response.Write("<span class='PageTitle'>No Digital OVU Available.</span>");
			}
		}
	}
}