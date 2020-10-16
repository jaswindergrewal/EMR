using AjaxControlToolkit;
using Calendar;
using DayPilot.Web.Ui;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Database_Default : LMCBase
{
    #region "Variable"
    ICalendarService objService = null;
    string strAlert = string.Empty;
    #endregion

    #region "Events"

    /// <summary>
    /// To show the appointments for the selected provider.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime startDate = DateTime.Today;
        Session["LockedAppointment"] = "true";
        if (Request.QueryString["StartDate"] != null)
        {
            startDate = DateTime.Parse(Request.QueryString["StartDate"]);
        }
        foreach (ListItem lst in ProvidersCBox.Items)
        {
            lst.Attributes.Add("onclick", "onProviderChecked(this);");
        }


        Response.Cache.SetNoStore();

        User user = null;
        if (Session["UserID"] != null)
            user = new User(((int)Session["UserID"]).ToString());
        if (!IsCallback)
        {
            

            if (user != null && user.Recurring)
                ButtonRecur.Visible = true;
            else
                ButtonRecur.Visible = false;
        }

        if (user.AccessLevel == "super" || user.AccessLevel == "emr_admin")
        {
            Session["LockedAppointment"] = "false";
        }
        Settings settings = new Settings();

        DayPilotNavigator1.StartDate = startDate;
        Session["AptType"] = settings.ContactAptType;
        Session["EmailFromName"] = settings.EmailFromName;
        Session["EmailFromAddress"] = settings.EmailFromAddress;
        if (!IsPostBack)
        {
            int proSelected = 0;
            List<Calendar.Provider> provList = Providers.getProviderList();
            foreach (Calendar.Provider prov in provList)
            {
                ListItem it = new ListItem(prov.ProviderName, prov.id.ToString());
                it.Attributes.Add("onclick", "onProviderChecked(this);");
                ProvidersCBox.Items.Add(it);

                //show the logged in provider
                //jan 17 2014
                if (prov.EmployeeID.ToString() == Session["UserID"].ToString())
                {
                    it.Selected = true;
                    proSelected = 1;
                }

            }


            if (Request.Cookies["Calendar"] != null)
            {
                // need to show all the providers that are stored in cookies

                if (Request.Cookies["Calendar"]["cals"] != null)
                {
                    string[] provArray = Request.Cookies["Calendar"]["cals"].Split(' ');
                    foreach (string provID in provArray)
                    {
                        foreach (ListItem lst in ProvidersCBox.Items)
                        {
                            if (lst.Value == provID)
                            {
                                lst.Selected = true;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    //logged in staff is not provider than show the first one as selected

                    if (proSelected == 0)
                    {
                        foreach (ListItem lstProvider in ProvidersCBox.Items)
                        {
                            lstProvider.Selected = true;
                            break;
                        }

                    }
                }
                if (Request.Cookies["Calendar"]["cell"] != null)
                {
                    switch (Request.Cookies["Calendar"]["cell"])
                    {
                        case "30":
                            RadioButton1.Checked = true;
                            break;
                        case "15":
                            RadioButton2.Checked = true;
                            break;
                        case "10":
                            RadioButton3.Checked = true;
                            break;
                        case "5":
                            RadioButton4.Checked = true;
                            break;
                        default:
                            RadioButton1.Checked = true;
                            break;
                    }
                }
                if (Request.Cookies["Calendar"]["days"] != null)
                {
                    if (Request.Cookies["Calendar"]["days"] == "7")
                    {
                        radio7Days.Checked = true;
                        Session["Days"] = "7";
                    }
                    else
                    {
                        radio1Day.Checked = true;
                        Session["Days"] = "1";
                    }

                }


            }
            else
            {
                RadioButton1.Checked = true;
                Session["Days"] = 7;
                radio7Days.Checked = true;

            }

           


        }
        foreach (ListItem lst in ProvidersCBox.Items)
        {
            if (lst.Selected)
            {
                AddCalendar(lst.Value);
            }
        }


        radio1Day.AutoPostBack = true;
        radio7Days.AutoPostBack = true;
        RadioButton1.AutoPostBack = true;
        RadioButton2.AutoPostBack = true;
        RadioButton3.AutoPostBack = true;
        RadioButton4.AutoPostBack = true;


        foreach (Control theCell in CalRow.Controls)
        {
            foreach (Control theCal in theCell.Controls)
            {
                DayPilotCalendar DayPilotCalendar1 = (DayPilotCalendar)theCal.FindControl("DayPilotCalendar1");
                if (DayPilotCalendar1 != null)
                {
                    if (Session["Days"] != null)
                    {
                        DayPilotCalendar1.Days = int.Parse(Session["Days"].ToString()); ;
                    }
                    else
                    {
                        Session["Days"] = "7";
                        DayPilotCalendar1.Days = 1;
                    }

                    if (RadioButton1.Checked)
                        DayPilotCalendar1.CellDuration = 30;
                    if (RadioButton2.Checked)
                        DayPilotCalendar1.CellDuration = 15;
                    if (RadioButton3.Checked)
                        DayPilotCalendar1.CellDuration = 10;
                    if (RadioButton4.Checked)
                        DayPilotCalendar1.CellDuration = 5;
                    Session["Interval"] = DayPilotCalendar1.CellDuration;

                    if (radio1Day.Checked)
                    {
                        DayPilotCalendar1.Days = 1;
                        if (Session["SelDate"] == null)
                            DayPilotCalendar1.StartDate = startDate;
                        else
                            DayPilotCalendar1.StartDate = (DateTime)Session["SelDate"];


                    }

                    if (radio7Days.Checked)
                    {
                        DayPilotCalendar1.Days = 7;
                        if (Session["SelDate"] == null)
                            DayPilotCalendar1.StartDate = startDate;
                        else
                            DayPilotCalendar1.StartDate = DayPilot.Utils.Week.FirstDayOfWeek((DateTime)Session["SelDate"]);


                    }
                    DayPilotCalendar1.ScrollPositionHour = 9;
                    DayPilotCalendar1.HeaderDateFormat = DateTime.Today.ToShortDateString();
                    OneCal cal = (OneCal)DayPilotCalendar1.Parent.Parent.Parent;
                    SetCalLabel(cal.ProviderID, DayPilotNavigator1.SelectionStart, cal);
                    cal.BindData();
                    UpdatePanelCalendar.Update();
                }
            }
        }
        if (radio1Day.Checked)
        {
            DayPilotNavigator1.SelectMode = DayPilot.Web.Ui.Enums.NavigatorSelectMode.Day;

            if (Session["SelDate"] == null)
                DayPilotNavigator1.SelectionStart = DateTime.Today;

            else
                DayPilotNavigator1.SelectionStart = (DateTime)Session["SelDate"];

            Session["Days"] = "1";
        }
        if (radio7Days.Checked)
        {

            if (Session["SelDate"] == null)
                DayPilotNavigator1.SelectionStart = DateTime.Today;
            else
                DayPilotNavigator1.SelectionStart = (DateTime)Session["SelDate"];

            DayPilotNavigator1.SelectMode = DayPilot.Web.Ui.Enums.NavigatorSelectMode.Week;
            Session["Days"] = "7";
        }
        if (Session["SelDate"] != null)
            DayPilotNavigator1.StartDate = ((DateTime)Session["SelDate"]).AddMonths(-1);
        HttpCookie cookie = new HttpCookie("calendar");
        //Code to store all the providers  in cookies

        foreach (ListItem it in ProvidersCBox.Items)
        {
            if (it.Selected)
                cookie["cals"] += it.Value + " ";
        }
        if (RadioButton1.Checked)
            cookie["cell"] = "30";
        if (RadioButton2.Checked)
            cookie["cell"] = "15";
        if (RadioButton3.Checked)
            cookie["cell"] = "10";
        if (RadioButton4.Checked)
            cookie["cell"] = "5";

        if (Session["Days"] != null)
            cookie["days"] = Session["Days"].ToString();
        else
            cookie["days"] = "1";

        cookie.Expires = DateTime.Today.AddDays(365);
        Response.Cookies.Add(cookie);
        


        //Page.ClientScript.RegisterStartupScript(this.GetType(), "somescript", "<script type=text/javascript> $('[id$=loadingdivbackground]').hide(); </script>");
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "somescript", "CloseDiv();", true);
        ////loadingdivbackground.Visible = false;
    }

    private static string AddOrUpdateListMember(string dataCenter, string apiKey, string listId, string subscriberEmail, string FirstName, string LastName)
    {
        var sampleListMember = new JavaScriptSerializer().Serialize(
            new
            {
                email_address = subscriberEmail,
                merge_fields =
                new
                {
                    FNAME = FirstName,
                    LNAME = LastName
                },
                status_if_new = "subscribed"
            });

        var hashedEmailAddress = string.IsNullOrEmpty(subscriberEmail) ? "" : CalculateMD5Hash(subscriberEmail.ToLower());
        var uri = string.Format("https://{0}.api.mailchimp.com/3.0/lists/{1}/members/{2}", dataCenter, listId, hashedEmailAddress);
        try
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Accept", "application/json");
                webClient.Headers.Add("Authorization", "apikey " + apiKey);

                return webClient.UploadString(uri, "PUT", sampleListMember);
            }
        }
        catch (WebException we)
        {
            using (var sr = new StreamReader(we.Response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
    private static string CalculateMD5Hash(string input)
    {
        // Step 1, calculate MD5 hash from input.
        var md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string.
        var sb = new StringBuilder();
        foreach (var @byte in hash)
        {
            sb.Append(@byte.ToString("X2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Bind the Appointment details when we click on calaendar field.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void EventDetail_DataBound(object sender, EventArgs e)
    {
        TextBox EventIDText = (TextBox)EventDetail.FindControl("txtEventID");
        EventDetail.Visible = true;
        DropDownList ddlClinic = (DropDownList)EventDetail.FindControl("ddlClinic");
        PatientService objPatientService = null;

        objPatientService = new PatientService();
        ddlClinic.DataSource = objPatientService.GetClinics();
        ddlClinic.DataTextField = "ClinicName";
        ddlClinic.DataValueField = "ClinicName";
        ddlClinic.DataBind();
        ddlClinic.Items.Insert(0, new ListItem("Select a clinic"));

        IStaffService objStaffService = new StaffService();
        List<StaffViewModel> HARep = objStaffService.GetStaff().Where(o => o.IsHARep == true).ToList();
        DropDownList drpHARep = (DropDownList)EventDetail.FindControl("drpHARep");
        drpHARep.DataSource = HARep;
        drpHARep.DataTextField = "EmployeeName";
        drpHARep.DataValueField = "EmployeeID";
        drpHARep.DataBind();
        drpHARep.Items.Insert(0, new ListItem("Select HA Rep","0"));

        if (EventIDText.Text != "0")
        {
            Appointment appt = ProviderCal.GetApptByID(EventIDText.Text);
            Controls_TimeDropDown txtStart = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");

            DropDownList ApptType = (DropDownList)EventDetail.FindControl("ApptTypeDropDown");
            DropDownList ProviderDD = (DropDownList)EventDetail.FindControl("ProviderDropDown");
            DropDownList Results = (DropDownList)EventDetail.FindControl("ResultsDropDown");
            DropDownList Status = (DropDownList)EventDetail.FindControl("StatusDropDown");
            CheckBoxList FollowUps = (CheckBoxList)EventDetail.FindControl("ddFollowups");
            TextBox EventIDTxt = (TextBox)EventDetail.FindControl("txtEventID");
            Button btnEmail = (Button)EventDetail.FindControl("btnEmail");
            TextBox pt = (TextBox)EventDetail.FindControl("txtPatient");
            TextBox PatientID = (TextBox)EventDetail.FindControl("txtPatientID");
            Calendar.Patient patient = new Calendar.Patient(appt.PatientID);
            FollowU fups = new FollowU(patient.ID);
            Label lblNoFollowups = (Label)EventDetail.FindControl("lblNoFollowups");
            Label lblInactive = (Label)EventDetail.FindControl("lblInactive");
            //CheckBox cbLockedAppointment = (CheckBox)EventDetail.FindControl("cbLockedAppointment");
            CheckBox cbAllDay = (CheckBox)EventDetail.FindControl("cbAllDay");

            string followpIDS = "";

            if (patient.Inactive)
            {
                lblInactive.Visible = true;
                PatientID.Text = patient.ID.ToString();
            }
            else
                lblInactive.Visible = false;

            
            List<FollowUp> apptFollow = new List<FollowUp>();
            foreach (FollowUp fup in fups.Fups)
            {
                if ((fup.AptAssigned == int.Parse(EventIDText.Text) || fup.Complete == "No") && fup.FollowUp_Type_Desc != "AS/Order Adjustment")
                {
                    apptFollow.Add(fup);
                }
            }
            if (apptFollow.Count > 0)
            {

                FollowUps.DataSource = apptFollow;
                FollowUps.DataTextField = "Label";
                FollowUps.DataValueField = "FollowUpID";
                FollowUps.DataBind();
                foreach (ListItem lst in FollowUps.Items)
                {
                    var thisItem = from f in fups.Fups
                                   where f.FollowUpID == int.Parse(lst.Value)
                                   select new
                                   {
                                       element = f,
                                   };
                    if (thisItem.First().element.AptAssigned == int.Parse(EventIDText.Text))
                    {
                        lst.Selected = true;
                        followpIDS += lst.Value + ",";
                    }
                }
                if (followpIDS != "")
                    followpIDS = followpIDS.Substring(0, followpIDS.Length - 1);
                lblNoFollowups.Visible = false;
                FollowUps.Visible = true;
                Button btnFollowup = (Button)EventDetail.FindControl("btnFollowup");
                btnFollowup.Visible = true;
                if (followpIDS == "") btnFollowup.Enabled = false;
                btnFollowup.Attributes.Add("onclick", "window.open('PrintFollowup.aspx?FollowUpID=" + followpIDS + "');");
                ((Label)EventDetail.FindControl("lblFollow")).Text = "Open follow ups";
            }
            else
            {

                lblNoFollowups.Visible = true;
                FollowUps.Visible = false;
                ((Button)EventDetail.FindControl("btnFollowup")).Visible = false;
                ((Label)EventDetail.FindControl("lblFollow")).Text = "Open follow ups";

                FollowUps.Enabled = false;
            }

            DataTable apptSource = AppointmentTypes.getApptTypeListOnly(appt.ProviderID.ToString());

            Calendar.AppointmentType thisType = Calendar.AppointmentTypes.GetApptType(appt.ApptTypeID);


            if (thisType.ConfirmationEmail)
            {
                btnEmail.Visible = true;
            }
            else
            {
                btnEmail.Visible = false;
            }

            ApptType.DataSource = apptSource;
            ApptType.DataBind();
            ApptType.SelectedValue = appt.ApptTypeID.ToString();
            ProviderDD.SelectedValue = appt.ProviderID.ToString();
            Results.SelectedValue = appt.Results.ToString();
            Status.SelectedValue = appt.StatusID.ToString();
            //cbLockedAppointment.Checked = appt.LockedAppointment;
            cbAllDay.Checked = appt.AllDay;
            
            //if (!string.IsNullOrEmpty(appt.clinic))
            //{
                ddlClinic.SelectedValue = appt.clinic;
            //}
            //else
            //{
            //    ddlClinic.SelectedValue = patient.Clinic;
            //}
            
            drpHARep.SelectedValue = (appt.HARep).ToString();
            if (!Patients.CheckReassign(appt.PatientID))
            {
                pt.ReadOnly = true;
                pt.BorderWidth = 0;
                pt.BackColor = System.Drawing.Color.FromArgb(0xE7D5B4);
                pt.ForeColor = System.Drawing.Color.Black;
                pt.CausesValidation = false;
                AjaxControlToolkit.AutoCompleteExtender ex = (AjaxControlToolkit.AutoCompleteExtender)EventDetail.FindControl("AutoCompleteExtender1");
                ex.Enabled = false;
                Controls_TimeDropDown start1 = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");
                start1.Focus();
            }
            else
            {
                pt.Focus();
                lblInactive.Visible = false;
            }

            Session["Provider"] = appt.ProviderID.ToString();
            ViewState.Add("oldAppt", appt);
        }
        else
        {
            TextBox pt = (TextBox)EventDetail.FindControl("txtPatient");

            Label lblInactive = (Label)EventDetail.FindControl("lblInactive");
            DropDownList ddStatus = (DropDownList)EventDetail.FindControl("StatusDropDown");
            ddStatus.SelectedValue = "8";
            Button Insert = (Button)EventDetail.FindControl("btnUpdate");
            Insert.Text = "Insert";
            //Button PatientLookup = (Button)EventDetail.FindControl("PatientLookup");

            pt.Focus();
            lblInactive.Visible = false;
            ViewState.Remove("oldAppt");
        }


        TextBox start = (TextBox)EventDetail.FindControl("txtStartDate");
        start.Attributes.Add("readonly", "readonly");
        DateTime tt = DateTime.Parse(start.Text);
        start.Text = tt.ToString("d");

        Controls_TimeDropDown startTime = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");
        startTime.StartTime = tt;
        TextBox end = (TextBox)EventDetail.FindControl("txtEndDate");
        end.Attributes.Add("readonly", "readonly");
        tt = DateTime.Parse(end.Text);
        end.Text = tt.ToString("d");
        Controls_TimeDropDown endTime = (Controls_TimeDropDown)EventDetail.FindControl("txtEndTime");
        endTime.StartTime = tt;

    }

    /// <summary>
    /// calling PopulateFollowups and PopulateDetails methos on click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PatientLookup_Click(object sender, EventArgs e)
    {
        //TextBox patientName = (TextBox)EventDetail.FindControl("txtPatient");
        PopulateDetails((Calendar.Patient)Session["Patient"]);
        PopulateFollowups(Convert.ToInt32(Session["Patient"]));
    }

    /// <summary>
    /// set the provider value in provider dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EventDetail_PreRender(object sender, EventArgs e)
    {

        DropDownList ProviderDD = (DropDownList)EventDetail.FindControl("ProviderDropDown");
        try
        {
            ProviderDD.SelectedValue = Session["Provider"].ToString();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        TextBox patientName = (TextBox)EventDetail.FindControl("txtPatient");

        //Added by jaswinder a optional parameter to check if we update the appointment and if patient  is inactive
        Label lblInactive = (Label)EventDetail.FindControl("lblInactive");
        Calendar.Patient pat;
        if (lblInactive.Visible == true)
        {
            pat = Calendar.Patients.CheckPatient(patientName.Text, 1);
        }
        else
        {
            pat = Calendar.Patients.CheckPatient(patientName.Text);
        }


        if (pat.LastName != "")
        {
            PopulateDetails(pat);
            PopulateFollowups(pat.ID);
        }
        Session["Updating"] = "true";
    }

    /// <summary>
    /// On cancel button click close the model popup and show the calendar screen again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EventDetail_ItemCommand(object sender, System.Web.UI.WebControls.FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            TextBox txtPatName = (TextBox)EventDetail.FindControl("txtPatient");
            txtPatName.CausesValidation = false;
            //txtPatName.CausesValidation = false;
            TextBox tEmail = (TextBox)EventDetail.FindControl("txtEmail");
            tEmail.CausesValidation = false;

            ModalPopup.Hide();
            EventDetail.Visible = false;
        }
    }

    /// <summary>
    /// Update the appointment details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EventDetail_ItemUpdating(object sender, EventArgs e)
    {
        try
        {
            DropDownList ApptType = (DropDownList)EventDetail.FindControl("ApptTypeDropDown");
            DropDownList ProviderDD = (DropDownList)EventDetail.FindControl("ProviderDropDown");
            DropDownList Results = (DropDownList)EventDetail.FindControl("ResultsDropDown");
            DropDownList Status = (DropDownList)EventDetail.FindControl("StatusDropDown");
            TextBox EventIDTxt = (TextBox)EventDetail.FindControl("txtEventID");
            TextBox PatientID = (TextBox)EventDetail.FindControl("txtPatientID");
            TextBox PatientName = (TextBox)EventDetail.FindControl("txtPatient");
            TextBox ApptStart = (TextBox)EventDetail.FindControl("txtStartDate");
            TextBox ApptEnd = (TextBox)EventDetail.FindControl("txtEndDate");
            Controls_TimeDropDown ApptStartTime = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");
            Controls_TimeDropDown ApptEndTime = (Controls_TimeDropDown)EventDetail.FindControl("txtEndTime");
            CheckBox cbAllDay = (CheckBox)EventDetail.FindControl("cbAllDay");
            CheckBox EmailOnChange = (CheckBox)EventDetail.FindControl("cbEmailOnChange");
            CheckBox LabsCheckedIn = (CheckBox)EventDetail.FindControl("cbLabsCheckedIn");
            TextBox Email = (TextBox)EventDetail.FindControl("txtEmail");
            TextBox Notes = (TextBox)EventDetail.FindControl("NotesBox");
            Button SendMail = (Button)EventDetail.FindControl("btnEmail");
            DropDownList ddlClinic = (DropDownList)EventDetail.FindControl("ddlClinic");
            DropDownList drpHARep = (DropDownList)EventDetail.FindControl("drpHARep");
            // CheckBox cbLockedAppointment = (CheckBox)EventDetail.FindControl("cbLockedAppointment");
            Session["AppointmentType"] = ApptType.SelectedValue;
            Appointment newAppt = new Appointment();
            newAppt.AllDay = cbAllDay.Checked;
            //newAppt.LockedAppointment = cbLockedAppointment.Checked;
            newAppt.ApptEnd = DateTime.Parse(ApptEnd.Text + " " + ApptEndTime.Text);
            newAppt.ApptStart = DateTime.Parse(ApptStart.Text + " " + ApptStartTime.Text);
            try
            {
                newAppt.ApptTypeID = int.Parse(ApptType.SelectedValue);
            }
            catch
            {
                newAppt.ApptTypeID = 55;
            }
            newAppt.Email = Email.Text;
            newAppt.EmailOnChange = EmailOnChange.Checked;
            newAppt.LabsCheckedIn = LabsCheckedIn.Checked;
            newAppt.EventID = int.Parse(EventIDTxt.Text);
            newAppt.Notes = Notes.Text;
            newAppt.Patient = PatientName.Text;
            newAppt.PatientID = int.Parse(PatientID.Text);
            newAppt.ProviderID = int.Parse(ProviderDD.SelectedValue);
            newAppt.Results = int.Parse(Results.SelectedValue);
            newAppt.StatusID = int.Parse(Status.SelectedValue);
            newAppt.patient = (Calendar.Patient)Session["Patient"];
            newAppt.clinic = ddlClinic.SelectedValue;
            newAppt.HARep = int.Parse(drpHARep.SelectedValue);
            string NewPatient = string.Empty;
            string LMCPhys = string.Empty;
            if (newAppt.patient != null)
            {
                NewPatient = newAppt.patient.FirstName;
                LMCPhys = newAppt.patient.LMCPhys;
            }
            objService = new CalendarService();
            int? currProvider = objService.UpdateCalAppointments(int.Parse(PatientID.Text), newAppt.ProviderID, LMCPhys, NewPatient, newAppt.ApptTypeID);


            newAppt.ActionNeeded = "No";
            Session["AppointmentType"] = null;
            Session["AppointmentID"] = Calendar.Appointments.dbUpdateEvent(newAppt, int.Parse(Session["UserID"].ToString()));
            if (Session["AppointmentID"].ToString() == "" || Session["AppointmentID"].ToString() == null)
            {
                return;
            }
            //FormailChimp campaign
            if (!string.IsNullOrEmpty(newAppt.patient.Email))
            {
                

                string APIKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"];
                string[] ItemData = Regex.Split(APIKey, "-");

                if (ItemData != null)
                {
                    if (ItemData.Length == 2)
                    {
                        Calendar.AppointmentType thisType = Calendar.AppointmentTypes.GetApptType(newAppt.ApptTypeID);
                        if (!string.IsNullOrEmpty(thisType.MailChimpCampaignId))
                        {
                            string[] CampaignData = Regex.Split(thisType.MailChimpCampaignId, "~");

                            if (CampaignData != null)
                            {
                                if (CampaignData.Length == 2)
                                {
                                    var SaveData = AddOrUpdateListMember(ItemData[1], ItemData[0], CampaignData[1], newAppt.patient.Email, newAppt.patient.FirstName, newAppt.patient.LastName);

                                }
                            }
                        }
                        

                    }
                }
            }

            //End for mailchimpcampaign

            CheckBoxList ddFollowups = (CheckBoxList)EventDetail.FindControl("ddFollowups");
            if (newAppt.Results == 0 || newAppt.Results == 3)
            {
                Label lblEventID = (Label)Utilities.FindControlRecursive(Page.Master, "lblEventID");



                foreach (ListItem lst in ddFollowups.Items)
                {
                    if (lst.Selected)
                    {
                        Calendar.Appointments.ToggleFollowup((int.Parse(Session["AppointmentID"].ToString())), lst.Value, false);
                    }
                    else
                    {
                        int id = int.Parse(Session["AppointmentID"].ToString());
                        Calendar.Appointments.ToggleFollowup(id, lst.Value, true);
                    }
                }
            }
            Appointment oldAppt = (Appointment)ViewState["oldAppt"];

            Session["Provider"] = newAppt.ProviderID;

            try
            {
                Calendar.Appointments.LogChange(57, newAppt.PatientID, int.Parse(Session["UserID"].ToString()), oldAppt, newAppt, (string)Session["EmailFromAddress"], (string)Session["EmailFromName"], (string)Session["AppointmentID"]);
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }
            if (SendMail.Text == "Confirmation will be sent" && SendMail.Visible)
            {
                SendConfirmation(newAppt);
            }


            UpdatePanelDetail.Update();
            ModalPopup.Hide();

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "init", "window.setTimeout(function() { dpc1.commandCallBack('refresh'); }, 0);", true);
            bool changedProvider = false;
            if (oldAppt != null)
                changedProvider = !(oldAppt.ProviderID == newAppt.ProviderID);

            CalRow.DataBind();
            foreach (Control theCell in CalRow.Controls)
            {
                foreach (Control theCal in theCell.Controls)
                {
                    UpdatePanel DayPilotCalendar1 = (UpdatePanel)theCal.FindControl("updCal");
                    if (DayPilotCalendar1 != null)
                    {
                        if (((Label)theCal.Controls[0].Controls[0].Controls[5]).Text == newAppt.ProviderID.ToString() || changedProvider)
                        {
                            OneCal parent = (OneCal)DayPilotCalendar1.Parent;
                            parent.BindData();
                            DayPilotCalendar1.DataBind();
                            DayPilotCalendar1.Update();
                        }
                    }
                }
            }
            Session["Updating"] = null;
            ModalPopupExtender modExpiring = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "modExpiring");

            Session["PatientID"] = newAppt.PatientID;
            if (currProvider != newAppt.ProviderID)
            {
                modExpiring.Show();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// If we want to change the current provider for the appointment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangeProvder(object sender, EventArgs e)
    {
        try
        {
            Button ctrl = (Button)sender;
            //TextBox PatientID = (TextBox)EventDetail.FindControl("txtPatientID");
            //DropDownList ProviderDD = (DropDownList)EventDetail.FindControl("ProviderDropDown");
            if (ctrl.ID == "btnChangeProv")
            {
                objService = new CalendarService();
                objService.UpdateProvider((int)Session["PatientID"], (int)Session["Provider"]);

            }
            ModalPopupExtender modExpiring = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "modExpiring");
            modExpiring.Hide();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void closePopupModalPatientAlert(object sender, EventArgs e)
    {
        try
        {

            ModalPopupExtender modExpiring = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "ModalPatientAlert");
            modExpiring.Hide();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void closePopupModalPatientAlert1(object sender, EventArgs e)
    {
        try
        {

            ModalPopupExtender modExpiring = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "ModalPatientAlert1");
            modExpiring.Hide();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// Check for valid paitients
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckPatient(object sender, ServerValidateEventArgs e)
    {
        try
        {
            TextBox txtPatient = (TextBox)EventDetail.FindControl("txtPatient");
            if (!txtPatient.ReadOnly)
            {
                Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);
                if (pat.LastName != null)
                {
                    Session["Patient"] = pat;
                    e.IsValid = true;
                }
                else
                    e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// Check for valid email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckEmail(object sender, ServerValidateEventArgs e)
    {
        CheckBox cbo = (CheckBox)EventDetail.FindControl("cbEmailOnChange");
        TextBox txt = (TextBox)EventDetail.FindControl("txtEmail");
        if (cbo.Checked && txt.Text == "")
            e.IsValid = false;
        else
            e.IsValid = true;
    }

    /// <summary>
    /// Check for valid datettime
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckDates(object sender, ServerValidateEventArgs e)
    {
        TextBox end = (TextBox)EventDetail.FindControl("txtEndDate");
        TextBox start = (TextBox)EventDetail.FindControl("txtStartDate");
        Controls_TimeDropDown endTime = (Controls_TimeDropDown)EventDetail.FindControl("txtEndTime");
        Controls_TimeDropDown startTime = (Controls_TimeDropDown)EventDetail.FindControl("txtStartTime");
        DateTime startDate = DateTime.MinValue;
        DateTime endDate = DateTime.MinValue;
        try
        {
            startDate = DateTime.Parse(start.Text + " " + startTime.Text);
            endDate = DateTime.Parse(end.Text + " " + endTime.Text);
        }
        catch
        {
            e.IsValid = false;
            return;
        }
        if (startDate > endDate)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    protected void OnSelChange(object sender, EventArgs e) { }

    //protected void btnLookup_Click(object sender, EventArgs e)
    //{
    //    TextBox patientName = (TextBox)EventDetail.FindControl("txtPatient");
    //    Calendar.Patient pat = Calendar.Patients.CheckPatient(patientName.Text);
    //    if (pat.LastName != "")
    //    {
    //        PopulateDetails(pat);
    //        PopulateFollowups(pat);
    //    }
    //}

    /// <summary>
    /// Bind the data when we select days radio button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DayPilotNavigator1_TimeRangeSelected(object sender, DayPilot.Web.Ui.Events.TimeRangeSelectedEventArgs e)
    {
        Session["SelDate"] = DayPilotNavigator1.SelectionStart; //Calendar1.SelectedDate;
        foreach (Control calCell in CalRow.Controls)
        {
            foreach (Control theCal in calCell.Controls)
            {
                try
                {
                    DayPilotCalendar DayPilotCalendar1 = (DayPilotCalendar)theCal.FindControl("DayPilotCalendar1");
                    if (DayPilotCalendar1 != null)
                    {
                        if (radio7Days.Checked)
                        {
                            DayPilotCalendar1.StartDate = DayPilot.Utils.Week.FirstDayOfWeek(DayPilotNavigator1.SelectionStart);
                            DayPilotCalendar1.Days = 7;

                            DateTime startDate = DayPilot.Utils.Week.FirstDayOfWeek(DayPilotNavigator1.SelectionStart);
                            for (int i = 0; i < 7; i++)
                            {
                                DateTime selected = startDate.AddDays(i);
                            }
                        }
                        if (radio1Day.Checked)
                        {
                            DayPilotCalendar1.StartDate = DayPilotNavigator1.SelectionStart;
                        }
                        OneCal cal = (OneCal)DayPilotCalendar1.Parent.Parent.Parent;
                        SetCalLabel(cal.ProviderID, DayPilotNavigator1.SelectionStart, cal);
                        cal.BindData();
                    }
                }
                catch (System.Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
                }
            }
        }
        UpdatePanelCalendar.Update();


    }

    protected void CheckValidEmail(object sender, ServerValidateEventArgs e)
    {
        if (e.Value.ToLowerInvariant().EndsWith("longevitymedicalclinic.com"))
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        Button button = (Button)EventDetail.FindControl("btnEmail");
        if (button.Text == "Confirmation will be sent")
        {
            button.Text = "Confirmation will NOT be sent";
        }
        else
        {
            button.Text = "Confirmation will be sent";
        }
    }

    protected void ApptTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button btnEmail = (Button)EventDetail.FindControl("btnEmail");
        Calendar.AppointmentType thisType = Calendar.AppointmentTypes.GetApptType(int.Parse(((DropDownList)EventDetail.FindControl("ApptTypeDropDown")).SelectedValue));


        if (thisType.ConfirmationEmail)
        {
            btnEmail.Visible = true;
        }
        else
        {
            btnEmail.Visible = false;
        }
    }

    protected void txtPatient_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPatient = (TextBox)EventDetail.FindControl("txtPatient");
            DropDownList ddlClinic = (DropDownList)EventDetail.FindControl("ddlClinic");
            DropDownList drpHARep = (DropDownList)EventDetail.FindControl("drpHARep");
            if (txtPatient.Enabled)
            {
                Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);
                if (pat != null)
                {

                    FollowU fups = new FollowU(pat.ID);
                    CheckBoxList FollowUps = (CheckBoxList)EventDetail.FindControl("ddFollowups");
                    PatientViewModel strMatchQB = null;
                    QBCustMatchPatientService objService = null;
                    objService = new QBCustMatchPatientService();
                    strMatchQB = objService.GetPatientDetailById(pat.ID);
                    ddlClinic.SelectedValue = pat.Clinic;
                    try
                    {
                        drpHARep.SelectedValue = pat.ConciergeID.ToString();
                    }
                    catch { }
                    if (strMatchQB != null)
                    {




                        if (strMatchQB.RenewalDate != null)
                        {
                            if (strMatchQB.RenewalDate < DateTime.Now)
                            {
                                ModalPopupExtender ModalPatientAlert = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "ModalPatientAlert");

                                ModalPatientAlert.Show();
                            }
                        }
                        else
                        {
                            if (strMatchQB.Medical == true)
                            {


                                ModalPopupExtender ModalPatientAlert1 = (ModalPopupExtender)Utilities.FindControlRecursive(Page, "ModalPatientAlert1");

                                ModalPatientAlert1.Show();
                            }
                        }
                    }

                    if (fups.Fups_Open.Count > 0)
                    {
                        FollowUps.DataSource = fups.Fups_Open;
                        FollowUps.DataTextField = "Label";
                        FollowUps.DataValueField = "FollowUpID";
                        FollowUps.DataBind();
                    }
                    else
                    {
                        FollowUps.Items.Add(new ListItem("No open followups", "0"));
                        FollowUps.Enabled = false;
                    }

                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        ModalPopupExtender modExpiring = (ModalPopupExtender)FindControlRecursive(Page.Master, "modExpiring");
        modExpiring.Hide();

    }

    protected void btnDetach_Click(object sender, EventArgs e)
    {
        try
        {
            //TextBox EventIDText = (TextBox)EventDetail.FindControl("txtEventID");
            DropDownList FollowUps = (DropDownList)EventDetail.FindControl("ddFollowups");
            int FollowupID = int.Parse(FollowUps.SelectedValue);
            FollowUp fup = new FollowUp(FollowupID);
            fup.Detach();

            //TextBox pt = (TextBox)EventDetail.FindControl("txtPatient");
            //Calendar.Patient patient = (Calendar.Patient)Session["Patient"];
            FollowU fups = new FollowU(Convert.ToInt32(Session["Patient"]));
            //FollowUp follow = new FollowUp(int.Parse(EventIDText.Text), "");
            FollowUps.DataSource = fups.Fups_Open;
            FollowUps.DataTextField = "Label";
            FollowUps.DataValueField = "FollowUpID";
            FollowUps.DataBind();
            FollowUps.Enabled = true;
            FollowUps.SelectedIndex = 0;
            ((Button)EventDetail.FindControl("btnFollowup")).Visible = false;
            ((Button)EventDetail.FindControl("btnDetach")).Visible = false;
            ((Label)EventDetail.FindControl("lblFollow")).Text = "Open follow ups";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void rptFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Attributes.Add("style", "white-space:normal;");
        e.Row.Cells[0].Attributes.Add("style", "white-space:nowrap;");
    }

    protected void ddFollowups_SelectedIndexChanged(object sender, EventArgs e)
    {
        int AptID = int.Parse(((TextBox)EventDetail.FindControl("txtEventID")).Text);


    }

    protected void EventDetail_ModeChanging(object sender, EventArgs e)
    {

    }

    #endregion

    #region "Methods"
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

    /// <summary>
    /// populates the details of patient into calender
    /// </summary>
    /// <param name="pat"></param>

    private void PopulateDetails(Calendar.Patient pat)
    {
        AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)EventDetail.FindControl("DetailsTab");
        AjaxControlToolkit.TabPanel pnl = (AjaxControlToolkit.TabPanel)container.FindControl("DetailsPanel");
        HtmlTableCell BillingStreet = (HtmlTableCell)pnl.FindControl("billingStreet");
        HtmlTableCell billingCityStateZip = (HtmlTableCell)pnl.FindControl("billingCityStateZip");
        TextBox EventIDTxt = (TextBox)EventDetail.FindControl("txtEventID");
        TextBox PatientID = (TextBox)EventDetail.FindControl("txtPatientID");

        HtmlTableCell ShippingStreet = (HtmlTableCell)pnl.FindControl("shippingAddress");
        HtmlTableCell shippingCityStateZip = (HtmlTableCell)pnl.FindControl("shippingCityStateZip");

        HtmlTableCell HomePhone = (HtmlTableCell)pnl.FindControl("homePhone");
        HtmlTableCell CellPhone = (HtmlTableCell)pnl.FindControl("cellPhone");
        HtmlTableCell WorkPhone = (HtmlTableCell)pnl.FindControl("workPhone");
        HtmlTableCell Email = (HtmlTableCell)pnl.FindControl("patientEmail");
        HtmlTableCell FaxPhone = (HtmlTableCell)pnl.FindControl("faxPhone");
        HtmlTableCell Clinic = (HtmlTableCell)pnl.FindControl("clinic");
        HtmlTableCell Birthday = (HtmlTableCell)pnl.FindControl("birthday");
        HtmlTableCell Inactive = (HtmlTableCell)pnl.FindControl("active");
        HtmlTableCell CancelNSFormSigned = (HtmlTableCell)pnl.FindControl("cancelNSFormSigned");
        HtmlTableCell HIPAAFormSigned = (HtmlTableCell)pnl.FindControl("hipaaSigned");
        HtmlTableCell Gender = (HtmlTableCell)pnl.FindControl("gender");
        HtmlTableCell Concierge = (HtmlTableCell)pnl.FindControl("Concierge");
        HtmlTableCell LabMailed = (HtmlTableCell)pnl.FindControl("LabMailed");
        HtmlTableCell PCP = (HtmlTableCell)pnl.FindControl("pcp");
        HtmlTableCell NameAlert = (HtmlTableCell)pnl.FindControl("nameAlert");
        HtmlTableCell EmeregencyContact = (HtmlTableCell)pnl.FindControl("emergencyContact");
        HtmlTableCell LMCPhys = (HtmlTableCell)pnl.FindControl("lmcProvider");
        HtmlTableCell EmergencyPhone = (HtmlTableCell)pnl.FindControl("emergencyPhone");
        HtmlTableCell ContactPreference = (HtmlTableCell)pnl.FindControl("contactPreference");
        HtmlTableCell EmergencyRelationship = (HtmlTableCell)pnl.FindControl("emergencyRelationship");

        if (pat.BillingStreet != null)
            BillingStreet.InnerHtml = pat.BillingStreet;

        billingCityStateZip.InnerHtml = pat.BillingCity + ", " + pat.BillingState + " " + pat.BillingZip;
        ShippingStreet.InnerHtml = pat.ShippingStreet;
        shippingCityStateZip.InnerHtml = pat.ShippingCity + ", " + pat.ShippingState + " " + pat.ShippingZip;
        HomePhone.InnerHtml = pat.HomePhone;
        if (pat.Home_detailed_info)
        {
            HomePhone.InnerHtml += "<font color=\"#FF0000\"> Detailed Info OK</font>";
        }
        if (pat.Home_cb_only)
        {
            HomePhone.InnerHtml += "<font color=\"#FF0000\"> Call Back ONLY</font>";
        }
        CellPhone.InnerHtml = pat.CellPhone;
        if (pat.cell_detailed_info)
        {
            CellPhone.InnerHtml += "<font color=\"#FF0000\"> Detailed Info OK</font>";
        }
        if (pat.cell_cb_only)
        {
            CellPhone.InnerHtml += "<font color=\"#FF0000\"> Call Back ONLY</font>";
        }
        WorkPhone.InnerHtml = pat.WorkPhone;
        if (pat.work_detailed_info)
        {
            WorkPhone.InnerHtml += "<font color=\"#FF0000\"> Detailed Info OK</font>";
        }
        if (pat.work_cb_only)
        {
            WorkPhone.InnerHtml += "<font color=\"#FF0000\"> Call Back ONLY</font>";
        }
        Email.InnerHtml = "<font color=\"#666666\"><b>Email Address</b></font>&nbsp;&nbsp;<font color=\"#666666\">" + pat.Email + "</font>";
        if (pat.email_auth_detailed_info)
        {
            Email.InnerHtml += "<font color=\"#FF0000\"> Detailed Info OK</font>";
        }
        FaxPhone.InnerHtml = pat.FaxPhone;
        if (pat.fax_auth_detailed_info)
        {
            FaxPhone.InnerHtml += "<font color=\"#FF0000\"> Detailed Info OK</font>";
        }
        Clinic.InnerHtml = pat.Clinic;
        Birthday.InnerHtml = pat.Birthday.ToString("MM/dd/yyyy");
        if (pat.Inactive)
            Inactive.InnerHtml = "<font color=\"#FF0000\">Inactive</font>";
        else
            Inactive.InnerHtml = "<font color=\"#FF0000\">Active</font>"; CancelNSFormSigned.InnerHtml = pat.CancelNSFormSigned.ToString();
        HIPAAFormSigned.InnerHtml = pat.HIPAAFormSigned.ToString();
        Gender.InnerHtml = pat.Gender;
        Concierge.InnerHtml = pat.Concierge;
        PCP.InnerHtml = pat.PCP;
        LabMailed.InnerHtml = pat.LabsMailed.ToString();
        NameAlert.InnerHtml = "Name Alert: " + pat.NameAlert.ToString();
        EmeregencyContact.InnerHtml = pat.EmeregencyContact;
        LMCPhys.InnerHtml = pat.LMCPhys;
        EmergencyPhone.InnerHtml = pat.EmergencyPhone;
        ContactPreference.InnerHtml = pat.ContactPreference;
        EmergencyRelationship.InnerHtml = pat.EmergencyRelationship;
        PatientID.Text = pat.ID.ToString();

    }

    /// <summary>
    /// binding the patient folloe up list with the grid
    /// </summary>
    /// <param name="pat"></param>
    protected void PopulateFollowups(int patientID)
    {
        try
        {
            FollowU Follow = new FollowU(patientID);
            if (Follow.Fups.Count != 0)
            {
                rptFollowUp.DataSource = Follow.Fups;
                rptFollowUp.DataBind();

            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }

    protected string AddCalendar(string ProviderID)
    {
        OneCal cal = (OneCal)LoadControl("~/controls/OneCal.ascx");
        try
        {


            //set provider ID
            cal.ProviderID = ProviderID;
            //Calendar.Provider prov = Calendar.Provider.GetProvider(ProviderID)[0];
            SetCalLabel(ProviderID, DateTime.Today, cal);
            //add to cell

            System.Web.UI.HtmlControls.HtmlTableCell td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Width = "1000px";
            td.Align = "center";
            td.VAlign = "top";
            td.Controls.Add(cal);
            //add the cell to the row.
            //CalRow.Controls.Counttd);
            CalRow.Controls.Add(td);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        return cal.ClientID;
    }

    private void SetCalLabel(string ProviderID, DateTime calDate, OneCal cal)
    {
        try
        {
            Calendar.Provider prov = Calendar.Provider.GetProvider(ProviderID)[0];

            cal.ProviderName = Calendar.Provider.GetProviderName(ProviderID);
            cal.ID = "cal" + ProviderID;
            if (radio1Day.Checked)
            {
                switch (calDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (prov.MondayStart != "0")
                            cal.ProviderName += "<br />" + prov.MondayStart + " - " + prov.MondayEnd;
                        else
                            cal.ProviderName += "<br />Not in office.";
                        break;
                    case DayOfWeek.Tuesday:
                        if (prov.TuesdayStart != "0")
                            cal.ProviderName += "<br />" + prov.TuesdayStart + " - " + prov.TuesdayEnd;
                        else
                            cal.ProviderName += "<br />Not in office.";
                        break;
                    case DayOfWeek.Wednesday:
                        if (prov.WednesdayStart != "0")
                            cal.ProviderName += "<br />" + prov.WednesdayStart + " - " + prov.WednesdayEnd;
                        else
                            cal.ProviderName += "<br />Not in office.";
                        break;
                    case DayOfWeek.Thursday:
                        if (prov.ThursdayStart != "0")
                            cal.ProviderName += "<br />" + prov.ThursdayStart + " - " + prov.ThursdayEnd;
                        else
                            cal.ProviderName += "<br />Not in office.";
                        break;
                    case DayOfWeek.Friday:
                        if (prov.FridayStart != "0")
                            cal.ProviderName += "<br />" + prov.FridayStart + " - " + prov.FridayEnd;
                        else
                            cal.ProviderName += "<br />Not in office.";
                        break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }

    private void SendConfirmation(Appointment newAppt)
    {
        try
        {
            Calendar.AppointmentType theType = Calendar.AppointmentTypes.GetApptType(newAppt.ApptTypeID);
            if (theType.ConfirmationEmail)
            {
                DateTime ApptStart = newAppt.ApptStart;
                DateTime ApptEnd = newAppt.ApptEnd;
                string FirstName = newAppt.patient.FirstName;
                string LastName = newAppt.patient.LastName;
                string MiddleInitial = newAppt.patient.MiddleInitial;
                string FullName = newAppt.patient.FullName;
                string Provider = Calendar.Provider.GetProviderName(newAppt.ProviderID);
                string ApptType = theType.TypeName;
                string ApptStartDate = newAppt.ApptStart.ToString("d");
                string ApptStartTime = newAppt.ApptStart.ToString("t");
                string ApptEndDate = newAppt.ApptEnd.ToString("d");
                string ApptEndTime = newAppt.ApptEnd.ToString("t");

                string MessageBody = theType.ConfirmationText;
                string toAddress = newAppt.patient.Email;
                string fromAddress = theType.EmailFromAddress;
                string fromName = theType.EmailFromName;
                if (!ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToLower().Contains("lmc_020505"))
                {
                    toAddress = "aspdev@longevitymedicalclinic.com";
                }

                MessageBody = MessageBody.Replace("^ApptStart^", ApptStart.ToString());
                MessageBody = MessageBody.Replace("^ApptEnd^", ApptEnd.ToString());
                MessageBody = MessageBody.Replace("^ApptStartDate^", ApptStartDate);
                MessageBody = MessageBody.Replace("^ApptStartTime^", ApptStartTime);
                MessageBody = MessageBody.Replace("^ApptEndDate^", ApptEndDate);
                MessageBody = MessageBody.Replace("^ApptEndTime^", ApptEndTime);
                MessageBody = MessageBody.Replace("^FirstName^", FirstName);
                MessageBody = MessageBody.Replace("^LastName^", LastName);
                MessageBody = MessageBody.Replace("^MiddleInitial^", MiddleInitial);
                MessageBody = MessageBody.Replace("^FullName^", FullName);
                MessageBody = MessageBody.Replace("^Provider^", Provider);
                MessageBody = MessageBody.Replace("^ApptType^", ApptType);
                string Attach = "";
                if (theType.Attachment != "")
                {
                    Attach = Server.MapPath("./Output_Files") + "\\" + theType.Attachment;
                }
                if (newAppt.patient.Email != "")
                {
                    Mail.SendMessage(fromAddress, fromName, toAddress, newAppt.patient.FirstName + " " + newAppt.patient.LastName, MessageBody, Attach, theType.Subject);
                    Calendar.Appointments.LogEmail(newAppt.ApptTypeID, newAppt.PatientID, int.Parse(Session["UserID"].ToString()), newAppt, false);

                }
                else
                {
                    string BaseURL = "http://emr";
                    if (!ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToLower().Contains("lmc_020505"))
                        BaseURL = "http://emr:81";
                    string tName = "<a href=\"" + BaseURL + "/Manage.aspx?PatientID=" + newAppt.PatientID + "\" target=\"_new\">" + newAppt.patient.FirstName + " " + newAppt.patient.LastName + "</a>";
                    Mail.NoMail(tName, MessageBody, Attach, "Appointment Confirmation");
                    Calendar.Appointments.LogEmail(newAppt.ApptTypeID, newAppt.PatientID, int.Parse(Session["UserID"].ToString()), newAppt, true);
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    private void LogConfirmation(Appointment appt)
    {
        try
        {
            Calendar.AppointmentType theType = Calendar.AppointmentTypes.GetApptType(appt.ApptTypeID);
            string msg = DateTime.Now.ToString("g") + " - Sent confirmation email.  Appointment type: " + theType.TypeName;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

   

    #endregion
    //protected void ProvidersCBox_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    string result = Request.Form["__EVENTTARGET"];
    //    string[] checkedBox = result.Split('$'); ;
    //    int index = int.Parse(checkedBox[checkedBox.Length - 1]);
    //    loadingdivbackground.Visible = true;


    //    //foreach (ListItem lst in ProvidersCBox.Items)
    //    //{
    //    //    if (lst.Selected)
    //    //    {
    //    //        AddCalendar(lst.Value);
    //    //    }
    //    //}
    //    //loadingdivbackground.Attributes.Add("Style", "visibility:hide");
    //    //if (ProvidersCBox.Items[index].Selected)
    //    //{
    //    //    AddCalendar(ProvidersCBox.Items[index].Value);

    //    //}
    //    //else
    //    //{


    //    //}


    //}


}
