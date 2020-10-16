using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Permissions;


[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
public partial class UploadFile : LMCBase
{
	protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
    {
		//if(Request.QueryString["PatientID"] == null)
		//    Response.Redirect("../patient_Details.asp");
    }
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		
		//save the file
		FileUpload1.SaveAs(@"\\emr\emr_new\uploads\" + Request.QueryString["PatientID"] + @"\" + FileUpload1.FileName);
		//update the db
		Upload_tbl tbl = new Upload_tbl();
		tbl.PatientID = int.Parse(Request.QueryString["PatientID"]);
		tbl.Upload_Path = FileUpload1.FileName;
		tbl.Upload_Title = txtFileName.Text;
		tbl.DateEntered = DateTime.Now;
		tbl.Category = ddCategory.SelectedItem.Text;
		ctx.Upload_tbls.InsertOnSubmit(tbl);
		ctx.SubmitChanges();

		Response.Redirect("Manage.aspx?patientID=" + Request.QueryString["PatientID"]);
	}

	protected void val_NoPound(object sender, ServerValidateEventArgs e)
	{
		if (FileUpload1.FileName.Contains("#") 
			|| FileUpload1.FileName.Contains(":") 
			|| FileUpload1.FileName.Contains(":") 
			|| FileUpload1.FileName.Contains("%"))
			e.IsValid = false;
		else
			e.IsValid = true;
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		Response.Redirect("manage.aspx?patientID=" + Request.QueryString["PatientID"]);
	}
}