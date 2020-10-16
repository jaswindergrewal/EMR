using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;

public partial class CRM_admin_crm : LMCBase
{
 	protected CRMDataContext ctx = new CRMDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
   protected void Page_Load(object sender, EventArgs e)
    {

    }

   protected void grdStatus_UpdateInsert(object sender, GridRecordEventArgs e)
   {

	   CRM_Status rs = new CRM_Status();
	   if (e.Record["StatusID"].ToString() != "")
	   {
		   rs = (from r in ctx.CRM_Status
				 where r.StatusID == int.Parse(e.Record["StatusID"].ToString())
				 select r).First();
	   }

	   rs.Active_YN = bool.Parse(e.Record["Active_YN"].ToString());
	   rs.StatusName = e.Record["StatusName"].ToString();
	   if (e.Record["StatusID"].ToString() == "") ctx.CRM_Status.InsertOnSubmit(rs);
	   ctx.SubmitChanges();
   }

   protected void grdStatus_Rebind(object sender, EventArgs e)
   {

	   //select * from ResellerStatus where Active_YN=1  order by StatusName
	   grdStatus.DataSource = from rs in ctx.CRM_Status
							  where rs.Active_YN == true
							  orderby rs.StatusName
							  select rs;
	   grdStatus.DataBind();
   }
   protected void grdMSource_UpdateInsert(object sender, GridRecordEventArgs e)
   {

	   CRM_MarketingSource rs = new CRM_MarketingSource();
	   if (e.Record["MarketingSourceID"].ToString() != "")
	   {
		   rs = (from r in ctx.CRM_MarketingSources
				 where r.MarketingSourceID == int.Parse(e.Record["MarketingSourceID"].ToString())
				 select r).First();
	   }

	   rs.Active_YN = bool.Parse(e.Record["Active_YN"].ToString());
	   rs.MarketingSourceName = e.Record["MarketingSourceName"].ToString();
	   if (e.Record["MarketingSourceID"].ToString() == "") ctx.CRM_MarketingSources.InsertOnSubmit(rs);
	   ctx.SubmitChanges();
   }

   protected void grdMSource_Rebind(object sender, EventArgs e)
   {

	   grdMSource.DataSource = from rs in ctx.CRM_MarketingSources
							  where rs.Active_YN == true
							  orderby rs.MarketingSourceName
							  select rs;
	   grdMSource.DataBind();
   }
}