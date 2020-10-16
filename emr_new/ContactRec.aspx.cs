using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class _ContactRec : LMCBase
{
    #region "Variables"
    IAppointmentConsole objIAppointmentConsole = null;
    protected int PatientID = 0;
    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        PatientID = int.Parse(Request.QueryString["PatientID"]);

        inpPatientID.Value = PatientID.ToString();
        if (!IsPostBack)
        {
            BindPatientAndContactType();
        }
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            string content = ed.Content;
            objIAppointmentConsole.AddContactRecords(int.Parse(AptType.SelectedValue), PatientID, content, (int)Session["StaffID"], 0);
        }
        catch (System.Exception)
        {

            throw;
        }
       
        Response.Redirect("Contacts.aspx?patientID=" + PatientID.ToString());
    }

    #endregion

    #region "Methods"
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
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objIAppointmentConsole = new AppointmentConsole();
        }
    }
    #endregion
}
