using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CRM_SeminarList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PatientService objService = new PatientService();

            //ddlClinic.DataSource = objService.GetClinics();
            //ddlClinic.DataTextField = "ClinicName";
            //ddlClinic.DataValueField = "ClinicName";
            //ddlClinic.DataBind();
        }
    }


    //function to Get the provider List 
    [WebMethod]
    public static List<ClinicsViewModel> GetClinic()
    {
        List<ClinicsViewModel> List = null;
        try
        {
            PatientService objService = new PatientService();

            List = objService.GetClinics().ToList();

        }

        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }

        return List;

    }

    [WebMethod]
    public static List<PostSeminarAppointment> GetPostSeminars(string Isvalue, string ClinicID)
    {
        DateTime StartDate = new DateTime(2016, 1, 1);
        ISeminarScheduleService objServices = new SeminarScheduleService();
        List<PostSeminarAppointment> list = objServices.GetPostSeminarAppointment(StartDate, ClinicID);
        //DateTime LastDate=DateTime.Now.Date.AddDays(-2);
        //string Week=string.Empty;
        //string month = string.Empty;
        //string day = string.Empty;
        //foreach(var i in list)
        //{
        //    DateTime ApptDate = i.ApptStart.Date;

        //    if (ApptDate > LastDate)
        //    {
        //        if (i.WeekdayName == "Sunday")
        //        {

        //            i.WeekDayColor = string.Format("MMM dd", ApptDate.ToString()) + " - " + string.Format("MMM dd",ApptDate.AddDays(6).ToString());
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(6);
        //        }
        //        else if (i.WeekdayName == "Monday")
        //        {
        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-1).ToString()) + " - " + ApptDate.AddDays(5).ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(5);
        //        }
        //        else if (i.WeekdayName == "Tuesday")
        //        {
        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-2).ToString()) + " - " + ApptDate.AddDays(4).ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(4);
        //        }
        //        else if (i.WeekdayName == "Wednesday")
        //        {

        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-3).ToString()) + " - " + ApptDate.AddDays(3).ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(3);
        //        }
        //        else if (i.WeekdayName == "Thursday")
        //        {
        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-4).ToString()) + " - " + ApptDate.AddDays(2).Date.ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(2);
        //        }
        //        else if (i.WeekdayName == "Friday")
        //        {
        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-5).ToString()) + " - " + ApptDate.AddDays(1).Date.ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate.AddDays(1);
        //        }
        //        else if (i.WeekdayName == "Saturday")
        //        {
        //            i.WeekDayColor = string.Format("MMM dd",ApptDate.AddDays(-6).Date.ToString()) + " - " + ApptDate.Date.ToString();
        //            Week = i.WeekDayColor;
        //            LastDate = ApptDate;
        //        }
        //    }
        //    else
        //    {
        //        i.WeekDayColor = Week;
        //    }
        //}
        return list;

    }

}