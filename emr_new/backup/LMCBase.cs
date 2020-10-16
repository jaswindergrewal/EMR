using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for LMCBase
/// </summary>
public class LMCBase : System.Web.UI.Page
{
	protected override void OnLoad(EventArgs e)
	{
		EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
		//if no session staffid
		if (Session["StaffID"] == null || Session["TicketCount"] == null || Session["MM_Username"] == null || Session["UserID"] == null)
		{
			if (Request.Cookies["session"] != null)
			{
				Session["StaffID"] = int.Parse(Request.Cookies["session"]["UserID"]);
				Session["TicketCount"] = int.Parse(Request.Cookies["session"]["OpenTickets"]);
				Session["MM_Username"] = Request.Cookies["session"]["MM_Username"];
				Session["UserID"] = (int)Session["StaffID"];
			}
			else
			{
				// GEt user name
				string username = Request.ServerVariables["AUTH_USER"].Split('\\')[1];

				//lookup user
				Staff user = (from s in ctx.Staffs where s.username == username && s.Active_YN == true select s).FirstOrDefault();
				if (user == null) Response.Redirect("BadUser.aspx");
				// set asp session values in cookie
				HttpCookie cookie = new HttpCookie("session");
				cookie["UserID"] = user.EmployeeID.ToString();
				cookie["MM_Username"] = user.username;
				cookie["Access_Level"] = user.access_level;
				cookie["MM_UserAuthorization"] = user.access_level;
				//set .NET session values
				Session["StaffID"] = user.EmployeeID;
				Session["TicketCount"] = (from t in ctx.apt_FollowUps
										  where t.Assigned == (int)Session["StaffID"]
										  && t.FollowUp_Completed_YN == false
										  && t.DueDate <= DateTime.Now
										  select t).Count();
				cookie["OpenTickets"] = Session["TicketCount"].ToString();
				Session["MM_Username"] = user.username;
				Session["UserID"] = (int)Session["StaffID"];
				Response.Cookies.Add(cookie);
			}
			// end if
		}

		// Be sure to call the base class's OnLoad method!
		base.OnLoad(e);
	}

}