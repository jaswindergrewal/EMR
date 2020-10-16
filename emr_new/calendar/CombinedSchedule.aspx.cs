using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DayPilot.Web.Ui.Events;
using Calendar;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;
using System.Linq;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class CombinedSchedule : LMCBase
{
    #region Variable
    ICalendarService objService = null;
    #endregion

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtStart.Text = DateTime.Today.ToShortDateString();
                List<Calendar.Provider> provList = Providers.getProviderList();
                foreach (Calendar.Provider prov in provList)
                {
                    System.Web.UI.WebControls.ListItem it = new System.Web.UI.WebControls.ListItem(prov.ProviderName, prov.id.ToString());
                    ProvidersCBox.Items.Add(it);
                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
            }

        }
    }

    protected void StartSked_OnClick(object sender, EventArgs e)
    {
        AddCalendar(ProvidersCBox.SelectedValue);
    }

    protected void ProvidersCBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartSked.Enabled = true;
    }

    protected void DayPilotCalendar1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        int attempts = 0;
        DataTable typeList = new DataTable();
        try
        {
            while (attempts < 3)
            {
                try
                {
                    typeList = AppointmentTypes.getApptTypeListOnly(ProvidersCBox.SelectedItem.Value);
                    break;
                }
                catch
                {
                    attempts++;
                }
            }
            foreach (DataRow apptType in typeList.Rows)
            {
                if (apptType["id"].ToString() == e.Tag["ApptTypeID"])
                {

                    e.InnerHTML += " " + (string)apptType["TypeName"];
                    e.BackgroundColor = (string)apptType["Color"];
                    if (((string)apptType["Color"]).ToLower() == "yellow" || ((string)apptType["Color"]).ToLower() == "aqua" || ((string)apptType["Color"]).ToLower() == "burleywood" || ((string)apptType["Color"]).ToLower() == "white")
                        e.FontColor = "Black";
                    else
                        e.FontColor = "White";
                    break;
                }
            }

            e.InnerHTML = e.InnerHTML.Replace("Unassigned Name", "");
            objService = new CalendarService();
            CombinedScheduleViewModel _objSchedule = objService.GetCombinedScheduled(int.Parse(e.Tag["EventID"]));
            if (_objSchedule.Notes != null && _objSchedule.Notes != "")
                e.InnerHTML += "<br/>" + _objSchedule.Notes;

            e.ToolTip = "Name: " + _objSchedule.Patient;
            e.ToolTip += "\r\nTime: " + ((DateTime)_objSchedule.ApptStart).ToString("t");
            e.ToolTip += "\r\nAppointment Type: " + _objSchedule.TypeName;
            e.ToolTip += "\r\nStatus: " + _objSchedule.StatusName;
            e.InnerHTML += "<br />Status: " + _objSchedule.StatusName;
            if (_objSchedule.Notes != "")
                e.ToolTip += "\r\nNotes: " + Server.HtmlEncode(_objSchedule.Notes);
            else
                e.ToolTip += "\r\nNotes: None";
            e.ToolTip += "\r\nResults: " + _objSchedule.ResultName;

            string lastTouched = Calendar.Appointments.GetLastTouched(e.Tag["EventID"]);
            e.InnerHTML += "<br />Last edited by " + lastTouched;
            e.ToolTip += "\r\nLast editied by by " + lastTouched;
            if (e.InnerHTML.Contains("Location<br"))
            {
                e.InnerHTML = e.InnerHTML.Replace("Location<br/>", "<br/>Location ");
                e.InnerHTML = e.InnerHTML.Remove(e.InnerHTML.IndexOf("Status") - 6);
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
    protected void DayPilotCalendar1_BeforeHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
    {
        try
        {
            SetCalLabel(ProvidersCBox.SelectedValue, DateTime.Parse(txtStart.Text));
            Calendar.Provider prov = Calendar.Provider.GetProvider(ProvidersCBox.SelectedValue)[0];

            string ProviderName = Calendar.Provider.GetProviderName(ProvidersCBox.SelectedValue);

            e.InnerHTML = ProviderName + " " + e.InnerHTML;
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

    #region Methods

    protected void AddCalendar(string ProviderID)
    {
        try
        {
            //set provider ID
            Calendar.Provider prov = Calendar.Provider.GetProvider(ProviderID)[0];
            //add to cell

            Settings settings = new Settings();
            DayPilotCalendar1.BusinessBeginsHour = settings.BusinessBeginsHour;
            DayPilotCalendar1.BusinessEndsHour = settings.BusinessEndsHour;
            DayPilotCalendar1.EventSelectColor = System.Drawing.ColorTranslator.FromHtml(settings.EventSelectColor);
            DayPilotCalendar1.ShowAllDayEvents = settings.ShowAllDayEvents;
            DayPilotCalendar1.AllDayEventBackColor = System.Drawing.ColorTranslator.FromHtml(settings.AllDayEventBackColor);
            DayPilotCalendar1.BackColor = System.Drawing.ColorTranslator.FromHtml(settings.BackColor);
            DayPilotCalendar1.BorderColor = System.Drawing.ColorTranslator.FromHtml(settings.BorderColor);
            DayPilotCalendar1.CellSelectColor = System.Drawing.ColorTranslator.FromHtml(settings.CellSelectColor);
            DayPilotCalendar1.EventBackColor = System.Drawing.ColorTranslator.FromHtml(settings.EventBackColor);
            DayPilotCalendar1.EventBorderColor = System.Drawing.ColorTranslator.FromHtml(settings.EventBorderColor);
            DayPilotCalendar1.HourBorderColor = System.Drawing.ColorTranslator.FromHtml(settings.HourBorderColor);
            DayPilotCalendar1.HourHalfBorderColor = System.Drawing.ColorTranslator.FromHtml(settings.HourHalfBorderColor);
            DayPilotCalendar1.HourNameBackColor = System.Drawing.ColorTranslator.FromHtml(settings.HourNameBackColor);
            DayPilotCalendar1.HourNameBorderColor = System.Drawing.ColorTranslator.FromHtml(settings.HourNameBorderColor);
            DayPilotCalendar1.NonBusinessBackColor = System.Drawing.ColorTranslator.FromHtml(settings.NonBusinessBackColor);
            DayPilotCalendar1.ScrollPositionHour = settings.ScrollPositionHour;
            DayPilotCalendar1.HeaderDateFormat = "ddd M/d/yyyy";
            DayPilotCalendar1.DayBeginsHour = 8;
            DayPilotCalendar1.BusinessBeginsHour = 8;
            DayPilotCalendar1.BusinessEndsHour = 18;
            DayPilotCalendar1.DayEndsHour = 4;
            DayPilotCalendar1.CellHeight = 40;
            DayPilotCalendar1.HeightSpec = DayPilot.Web.Ui.Enums.HeightSpecEnum.Parent100Pct;
            DayPilotCalendar1.EventFontSize = "12pt";
            DayPilotCalendar1.StartDate = DateTime.Parse(txtStart.Text);
            DayPilotCalendar1.CellDuration = int.Parse(rdoTime.SelectedValue);
            DayPilotCalendar1.Days = 1;
            DayPilotCalendar1.CssClassPrefix = "calendar_";
            List<Appointment> data = ProviderCal.GetCal(ProviderID, "0", "0", DayPilotCalendar1.StartDate, DayPilotCalendar1.Days);
            DayPilotCalendar1.DataSource = data;
            DayPilotCalendar1.DataBind();
            AdminStuff.Visible = false;
            Cal.Visible = true;
            return;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }

    }

    //private void SetCalLabel(string ProviderID, DateTime calDate, OneCal cal)
    //{
    //    try
    //    {
    //        Calendar.Provider prov = Calendar.Provider.GetProvider(ProviderID)[0];

    //        cal.ProviderName = Calendar.Provider.GetProviderName(ProviderID);
    //        cal.ID = "cal" + ProviderID;

    //        switch (calDate.DayOfWeek)
    //        {
    //            case DayOfWeek.Monday:
    //                if (prov.MondayStart != "0")
    //                    cal.ProviderName += "<br />" + prov.MondayStart + " - " + prov.MondayEnd;
    //                else
    //                    cal.ProviderName += "<br />Not in office.";
    //                break;
    //            case DayOfWeek.Tuesday:
    //                if (prov.TuesdayStart != "0")
    //                    cal.ProviderName += "<br />" + prov.TuesdayStart + " - " + prov.TuesdayEnd;
    //                else
    //                    cal.ProviderName += "<br />Not in office.";
    //                break;
    //            case DayOfWeek.Wednesday:
    //                if (prov.WednesdayStart != "0")
    //                    cal.ProviderName += "<br />" + prov.WednesdayStart + " - " + prov.WednesdayEnd;
    //                else
    //                    cal.ProviderName += "<br />Not in office.";
    //                break;
    //            case DayOfWeek.Thursday:
    //                if (prov.ThursdayStart != "0")
    //                    cal.ProviderName += "<br />" + prov.ThursdayStart + " - " + prov.ThursdayEnd;
    //                else
    //                    cal.ProviderName += "<br />Not in office.";
    //                break;
    //            case DayOfWeek.Friday:
    //                if (prov.FridayStart != "0")
    //                    cal.ProviderName += "<br />" + prov.FridayStart + " - " + prov.FridayEnd;
    //                else
    //                    cal.ProviderName += "<br />Not in office.";
    //                break;
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
    //    }
    //    finally
    //    {
    //        objService = null;
    //    }
    //}

    private void SetCalLabel(string ProviderID, DateTime calDate)
    {
        try
        {
            Calendar.Provider prov = Calendar.Provider.GetProvider(ProviderID)[0];

            string ProviderName = Calendar.Provider.GetProviderName(ProviderID);
            switch (calDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (prov.MondayStart != "0")
                        ProviderName += "<br />" + prov.MondayStart + " - " + prov.MondayEnd;
                    else
                        ProviderName += "<br />Not in office.";
                    break;
                case DayOfWeek.Tuesday:
                    if (prov.TuesdayStart != "0")
                        ProviderName += "<br />" + prov.TuesdayStart + " - " + prov.TuesdayEnd;
                    else
                        ProviderName += "<br />Not in office.";
                    break;
                case DayOfWeek.Wednesday:
                    if (prov.WednesdayStart != "0")
                        ProviderName += "<br />" + prov.WednesdayStart + " - " + prov.WednesdayEnd;
                    else
                        ProviderName += "<br />Not in office.";
                    break;
                case DayOfWeek.Thursday:
                    if (prov.ThursdayStart != "0")
                        ProviderName += "<br />" + prov.ThursdayStart + " - " + prov.ThursdayEnd;
                    else
                        ProviderName += "<br />Not in office.";
                    break;
                case DayOfWeek.Friday:
                    if (prov.FridayStart != "0")
                        ProviderName += "<br />" + prov.FridayStart + " - " + prov.FridayEnd;
                    else
                        ProviderName += "<br />Not in office.";
                    break;
            }
            lblProviderName.Text = ProviderName;
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


    private void Tester()
    {
        try
        {
            int hourHeight = DayPilotCalendar1.CellDuration * DayPilotCalendar1.CellHeight;
            Response.Clear(); Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=BSW_Calendar.png");
            System.IO.MemoryStream img = DayPilotCalendar1.Export(System.Drawing.Imaging.ImageFormat.Gif, 9 * hourHeight);
            img.WriteTo(Response.OutputStream);
            Response.AppendHeader("Content-Length", img.Length.ToString());

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