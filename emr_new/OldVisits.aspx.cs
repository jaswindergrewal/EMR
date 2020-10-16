using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class OldVisits : LMCBase
{
    #region "Variable"
    IOldVisitService objService = null;
    #endregion

    #region "Event"
    /// <summary>
    /// get the patients old visits
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objService = new OldVisitService();
            rptVisits.DataSource = objService.GetOldVisits(int.Parse(Request.QueryString["PatientID"]));
            rptVisits.DataBind();

            rptNotes.DataSource = objService.GetOldNotes(int.Parse(Request.QueryString["PatientID"]));
            rptNotes.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }
    #endregion
}