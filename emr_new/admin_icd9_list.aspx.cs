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
/// 7th aug 2013 jaswinder
/// </summary>
public partial class admin_icd9_list : System.Web.UI.Page
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

    #region"Method"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder 7th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<DiagnosistblViewModel> BindDiagnosis(int PageIndex, int PageSize)
    {
        List<DiagnosistblViewModel> lstDiagnosis = null;
        IProtocolService objService = null;
        try
        {
            objService = new ProtocolService();
            lstDiagnosis = objService.BindDiagnosisList(PageIndex, PageSize);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
           
        }
        return lstDiagnosis;

    }

    #endregion
}