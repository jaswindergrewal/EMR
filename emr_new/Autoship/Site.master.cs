using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
        if (!IsPostBack)
        {
            lblDev.Text = "";
            if (ConfigurationManager.ConnectionStrings["db"].ConnectionString.Contains("dev") || Request.ServerVariables["PATH_TRANSLATED"].Contains("emr_test"))
            {
                if (Request.ServerVariables["PATH_TRANSLATED"].Contains("emr_test"))
                {
                    lblDev.Text = "THIS IS THE TEST SITE!";
                    lblDev.Visible = true;
                    devRow.Visible = true;
                }
                else if (Request.ServerVariables["PATH_TRANSLATED"].Contains("emr_dev"))
                {
                    lblDev.Text = "THIS IS THE DEV SITE!";
                    lblDev.Visible = true;
                    devRow.Visible = true;
                }
                if ((ConfigurationManager.ConnectionStrings["db"].ConnectionString.Contains("dev")))
                {
                    lblDev.Text += " USING DEV DATABASE.";
                }
                else
                {
                    lblDev.Text += " USING LIVE DATABASE.";

                }
            }
            if (Session["TicketCount"] != null && Session["StaffID"] != null)
                Session["TicketCount"] = (from t in ctx.apt_FollowUps
                                          where t.Assigned == (int)Session["StaffID"]
                                          && t.FollowUp_Completed_YN == false
                                          && t.DueDate <= DateTime.Now
                                          select t).Count();
            else
                Response.Redirect("../login.asp");
        }
	}

	protected void txtPatient_TextChanged(object sender, EventArgs e)
	{
		if (txtPatient.Enabled)
		{
			Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);
			if (pat != null)
			{
				Response.Redirect("../Manage.aspx?PatientID=" + pat.ID.ToString());
			}
			else
			{
			}
		}
	}

}
