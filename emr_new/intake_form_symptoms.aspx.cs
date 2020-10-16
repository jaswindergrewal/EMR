using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of asp to aspx
/// jaswinder 22nd aug 2013
/// </summary>
public partial class intake_form_symptoms : System.Web.UI.Page
{
    #region"variables"
    IIntakeService objService = null;
    #endregion

    #region"events"
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Submit the symptoms data
    /// jaswinder 22nd aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        int MasterId = 0;
        int PatientId = 0;
        try
        {
            if (Request.QueryString["patientid"] != null)
            {
                PatientId = int.Parse(Request.QueryString["patientid"]);
                if (Request.QueryString["form_id"] != null)
                {
                    MasterId = int.Parse(Request.QueryString["form_id"]);
                }
                List<String> chkStrList = new List<string>();

                // Loop through each item.
                foreach (ListItem item in chkSymptoms.Items)
                {
                    if (item.Selected)
                    {
                        // If the item is selected, add the value to the list.
                        chkStrList.Add(item.Value);
                    }
                    else
                    {
                        // Item is not selected, do something else.
                        chkStrList.Add("0");
                    }
                }

                objService = new IntakeService();

                // Join the string together using the , delimiter.
                String chkStr = String.Join(",", chkStrList.ToArray());
                objService.InsertIntakeSymptoms(PatientId, MasterId, chkStr, other_symptoms.Value);
                Response.Redirect("intake_form_surgeries.aspx?patientid=" + PatientId + "&form_id=" + MasterId, false);
            }
            else
            { Response.Redirect("LandingPage.aspx", false); }
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