using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class admin_RemoveRecurring : LMCBase
{
    #region "Variables"
    private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    IRecurringApptService objRecurringApptService = null;
    #endregion

    #region "Events"
    /// <summary>
    /// bind appointment and provider dropdownlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

            BindAppointmentTypeDropDown();
            BindProviderDropDown();

            grdPreview.Visible = false;
        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {


        List<RecurringAppointmentViewModel> lstRecAptViewModel = null;
        try
        {
            objRecurringApptService = new RecurringApptService();
            lstRecAptViewModel = new List<RecurringAppointmentViewModel>();

            lstRecAptViewModel = objRecurringApptService.PreviewRecurringAppointment(int.Parse(ddlProvider.SelectedValue), int.Parse(ddlApptType.SelectedValue), DateTime.Parse(txtStart.Text), DateTime.Parse(txtEnd.Text), txtStartTime.Text, txtEndTime.Text, txtStartMinutes.Text, txtEndMinutes.Text);

            if (lstRecAptViewModel.Count > 0)
            {
                foreach (RecurringAppointmentViewModel d in lstRecAptViewModel)
                {
                    Apt_Rec_ViewModel clsApt = objRecurringApptService.GetApt_Rec(d.AppointmentID);
                    string messageBody = "Appointment deleted. Date/Time" + clsApt.ApptStart + ". Deleted by " + (from s in ctx.Staffs where s.EmployeeID == (int)Session["StaffID"] select s.EmployeeName).First() + ".";
                    int employeeId = (int)Session["StaffID"];
                    int apt_id = clsApt.apt_id;
                    int PatientId = Convert.ToInt32(clsApt.patient_id);
                    objRecurringApptService.AddUpdateRecurringAppointment(57, PatientId, messageBody, employeeId, apt_id);
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }


    protected void UpdatePreview(object sender, EventArgs e)
    {


        List<RecurringAppointmentViewModel> lstRecAptViewModel = null;
        try
        {
            objRecurringApptService = new RecurringApptService();
            lstRecAptViewModel = new List<RecurringAppointmentViewModel>();

            lstRecAptViewModel = objRecurringApptService.PreviewRecurringAppointment(int.Parse(ddlProvider.SelectedValue), int.Parse(ddlApptType.SelectedValue), DateTime.Parse(txtStart.Text), DateTime.Parse(txtEnd.Text), txtStartTime.Text, txtEndTime.Text, txtStartMinutes.Text, txtEndMinutes.Text);
            if (lstRecAptViewModel.Count > 0)
            {
                grdPreview.Visible = true;
                grdPreview.DataSource = lstRecAptViewModel;
                grdPreview.DataBind();
            }
            else
            {
                grdPreview.Visible = false;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstRecAptViewModel = null;
            objRecurringApptService = null;
        }

        if (grdPreview.Rows.Count > 1)
            btnDelete.Visible = true;
    }

    protected void NoPreview(object sender, EventArgs e)
    {
        btnDelete.Visible = false;
    }

#endregion


    #region "Methods"

    /// <summary>
    /// binding the dropdownlist with appointment type
    /// </summary>
    private void BindAppointmentTypeDropDown()
    {
        IAppointmentConsole objAppointmentConsoleService = null;
        List<AppointmentTypeViewModel> lstAptConsoleViewModel = null;
        try
        {
            objAppointmentConsoleService = new AppointmentConsole();
            lstAptConsoleViewModel = new List<AppointmentTypeViewModel>();
            lstAptConsoleViewModel = objAppointmentConsoleService.GetAppointmentTypeList();

            ddlApptType.DataSource = lstAptConsoleViewModel;
            ddlApptType.DataTextField = "TypeName";
            ddlApptType.DataValueField = "ID";
            ddlApptType.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objAppointmentConsoleService = null;
            lstAptConsoleViewModel = null;
        }
    }

    /// <summary>
    /// binding provider dropdownlist with provider details
    /// </summary>
    private void BindProviderDropDown()
    {
        IProviderService objIProviderService = null;
        List<ProviderViewModel> lstProviderViewModel = null;
        try
        {
            objIProviderService = new ProviderService();
            lstProviderViewModel = new List<ProviderViewModel>();
            lstProviderViewModel = objIProviderService.GetProviderDetails();

            ddlProvider.DataSource = lstProviderViewModel;
            ddlProvider.DataTextField = "ProviderName";
            ddlProvider.DataValueField = "id";
            ddlProvider.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIProviderService = null;
            lstProviderViewModel = null;
        }
    }

 
    #endregion

   
}
