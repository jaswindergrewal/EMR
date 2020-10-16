using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;

public partial class CRM_admin_prospects : LMCBase
{
	protected CRMDataContext ctx = new CRMDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void InsertUpdateProspect(object sender, GridRecordEventArgs e)
	{
		CRM_Prospect pros = new CRM_Prospect();
		if (e.Record["ProspectID"].ToString() != "")
		{
			pros = (from p in ctx.CRM_Prospects
					where p.ProspectID == int.Parse(e.Record["ProspectID"].ToString())
					select p).First();
		}
		pros.Address = e.Record["Address"].ToString();
		pros.AltPhone = e.Record["AltPhone"].ToString();
		pros.City = e.Record["City"].ToString();
		pros.ContactMethod = e.Record["ContactMethod"].ToString();
		pros.Email = e.Record["Email"].ToString();
		pros.FirstName = e.Record["FirstName"].ToString();
		pros.Flagged = bool.Parse(e.Record["Flagged"].ToString());
		pros.LastName = e.Record["LastName"].ToString();
		pros.MainPhone = e.Record["MainPhone"].ToString();
		pros.MarketingSources = e.Record["MarketingSources"].ToString();
		pros.Notes = e.Record["Notes"].ToString();
		pros.State = e.Record["State"].ToString();
		pros.StatusID = int.Parse(e.Record["StatusID"].ToString());
		pros.Zip = e.Record["Zip"].ToString();
		if (e.Record["ProspectID"].ToString() == "")
		{
			//pros.CreatedBy = (int)Session["StaffID"];
			ctx.CRM_Prospects.InsertOnSubmit(pros);
		}
		ctx.SubmitChanges();
	}
}