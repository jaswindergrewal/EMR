using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class Appointments : LMCBase
{
    #region "Variables"
    protected int PatientID = 0;
    IAppointmentsService objService = null;
    #endregion

    #region "Events"

    /// <summary>
   /// Get the appointments by patientid 
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PatientID"] != null)
                {
                    PatientID = int.Parse(Request.QueryString["PatientID"]);
                    objService = new AppointmentsService();
                    rptAppoinments.DataSource = objService.GetAppointsbyPatientID(PatientID);
                    rptAppoinments.DataBind();
                }
            }
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