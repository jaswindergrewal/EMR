using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 26th aug 2013
/// </summary>
public partial class intake_form_surgeries : System.Web.UI.Page
{
    #region "variable"
    public string PatientID;
    public string FormID;

    #endregion

    #region"Events"

    /// <summary>
    /// Set the formid & patientid on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtSurgeryDate.Attributes.Add("readonly", "readonly");
            if (Request.QueryString["patientid"] != null)
            {
                PatientID = Request.QueryString["patientid"];
            }

            if (Request.QueryString["form_id"] != null)
            {
                FormID = Request.QueryString["form_id"];
            }
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }

    /// <summary>
    /// Redirect to intake_form_prescriptions form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("intake_form_prescriptions.aspx?patientid=" + PatientID + "&form_id=" + FormID);
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Bind sugery data
    /// </summary>
    /// <param name="PatientID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<IntakeSurgeryViewModel> BindData(int PatientID)
    {
        IIntakeService objService = null;
        List<IntakeSurgeryViewModel> lstIntakeTest = null;
        try
        {
            objService = new IntakeService();
            lstIntakeTest = objService.GetPatientRecentsurgeries(PatientID);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstIntakeTest;
    }


    /// <summary>
    /// insert surgery details
    /// </summary>
    /// <param name="surgery_name"></param>
    /// <param name="surgery_date"></param>
    /// <param name="surgery_reason"></param>
    /// <param name="PatientID"></param>
    /// <param name="FormID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertSurgeryDetails(string surgery_name, string surgery_date, string surgery_reason, int PatientID, int FormID)
    {
        int result = 0;
        IIntakeService objService = null;
        try
        {
            IntakeSurgeryViewModel TestViewModel = new IntakeSurgeryViewModel();
            TestViewModel.other_surgeries = surgery_name;
            TestViewModel.other_reason = surgery_reason;
            TestViewModel.patient_id = PatientID;
            TestViewModel.Date_Entered = DateTime.Now;
            TestViewModel.master_form_id = FormID;
            TestViewModel.other_date = Convert.ToDateTime(surgery_date);

            objService = new IntakeService();
            result = objService.InsertSurgeryDetails(TestViewModel);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return result;
    }

    #endregion

}