using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;

public partial class SpecialAttention : LMCBase
{
    protected int PatientID = 0;
    ISpecialAttentionService objService = null;

    /// <summary>
    /// Get the details of the special attentions given to the patient and bind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request.QueryString["PatientID"] != null)
            {
                PatientID = int.Parse(Request.QueryString["PatientID"]);
                if (!IsPostBack)
                {
                    objService = new SpecialAttentionService();
                    rptAttnetion.DataSource = objService.GetSpecialAttentionByPatientId(PatientID);
                    rptAttnetion.DataBind();
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

    /// <summary>
    /// Delete the paticular special attentions given to patient and again bind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptAttnetion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objService = new SpecialAttentionService();
            objService.DeleteSpecialAttentionFlag((int)e.Keys["SpecialAttentionID"]);
            rptAttnetion.DataSource = objService.GetSpecialAttentionByPatientId(int.Parse(Request.QueryString["PatientID"]));
            rptAttnetion.DataBind();
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