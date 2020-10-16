using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CalendarStatusTicket : LMCBase
{
    #region "Variable"
    CalendarService _objService = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        BindStatusMgmt();
    }

    protected void grdStatus_Rebind(object sender, EventArgs e)
    {
        BindStatusMgmt();
    }

    private void BindStatusMgmt()
    {
        try
        {
            _objService = new CalendarService();
            List<CalStatusViewModel> status = _objService.GetCalStatus().ToList();
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

    protected void grdStatus_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        try
        {
            CalStatusViewModel calStatus = new CalStatusViewModel();
            calStatus.StatusId = 0;
            calStatus.Active = false;
            calStatus.Ticket = false;
            if (e.Record["StatusId"].ToString() != "")
            {
                calStatus.StatusId = int.Parse(e.Record["StatusId"].ToString());
            }

            if (e.Record["Active"].ToString() != "")
            {
                calStatus.Active = bool.Parse(e.Record["Active"].ToString());
            }

            if (e.Record["Ticket"].ToString() != "")
            {
                calStatus.Ticket = bool.Parse(e.Record["Ticket"].ToString());
            }
            var isNumeric = !string.IsNullOrEmpty(e.Record["CalledDays"].ToString()) && e.Record["CalledDays"].ToString().All(Char.IsDigit);
            if (isNumeric == true)
            {
                calStatus.CalledDays = int.Parse(e.Record["CalledDays"].ToString());
            }
            else
            {
                calStatus.CalledDays = 0;
            }
            calStatus.StatusName = e.Record["StatusName"].ToString();
            calStatus.TicketText = e.Record["TicketText"].ToString();
            _objService = new CalendarService();
            _objService.InsertUpdateCalStatus(calStatus);
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