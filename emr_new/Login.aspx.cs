using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
public partial class Login : System.Web.UI.Page
{
    #region Global Variables/Objects
    
    #endregion

    #region Page_Load
    /// <summary>
    /// this is page load event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
            ExpireAllCookies();
        }
    }
    #endregion

    #region btnLogin_Click
    /// <summary>
    /// check user login details and redirect to users role.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        ILMCBaseService objService = new LMCBaseService();
        StaffViewModel user = objService.Get(txtUserName.Text);
        if(user!=null)
        {
            if(user.password==txtPassword.Text)
            {
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
                Response.Redirect("LandingPage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('Password incorrect'); </script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('Login failed'); </script>");
        }
        
    }
    #endregion

    private void ExpireAllCookies()
    {
        if (HttpContext.Current != null)
        {
            int cookieCount = HttpContext.Current.Request.Cookies.Count;
            for (var i = 0; i < cookieCount; i++)
            {
                var cookie = HttpContext.Current.Request.Cookies[i];
                if (cookie != null)
                {
                    var cookieName = cookie.Name;
                    var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                    HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                }
            }

            // clear cookies server side
            HttpContext.Current.Request.Cookies.Clear();
        }
    }
}