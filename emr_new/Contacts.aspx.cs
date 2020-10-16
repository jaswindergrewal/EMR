
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class Contacts : System.Web.UI.Page
{
    #region "Variables"
    public static int PatientID = 0;
    public static int ContactID = 0;
    IAppointmentConsole objIAppointmentConsole = null;
    protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PatientID"] != null)
            {
                PatientID = int.Parse(Request.QueryString["PatientID"]);

            }

            if (!IsPostBack)
            {
                BindPatientAndContactType();
            }
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

    }



    /// <summary>
    /// Binds patients and contact types
    /// </summary>
    private void BindPatientAndContactType()
    {
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            List<Patient_Details_ViewModel> lstPatient = objIAppointmentConsole.GetPatientDetailListNew(PatientID);
            var varPatient = lstPatient.FirstOrDefault();
            lblPatientName.Text = varPatient.FirstName + " " + varPatient.LastName;

            // bind the list for ContactTypeTbl
            List<Contact_Type_tblViewModel> lstViewModel = objIAppointmentConsole.GetContactTypeTblList().Where(t => t.Viewable_yn == true).OrderByDescending(t => t.AptTypeDesc).ToList();

            AptType.DataSource = lstViewModel;
            AptType.DataTextField = "AptTypeDesc";
            AptType.DataValueField = "AptTypeID";
            AptType.DataBind();

            drpContacttype.DataSource = lstViewModel;
            drpContacttype.DataTextField = "AptTypeDesc";
            drpContacttype.DataValueField = "AptTypeID";
            drpContacttype.DataBind();
            drpContacttype.Items.Insert(0, new ListItem("", "0"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objIAppointmentConsole = new AppointmentConsole();
        }
    }


    /// <summary>
    /// Get contact details by contactid
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static ContactStaffPatientTypeViewModel GetContactDetails(int ContactID)
    {
        IAppointmentConsole objIAppointmentConsole = null;
        ContactStaffPatientTypeViewModel Contact = new ContactStaffPatientTypeViewModel();

        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            Contact = objIAppointmentConsole.GetContactFromMultipleTableByContactId(ContactID);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objIAppointmentConsole = null;
        }

        return Contact;

    }

    /// <summary>
    /// Get all contacts by patientid
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="CboAutoBox"></param>
    /// <param name="cboCalBox"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<Contact_tblViewModel> GetAllContactsByPatientID(int PID, int CboAutoBox, int cboCalBox, int PageSize, int PageIndex, int Contacttype, DateTime txtEventDate)
    {
        List<Contact_tblViewModel> viewModelContactTbl = new List<Contact_tblViewModel>();
        AppointmentConsole objIAppointmentConsole = new AppointmentConsole();
        //bool CboAuto = Convert.ToBoolean(CboAutoBox);
        //bool cboCal = Convert.ToBoolean(cboCalBox);
        try
        {
            decimal TotalRowCount = objIAppointmentConsole.CountContactRecords(PID, CboAutoBox, cboCalBox, PageSize, Contacttype, txtEventDate);
            viewModelContactTbl = objIAppointmentConsole.GetContactTblByPatientId(PID, CboAutoBox, cboCalBox, PageIndex, Contacttype, txtEventDate);
            if (viewModelContactTbl != null)
            {
                foreach (var item in viewModelContactTbl)
                {
                    item.RecordCount = Convert.ToInt32(TotalRowCount);
                    item.MessageBody = item.MessageBody.Replace("class=", "t=");
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return viewModelContactTbl;

    }

    /// <summary>
    /// Updates contact records
    /// </summary>
    /// <param name="content"></param>
    [WebMethod]
    public static void UpdateContactDetails(string content, int ContactID)
    {
        IAppointmentConsole objIAppointmentConsole = null;
        ContactStaffPatientTypeViewModel Contact = new ContactStaffPatientTypeViewModel();

        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            objIAppointmentConsole.UpdateMedicalNote(content, ContactID);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    /// <summary>
    /// Add contacts details
    /// </summary>
    /// <param name="content"></param>
    /// <param name="AptType"></param>
    /// <param name="PatientID"></param>
    [WebMethod]
    public static void AddContactDetails(string content, int AptType, int PatientID)
    {
        IAppointmentConsole objIAppointmentConsole = null;
        ContactStaffPatientTypeViewModel Contact = new ContactStaffPatientTypeViewModel();

        try
        {
            objIAppointmentConsole = new AppointmentConsole();

            objIAppointmentConsole.AddContactRecords(AptType, PatientID, content, (int)HttpContext.Current.Session["StaffID"], 0);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

}
