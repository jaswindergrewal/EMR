using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;
public partial class NewTicketNoPatient : LMCBase
{
    #region Variable
    INewTicketNoPatientService objService = null;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtDueDate.Attributes.Add("readonly", "readonly");
                objService = new NewTicketNoPatientService();
                txtDueDate.Text = DateTime.Today.ToShortDateString();
                string thisPath = Server.MapPath("~");
                thisPath += "\\UserImages\\" + ((int)Session["StaffID"]).ToString();

                if (!Directory.Exists(thisPath + Request.QueryString["PatientID"]))
                {
                    Directory.CreateDirectory(thisPath + Request.QueryString["PatientID"]);
                }

                btnImageInsert.UploadFolder = "~/UserImages/" + ((int)Session["StaffID"]).ToString() + "/";
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
    }
    #endregion
    #region Web Methods
    /// <summary>
    /// Method to bind employee drop down on employee radiobutton checked event
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<DepartmentStaffViewModel> BindEmployee()
    {

        INewTicketNoPatientService objService = new NewTicketNoPatientService();
        List<DepartmentStaffViewModel> objEmployee = new List<DepartmentStaffViewModel>();
        objEmployee = objService.GetDepartmentStaff();
        return objEmployee;
    }
    /// <summary>
    ///  Method to bind department drop down on department radiobutton checked event
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<DepartmentViewModel> BindDepartment()
    {
        INewTicketNoPatientService objService = new NewTicketNoPatientService();
        List<DepartmentViewModel> objEmployee = new List<DepartmentViewModel>();
        objEmployee = objService.GetDepartments();
        return objEmployee;

    }
    /// <summary>
    /// Method to insert  information on submit button click
    /// </summary>
    /// <param name="EnteredBy"></param>
    /// <param name="body"></param>
    /// <param name="followup_Cat"></param>
    /// <param name="severity"></param>
    /// <param name="selectedValue"></param>
    /// <param name="subject"></param>
    /// <param name="dueDate"></param>
    /// <returns></returns>
    [WebMethod]
    public static int SaveData(int EnteredBy, string body, int followup_Cat, int severity, string selectedValue, string subject, string dueDate)
    {
        try
        {

            string theContent = body;
            int startPos = 0;

            while (theContent.IndexOf("<img", startPos) != -1)
            {
                //this  will execute only when we upload the image and the image will stored in the userimages\StaffID folder
                startPos = theContent.IndexOf("<img", startPos);
                string workString = theContent.Substring(startPos);
                string src = workString.Substring(workString.IndexOf("src") + 5);
                string uri = src.Split('\"')[0];
                string timeStamp = DateTime.Now.Ticks.ToString();

                string origName = uri.Split('/').Last();
                string NewName = origName.Split('.').First() + timeStamp + "." + origName.Split('.').Last();

                theContent = theContent.Replace(origName, NewName);
                string thisPath = HttpContext.Current.Server.MapPath("~");
                thisPath += "\\UserImages\\" + ((int)HttpContext.Current.Session["StaffID"]).ToString() + "\\";

                File.Move(thisPath + origName, thisPath + NewName);
                startPos = startPos + 5;
            }


            apt_FollowUpsViewModel theFollow = new apt_FollowUpsViewModel();
            theFollow.DateEntered = DateTime.Now;
            theFollow.Entered_By = EnteredBy;
            theFollow.FollowUp_Body = body;
            theFollow.FollowUp_Completed_YN = false;
            theFollow.FollowUp_Cat = followup_Cat;
            theFollow.Severity = severity;
            string[] assign = selectedValue.Split(' ');
            if (assign[1] == "Emp")
                theFollow.Assigned = Convert.ToInt32(assign[0]);
            else
                theFollow.DepartmentAssign = Convert.ToInt32(assign[0]);
            theFollow.FollowUp_Subject = subject;
            theFollow.DueDate = DateTime.Parse(dueDate);
            theFollow.FirstCall = false;
            theFollow.SecondCall = false;
            theFollow.FinalCall = false;
            theFollow.Letter = false;
            theFollow.FinalCallNote = "";
            theFollow.FirstCallNote = "";
            theFollow.SeconCallNote = "";
            theFollow.LetterNote = "";
            NewTicketNoPatientService objService = new NewTicketNoPatientService();
            //Insert the data in apt_FollowUps table
            objService.InsertAptFollowUp(theFollow, body, EnteredBy);
            return 1;

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return 0;
        }
    }
    /// <summary>
    ///  Method to bind Category Dropdown on select index change of assign dropdown
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<AptFollowupsTypeViewModel> BindCategory(int ID, string Type)
    {
        List<AptFollowupsTypeViewModel> Objlist = new List<AptFollowupsTypeViewModel>();
        NewTicketNoPatientService objService = new NewTicketNoPatientService();
        try
        {
            if (ID != 0)
            {
                if (Type == "Emp")
                {
                    Objlist = objService.GetAptFollowups(ID, 1);
                }
                else
                {
                    Objlist = objService.GetAptFollowups(ID, 0);
                }
            }

            else
            {
                Objlist = objService.GetAptFollowups(ID, 2);
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            HttpContext.Current.Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        return Objlist;
    }
    #endregion
}