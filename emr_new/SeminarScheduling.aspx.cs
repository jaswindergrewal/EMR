using Emrdev.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.Services;

public partial class Seminar_scheduling_SeminarScheduling : LMCBase
{
    #region "Variable"
    ManageService _objService = null;
    List<PostSeminarAppointment> ApptTypes;
    List<PostSeminarAppointment> filteredList;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        StaffID.Value = ((int)Session["StaffID"]).ToString();

        if (!IsPostBack)
        {

            _objService = new ManageService();
            //fill Event dropdown with all events data
            drpEvents.DataSource = _objService.GetAllEvents(); ;
            drpEvents.DataTextField = "EventName";
            drpEvents.DataValueField = "EventID";
            drpEvents.DataBind();
            drpEvents.Items.Insert(0, new ListItem("--Select Event--", "0"));


            var jan = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var WeekDay = DateTime.Today.DayOfWeek;

            var startOfFirstWeek = new DateTime();
            if (WeekDay.ToString() == "Sunday")
            {
                startOfFirstWeek = jan;
            }
            else if (WeekDay.ToString() == "Saturday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }

            else if (jan.DayOfWeek.ToString() == "Friday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }
            else if (jan.DayOfWeek.ToString() == "Thursday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }
            else if (jan.DayOfWeek.ToString() == "Wednesday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }
            else if (jan.DayOfWeek.ToString() == "Tuesday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }
            else if (jan.DayOfWeek.ToString() == "Monday")
            {
                startOfFirstWeek = jan.AddDays(-(int)(WeekDay));
            }

            //var startOfFirstWeek = jan.AddDays((int)(jan.DayOfWeek));
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new
                    {
                        weekStart = startOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Year <= jan.Year)
                    .Select(x => new
                    {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(6)
                    })
                // .SkipWhile(x => x.weekFinish < jan.AddDays(1))
                    .Select((x, i) => new
                    {
                        x.weekStart,
                        x.weekFinish,
                        weekNum = i + 1
                    });
            List<SeminarDateRangeViewModel> DateRangeList = new List<SeminarDateRangeViewModel>();

            foreach (var i in weeks)
            {
                SeminarDateRangeViewModel SeminarDateRangeViewModel = new SeminarDateRangeViewModel();
                string date = "Sun " + i.weekStart.ToString("MM/dd") + " - Sat " + i.weekFinish.ToString("MM/dd");
                SeminarDateRangeViewModel.Name = date;
                SeminarDateRangeViewModel.Id = i.weekStart;
                DateRangeList.Add(SeminarDateRangeViewModel);
            }

            drpDate.DataSource = DateRangeList;
            drpDate.DataTextField = "Name";
            drpDate.DataValueField = "Id";
            drpDate.DataBind();
            drpDate.Items.Insert(0, new ListItem("--Select Week--", (startOfFirstWeek.AddDays(-1)).ToString()));
            var CurrentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);


        }
    }

    protected void drpEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnKirkland.CssClass = "rcorners";
        btnTacoma.CssClass = "rcorners";
        btnLynnwood.CssClass = "rcorners";
        drpDate.SelectedIndex = 0;

        WeekButton();
        hdnEventId.Value = drpEvents.SelectedValue;
        grdHeading.Text = "";
        grdSeminar.ClearPreviousDataSource();
        if (Convert.ToInt32(drpEvents.SelectedValue) > 0)
        {
            _objService = new ManageService();
            //fill Event dropdown with all events data
            dynamic lstRegistrant = _objService.GetAllAttend(Convert.ToInt32(drpEvents.SelectedValue));
            List<ProspectViewmodel> lstProspect = new List<ProspectViewmodel>();
            foreach (var i in lstRegistrant)
            {
                if (i.Appointments == "None")
                {
                    ProspectViewmodel prospect = new ProspectViewmodel();
                    prospect.LastName = i.LastName + " " + i.FirstName;
                    prospect.ProspectID = i.ProspectID;
                    lstProspect.Add(prospect);
                }
            }
            drpRegistrants.DataSource = lstProspect;
            drpRegistrants.DataTextField = "LastName";
            drpRegistrants.DataValueField = "ProsPectID";
            drpRegistrants.DataBind();
            drpRegistrants.Items.Insert(0, new ListItem("--Select Registrant--", "0"));

        }
    }

    protected void btnKirkland_Click(object sender, EventArgs e)
    {
        btnKirkland.CssClass = "selectedClinicButton";
        btnTacoma.CssClass = "rcorners";
        btnLynnwood.CssClass = "rcorners";
        drpDate.SelectedIndex = 0;
        hdnClincId.Value = "Kirkland";
        grdHeading.Text = "";
        grdSeminar.ClearPreviousDataSource();
        WeekButton();
    }

    protected void btnTacoma_Click(object sender, EventArgs e)
    {
        btnKirkland.CssClass = "rcorners";
        btnTacoma.CssClass = "selectedClinicButton";
        btnLynnwood.CssClass = "rcorners";

        hdnClincId.Value = "South";
        grdHeading.Text = "";
        grdSeminar.ClearPreviousDataSource();
        drpDate.SelectedIndex = 0;
        WeekButton();
    }
    protected void btnLynnwood_Click(object sender, EventArgs e)
    {
        btnKirkland.CssClass = "rcorners";
        btnTacoma.CssClass = "rcorners";
        btnLynnwood.CssClass = "selectedClinicButton";

        hdnClincId.Value = "Lynnwood";
        grdHeading.Text = "";
        grdSeminar.ClearPreviousDataSource();
        drpDate.SelectedIndex = 0;
        WeekButton();
    }
    protected void drpDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDate.SelectedItem.Text == "--Select Week--")
        {
            WeekButton();
        }
        else
        {
            string Id = drpDate.SelectedItem.Value;
            ApptTypes = new List<PostSeminarAppointment>();
            ApptTypes = GetPostSeminars(Id, hdnClincId.Value);
            Session["CurrentDate"] = Id.ToString();
            Changebuttoncolor(ApptTypes);
            grdHeading.Text = "";
            grdSeminar.ClearPreviousDataSource();

        }

    }

    public void WeekButton()
    {
        btnMonday.CssClass = "NoColorWeekDayButton";
        btnMonday.Text = "Monday";
        btnMonday.Enabled = false;

        btnTuesday.CssClass = "NoColorWeekDayButton";
        btnTuesday.Text = "Tuesday";
        btnTuesday.Enabled = false;

        btnWednesday.CssClass = "NoColorWeekDayButton";
        btnWednesday.Text = "Wednesday";
        btnWednesday.Enabled = false;

        btnThursday.CssClass = "NoColorWeekDayButton";
        btnThursday.Text = "Thursday";
        btnThursday.Enabled = false;

        btnFriday.CssClass = "NoColorWeekDayButton";
        btnFriday.Text = "Friday";
        btnFriday.Enabled = false;
    }

    public List<PostSeminarAppointment> GetPostSeminars(string Date, string Clinic)
    {
        DateTime StartDate = DateTime.Parse(Date);
        ISeminarScheduleService objServices = new SeminarScheduleService();
        return objServices.GetPostSeminarAppointment(StartDate, Clinic);

    }

    public void Changebuttoncolor(List<PostSeminarAppointment> ApptTypes)
    {
        List<PostSeminarAppointment> countWeekList = new List<PostSeminarAppointment>();
        DateTime SelectedDate = DateTime.Parse(drpDate.SelectedItem.Value).Date;
        System.Text.StringBuilder buttonText = new System.Text.StringBuilder();
        buttonText.AppendLine("Monday");
        buttonText.AppendLine(SelectedDate.AddDays(1).ToString("MM/dd/yyyy"));
        btnMonday.Text = buttonText.ToString();
        countWeekList = ApptTypes.FindAll(x => x.WeekdayName == "Monday");
        if (countWeekList.Count > 0)
        {
            btnMonday.CssClass = "rcornersWeek";
            btnMonday.Enabled = true;
        }
        else
        {
            btnMonday.CssClass = "NoColorWeekDayButton";
            btnMonday.Enabled = false;
        }
        //*****************************
        btnTuesday.Text = "Tuesday" + Environment.NewLine + SelectedDate.AddDays(2).ToString("MM/dd/yyyy");
        countWeekList = ApptTypes.FindAll(x => x.WeekdayName == "Tuesday");
        if (countWeekList.Count > 0)
        {
            btnTuesday.CssClass = "rcornersWeek";
            btnTuesday.Enabled = true;
        }
        else
        {
            btnTuesday.CssClass = "NoColorWeekDayButton";
            btnTuesday.Enabled = false;
        }
        //*************************************
        btnWednesday.Text = "Wednesday" + Environment.NewLine + SelectedDate.AddDays(3).ToString("MM/dd/yyyy");
        countWeekList = ApptTypes.FindAll(x => x.WeekdayName == "Wednesday");
        if (countWeekList.Count > 0)
        {
            btnWednesday.CssClass = "rcornersWeek";
            btnWednesday.Enabled = true;
        }
        else
        {
            btnWednesday.CssClass = "NoColorWeekDayButton";
            btnWednesday.Enabled = false;
        }
        //*********************************
        btnThursday.Text = "Thursday" + Environment.NewLine + SelectedDate.AddDays(4).ToString("MM/dd/yyyy");
        countWeekList = ApptTypes.FindAll(x => x.WeekdayName == "Thursday");
        if (countWeekList.Count > 0)
        {
            btnThursday.CssClass = "rcornersWeek";
            btnThursday.Enabled = true;
        }
        else
        {
            btnThursday.CssClass = "NoColorWeekDayButton";
            btnThursday.Enabled = false;
        }
        //**********************************
        btnFriday.Text = "Friday" + Environment.NewLine + SelectedDate.AddDays(5).ToString("MM/dd/yyyy");
        countWeekList = ApptTypes.FindAll(x => x.WeekdayName == "Friday");
        if (countWeekList.Count > 0)
        {
            btnFriday.CssClass = "rcornersWeek";
            btnFriday.Enabled = true;
        }
        else
        {
            btnFriday.CssClass = "NoColorWeekDayButton";
            btnFriday.Enabled = false;
        }
    }

    protected void btnMonday_Click(object sender, EventArgs e)
    {
        filteredList = new List<PostSeminarAppointment>();
        filteredList = GetPostSeminars(drpDate.SelectedItem.Value, hdnClincId.Value);
        Changebuttoncolor(filteredList);
        btnMonday.CssClass = "selectedWeekDayButton";
        hdnDay.Value = "Monday";
        BindGrid("Monday", drpDate.SelectedItem.Value);

    }
    protected void btnTuesday_Click(object sender, EventArgs e)
    {
        filteredList = new List<PostSeminarAppointment>();
        filteredList = GetPostSeminars(drpDate.SelectedItem.Value, hdnClincId.Value);
        Changebuttoncolor(filteredList);
        btnTuesday.CssClass = "selectedWeekDayButton";
        hdnDay.Value = "Tuesday";
        BindGrid("Tuesday", drpDate.SelectedItem.Value);

    }
    protected void btnWednesday_Click(object sender, EventArgs e)
    {
        filteredList = new List<PostSeminarAppointment>();
        filteredList = GetPostSeminars(drpDate.SelectedItem.Value, hdnClincId.Value);
        Changebuttoncolor(filteredList);
        btnWednesday.CssClass = "selectedWeekDayButton";
        hdnDay.Value = "Wednesday";
        BindGrid("Wednesday", drpDate.SelectedItem.Value);

    }
    protected void btnThursday_Click(object sender, EventArgs e)
    {
        filteredList = new List<PostSeminarAppointment>();
        filteredList = GetPostSeminars(drpDate.SelectedItem.Value, hdnClincId.Value);

        Changebuttoncolor(filteredList);
        btnThursday.CssClass = "selectedWeekDayButton";
        hdnDay.Value = "Thursday";
        BindGrid("Thursday", drpDate.SelectedItem.Value);
    }
    protected void btnFriday_Click(object sender, EventArgs e)
    {
        filteredList = new List<PostSeminarAppointment>();
        filteredList = GetPostSeminars(drpDate.SelectedItem.Value, hdnClincId.Value);

        Changebuttoncolor(filteredList);
        btnFriday.CssClass = "selectedWeekDayButton";
        hdnDay.Value = "Friday";
        BindGrid("Friday", drpDate.SelectedItem.Value);
    }

    public void BindGrid(string day, string currentDate)
    {

        grdHeading.Text = hdnClincId.Value + " - " + day + " appointments";
        List<PostSeminarAppointment> lstSeminarData = null;


        lstSeminarData = GetPostSeminars(currentDate, hdnClincId.Value);

        lstSeminarData = lstSeminarData.FindAll(x => x.WeekdayName == day);

        grdSeminar.DataSource = lstSeminarData;
        grdSeminar.DataBind();



    }


    protected void grdSeminar_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {

    }

    protected void drpRegistrants_SelectedIndexChanged1(object sender, EventArgs e)
    {
        _objService = new ManageService();
        hdnIsRegistrant.Value = "true";
        //fill Event dropdown with all events data
        dynamic lstRegistrant = _objService.GetAllAttend(Convert.ToInt32(drpEvents.SelectedValue));
        List<ProspectViewmodel> lstProspect = new List<ProspectViewmodel>();
        foreach (var i in lstRegistrant)
        {
            if (i.ProspectID == Convert.ToInt32(drpRegistrants.SelectedValue))
            {
                txtProspectFirstName.Text = i.FirstName;
                txtProspectLastName.Text = i.LastName;
                txtProspectMainPhone.Text = i.MainPhone;
                txtProspectEmail.Text = i.Email;
            }
        }

    }


    public void ClearText()
    {
        txtProspectEmail.Text = "";
        txtProspectFirstName.Text = "";
        txtProspectLastName.Text = "";
        txtProspectMainPhone.Text = "";
        BindGrid(hdnDay.Value, drpDate.SelectedItem.Value);

        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close_Window", "ScheduleAppointmentWindow.Close();", true);
    }

    protected void btnWalkIn_Click(object sender, EventArgs e)
    {
        if (drpRegistrants.Enabled == true)
        {
            drpRegistrants.Enabled = false;
            txtProspectEmail.Enabled = true;
            txtProspectFirstName.Enabled = true;
            txtProspectLastName.Enabled = true;
            txtProspectMainPhone.Enabled = true;
            hdnIsRegistrant.Value = "false";
            ClearText();
        }
        else
        {
            drpRegistrants.Enabled = true;
            txtProspectEmail.Enabled = false;
            txtProspectFirstName.Enabled = false;
            txtProspectLastName.Enabled = false;
            txtProspectMainPhone.Enabled = false;
            hdnIsRegistrant.Value = "true";
        }
    }
    protected void btnScheduleAppointment_Click(object sender, EventArgs e)
    {
        bool result;
        int AptID = Convert.ToInt32(hdnAppointmentId.Value);
        string Clinic = hdnClincId.Value;
        int StaffId = Convert.ToInt32(StaffID.Value); ;
        int EventID = Convert.ToInt32(drpEvents.SelectedValue);
        int PatientID = 0;
        if (drpRegistrants.Enabled == true)
        {

            IManageService _objService = new ManageService();

            int ProspectID = Convert.ToInt32(drpRegistrants.SelectedValue);
            result = _objService.ReCordAttendee(PatientID, ProspectID, AptID, StaffId, Clinic, EventID);
            if (result == true)
            {
                _objService = new ManageService();
                //fill Event dropdown with all events data
                dynamic lstRegistrant = _objService.GetAllAttend(Convert.ToInt32(drpEvents.SelectedValue));
                List<ProspectViewmodel> lstProspect = new List<ProspectViewmodel>();
                foreach (var i in lstRegistrant)
                {
                    if (i.Appointments == "None")
                    {
                        ProspectViewmodel prospect = new ProspectViewmodel();
                        prospect.LastName = i.LastName + " " + i.FirstName;
                        prospect.ProspectID = i.ProspectID;
                        lstProspect.Add(prospect);
                    }
                }
                drpRegistrants.DataSource = lstProspect;
                drpRegistrants.DataTextField = "LastName";
                drpRegistrants.DataValueField = "ProsPectID";
                drpRegistrants.DataBind();
                drpRegistrants.Items.Insert(0, new ListItem("--Select Registrant--", "0"));
            }
        }
        else
        {
            ISeminarScheduleService objServices = new SeminarScheduleService();
            int prospectid = objServices.InsertUpdateProspects(
                0,
                "",
                "",
               "",
                "",
                txtProspectEmail.Text,
                txtProspectFirstName.Text,
                false,
                txtProspectLastName.Text,
               txtProspectMainPhone.Text,
               "",
               "",
               "",
                4,
                "",
                StaffId,
                EventID
                );


            IManageService _objService = new ManageService();
            result = _objService.ReCordAttendee(PatientID, prospectid, AptID, StaffId, Clinic, EventID);
            ClearText();

        }

        string OpenWindow = "ScheduleAppointmentWindow.Close();"; ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", OpenWindow, true);


        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", OpenWindow, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "<script>alert('hi');ScheduleAppointmentWindow.Close();</script>", true);


    }

    [WebMethod]
    public static bool RecordEventAppt(string data)
    {
        bool test;
        IManageService _objService = new ManageService();
        try
        {
            int PatientID = 0;
            int ProspectID = int.Parse(data.Split('|')[1]);
            int AptID = int.Parse(data.Split('|')[0]);
            string Clinic = (data.Split('|')[2]);
            int StaffID = int.Parse(data.Split('|')[3]);
            int EventID = int.Parse(data.Split('|')[4]);
            test = _objService.ReCordAttendee(PatientID, ProspectID, AptID, StaffID, Clinic, EventID);


        }
        catch (System.Exception ex)
        {
            test = false;


        }
        finally
        {
            _objService = null;
        }

        return test;

    }


    [WebMethod]
    public static bool saveProspect(int ApptID, string ClinicID, int EventID, int StaffID, string FirstName, string LastName, string Phone, string Email)
    {
        bool Result = true;
        ISeminarScheduleService objServices = new SeminarScheduleService();

        try
        {
            int PatientID = 0;

            int prospectid = objServices.InsertUpdateProspects(
               0,
               "",
               "",
              "",
               "",
               Email,
               FirstName,
               false,
               LastName,
              Phone,
              "",
              "",
              "",
               4,
               "",
               StaffID,
               EventID
               );

            IManageService _objService = new ManageService();
            Result = _objService.ReCordAttendee(PatientID, prospectid, ApptID, StaffID, ClinicID, EventID);
        }
        catch
        {
            Result = false;
        }
        return Result;
    }


    protected void grdSeminar_Rebind(object sender, EventArgs e)
    {
        BindGrid(hdnDay.Value, drpDate.SelectedItem.Value);
    }
}
