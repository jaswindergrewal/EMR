using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class labmailed : System.Web.UI.Page
{
	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
		Patient pat = (from a in ctx.Patients
					   where a.PatientID == int.Parse(Request.QueryString["PatientID"].ToString())
					   select a).First();

		pat.LabsMailed = true;
		ctx.SubmitChanges();

		Response.Redirect("appointments.aspx?PatientID=" + Request.QueryString["PatientID"]);
	}
}