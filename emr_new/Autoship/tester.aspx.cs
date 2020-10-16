using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar;


public partial class tester : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		User user = new User(Request.QueryString["userid"]);

		Session["MM_Username"] = user.Username;
		Session["UserID"] = user.EmployeeID.ToString();
		Session["Access_Level"] = user.AccessLevel;
		Session["EmployeeName"] = user.EmployeeName;

		Response.Redirect("AutoShip.aspx");
    }
}