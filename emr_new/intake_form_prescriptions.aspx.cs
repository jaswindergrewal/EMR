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
/// Jaswinder 26th Aug 2013
/// </summary>

public partial class intake_form_prescriptions : System.Web.UI.Page
{
    #region "variable"
    public string PatientID;
    public string FormID;
    IIntakeService objService = null;

    #endregion

    #region "Events"

    /// <summary>
    /// bind the drug list at run time
    /// Jaswinder 26th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["patientid"] != null)
            {
                PatientID = Request.QueryString["patientid"];


                if (Request.QueryString["form_id"] != null)
                {
                    FormID = Request.QueryString["form_id"];
                }

                objService = new IntakeService();

                ddlDrug.DataSource = objService.GetDrugList();
                ddlDrug.DataTextField = "DrugName";
                ddlDrug.DataValueField = "DrugID";
                ddlDrug.DataBind();
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

    /// <summary>
    /// Redirect page to suppliment page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("intake_form_supplements.aspx?patientid=" + PatientID + "&form_id=" + FormID);
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Bind the data for prescriptions
    /// </summary>
    /// <param name="PatientID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<IntakePrescriptionViewModel> BindData(int PatientID)
    {
        IIntakeService objService = null;
        List<IntakePrescriptionViewModel> lstIntakeTest = null;
        try
        {
            objService = new IntakeService();
            lstIntakeTest = objService.GetPatientprescription(PatientID);
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
    /// Insert data for prescription
    /// </summary>
    /// <param name="DrugID"></param>
    /// <param name="medication"></param>
    /// <param name="dosage"></param>
    /// <param name="PatientID"></param>
    /// <param name="FormID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertPrescriptionDetails(int DrugID, string medication, string dosage, int PatientID, int FormID)
    {
        int result = 0;
        IIntakeService objService = null;

        try
        {
            IntakePrescriptionViewModel TestViewModel = new IntakePrescriptionViewModel();
            TestViewModel.drug_id = DrugID;
            TestViewModel.medication = medication;
            TestViewModel.patient_id = PatientID;
            TestViewModel.Date_Entered = DateTime.Now;
            TestViewModel.dosage = dosage;

            objService = new IntakeService();
            result = objService.InsertPrescriptionDetails(TestViewModel);

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