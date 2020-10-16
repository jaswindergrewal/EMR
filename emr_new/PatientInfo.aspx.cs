using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class PatientInfo : LMCBase
{
    #region "variables"
    protected PatientViewModel pat=new PatientViewModel();
    IPatientService objService = null;
	protected int? AGE = null;
	protected int PatientID = 0;

    #endregion

    #region "Event"

    /// <summary>
    /// Show the patient details with its events on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        if (Request.QueryString["PatientID"] != null)
        {
            PatientID = int.Parse(Request.QueryString["PatientID"]);
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["NewPhoto"] == "true")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('Photo uploaded successfully.') </script>");
                    }
                    if (PatientID != 0)
                    {
                        objService = new PatientService();
                        pat = objService.GetPatientDataById(PatientID);

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
        else
        {
            Response.Redirect("~/Landingpage.aspx");
        }
    }

    #endregion

   
}