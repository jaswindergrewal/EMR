using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Globalization;

public partial class intake_form_recent_tests : System.Web.UI.Page
{
    #region "variable"
    public string PatientID;
    public string FormID;

    #endregion

    #region "Variables"

    /// <summary>
    /// Page load method to set the formid and patient id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        test_date.Attributes.Add("readonly", "readonly");
        if (Request.QueryString["patientid"] != null)
        {
            PatientID = Request.QueryString["patientid"];

            if (Request.QueryString["form_id"] != null)
            {
                FormID = Request.QueryString["form_id"];
            }
        }
        else
        {
            Response.Redirect("LandingPage.aspx");
        }
    }

    /// <summary>
    /// Redirect page to suppliment page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("intake_form_habits.aspx?patientid=" + PatientID + "&form_id=" + FormID);
    }

    #endregion

    #region "Methods"

   
    /// <summary>
    /// Web service to bind the  recent test of patients
    /// </summary>
    /// <param name="PatientID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<IntakeTestViewModel> BindData(int PatientID)
    {
        IIntakeService objService = null;
        List<IntakeTestViewModel> lstIntakeTest = null;
        try
        {
            objService = new IntakeService();
            lstIntakeTest = objService.GetPatientRecentTestByPatientId(PatientID);
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
    /// insert the recent tests of patients
    /// </summary>
    /// <param name="test_name"></param>
    /// <param name="test_date"></param>
    /// <param name="test_reason"></param>
    /// <param name="test_result"></param>
    /// <param name="PatientID"></param>
    /// <param name="FormID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertTestResult(string test_name, string test_date, string test_reason, string test_result, int PatientID,int FormID)
    {
        int result = 0;
        IIntakeService objService = null;
        IntakeTestViewModel TestViewModel = new IntakeTestViewModel();
        TestViewModel.other_reason = test_reason;
        TestViewModel.other_result = test_result;
        TestViewModel.patient_id = PatientID;
        TestViewModel.Date_Entered = DateTime.Now;
        TestViewModel.master_form_id = FormID;
        TestViewModel.other_date = DateTime.ParseExact(test_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        TestViewModel.other_test = test_name;



        objService = new IntakeService();
      
        try
        {
            result = objService.InsertTestDetails(TestViewModel);

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