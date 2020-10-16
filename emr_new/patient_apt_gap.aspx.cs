using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion from asp to aspx
/// Jaswinder 13 th aug 2013
/// </summary>

public partial class patient_apt_gap : System.Web.UI.Page
{

    #region "events"
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 13th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<OpenAppointmentViewModel> BindAptGrid(int PageIndex, string Days)
    {
        IAppointmentsService objService = null;
        List<OpenAppointmentViewModel> lstopenApt = null;
        try
        {
            objService = new AppointmentsService();
            lstopenApt = objService.GetAppointmentsGap(Days, PageIndex, 30);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstopenApt;
    }
    #endregion
   
}