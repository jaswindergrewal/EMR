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
/// jaswinder 6th aug 2013
/// </summary>
public partial class protocols_protocol_list : System.Web.UI.Page
{
    #region "Variables"
    private const int PAGE_SIZE = 30;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder 6th aug 2013
    /// Apply try catch  jaswinder 7th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<ProtocolViewModel> BindProtocolList(int PageIndex)
    {
        IProtocolService objService=null;
        List<ProtocolViewModel> lstStaff = null;
        try
        {
            objService = new ProtocolService();
            lstStaff = objService.GetProtocolList(PageIndex, PAGE_SIZE);
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