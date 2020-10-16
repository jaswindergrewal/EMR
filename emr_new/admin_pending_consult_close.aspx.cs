using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class admin_pending_consult_close : LMCBase
{
    IPendingFollowupService objService = null;
    #region Event
    /// <summary>
    /// updating follow up completed YN or close apt followup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            int FollowupID = int.Parse(Request.QueryString["followup_id"]);
            objService = new PendingFollowupService();
            objService.CloseFollowup(FollowupID);
            Response.Redirect("admin_contact_add_pendingfollowups.aspx?followup_id=" + Request.QueryString["followup_id"] + "&patientid=" + Request.QueryString["patientid"], false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally {
            objService = null;
        }
    }
    #endregion
}