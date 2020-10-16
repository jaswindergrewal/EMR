using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of asp to aspx
/// 21st aug 2013
/// </summary>

public partial class intake_form_allergies : System.Web.UI.Page
{
    #region"events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Submit the allery details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        IntakeService objService = null;
        try
        {
            objService = new IntakeService();
            bool drug_or_med_allergy_YN = false;
            bool food_allergy_YN = false;

            int MasterId;
            if (Request.QueryString["form_id"] == null)
            {
                MasterId = 0;
            }
            else
            {
                MasterId = int.Parse(Request.QueryString["form_id"]);
            }

            if (rdldrug_or_med_allergy_YN.SelectedValue == "1")
            {
                drug_or_med_allergy_YN = true;
            }

            objService.InsertIntakeAllergy(int.Parse(Request.QueryString["patientid"]), MasterId, txtdrug_or_med_allergy.Text, txtfood_allergy.Text, drug_or_med_allergy_YN,
                                                food_allergy_YN);

            Response.Redirect("intake_form_recent_tests.aspx?patientid=" + Request.QueryString["patientid"] + "&form_id=" + MasterId,false);
          
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