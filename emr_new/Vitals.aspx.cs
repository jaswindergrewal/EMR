using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder on 14th aug 2013
/// </summary>
public partial class Vitals : System.Web.UI.Page
{
    #region "variable"
    public string PatientID;
    IPatientVitalsService objService = null;
    #endregion
    #region "Events"
    //set value of patientid
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null)
        {
            PatientID = Request.QueryString["PatientID"];
            HDPatientID.Value = PatientID;
        }
        else
        {
            Response.Redirect("landingpage.aspx");
        }
    }
    #endregion
    #region "Methods"
    /// <summary>
    /// Method to get the vital details for the patient
    /// Jaswinder 14th aug 2013
    /// </summary>
    /// <param name="PatientID"></param>s
    /// <returns></returns>
    [WebMethod]
    public static List<PatientVitalsViewModel> BindVitalDetails(int PatientID)
    {
        IPatientVitalsService objService = null;
        List<PatientVitalsViewModel> lstVital = null;
        try
        {
            objService = new PatientVitalsService();
            lstVital = objService.GetPatientVitalsByPatientId(PatientID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstVital;
    }
    /// <summary>
    /// Insert/update the vital detail of the patient in the database
    /// jaswinder 14th aug 2013
    /// </summary>
    /// <param name="txtweight"></param>
    /// <param name="txtbloodPres"></param>
    /// <param name="txttempr"></param>
    /// <param name="txtpulse"></param>
    /// <param name="txtwaistcir"></param>
    /// <param name="txthipcirm"></param>
    /// <param name="txtheight"></param>
    /// <param name="txtLeftGrip"></param>
    /// <param name="txtRightGrip"></param>
    /// <param name="PatientID"></param>
    /// <returns></returns>
    [WebMethod]
    public static int InsertUpdateVitals(string txtweight, string txtbloodPres, string txttempr, string txtpulse, string txtwaistcir, string txthipcirm, string txtheight, string txtLeftGrip, string txtRightGrip, string PatientID, int VitalID)
    {
        int result = 0;
        IPatientVitalsService objService = null;
        PatientVitalsViewModel vitalViewModel = new PatientVitalsViewModel();
        if (txtheight != "")
        {
            vitalViewModel.Height = Decimal.Parse(txtheight);
        }
        vitalViewModel.grip_l_lbs = txtLeftGrip;
        vitalViewModel.grip_r_lbs = txtRightGrip;
        vitalViewModel.Hip_Circm = txthipcirm;
        vitalViewModel.Patient_ID = int.Parse(PatientID);
        vitalViewModel.Pulse = txtpulse;
        vitalViewModel.Temperature = txttempr;
        vitalViewModel.Waist_Circm = txtwaistcir;
        if (txtweight != "")
        {
            vitalViewModel.Wgt = Decimal.Parse(txtweight);
        }
        vitalViewModel.BloodPres = txtbloodPres;
        vitalViewModel.DateEntered = DateTime.Now;
        objService = new PatientVitalsService();
        try
        {
            if (VitalID == 0)
            {
                result = objService.InsertPatientVitalDetails(vitalViewModel);
            }
            else
            {
                vitalViewModel.Vital_ID = VitalID;
                result = objService.UpdatePatientVitals(vitalViewModel, VitalID);
            }
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
    /// <summary>
    /// Method to fetch vital detail by vital id
    /// surabhi purohit 13th nov 2013
    /// </summary>
    /// <param name="vitalID"></param>
    /// <returns></returns>
    [WebMethod]
    public static PatientVitalsViewModel GetVitalsByVitalId(int vitalID)
    {
        IPatientVitalsService objService = null;
        var lstVitals = new PatientVitalsViewModel();
        try
        {
            objService = new PatientVitalsService();
            lstVitals = objService.GetPatientVitalsByVitalId(vitalID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstVitals;
    }
    /// <summary>
    /// Delete patient vital by vital id
    /// surabhi purohit 13th nov 2013
    /// </summary>
    /// <param name="ID"></param>
    [WebMethod]
    public static void DeleteVitalsByID(int ID)
    {
        IPatientVitalsService objService = null;
        try
        {
            objService = new PatientVitalsService();
            objService.DeleteVitalsByID(ID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion
}

