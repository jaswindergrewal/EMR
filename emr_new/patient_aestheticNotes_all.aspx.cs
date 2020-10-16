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
/// Jaswinder on 4th sep 2013
/// </summary>
public partial class patient_aestheticNotes_all : System.Web.UI.Page
{
    #region Variables
    IAllergieService objService = null;
    protected int PatientID = 0;
    #endregion

    #region "Events"

    /// <summary>
    /// Bind the grid on pageload
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
                    List<AestheticNotesViewModel> objList = objService.GetAestheticNotesALL(PatientID);
                    if (objList != null)
                    {
                        grdAestheticNotes.DataSource = objList;
                        grdAestheticNotes.DataBind();
                        divTitleText.InnerHtml = " <p class='PageTitle'>Aesthetic Notes for " + objList[0].FirstName + " &nbsp;" + objList[0].LastName + " [<a href='patientInfo.aspx?patientid=" + objList[0].PatientID + "'>Back to Patient Details</a>] </p>";
                    }
                }
                else
                {
                    divTitleText.InnerHtml = " <p class='PageTitle'>No Aesthetic Notes [<a href='patientInfo.aspx?patientid=" + Request.QueryString["PatientID"] + "'>Back to Patient Details</a>] </p>";
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