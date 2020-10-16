using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of asp to aspx
/// jaswinder 23 rd aug 2013
/// </summary>
public partial class intake_form_supplements : System.Web.UI.Page
{
    
    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    /// <summary>
    /// .Submit the suppliment details for patients
    /// Jaswinder 23 aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        IIntakeService objService = null;
        try
        {
            if (Request.QueryString["patientid"] != null)
            {
                int PatientID = 0;
                PatientID = int.Parse(Request.QueryString["patientid"]);
                int FormID = 0;

                if (Request.QueryString["form_id"] != null)
                {
                    FormID = int.Parse(Request.QueryString["form_id"]);
                }

                objService = new IntakeService();
                objService.InsertIntakeSuppliments(PatientID, FormID, hdnChkSupplimentlist.Value, other_Suppliments.Value);
                Response.Redirect("intake_form_allergies.aspx?patientid=" + PatientID + "&form_id=" + FormID, false);
            }
            else
            {
                Response.Redirect("LandingPage.aspx", false);
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
    #endregion
}