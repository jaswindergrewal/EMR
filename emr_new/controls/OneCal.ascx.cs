using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DayPilot.Web.Ui.Events;
using Calendar;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;
using System.Linq;

public partial class OneCal : System.Web.UI.UserControl
{

    public string ProviderID { get { return lblProvider1.Text; } set { lblProvider1.Text = value; } }
    public string ProviderName { get { return lblProviderName.Text; } set { lblProviderName.Text = value; } }
    private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    private CRMDataContext ctxCRM = new CRMDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

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
        DayPilotCalendar1.HeaderDateFormat = "ddd(dd)";

        //commented
        //List<Result> res = Results.getResultsList();
        //DayPilotMenu1.MenuItems.Clear();
        //foreach (Result r in res)
        //{
        //    DayPilot.Web.Ui.MenuItem itm = new DayPilot.Web.Ui.MenuItem();
        //    itm.Text = r.ResultName;
        //    itm.Command = r.ID.ToString();
        //    itm.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
        //    DayPilotMenu1.MenuItems.Add(itm);
        //}

        //DayPilot.Web.Ui.MenuItem itm1 = new DayPilot.Web.Ui.MenuItem();
        //itm1.Text = "Delete";
        //itm1.Command = "100";
        //itm1.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
        //DayPilotMenu1.MenuItems.Add(itm1);

        //commented end

        //DayPilot.Web.Ui.MenuItem imtLine = new DayPilot.Web.Ui.MenuItem();
        //imtLine.Text = "--------";

        //DayPilotMenu1.MenuItems.Add(imtLine);

        //DayPilot.Web.Ui.MenuItem itm2 = new DayPilot.Web.Ui.MenuItem();
        //itm2.Text = "Sale Yes";
        //itm2.Command = "1000";
        //itm2.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;

        //DayPilotMenu1.MenuItems.Add(itm2);
        //DayPilot.Web.Ui.MenuItem itm3 = new DayPilot.Web.Ui.MenuItem();
        //itm3.Text = "Sale No";
        //itm3.Command = "2000";
        //itm3.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
        //DayPilotMenu1.MenuItems.Add(itm3);


        //DayPilot.Web.Ui.MenuItem itm4 = new DayPilot.Web.Ui.MenuItem();
        //itm4.Text = "Sale Maybe";
        //itm4.Command = "3000";
        //itm4.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
        //DayPilotMenu1.MenuItems.Add(itm4);

        //DayPilot.Web.Ui.MenuItem imtLine1 = new DayPilot.Web.Ui.MenuItem();
        //imtLine1.Text = "--------";

        //DayPilotMenu1.MenuItems.Add(imtLine1);

        //commented
        //DayPilot.Web.Ui.MenuItem itm5 = new DayPilot.Web.Ui.MenuItem();
        //itm5.Text = "Patient Details";
        //itm5.Command = "4000";
        //itm5.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
        //DayPilotMenu1.MenuItems.Add(itm5);
        //commented End
    }



    protected void DayPilotCalendar1_Command(object sender, DayPilot.Web.Ui.Events.CommandEventArgs e)
    {
        if (e.Command == "refresh")
        {
            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.Update();
        }
    }
    protected void DayPilotCalendar1_EventClick(object sender, EventClickEventArgs e)
    {
        FormView EventDetail = (FormView)FindControlRecursive(Page.Master, "EventDetail");
        TextBox EventIDText = (TextBox)EventDetail.FindControl("txtEventID");
        ObjectDataSource ObjectDataSourceDetail = (ObjectDataSource)FindControlRecursive(Page.Master, "ObjectDataSourceDetail");
        ObjectDataSource AppointmentTypeSourceOnly = (ObjectDataSource)EventDetail.FindControl("AppointmentTypeSourceOnly");
        DropDownList ddProv = (DropDownList)EventDetail.FindControl("ProviderDropDown");
        if (AppointmentTypeSourceOnly != null)
        {
            if (ProviderID != null)
                AppointmentTypeSourceOnly.SelectParameters[0].DefaultValue = ProviderID;
            else
                AppointmentTypeSourceOnly.SelectParameters[0].DefaultValue = "0";
        }
        UpdatePanel UpdatePanelDetail = (UpdatePanel)FindControlRecursive(Page.Master, "UpdatePanelDetail");
        ModalPopupExtender ModalPopup = (ModalPopupExtender)FindControlRecursive(Page.Master, "ModalPopup");

        EventDetail.ChangeMode(FormViewMode.Edit);
        ObjectDataSourceDetail.SelectParameters["id"].DefaultValue = e.Value;
        //EventDetail.DataSource = Calendar.ProviderCal.GetApptByID(e.Value);
        Label lblEventID = (Label)Utilities.FindControlRecursive(Page.Master, "lblEventID");

        lblEventID.Text = e.Value;

        Appointment appt = ProviderCal.GetApptByID(e.Value);
        List<Appointment> lst = new List<Appointment>();
        lst.Add(appt);
        EventDetail.DataSource = lst;
        EventDetail.DataSourceID = null;
        EventDetail.DataBind();
        UpdatePanelDetail.Update();
        if (Session["LockedAppointment"] != null)
        {
            if (Session["LockedAppointment"] == "true")
            {

                if (appt.category != "Locked")
                {
                    ModalPopup.Show();
                }
            }
            else
            {
                ModalPopup.Show();
            }
        }
        //DayPilotCalendar1.Update();
    }

    protected void DayPilotCalendar1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        int attempts = 0;
        DataTable typeList = new DataTable();
        while (attempts < 3)
        {
            try
            {
                typeList = AppointmentTypes.getApptTypeListOnly(lblProvider1.Text);
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
                //e.DurationBarColor = "White";//apptType.Color;
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

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Appointment_GetByID", con);
            cmd.Parameters.AddWithValue("@EventID", e.Tag["EventID"]);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
            reader.Read();

            if (reader["Notes"] != DBNull.Value && (string)reader["Notes"] != "")
                e.InnerHTML += "<br/>" + (string)reader["Notes"];

            e.ToolTip = "Name: " + (string)reader["Patient"];
            e.ToolTip += "\r\nTime: " + ((DateTime)reader["ApptStart"]).ToString("t");
            e.ToolTip += "\r\nAppointment Type: " + (string)reader["TypeName"];
            e.ToolTip += "\r\nStatus: " + (string)reader["StatusName"];
            e.InnerHTML += "<br />Status: " + (string)reader["StatusName"];
            if (!string.IsNullOrEmpty(reader["Notes"].ToString()))
                e.ToolTip += "\r\nNotes: " + Server.HtmlEncode((string)reader["Notes"]);
            else
                e.ToolTip += "\r\nNotes: None";
            e.ToolTip += "\r\nResults: " + (String)reader["ResultName"];
            e.ToolTip += "\r\nHA Rep: " + (String)reader["HARepName"];
            e.ToolTip += "\r\nLabs checked in? ";
            e.ToolTip += reader["LabsCheckedIn"].ToString() == "True" ? "Yes" : "No" + ".";

            if (Session["LockedAppointment"] != null)
            {
                if (Session["LockedAppointment"] == "true")
                {


                    if ((string)reader["category"] != "Locked")
                    {
                        if (reader["SaleMade_yn"].ToString() == "1")
                        {

                            List<Result> res = Results.getResultsList();
                            DayPilotMenu1.MenuItems.Clear();
                            foreach (Result r in res)
                            {
                                DayPilot.Web.Ui.MenuItem itm = new DayPilot.Web.Ui.MenuItem();
                                itm.Text = r.ResultName;
                                itm.Command = r.ID.ToString();
                                itm.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                                DayPilotMenu1.MenuItems.Add(itm);
                            }

                            DayPilot.Web.Ui.MenuItem itm1 = new DayPilot.Web.Ui.MenuItem();
                            itm1.Text = "Delete";
                            itm1.Command = "100";
                            itm1.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu1.MenuItems.Add(itm1);

                            DayPilot.Web.Ui.MenuItem itm2 = new DayPilot.Web.Ui.MenuItem();
                            itm2.Text = "Patient Details";
                            itm2.Command = "4000";
                            itm2.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu1.MenuItems.Add(itm2);

                            DayPilot.Web.Ui.MenuItem itm3 = new DayPilot.Web.Ui.MenuItem();
                            itm3.Command = "5000";
                            itm3.Text = "<span style='color:green'>sale made</span>";

                            itm3.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu1.MenuItems.Add(itm3);

                            e.ContextMenuClientName = DayPilotMenu1.ClientObjectName;

                        }


                        else
                        {

                            List<Result> res1 = Results.getResultsList();
                            DayPilotMenu_MenuNew.MenuItems.Clear();
                            foreach (Result r in res1)
                            {
                                DayPilot.Web.Ui.MenuItem itm = new DayPilot.Web.Ui.MenuItem();
                                itm.Text = r.ResultName;
                                itm.Command = r.ID.ToString();
                                itm.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                                DayPilotMenu_MenuNew.MenuItems.Add(itm);
                            }

                            DayPilot.Web.Ui.MenuItem itm11 = new DayPilot.Web.Ui.MenuItem();
                            itm11.Text = "Delete";
                            itm11.Command = "100";
                            itm11.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu_MenuNew.MenuItems.Add(itm11);


                            DayPilot.Web.Ui.MenuItem itm22 = new DayPilot.Web.Ui.MenuItem();
                            itm22.Text = "Patient Details";
                            itm22.Command = "4000";
                            itm22.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu_MenuNew.MenuItems.Add(itm22);

                            DayPilot.Web.Ui.MenuItem itm33 = new DayPilot.Web.Ui.MenuItem();
                            itm33.Text = "sale made";
                            itm33.Command = "5000";
                            itm33.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu_MenuNew.MenuItems.Add(itm33);
                            e.ContextMenuClientName = DayPilotMenu_MenuNew.ClientObjectName;

                        }
                    }

                    else
                    {
                        DayPilotMenu2.MenuItems.Clear();
                        DayPilot.Web.Ui.MenuItem itm222 = new DayPilot.Web.Ui.MenuItem();
                        itm222.Text = "Patient Details";
                        itm222.Command = "4000";
                        itm222.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu2.MenuItems.Add(itm222);
                        e.ContextMenuClientName = DayPilotMenu2.ClientObjectName;
                    }
                }
                else
                {
                    if (reader["SaleMade_yn"].ToString() == "1")
                    {

                        List<Result> res = Results.getResultsList();
                        DayPilotMenu1.MenuItems.Clear();
                        foreach (Result r in res)
                        {
                            DayPilot.Web.Ui.MenuItem itm = new DayPilot.Web.Ui.MenuItem();
                            itm.Text = r.ResultName;
                            itm.Command = r.ID.ToString();
                            itm.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu1.MenuItems.Add(itm);
                        }

                        DayPilot.Web.Ui.MenuItem itm1 = new DayPilot.Web.Ui.MenuItem();
                        itm1.Text = "Delete";
                        itm1.Command = "100";
                        itm1.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu1.MenuItems.Add(itm1);

                        DayPilot.Web.Ui.MenuItem itm2 = new DayPilot.Web.Ui.MenuItem();
                        itm2.Text = "Patient Details";
                        itm2.Command = "4000";
                        itm2.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu1.MenuItems.Add(itm2);

                        DayPilot.Web.Ui.MenuItem itm3 = new DayPilot.Web.Ui.MenuItem();
                        itm3.Command = "5000";
                        itm3.Text = "<span style='color:green'>sale made</span>";

                        itm3.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu1.MenuItems.Add(itm3);

                        e.ContextMenuClientName = DayPilotMenu1.ClientObjectName;

                    }


                    else
                    {

                        List<Result> res1 = Results.getResultsList();
                        DayPilotMenu_MenuNew.MenuItems.Clear();
                        foreach (Result r in res1)
                        {
                            DayPilot.Web.Ui.MenuItem itm = new DayPilot.Web.Ui.MenuItem();
                            itm.Text = r.ResultName;
                            itm.Command = r.ID.ToString();
                            itm.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                            DayPilotMenu_MenuNew.MenuItems.Add(itm);
                        }

                        DayPilot.Web.Ui.MenuItem itm11 = new DayPilot.Web.Ui.MenuItem();
                        itm11.Text = "Delete";
                        itm11.Command = "100";
                        itm11.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu_MenuNew.MenuItems.Add(itm11);


                        DayPilot.Web.Ui.MenuItem itm22 = new DayPilot.Web.Ui.MenuItem();
                        itm22.Text = "Patient Details";
                        itm22.Command = "4000";
                        itm22.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu_MenuNew.MenuItems.Add(itm22);

                        DayPilot.Web.Ui.MenuItem itm33 = new DayPilot.Web.Ui.MenuItem();
                        itm33.Text = "sale made";
                        itm33.Command = "5000";
                        itm33.Action = DayPilot.Web.Ui.MenuItemAction.PostBack;
                        DayPilotMenu_MenuNew.MenuItems.Add(itm33);
                        e.ContextMenuClientName = DayPilotMenu_MenuNew.ClientObjectName;

                    }
                }

            }

            con.Close();
        }
        //string lastTouched = Appointments.GetLastTouched(e.Tag["EventID"]);
        //e.InnerHTML += "<br />Last edited by " + lastTouched;
        //e.ToolTip += "\r\nLast editied by by " + lastTouched;
        if (e.InnerHTML.Contains("Location<br"))
        {
            e.InnerHTML = e.InnerHTML.Replace("Location<br/>", "<br/>Location ");
            e.InnerHTML = e.InnerHTML.Remove(e.InnerHTML.IndexOf("Status") - 6);
        }
    }
    private void MakeTicket(Appointment newAppt)
    {
        int PatientID = newAppt.patient.ID;
        if (PatientID == 7447 || PatientID == 7529
                         || PatientID == 7530
                         || PatientID == 7447
                         || PatientID == 7445) return;

        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        string ResultText = "None";
        try
        {
            ResultText = (from r in ctx.AppointmentResults
                          where r.ResultID == newAppt.Results
                          select r).First().ResultName;
        }
        catch
        {

        }

        string ProviderName = (from p in ctx.Providers
                               where p.id == newAppt.ProviderID
                               select p).First().ProviderName;

        string AppointmentTypeName = (from t in ctx.AppointmentTypes
                                      where t.ID == newAppt.ApptTypeID
                                      select t).First().TypeName;

        TimeSpan apptSpan = newAppt.ApptEnd - newAppt.ApptStart;
        string apptLength = "";
        if (apptSpan.Hours > 0)
            apptLength = apptSpan.Hours.ToString() + " hr";
        else
            apptLength = apptSpan.Minutes.ToString("F") + " min";
        //string FollowUp_Body = "<br/>Calendar followup for " + newAppt.patient.FirstName + " " + newAppt.patient.LastName + "<br/>Date and time: " + newAppt.ApptStart.ToShortDateString() + " " + newAppt.ApptStart.ToShortTimeString() + "<br/>Result was " + ResultText + "<br/>";
        //FollowUp_Body += "Provider: " + ProviderName + "<br/>";
        //FollowUp_Body += "Appointment Type: " + AppointmentTypeName + "<br/>";
        //int newID = TicketUtils.MakeTicket(154, FollowUp_Body, 7, newAppt.patient.ID, 2, "i", (int)Session["UserID"], "Calendar followup " + AppointmentTypeName + " w/" + ProviderName + " " + apptLength);
        apt_FollowUp theFollow = new apt_FollowUp();
        theFollow.DateEntered = DateTime.Now;
        theFollow.Entered_By = 154;
        string FollowUp_Body = "<br/>Calendar followup for " + newAppt.patient.FirstName + " " + newAppt.patient.LastName + "<br/>Date and time: " + newAppt.ApptStart.ToShortDateString() + " " + newAppt.ApptStart.ToShortTimeString() + "<br/>Result was " + ResultText + "<br/>";
        FollowUp_Body += "Provider: " + ProviderName + "<br/>";
        FollowUp_Body += "Appointment Type: " + AppointmentTypeName + "<br/>";
        theFollow.FollowUp_Cat = 7;

        //set FollowUp_Completed_YN as true instead of false by jaswinder as per the client recommendation 
        //to create closed ticket when generating ticket at time of cancelled.

        theFollow.FollowUp_Body = FollowUp_Body;
        theFollow.FollowUp_Completed_YN = false;

        theFollow.PatientID = newAppt.patient.ID;
        theFollow.Severity = 2;
        theFollow.Assigned = (int)Session["UserID"];
        theFollow.FollowUp_Subject = "Calendar followup " + AppointmentTypeName + " w/" + ProviderName + " " + apptLength;
        theFollow.DueDate = DateTime.Now;
        theFollow.LetterNote = "";
        theFollow.Letter = false;
        theFollow.FinalCall = false;
        theFollow.FinalCallNote = "";
        theFollow.FirstCall = false;
        theFollow.FirstCallNote = "";
        theFollow.SecondCall = false;
        theFollow.SeconCallNote = "";
        ctx.apt_FollowUps.InsertOnSubmit(theFollow);
        ctx.SubmitChanges();
        ctx.contact_tbl_Ticket_Insert(newAppt.patient.ID, "New Auto Ticket Entered.\r\n" + FollowUp_Body + "\r\nTicket " + theFollow.FollowUp_ID.ToString(), (int)Session["UserID"], theFollow.FollowUp_ID);
    }

    private void MakeTicket(Appointment newAppt, int FollowupType)
    {
        int PatientID = newAppt.patient.ID;
        if (PatientID == 7447
            || PatientID == 7529
            || PatientID == 7530
            || PatientID == 7445) return;

        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);


        string ProviderName = (from p in ctx.Providers
                               where p.id == newAppt.ProviderID
                               select p).First().ProviderName;



        apt_FollowUp theFollow = new apt_FollowUp();
        theFollow.DateEntered = DateTime.Now;
        theFollow.Apt_ID = newAppt.EventID;
        theFollow.Entered_By = 154;
        string FollowUp_Body = "Diabetes Call Back for " + newAppt.patient.FirstName + " " + newAppt.patient.LastName + "\r\nDate and time: " + newAppt.ApptStart.ToShortDateString() + " " + newAppt.ApptStart.ToShortTimeString();
        FollowUp_Body += "Provider: " + ProviderName + "<br/>";
        theFollow.FollowUp_Cat = FollowupType;
        theFollow.FollowUp_Completed_YN = false;
        theFollow.PatientID = newAppt.patient.ID;
        theFollow.Severity = 2;
        theFollow.DepartmentAssign = 29;
        theFollow.FollowUp_Subject = "Diabetes Call Back followup for " + newAppt.patient.FirstName + " " + newAppt.patient.LastName;
        theFollow.DueDate = DateTime.Now.AddDays(42);
        ctx.apt_FollowUps.InsertOnSubmit(theFollow);
        ctx.SubmitChanges();
        ctx.contact_tbl_Ticket_Insert(newAppt.patient.ID, "New Auto Ticket Entered.\r\n" + FollowUp_Body + "\r\nTicket " + theFollow.FollowUp_ID.ToString(), (int)Session["UserID"], theFollow.FollowUp_ID);
    }

    protected void DayPilotCalendar1_EventMenuClick(object sender, EventMenuClickEventArgs e)
    {
        Appointment oldAppt = ProviderCal.GetApptByID(e.Tag[1]);
        if (int.Parse(e.Command) < 200)
        {

            Appointment newAppt = ProviderCal.GetApptByID(e.Tag[1]);
            newAppt.Results = int.Parse(e.Command);
            List<Result> res = Results.getResultsList();
            bool IsActionRequired=false;
            foreach (Result r in res)
            {
                if (r.ID == newAppt.Results)
                {
                    IsActionRequired = r.IsActionRequired;
                }
            }
            //if (newAppt.Results != 3 && newAppt.Results != 6 && newAppt.Results != 8 && newAppt.Results != 9 && newAppt.Results > 0 && newAppt.Results != 100)
            if (IsActionRequired == true && newAppt.Results > 0 && newAppt.Results != 100)
            {
                MakeTicket(newAppt);
                newAppt.ActionNeeded = "Yes";
            }
            if (newAppt.Results == 3 && newAppt.ApptTypeID == 6)
            {
                CreateFollowup(newAppt.PatientID, newAppt.ApptStart);
            }

            if (newAppt.Results == 3)
            {
                if (!string.IsNullOrEmpty(newAppt.Email) && !string.IsNullOrEmpty(newAppt.WufooFormKey))
                {
                    Utilities.SendSurveyMessage(newAppt.Email, newAppt.patient.FirstName + " " + newAppt.patient.LastName, "Longevity Patient Survey", newAppt.PatientID, Convert.ToInt32(e.Tag[1].ToString()), newAppt.WufooFormKey, newAppt.ApptTypeID);

                }
                else if (string.IsNullOrEmpty(newAppt.Email) && !string.IsNullOrEmpty(newAppt.patient.Email) && !string.IsNullOrEmpty(newAppt.WufooFormKey))
                {
                    Utilities.SendSurveyMessage(newAppt.patient.Email, newAppt.patient.FirstName + " " + newAppt.patient.LastName, "Longevity Patient Survey", newAppt.PatientID, Convert.ToInt32(e.Tag[1].ToString()), newAppt.WufooFormKey, newAppt.ApptTypeID);
                }
            }

            if (newAppt.Results == 100)
            {
                newAppt.ActionNeeded = "Delete";
                ctx.contact_tbl_EMR_Insert(57, newAppt.PatientID, "Appointment deleted. Date/Time" + newAppt.ApptStart + ". Deleted by " + (from s in ctx.Staffs where s.EmployeeID == (int)Session["StaffID"] select s.EmployeeName).First() + ".", (int)Session["StaffID"], newAppt.EventID);

            }
            if (newAppt.Results == 100 || (newAppt.Results != 3 && newAppt.Results != 0))
            {
                var fups = from f in ctx.apt_FollowUps
                           where f.Apt_ID == newAppt.EventID
                           select f;

                foreach (var fup in fups)
                {
                    Appointments.ToggleFollowup(newAppt.EventID, fup.FollowUp_ID.ToString(), true);
                }
            }
            Appointments.dbUpdateEvent(newAppt, int.Parse(Session["UserID"].ToString()));
            Appointments.LogChange(57, newAppt.PatientID, (int)Session["UserID"], oldAppt, newAppt, (string)Session["EmailFromAddress"], (string)Session["EmailFromName"], e.Tag[1]);
            if (newAppt.ActionNeeded != "No")
            {
                DayPilotCalendar1.DataBind();
                BindData();
                DayPilotCalendar1.Update();
            }
        }
        if (e.Command == "300")
        {

        }
        if (e.Command == "1000")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update apt_rec set SaleMade_yn = 1 where apt_id=" + e.Value, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Appointment newAppt = ProviderCal.GetApptByID(e.Tag[1]);
                CRM_Prospect pros = (from p in ctxCRM.CRM_Prospects
                                     where p.PatientID == newAppt.PatientID
                                     select p).FirstOrDefault();
                CRM_Log log = new CRM_Log();
                log.DateEntered = DateTime.Now;
                log.EnteredBy = (int)Session["StaffID"];
                log.OldStatus = pros.StatusID;
                log.NewStatus = 8;
                log.ProspectID = pros.ProspectID;
                ctxCRM.CRM_Logs.InsertOnSubmit(log);
                pros.StatusID = 8;
                ctxCRM.SubmitChanges();
            }
        }

        if (e.Command == "2000")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update apt_rec set SaleMade_yn = 0 where apt_id=" + e.Value, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Appointment newAppt = ProviderCal.GetApptByID(e.Tag[1]);
                CRM_Prospect pros = (from p in ctxCRM.CRM_Prospects
                                     where p.PatientID == newAppt.PatientID
                                     select p).FirstOrDefault();
                CRM_Log log = new CRM_Log();
                log.DateEntered = DateTime.Now;
                log.EnteredBy = (int)Session["StaffID"];
                log.OldStatus = pros.StatusID;
                log.NewStatus = 10;
                log.ProspectID = pros.ProspectID;
                ctxCRM.CRM_Logs.InsertOnSubmit(log);
                pros.StatusID = 10;
                ctxCRM.SubmitChanges();
            }
        }
        if (e.Command == "3000")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update apt_rec set SaleMade_yn = 2 where apt_id=" + e.Value, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        if (e.Command == "4000")
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                SqlCommand cmd1 = new SqlCommand("Appointment_GetByID", con);
                cmd1.Parameters.AddWithValue("@EventID", e.Tag["EventID"]);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                reader.Read();
                int PatientID = (int)reader["Patient_ID"];
                Response.Redirect("../manage.aspx?PatientID=" + PatientID.ToString());
            }
        }

        // Added By Rakesh for Sale Made Functionality
        if (e.Command == "5000")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {


                con.Open();
                if (oldAppt.SaleMadeYn == 1)
                {
                    SqlCommand cmd = new SqlCommand("update apt_rec set SaleMade_yn = 0 where apt_id=" + e.Value, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                else if (oldAppt.SaleMadeYn == -1 || oldAppt.SaleMadeYn == 2 || oldAppt.SaleMadeYn == 0)
                {
                    SqlCommand cmd = new SqlCommand("update apt_rec set SaleMade_yn = 1 where apt_id=" + e.Value, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }


            }
        }
    }

    private void CreateFollowup(int PatientID, DateTime ApptStart)
    {
        apt_FollowUp fup = new apt_FollowUp();
        fup.Apt_ID = 60;
        fup.AptAssigned = null;
        fup.Assigned = null;
        fup.DateEntered = DateTime.Now;
        fup.DepartmentAssign = null;
        fup.DueDate = null;
        fup.Entered_By = (int)Session["StaffID"];
        fup.FollowUp_Assigned_YN = false;
        fup.FollowUp_Cat = 1;
        fup.FollowUp_Completed_YN = false;
        fup.FollowUp_Subject = "Schedule Futuure Appoinments";
        fup.PatientID = PatientID;
        fup.FinalCall = false;
        fup.FinalCallNote = "";
        fup.FirstCall = false;
        fup.FirstCallNote = "";
        fup.SeconCallNote = "";
        fup.SecondCall = false;
        fup.Letter = false;
        fup.LetterNote = "";
        fup.FollowUp_Body = BuildBody(ApptStart);
        ctx.apt_FollowUps.InsertOnSubmit(fup);
        ctx.SubmitChanges();
    }

    private string BuildBody(DateTime ApptStart)
    {
        string body = "<p class='PageTitle'>Appointments to be scheduled:</p>";
        //2 weeks later MA Call
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(14).ToShortDateString() + "' target='CalWin'> 2 week MA follow up call - " + ApptStart.AddDays(14).ToShortDateString() + "</a></p>";
        //4 weeks later MD Call
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(28).ToShortDateString() + "' target='CalWin'> 4 week Provider follow up call - " + ApptStart.AddDays(28).ToShortDateString() + "</a></p>";
        //6 weeks later Blood Draw
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(42).ToShortDateString() + "' target='CalWin'> 6 week blood draw - " + ApptStart.AddDays(42).ToShortDateString() + "</a></p>";
        //8 weeks later Lab Review
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(56).ToShortDateString() + "' target='CalWin'> 8 week lab review - " + ApptStart.AddDays(56).ToShortDateString() + "</a></p>";
        //14 weeks later Blood Draw
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(98).ToShortDateString() + "' target='CalWin'> 14 week blood draw - " + ApptStart.AddDays(98).ToShortDateString() + "</a></p>";
        //16 weeks later Lab Review
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(112).ToShortDateString() + "' target='CalWin'> 16 week lab review - " + ApptStart.AddDays(112).ToShortDateString() + "<</a></p>";
        //22 weeks later Blood Draw
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(154).ToShortDateString() + "' target='CalWin'> 22 week blood draw - " + ApptStart.AddDays(154).ToShortDateString() + "</a></p>";
        //24 weeks later Lab Review
        body += "<p><a href='calendar/CalendarAppointment.aspx?StartDate=" + ApptStart.AddDays(168).ToShortDateString() + "' target='CalWin'> 24 wweek lab review - " + ApptStart.AddDays(168).ToShortDateString() + "</a></p>";

        return body;

    }


    protected void DayPilotCalendar1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
        FormView EventDetail = (FormView)FindControlRecursive(Page.Master, "EventDetail");//(FormView)Page.Master.FindControl("EventDetail");
        ObjectDataSource ObjectDataSourceDetail = (ObjectDataSource)FindControlRecursive(Page.Master, "ObjectDataSourceDetail");
        UpdatePanel UpdatePanelDetail = (UpdatePanel)FindControlRecursive(Page.Master, "UpdatePanelDetail");
        ModalPopupExtender ModalPopup = (ModalPopupExtender)FindControlRecursive(Page.Master, "ModalPopup");
        ViewState["EventID"] = "0";
        ObjectDataSourceDetail.SelectParameters["id"].DefaultValue = "0";
        EventDetail.DataSourceID = "ObjectDataSourceDetail";
        EventDetail.DataBind();
        ((Label)EventDetail.FindControl("lblFollow")).Text = "Open follow ups";

        TextBox startTextBox = (TextBox)EventDetail.FindControl("txtStartDate");//.Rows[1].Cells[1].Controls[0];

        startTextBox.Text = e.Start.ToString("d");
        Controls_TimeDropDown startTextBoxTime = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");
        startTextBoxTime.StartTime = e.Start;
        TextBox endTextBox = (TextBox)EventDetail.FindControl("txtEndDate");//.Rows[2].Cells[1].Controls[0];
        endTextBox.Text = e.End.ToString("d");
        Controls_TimeDropDown endTextBoxTime = (Controls_TimeDropDown)EventDetail.FindControl("txtEndTime");
        endTextBoxTime.StartTime = e.End;
        TextBox txtName = (TextBox)EventDetail.FindControl("txtPatient");
        txtName.ReadOnly = false;
        txtName.ForeColor = endTextBox.ForeColor;
        txtName.BorderWidth = endTextBox.BorderWidth;
        txtName.Text = "";
        ObjectDataSource AppointmentTypeSourceOnly = (ObjectDataSource)EventDetail.FindControl("AppointmentTypeSourceOnly");
        DropDownList ddProv = (DropDownList)EventDetail.FindControl("ProviderDropDown");
        DropDownList ApptType = (DropDownList)EventDetail.FindControl("ApptTypeDropDown");
        ApptType.DataSource = AppointmentTypes.getApptTypeListOnly(lblProvider1.Text);
        ApptType.DataBind();

        AppointmentTypeSourceOnly.SelectParameters[0].DefaultValue = ProviderID;

        Session["Provider"] = lblProvider1.Text;

        UpdatePanelDetail.Update();
        ModalPopup.Show();

    }

    protected void DayPilotCalendar1_EventMove(object sender, EventMoveEventArgs e)
    {





        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            con.Open();
            string MessageBody = "Appointment moved from " + e.OldStart.ToString() + " to " + e.NewStart.ToString() + ".";
            SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
            SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
            rd.Read();
            string StaffName = (string)rd["EmployeeName"];
            string accessLevel = (string)rd["access_level"];

            rd.Close();
            MessageBody += "Changed by " + StaffName;
            cmd1 = new SqlCommand("Appointment_GetByID", con);
            cmd1.Parameters.AddWithValue("@EventID", e.Tag["EventID"]);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
            reader.Read();
            int AptID = (int)reader["apt_id"];
            int PatientID = (int)reader["Patient_ID"];
            int AptType = (int)reader["AppointmentTypeID"];
            string category = (string)reader["category"];

            reader.Close();
            if (category != "Locked")
            {
                cmd1 = new SqlCommand("contact_tbl_Cal_Insert", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@AptType", 57));
                cmd1.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
                cmd1.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));
                cmd1.Parameters.Add(new SqlParameter("@Apt_ID", AptID));
                cmd1.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("UPDATE [apt_rec] SET [ApptStart] = @start, [ApptEnd] = @end WHERE [apt_id] = @id", con);

                cmd.Parameters.AddWithValue("id", e.Value);
                cmd.Parameters.AddWithValue("start", e.NewStart);
                cmd.Parameters.AddWithValue("end", e.NewEnd);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
            {
                if (accessLevel == "super" || accessLevel == "emr_admin")
                {
                    cmd1 = new SqlCommand("contact_tbl_Cal_Insert", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@AptType", 57));
                    cmd1.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                    cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
                    cmd1.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));
                    cmd1.Parameters.Add(new SqlParameter("@Apt_ID", AptID));
                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("UPDATE [apt_rec] SET [ApptStart] = @start, [ApptEnd] = @end WHERE [apt_id] = @id", con);

                    cmd.Parameters.AddWithValue("id", e.Value);
                    cmd.Parameters.AddWithValue("start", e.NewStart);
                    cmd.Parameters.AddWithValue("end", e.NewEnd);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        BindData();
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.Update();
    }

    protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
    {

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            con.Open();
            string MessageBody = "Appointment changed.  Old Start and end: " + e.OldStart.ToString() + "  " + e.OldEnd.ToString() + " New start and end: " + e.NewStart.ToString() + "  " + e.NewEnd.ToString();
            SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
            SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
            rd.Read();
            string StaffName = (string)rd["EmployeeName"];
            string accessLevel = (string)rd["access_level"];

            rd.Close();
            MessageBody += "Changed by " + StaffName;
            cmd1 = new SqlCommand("Appointment_GetByID", con);
            cmd1.Parameters.AddWithValue("@EventID", e.Tag["EventID"]);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
            reader.Read();
            int AptID = (int)reader["apt_id"];
            int PatientID = (int)reader["Patient_ID"];
            int AptType = (int)reader["AppointmentTypeID"];
            string category = (string)reader["category"];

            reader.Close();
            if (category != "Locked")
            {

                cmd1 = new SqlCommand("contact_tbl_Cal_Insert", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@AptType", 57));
                cmd1.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
                cmd1.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));
                cmd1.Parameters.Add(new SqlParameter("@Apt_ID", AptID));
                cmd1.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("UPDATE [apt_rec] SET [ApptStart] = @start, [ApptEnd] = @end WHERE [apt_id] = @id", con);
                cmd.Parameters.AddWithValue("id", e.Value);
                cmd.Parameters.AddWithValue("start", e.NewStart);
                cmd.Parameters.AddWithValue("end", e.NewEnd);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
            {
                if (accessLevel == "super" || accessLevel == "emr_admin")
                {

                    cmd1 = new SqlCommand("contact_tbl_Cal_Insert", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@AptType", 57));
                    cmd1.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                    cmd1.Parameters.Add(new SqlParameter("@EmployeeID", (int)Session["UserID"]));
                    cmd1.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));
                    cmd1.Parameters.Add(new SqlParameter("@Apt_ID", AptID));
                    cmd1.ExecuteNonQuery();


                    SqlCommand cmd = new SqlCommand("UPDATE [apt_rec] SET [ApptStart] = @start, [ApptEnd] = @end WHERE [apt_id] = @id", con);
                    cmd.Parameters.AddWithValue("id", e.Value);
                    cmd.Parameters.AddWithValue("start", e.NewStart);
                    cmd.Parameters.AddWithValue("end", e.NewEnd);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        BindData();
        DayPilotCalendar1.Update();
    }

    /// <summary>
    /// Finds a Control recursively. Note finds the first match and exists
    /// </summary>
    /// <param name="ContainerCtl"></param>
    /// <param name="IdToFind"></param>
    /// <returns></returns>

    public static Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id)
            return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null)
                return FoundCtl;
        }

        return null;
    }


    public void BindData()
    {
        DropDownList ApptTypeDropDown = (DropDownList)FindControlRecursive(Page.Master, "ApptTypeDropDown");
        DropDownList StatusDropDown = (DropDownList)FindControlRecursive(Page.Master, "StatusDropDown");
        string ApptType = ApptTypeDropDown.SelectedValue == "" ? "0" : ApptTypeDropDown.SelectedValue;
        string Status = StatusDropDown.SelectedValue == "" ? "0" : StatusDropDown.SelectedValue;
        List<Appointment> data = ProviderCal.GetCal(lblProvider1.Text, ApptType, Status, DayPilotCalendar1.StartDate, DayPilotCalendar1.Days);
        DayPilotCalendar1.DataSource = data;
        DayPilotCalendar1.DataBind();
    }
}


