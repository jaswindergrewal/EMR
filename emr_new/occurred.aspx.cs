using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class occurred : LMCBase
{
    #region "Variable"
    IAppointmentsService objService = null;
    #endregion

    #region "Event"
    /// <summary>
    /// Mark the appointment as occurred by setting result column in apt_rec table as 3
    /// </summary>
	protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (Request.QueryString["aptid"] != null)
            {
                objService = new AppointmentsService();
                objService.UpdateAppointmentOccured(int.Parse(Request.QueryString["aptid"].ToString()));
                Response.Redirect("appointments.aspx?PatientID=" + Request.QueryString["PatientID"],false);
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion

}