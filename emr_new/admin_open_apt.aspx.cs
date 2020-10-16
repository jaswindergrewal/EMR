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
/// Conversion from asp to aspx
/// Jaswinder 13 th aug 2013
/// </summary>
public partial class admin_open_apt : System.Web.UI.Page
{
    #region"variables"
    public string PAGE_SIZE = (ConfigurationManager.AppSettings["PAGE_SIZE"].ToString());
    IPatientService objService = null;
    #endregion

    #region "Events"
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new PatientService();
                ddlClinic.DataSource = objService.GetClinics();
                ddlClinic.DataTextField = "ClinicName";
                ddlClinic.DataValueField = "ClinicName";
                ddlClinic.DataBind();
                ddlClinic.Items.Insert(0, new ListItem("Select Clinic"));
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
    }

   
    #endregion


    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder on 13th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<OpenAppointmentViewModel> BindAptGrid(int PageIndex, string Clinic, int PageSize)
    {
        IAppointmentsService objService = null;
        List<OpenAppointmentViewModel> lstStaff = null;
        try
        {
            objService = new AppointmentsService();
            lstStaff = objService.GetAppointmentsByClinic(Clinic, PageIndex, PageSize);
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