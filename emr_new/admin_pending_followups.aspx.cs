using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


/// <summary>
/// Conversion from asp to aspx
/// Jaswinder 13 th aug 2013
/// </summary>

public partial class admin_pending_followups : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IPatientService objIPatientService = new PatientService();
            ddlClinic.DataSource = objIPatientService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("Select Clinic"));
        }   

    }
    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 13th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<PendingFollowupClinicViewModel> BindAptGrid(int PageIndex, string Clinic, string OrderBy)
    {
        IAppointmentsService objService = null;
        List<PendingFollowupClinicViewModel> lstopenApt = null;
        try
        {
            objService = new AppointmentsService();
            lstopenApt = objService.GetPendingFollowupsByClinic(Clinic, OrderBy, PageIndex, 30);
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