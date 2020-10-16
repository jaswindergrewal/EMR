using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
///conversion of asp to aspx
///jaswinder 16th aug 2013
/// </summary>
public partial class protocols_protocol_add : LMCBase
{
    #region "Variables"
    IProtocolService objService = null;

    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Add the specialattention info in tables
    /// Jaswinder 16th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
                objService = new ProtocolService();
                ProtocolViewModel objProtocolView = new ProtocolViewModel();
                objProtocolView.Protocol_Desc = ed.Content;
                objProtocolView.Protocol_Title = txtTitle.Text;
                objProtocolView.DateEntered = DateTime.Now;
                objProtocolView.EnteredBy = Convert.ToInt32(Session["StaffID"].ToString());
                objProtocolView.Viewable_YN = true;
                objProtocolView.Lastupdated = DateTime.Now;
                objService.AddProtocolDetails(objProtocolView);
            
                Response.Redirect("protocols_protocol_list.aspx", false);
            
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
    
    /// <summary>
    /// Redirect page to protocol list page
    /// Jaswinder 16th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("protocols_protocol_list.aspx", false);
    }

    #endregion
}