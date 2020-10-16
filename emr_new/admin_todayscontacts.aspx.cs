using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of ASP to ASPX page
/// Jaswinder 5th Aug 2013
/// </summary>

public partial class admin_todayscontacts : System.Web.UI.Page
{
    #region "Variables"
    public string  PAGE_SIZE = (ConfigurationManager.AppSettings["PAGE_SIZE"].ToString());
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
    public static List<TodaysContactViewModel> BindList(int PageIndex ,int PageSize)
    {
        ISurveyResultService objService = null;
        List<TodaysContactViewModel> lstTodaysPatient = null;
        try
        {
            objService = new SurveyResultService();
            lstTodaysPatient = objService.GetTodaysContactDetails(PageIndex, PageSize);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstTodaysPatient;
    }
    #endregion
}