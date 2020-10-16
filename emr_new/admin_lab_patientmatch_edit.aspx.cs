using System;
using System.Collections.Generic;
using System.Web.UI;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Data.Entity.Validation;
public partial class admin_lab_patientmatch_edit : System.Web.UI.Page
{
    #region Global Variables/Objects
    ILabPatientsService objLabPatientsService = null;
    IPatientService objPatientService = null;
    LabPatientsViewModel objLabPatientsViewModel = null;
    #endregion

    #region Page_Load
    /// <summary>
    /// this is page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["labid"] != null && Request["labid"].ToString() != string.Empty)
            {
                BindLabQuestPatientMatchByLabID(Convert.ToInt32(Request["labid"].ToString()));
                BindPatientList();
            }
        }
    }

    private void BindPatientList()
    {
        objPatientService = new PatientService();
        IList<PatientViewModel> objPatientViewModel = objPatientService.GetPatientList();
        ddlPatientList.DataSource = objPatientViewModel;
        ddlPatientList.DataBind();
    }
    #endregion

    #region BindLabQuestPatientMatchByLabID
    /// <summary>
    /// this function used for bind lab_Patients details according the labID
    /// </summary>
    /// <param name="LabID"></param>
    /// Created Date : Rakesh Kumar
    /// Created Date : 4-Sep-2013
    private void BindLabQuestPatientMatchByLabID(int labid)
    {
        try
        {
            objLabPatientsService = new LabPatientsService();
            objLabPatientsViewModel = new LabPatientsViewModel();
            objLabPatientsViewModel = objLabPatientsService.LabQuestPatientMatchByLabID(labid);
            lblPatientNameFirstName.Text = objLabPatientsViewModel.PatientNameFirstName;
            lblPatientNameLastName.Text = objLabPatientsViewModel.PatientNameLastName;
            lblDateOfBirth.Text = Convert.ToDateTime(objLabPatientsViewModel.DateOfBirth).ToShortDateString();
            lblSex.Text = objLabPatientsViewModel.Sex;
            hfID.Value = objLabPatientsViewModel.ID.ToString();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objLabPatientsService = null;
            objLabPatientsViewModel = null;
        }
    }
    #endregion

    #region btnSubmit_Click
    /// <summary>
    /// this event used for update labpatient details.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objLabPatientsService = new LabPatientsService();
            objLabPatientsViewModel = new LabPatientsViewModel();
            objLabPatientsViewModel.ID = Convert.ToInt32(hfID.Value);
            objLabPatientsViewModel.PatientId = Convert.ToInt32(ddlPatientList.SelectedValue);
            int id = objLabPatientsService.UpdaterLabPatientDetails(objLabPatientsViewModel);
            if (id > 0)
            {
                //Page.ClientScript.RegisterStartupScript(typeof(Page), "Success", "alert('You have successfully added the record.');", true);
                Response.Redirect("admin_lab_patientmatch.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "Success", "alert('Something went wrong.');", true);
            }
        }
        catch (DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
        }
        finally
        {
            objLabPatientsService = null;
            objLabPatientsViewModel = null;
        }
    }
    #endregion
}