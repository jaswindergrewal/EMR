using System;
using Emrdev.ServiceLayer;
using Obout.Grid;

public partial class admin_LabLog : LMCBase
{
    #region "Variables"
    ILabLogService objService = null;
    #endregion

    #region "Events"

    /// <summary>
    /// Get the detail list for labs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objService = new LabLogService();
            grdLabLog.DataSource = objService.GetLabLogList();
            grdLabLog.DataBind();
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