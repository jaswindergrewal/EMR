using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;

public partial class CRM_CampaignEvents : LMCBase
{
	protected CRMDataContext ctx = new CRMDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void InsertUpdateCampaign(object sender, GridRecordEventArgs e)
	{
		CRM_Campaign camp = new CRM_Campaign();
		if (e.Record["CampaignID"].ToString() != "")
		{
			camp = (from c in ctx.CRM_Campaigns
					where c.CampaignID == int.Parse(e.Record["CampaignID"].ToString())
					select c).First();
		}
		camp.CampaignName = e.Record["CampaignName"].ToString();
		camp.CampaignType = e.Record["CampaignType"].ToString();
		camp.MarketingBudget = e.Record["MarketingBudget"].ToString() != "" ? decimal.Parse(e.Record["MarketingBudget"].ToString().Replace("$", "")) : (decimal?)null;
		camp.StartDate = e.Record["StartDate"].ToString() != "" ? DateTime.Parse(e.Record["StartDate"].ToString()) : (DateTime?)null;
		camp.EndDate = e.Record["EndDate"].ToString() != "" ? DateTime.Parse(e.Record["EndDate"].ToString()) : (DateTime?)null;
		camp.MarketingSources = e.Record["MarketingSources"].ToString();
		if (e.Record["CampaignID"].ToString() == "") ctx.CRM_Campaigns.InsertOnSubmit(camp);
		ctx.SubmitChanges();
	}

	protected void InsertUpdateEvent(object sender, GridRecordEventArgs e)
	{
		SqlDataSource sqlEvent = (SqlDataSource)Utilities.FindControlRecursive(Master, "sqlEvent");
		CRM_Event eve = new CRM_Event();
		if (e.Record["EventID"].ToString() != "")
		{
			eve = (from ev in ctx.CRM_Events
				   where ev.EventID == int.Parse(e.Record["EventID"].ToString())
				   select ev).First();
		}
		eve.EventDate = DateTime.Parse(e.Record["EventDate"].ToString());
		eve.EventName = e.Record["EventName"].ToString();
		eve.Notes = e.Record["Notes"].ToString();
		eve.Venue = e.Record["Venue"].ToString();
		eve.Appointments = e.Record["Appointments"].ToString() != "" ? int.Parse(e.Record["Appointments"].ToString()) : (int?)null;
		eve.AudienceQuality = e.Record["AudienceQuality"].ToString() != "" ? int.Parse(e.Record["AudienceQuality"].ToString()) : (int?)null;
		eve.AudienceReaction = e.Record["AudienceReaction"].ToString() != "" ? int.Parse(e.Record["AudienceReaction"].ToString()) : (int?)null;
		eve.Callbacks = e.Record["Callbacks"].ToString() != "" ? int.Parse(e.Record["Callbacks"].ToString()) : (int?)null;
		eve.EventLength = e.Record["EventLength"].ToString();
		eve.FacilityInteriorExterior = e.Record["FacilityInteriorExterior"].ToString() != "" ? int.Parse(e.Record["FacilityInteriorExterior"].ToString()) : (int?)null;
		eve.Location = e.Record["Location"].ToString();
		eve.OverallPerformance = e.Record["OverallPerformance"].ToString() != "" ? int.Parse(e.Record["OverallPerformance"].ToString()) : (int?)null;
		eve.Parking = e.Record["Parking"].ToString() != "" ? int.Parse(e.Record["Parking"].ToString()) : (int?)null;
		eve.VenueQuality = e.Record["VenueQuality"].ToString() != "" ? int.Parse(e.Record["VenueQuality"].ToString()) : (int?)null;
		eve.Walkins = e.Record["Walkins"].ToString();
		if (e.Record["EventID"].ToString() == "")
		{
			eve.CampaignID = int.Parse(sqlEvent.SelectParameters[0].DefaultValue);
			ctx.CRM_Events.InsertOnSubmit(eve);
		}
		ctx.SubmitChanges();
	}

	protected void grdEvent_DataSourceNeeded(object sender, GridDataSourceNeededEventArgs e)
	{
		List<CRM_Event> events = (from ev in ctx.CRM_Events
								  where ev.CampaignID == int.Parse(e.ForeignKeysValues["CampaignID"])
								  select ev).ToList();

		grdEvent.DataSource = events;
		grdEvent.DataBind();


	}
}