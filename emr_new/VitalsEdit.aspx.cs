using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class VitalsEdit : LMCBase
{
    protected int VitalID = 0;
    IPatientVitalsService objService = null;
    
    /// <summary>
    /// Get the details of the vitals corresponding to a particulat vitalid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["VitalID"] != null)
                {
                    VitalID = int.Parse(Request.QueryString["VitalID"]);
                    objService = new PatientVitalsService();
                    PatientVitalsViewModel theVital = objService.GetPatientVitalsByVitalId(VitalID);

                    if (theVital != null)
                    {
                        txtBloodPress.Text = theVital.BloodPres;
                        txtHeight.Text = theVital.Height.ToString();
                        txtHip.Text = theVital.Hip_Circm;
                        txtLeftGrip.Text = theVital.grip_l_lbs;
                        txtPulse.Text = theVital.Pulse;
                        txtRightGrip.Text = theVital.grip_r_lbs;
                        txtTemp.Text = theVital.Temperature;
                        txtWaist.Text = theVital.Waist_Circm;
                        txtWeight.Text = theVital.Wgt.ToString();
                    }
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

    }

    /// <summary>
    /// Update the details of the vital and save in the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            VitalID = int.Parse(Request.QueryString["VitalID"]);
            PatientVitalsViewModel theVital = new PatientVitalsViewModel();
            theVital.BloodPres = txtBloodPress.Text;
            theVital.Height = decimal.Parse(txtHeight.Text);
            theVital.Hip_Circm = txtHip.Text;
            theVital.grip_l_lbs = txtLeftGrip.Text;
            theVital.Pulse = txtPulse.Text;
            theVital.grip_r_lbs = txtRightGrip.Text;
            theVital.Temperature = txtTemp.Text;
            theVital.Waist_Circm = txtWaist.Text;
            theVital.Wgt = decimal.Parse(txtWeight.Text);
            theVital.Vital_ID = VitalID;

            objService = new PatientVitalsService();
            objService.UpdatePatientVitals(theVital, VitalID);
            Session["ActiveTab"] = "Vitals";
            Response.Redirect("apt_console.aspx?AptID=" + Request.QueryString["AptID"],false);
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