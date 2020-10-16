using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class AestheticNotes : LMCBase
{
    #region Variables
    IAllergieService objService = null;
	protected int PatientID = 0;
    #endregion

    #region Events
    /// <summary>
    /// Get the Anesthetic notes for the patient and bind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["PatientID"]) != null)
                {
                    PatientID = int.Parse(Request.QueryString["PatientID"]);
                    objService = new AllergieService();
                    rptNotes.DataSource = objService.GetAestheticNotes(PatientID);
                    rptNotes.DataBind();
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }

    }
    #endregion
}