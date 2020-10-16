using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of ASP to Aspx
/// Jaswinder on 5th aug 2013
/// </summary>
public partial class admin_stafflogin_list : System.Web.UI.Page
{
    #region "Variables"
    private const int PAGE_SIZE = 30;
    #endregion

    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 5th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<StaffViewModel> BindStaffList(int PageIndex)
    {
        IStaffService objService = null;
        List<StaffViewModel> lstStaff = null;
        try
        {
            objService = new StaffService();
            lstStaff = objService.GetStaffList(PageIndex, PAGE_SIZE);
            
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstStaff;
    }
    #endregion
}