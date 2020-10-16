using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class OrderHistory : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ReportParameter param = null;
			if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
				param = new ReportParameter("ShipName", "3122");
			else
				param = new ReportParameter("ShipName", Request.QueryString["PatientID"]);
			ReportViewer1.ServerReport.SetParameters(param);
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("Patient_Details", conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@PatientID", param.Values[0]);
				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				lblHeader.Text = (string)reader["FirstName"] + " " + (string)reader["LastName"];
				btnDetails.OnClientClick = "MM_goToURL('parent','../Manage.aspx?patientid=" + param.Values[0] + "');return document.MM_returnValue";
				reader.Close();
			}
		}

    }
}