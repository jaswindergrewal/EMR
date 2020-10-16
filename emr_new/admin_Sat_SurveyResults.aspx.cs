using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class admin_Sat_SurveyResults : System.Web.UI.Page
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
    public static List<SurveyResultViewModel> BindList(int PageIndex)
    {
        ISurveyResultService objService = null;
        List<SurveyResultViewModel> lstSurveyPatient = null;
        try
        {
            objService = new SurveyResultService();
            lstSurveyPatient = objService.GetSurveyDetails(PageIndex, PAGE_SIZE);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstSurveyPatient;
    }
    #endregion
}