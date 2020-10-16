using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar;
using Emrdev.ServiceLayer;

public partial class PatientAppointments : LMCBase
{
    #region "variables"
    ICalendarService objService = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "Events"
    /// <summary>
    /// on text changed we get the list of patient appointments and bind in grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPatient_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);

            objService = new CalendarService();
            gridAppts.DataSource = objService.GetAppointment(pat.ID);
            gridAppts.DataBind();
            gridAppts.Visible = true;
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

    protected void gridAppts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridAppts.PageIndex = e.NewPageIndex;
        Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);

        objService = new CalendarService();
        gridAppts.DataSource = objService.GetAppointment(pat.ID);
        gridAppts.DataBind();
        gridAppts.Visible = true;
    }

    #endregion
}