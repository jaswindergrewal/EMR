using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of asp to Aspx
/// Jaswinder 21st aug 2013
/// </summary>
public partial class intake_form_start : System.Web.UI.Page
{
    #region "Variables"
    IntakeService objService = null;
    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Submit the master form id in the database and redirct the page to personalinfo
    /// Jaswinder 21 aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["patientid"] != null)
            {
                objService = new IntakeService();

                int form_id = objService.InsertMasterFormIntake(int.Parse(Request.QueryString["patientid"]));
                Response.Redirect("intake_form_personal_info.aspx?patientid=" + Request.QueryString["patientid"] + "&form_id=" + form_id, false);
            }
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally { objService = null; }
    }
    #endregion

}