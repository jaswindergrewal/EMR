using System;
using System.Web;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;



public class LMCBase : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        ILMCBaseService objService = null;
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
                //string username = Request.ServerVariables["AUTH_USER"].Split('\\')[1];
                string username = "jerry.mixon";
                objService = new LMCBaseService();

                //lookup user
                StaffViewModel user = objService.Get(username);
                if (user == null) Response.Redirect("BadUser.aspx");
                // set asp session values in cookie
                HttpCookie cookie = new HttpCookie("session");
                cookie["UserID"] = user.EmployeeID.ToString();
                cookie["MM_Username"] = user.username;
                cookie["Access_Level"] = user.access_level;
                cookie["MM_UserAuthorization"] = user.access_level;
                //set .NET session values
                Session["StaffID"] = user.EmployeeID;
                long count = objService.Count((int)Session["StaffID"]);
                Session["TicketCount"] = count;
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

