using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion Asp to ASPX
/// By jaswinder on 5 th aug 2013
/// </summary>
public partial class admin_stafflogin_edit : System.Web.UI.Page
{
    #region "Variables"
    IStaffService objService = null;
    public string Pwd;

    #endregion

    #region "Events"

    /// <summary>
    /// Load the data on the basis of employee id and set the password value
    /// By jaswinder on 5 th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            User user = null;
            if (Session["UserID"] != null)
                user = new User(((int)Session["UserID"]).ToString());
           // if (user.AccessLevel == "emr_admin")
            //{

                if ((Request.QueryString["EmployeeId"]) != null)
                {
                    if (!IsPostBack)
                    {
                        objService = new StaffService();
                        StaffViewModel StaffDetail = objService.GetFirstOrDefaultStaffByEmployeeId(int.Parse(Request.QueryString["EmployeeId"]));
                        EmployeeName.Text = StaffDetail.EmployeeName;
                        Pwd = StaffDetail.password;
                        ClientScript.RegisterStartupScript(this.GetType(), "somescript", "setPwd();", true);
                        lblUsername.Text = StaffDetail.username;
                        if (StaffDetail.CanWritePrescript == true)
                        {
                            CanWritePresc.Checked = true;
                        }
                        else
                        {
                            CanWritePresc.Checked = false;
                        }
                        chkHARep.Checked = StaffDetail.IsHARep;
                  
                        chkActive.Checked = Convert.ToBoolean(StaffDetail.Active_YN); 
                        AccessLevel.Value = StaffDetail.access_level;
                    }
                }
            //}
            //else
            //{
            //    Response.Redirect("admin_stafflogin_list.aspx", false);
            //}
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    /// Update the staff details and redirect to the staff login list page
    /// By jaswinder on 5 th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new StaffService();
            bool CanWritePrescript;
            if (CanWritePresc.Checked == true)
            {
                CanWritePrescript = true;
            }
            else
            {
                CanWritePrescript = false;
            }

            bool HARep;
            if (chkHARep.Checked == true)
            {
                HARep = true;
            }
            else
            {
                HARep = false;
            }

            bool IsActive;
            if (chkActive.Checked == true)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }

            objService.UpdateStaffLogin(EmployeeName.Text, Password.Text, CanWritePrescript, AccessLevel.Value, int.Parse(Request.QueryString["EmployeeId"]), HARep,IsActive);
            Response.Redirect("admin_stafflogin_list.aspx",false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }

    #endregion
}