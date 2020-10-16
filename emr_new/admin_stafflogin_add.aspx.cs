using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Emrdev.ServiceLayer;
using System.IO;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder on 5 th Aug 2013
/// </summary>
public partial class admin_stafflogin_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Insert the staff record in database by click on submint button
    /// </summary>
    /// <param name="StaffName"></param>
    /// <param name="StaffUserName"></param>
    /// <param name="StaffPassword"></param>
    /// <param name="prescription"></param>
    /// <param name="StaffType"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static string InsertStaff(string StaffUserName, string StaffPassword, string StaffName, string StaffType, bool prescription,bool HARep)
    {
        string strIsExists="false";
        IStaffService objService = new StaffService();
        try
        {
            int result = objService.InsertStaffLogin(StaffUserName, StaffPassword, StaffName, StaffType, prescription, HARep);
            if (result == 1)
            {
                strIsExists = "true";
            }

            else
            {
                strIsExists = "false";
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return strIsExists;
    }
}
