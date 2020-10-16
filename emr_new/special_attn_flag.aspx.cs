using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using NineRays.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 14 th aug 2013
/// </summary>
public partial class special_attn_flag : LMCBase
{
    #region "Variables"
    ISpecialAttentionService objService = null;
    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    /// <summary>
    /// Add the specialattention info in tables
    /// Jaswinder 14th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PatientID"] != null)
            {
                int StaffId = ((int)Session["StaffID"]);
                objService = new SpecialAttentionService();
                string theContent = ed.Content;
                objService.AddSpecialAttentionByPatientId(int.Parse(Request.QueryString["PatientID"]), theContent, StaffId);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                {
                    conn.Open();
                    string msg = "Special Attention:<br/>";
                    msg += theContent + "<br/>";
                    msg += "Added/Changed by: " + (string)Session["MM_Username"];

                    SqlCommand logItem = new SqlCommand("ssp_contact_tbl_AS_Insert", conn);
                    logItem.CommandType = CommandType.StoredProcedure;
                    logItem.Parameters.AddWithValue("@AptType", 63);
                    logItem.Parameters.AddWithValue("@PatientID", int.Parse(Request.QueryString["PatientID"]));
                    logItem.Parameters.AddWithValue("@MessageBody", msg);
                    logItem.Parameters.AddWithValue("@EmployeeID", StaffId);
                    logItem.ExecuteNonQuery();
                }
                Response.Redirect("SpecialAttention.aspx?PatientID=" + Request.QueryString["PatientID"].ToString(), false);
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
    /// on click of cancel redirect the page to patient info
    /// Jaswinder 14th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?patientID=" + Request.QueryString["PatientId"], false);
    }
    #endregion
}