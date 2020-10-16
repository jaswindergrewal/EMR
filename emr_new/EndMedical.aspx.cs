using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class EndMedical : LMCBase
{
    #region Variable
    IEndMedicalService objService = null;
    int PatientID = 0;
    #endregion

    #region Event
    /// <summary>
    /// getting the patient id and setting the date in textbox on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtDate.Text = DateTime.Now.ToShortDateString();

            if (Request.QueryString["PatientID"] != null) PatientID = int.Parse(Request.QueryString["PatientID"]);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }


    protected void btnEnd_Click(object sender, EventArgs e)
    {
        if (cboAutoship.Checked)
        {
            lblInactive.Text = "Patient will be set to incative and Auto Ship cancelled.";
            lblQBActive.Text = "INACTIVE CHECKBOX: check the box since they will no longer purchase Auto Ship products.";
        }
        else
        {
            lblInactive.Text = "Patient will be remain active as an Auto Ship patient.";
            lblQBActive.Text = "Change the Customer Type to Retail.";
        }

        modWelcome.Show();
    }

    protected void btnNext1_Click(object sender, EventArgs e)
    {
        modQB.Show();
    }

    protected void btnNext2_Click(object sender, EventArgs e)
    {
        modEMR.Show();
    }
    protected void btnNext3_Click(object sender, EventArgs e)
    {
        try
        {
            //add contact record
            objService = new EndMedicalService();
            bool cboAutoshipchk;
            if (cboAutoship.Checked)
            {
                cboAutoshipchk = true;
            }
            else
            {
                cboAutoshipchk = false;
            }
            objService.CloseAppointments(56, PatientID, "Customer dropped Medical - Auto entry", (int)Session["StaffID"], 0, cboAutoshipchk, Convert.ToDateTime(txtDate.Text),txtReason.Text.Trim());

            modLast.Show();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }
    protected void btnNext4_Click(object sender, EventArgs e)
    {
        ShowMessage();
        //Response.Redirect("PatientInfo.aspx?PatientID=" + PatientID.ToString(), false);
    }

    private void ShowMessage()
    {
        // code for refresh the parent page after click on the OK button of alert message.
        string strScript = "if(!alert('You have successfully close the account.')); this.window.parent.location=this.window.parent.location;";
        if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
            ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", strScript, true);

        // code for redirect the page after click on the OK button of alert message.
        //System.Text.StringBuilder strScript = new System.Text.StringBuilder();
        //strScript.Append("<script language=JavaScript>");      
        //strScript.Append("if(!alert('You have successfully close the account.')); window.location.href = 'PatientInfo.aspx?PatientID=" + PatientID.ToString() + "';</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "Pop", strScript.ToString());
    }
    #endregion
}