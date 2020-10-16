using System;
using System.Collections.Generic;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CalendarPatientLog : LMCBase
{
    CalendarService _objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindStatusMgmt();
                    BindStatus();
                }
                catch (System.Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
                }
                finally
                {

                }
            }
        }
        else
        {
            Response.Redirect("~/landingpage.aspx");
        }
    }

    public void BindStatus()
    {
        try
        {
            _objService = new CalendarService();
            ddlStatus.DataSource = _objService.GetCalStatus().Where(x=>x.Active=true);
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataValueField = "StatusId";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select a Status","0"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }
    private void BindStatusMgmt()
    {
        try
        {
            int patientId = int.Parse(Request.QueryString["PatientID"]);
            _objService = new CalendarService();
            List<CalStatusViewModel> status = _objService.GetCalStatusLog(patientId).ToList();
            grdStatus.DataSource = status;
            grdStatus.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    protected void grdStatus_UpdateCommand(object sender, GridRecordEventArgs e)
    {

        try
        {
            int patientId = int.Parse(Request.QueryString["PatientID"]);
            int StaffId = (int)Session["StaffID"];
            CalStatusViewModel calStatus = new CalStatusViewModel();
            calStatus.StatusId = 0;
           
            calStatus.IsTicketSet = false;
            calStatus.PatientId = patientId;
            calStatus.StaffId = StaffId;

            calStatus.StatusLogId = 0;
            if (e.Record["StatusId"].ToString() != "")
            {
                calStatus.StatusId = int.Parse(e.Record["StatusId"].ToString());
            }
            if (e.Record["StatusLogId"].ToString() != "")
            {
                calStatus.StatusLogId = int.Parse(e.Record["StatusLogId"].ToString());
            }

           

            if (e.Record["IsTicketSet"].ToString() != "")
            {
                calStatus.IsTicketSet = bool.Parse(e.Record["IsTicketSet"].ToString());
            }

            _objService = new CalendarService();
            _objService.InsertUpdateStatusLog(calStatus);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> parent.change_parent_url('Manage.aspx?PatientID=" + Request.QueryString["PatientID"] + "'); </script>");
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    protected void grdStatus_DeleteCommand(object sender, GridRecordEventArgs e)
    {
        try
        {
            int patientId = int.Parse(Request.QueryString["PatientID"]);
            int StaffId = (int)Session["StaffID"];
            CalStatusViewModel calStatus = new CalStatusViewModel();
            calStatus.StatusId = 0;
           
            calStatus.IsTicketSet = false;
            calStatus.PatientId = patientId;
            calStatus.StaffId = StaffId;

            calStatus.StatusLogId = 0;
            if (e.Record["StatusId"].ToString() != "")
            {
                calStatus.StatusId = int.Parse(e.Record["StatusId"].ToString());
            }
            if (e.Record["StatusLogId"].ToString() != "")
            {
                calStatus.StatusLogId = int.Parse(e.Record["StatusLogId"].ToString());
            }

            

            if (e.Record["IsTicketSet"].ToString() != "")
            {
                calStatus.IsTicketSet = bool.Parse(e.Record["IsTicketSet"].ToString());
            }

            _objService = new CalendarService();
            _objService.RemoveStatusLog(calStatus);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    protected void grdStatus_Rebind(object sender, EventArgs e)
    {
        BindStatusMgmt();
    }

    protected void btnAddStatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedValue != "0")
            {
                int patientId = int.Parse(Request.QueryString["PatientID"]);
                int StaffId = (int)Session["StaffID"];
                CalStatusViewModel calStatus = new CalStatusViewModel();
                calStatus.StatusId = Convert.ToInt16(ddlStatus.SelectedValue);

                calStatus.IsTicketSet = false;
                calStatus.PatientId = patientId;
                calStatus.StaffId = StaffId;

                calStatus.StatusLogId = 0;




                _objService = new CalendarService();
                _objService.InsertUpdateStatusLog(calStatus);
                BindStatusMgmt();
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> parent.change_parent_url('Manage.aspx?PatientID=" + Request.QueryString["PatientID"] + "'); </script>");
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }
}
