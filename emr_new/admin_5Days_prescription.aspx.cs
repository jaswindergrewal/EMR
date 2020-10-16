using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Configuration;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 12th aug 2013
/// </summary>
public partial class admin_5Days_prescription : System.Web.UI.Page
{
    #region"variables"
    public string PAGE_SIZE = (ConfigurationManager.AppSettings["PAGE_SIZE"].ToString());
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnPageSize.Value = PAGE_SIZE;
        }
    }

    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 12th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<PrescriptionFor5Days> BindAptGrid(int PageIndex, int PageSize)
    {
        IAppointmentsService objService = null;
        List<PrescriptionFor5Days> lstStaff = null;
        try
        {
            objService = new AppointmentsService();
            lstStaff = objService.GetLastFiveDayPrescription(PageIndex, PageSize);
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