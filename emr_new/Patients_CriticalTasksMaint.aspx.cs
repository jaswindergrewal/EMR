using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Patients_CriticalTasksMaint : LMCBase
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
	{
		int id = (int)grdUsers.SelectedDataKey["ID"];

		var uploads = from u in ctx.Upload_tbls
					  join p in ctx.Patients on u.PatientID equals p.PatientID
					  where u.PatientID == id
					  orderby u.Category, u.DateEntered descending
					  select new
					  {
						  Title = u.Upload_Title,
						  Date = u.DateEntered,
						  ID = u.UploadID,
						  PatientID = u.PatientID,
						  Upload_Path = u.Upload_Path,
						  Name=p.FirstName + " " + p.LastName,
					  };

		dsTypes.ConnectionString = ctx.Connection.ConnectionString;
		var pat = from p in ctx.Patients where p.PatientID == id select new { Name = p.FirstName + " " + p.LastName, };
		grdDocs.Visible = true;
		txtDate.Text = "";
		grdDocs.Caption = "Documents for " + pat.SingleOrDefault().Name;
		grdDocs.DataSource = uploads;
		grdDocs.DataBind();

	}

	protected void grdDocs_SelectedIndexChanged(object sender, EventArgs e)
	{
		DropDownList ddlAssign = (DropDownList)grdDocs.SelectedRow.Cells[2].Controls[1];
		int TypeID = int.Parse(ddlAssign.SelectedValue);
		var theTask = (from t in ctx.Patients_CriticalTasks 
					   where t.PatientID == (int)grdUsers.SelectedDataKey["ID"] && t.TaskTypeID == TypeID select t).SingleOrDefault();
		theTask.Received = true;
		theTask.ReceivedDate = DateTime.Parse(txtDate.Text);
		theTask.Requested = true;
		theTask.RequestedDate = DateTime.Parse(txtDate.Text);
		theTask.Reviewed = true;
		theTask.ReviewedDate = DateTime.Parse(txtDate.Text);
		theTask.UploadID = (int)grdDocs.SelectedDataKey["ID"];
		ctx.SubmitChanges();
		grdDocs.DataSource = new DataTable();
		
		grdDocs.DataBind();
		grdDocs.Visible = false;
		dsPatients.EnableCaching = false;
		grdUsers.DataBind();
		dsPatients.EnableCaching = true;
		txtDate.Visible = true;
		Response.Redirect("Patients_CriticalTasksMaint.aspx");
	}



	protected void grdUsers_PageIndexChanged(object sender, EventArgs e)
	{
		grdDocs.Visible = false;
		grdDocs.DataSource = new DataTable();
		grdDocs.DataBind();
	}
}