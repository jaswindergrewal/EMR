using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class FollowUpEdit : LMCBase
{
	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected Contact_Type_tbl Fup = new Contact_Type_tbl();
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			Fup = (from f in ctx.Contact_Type_tbls
				   where f.AptTypeID == int.Parse(Request.QueryString["AptTypeID"])
				   select f).First();

			cboViewable.Checked = Fup.Viewable_yn == null ? false : (bool)Fup.Viewable_yn;
		}
	}

	protected void btnUpdate_Click(object sender, EventArgs e)
	{
		Contact_Type_tbl type = (from f in ctx.Contact_Type_tbls
								 where f.AptTypeID == int.Parse(Request.QueryString["AptTypeID"])
								 select f).First();

		type.Viewable_yn = cboViewable.Checked;
		ctx.SubmitChanges();
		Response.Redirect("FollowupTypeMaint.aspx");
	}
}