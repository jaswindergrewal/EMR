using System;
using System.Configuration;

public partial class CRM_CrmDashBoard : System.Web.UI.MasterPage
{
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
                    lblDev.Text = " USING DEV DATABASE.";
                }
                else
                {
                    lblDev.Text = " USING LIVE DATABASE.";

                }
            }
        }

    }

    //Show the patient details on text change in a search patient box
    protected void txtPatient_TextChanged(object sender, EventArgs e)
    {
        if (txtPatient.Enabled)
        {
            Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);
            if (pat != null && pat.ID > 0)
            {
                Response.Redirect("~/Manage.aspx?PatientID=" + pat.ID.ToString());
            }
        }
    }

}
