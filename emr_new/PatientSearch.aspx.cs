using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class PatientSearch : LMCBase
{
    IPatientSearch objServicePatient = null;

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region "patient search on text change"
    /// <summary>
    /// event will be called whenever text will be changed and patient wil be searched accordingly
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextChanged(object sender, EventArgs e)
    {
        List<PatientViewModel> objPatientViewModel = null;
        try
        {
            objServicePatient = new PatientSearchList();
            objPatientViewModel = new List<PatientViewModel>();
            objPatientViewModel = objServicePatient.SearchPatientDetails(txtFirstName.Text, txtLastName.Text, txtMiddleInitial.Text, txtHomePhone.Text);
            lstResults.DataSource = objPatientViewModel;
            lstResults.DataBind();

            TextBox currentBox = (TextBox)sender;
            switch (currentBox.ID)
            {
                case "txtFirstName":
                    txtMiddleInitial.Focus();
                    break;
                case "txtMiddleInitial":
                    txtLastName.Focus();
                    break;
                case "txtLastName":
                    txtHomePhone.Focus();
                    break;
                case "txtHomePhone":
                    break;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServicePatient = null;
            objPatientViewModel = null;
        }
    }
     #endregion
    #region "redirects the control to manage page"
    /// <summary>
    /// redirects the control to manage the patient details taking its "ID" in query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstResults_SelectedIndexChanged(object sender, EventArgs e)
    {
      Response.Redirect("manage.aspx?PatientID=" + lstResults.SelectedValue);
    }
#endregion    
    protected void lstResults_SelectedIndexChanging(object sender, EventArgs e)
    {

    }
    #region "event to redirect the control to patient add page"
    /// <summary>
    /// values will be set in query string and control will be redirected to add patient details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patient_Add.aspx?FirstName=" + txtFirstName.Text + "&LastName=" + txtLastName.Text + "&HomePhone=" + txtHomePhone.Text + "&MI=" + txtMiddleInitial.Text);
    }
    #endregion
    #endregion

}