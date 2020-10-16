using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 14th aug 2013
/// </summary>
public partial class apt_99_special : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 14th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<SpecialAptViewModel> BindAptGrid(int PageIndex)
    {
        IAppointmentsService objService = null;
        List<SpecialAptViewModel> lstopenApt = null;
        try
        {
            objService = new AppointmentsService();
            lstopenApt = objService.GetSpecialAppointment(PageIndex, 30);
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