using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class Patient_Add : LMCBase
{
    #region "Variables"
    IAppointmentConsole objAppointmentConsoleService = null;
    IProviderService objProviderService = null;
    ICRMEventsService objCRMEventsService = null;
    IPatientService objPatientService = null;
    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        chkCopyAddress.Attributes.Add("onclick", "javascript:showHideDropDowns();");
        if (!IsPostBack)
        {

            txtHippaSignedDate.Attributes.Add("readonly", "readonly");
            txtMedicareOptoutDate.Attributes.Add("readonly", "readonly");

            BindDropDownControls();
            txtFirstName.Text = Request.QueryString["FirstName"];
            txtLastName.Text = Request.QueryString["LastName"];
            if (Request.QueryString["HomePhone"] != "")
             txtHomephone.Text = String.Format("{0:###-###-####}", Convert.ToInt64(Request.QueryString["HomePhone"]));
            txtMiddleInitial.Text = Request.QueryString["MI"];
        }
    }
    /// <summary>
    /// addind the patient details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            objAppointmentConsoleService = new AppointmentConsole();
            DateTime? BirthDate;
            if (txtBirthday.Text != "")
            { 
            BirthDate=DateTime.Parse(txtBirthday.Text);
            }
            else{
            BirthDate=null;
            }

            DateTime? RenewalDate;
            if (txtRenewalDate.Text != "")
            {
                RenewalDate = DateTime.Parse(txtRenewalDate.Text);
            }
            else
            {
                RenewalDate = null;
            }
            PatientViewModel objPatientViewModel = null;
            
            objPatientViewModel = new PatientViewModel();
            objPatientViewModel.Aesthetics = cbAesthetics.Checked;
            objPatientViewModel.Autoship = cbAutoship.Checked;
            objPatientViewModel.ShippingCity = txtShipCity.Text;
            objPatientViewModel.ShippingState = ddlShipState.SelectedValue;
            objPatientViewModel.ShippingStreet = txtShipAddress.Text;
            objPatientViewModel.ShippingZip = txtShipZip.Text;
            if (chkCopyAddress.Checked == true)
            {
                objPatientViewModel.BillingCity = txtShipCity.Text; 
                objPatientViewModel.BillingState = ddlShipState.SelectedValue;
                objPatientViewModel.BillingStreet = txtShipAddress.Text;
                objPatientViewModel.BillingZip = txtShipZip.Text;
            }
            else
            {
                objPatientViewModel.BillingCity = txtBillCity.Text;
                objPatientViewModel.BillingState = ddlBillState.SelectedValue;
                objPatientViewModel.BillingStreet = txtBillAddress.Text;
                objPatientViewModel.BillingZip = txtBillZip.Text;
            }
            objPatientViewModel.Birthday = BirthDate;
            objPatientViewModel.RenewalDate = RenewalDate;
            objPatientViewModel.Cell_CB_Only = cbCell_cbo.Checked;
            objPatientViewModel.Cell_Detailed_info = cbCell_detailed.Checked;
            objPatientViewModel.Cell_NoMessage = cbCell_NoMessage.Checked;
            objPatientViewModel.CellPhone = txtCellphone.Text;
            objPatientViewModel.Clinic = ddlClinic.SelectedValue;
            objPatientViewModel.ConciergeID = ddlConcierge.SelectedValue;
            if (ddlManagementProgram.SelectedValue == "None")
            {
                objPatientViewModel.PatientManagementProgramId = 0;
            }
            else
            {
                objPatientViewModel.PatientManagementProgramId = Convert.ToInt32(ddlManagementProgram.SelectedValue);
            }
            objPatientViewModel.ContactPreference = txtContact_pref.Text;
            objPatientViewModel.EatingPlanReceived_YN = cbEatingPlan.Checked;
            objPatientViewModel.Email = txtEmail.Text;
            objPatientViewModel.Email_auth_detailed_info = cbEmail_detailed.Checked;
            objPatientViewModel.EmergencyFirstName = txtEmerFirst.Text;
            objPatientViewModel.EmergencyLastName = txtEmerLast.Text;
            objPatientViewModel.EmergencyPhone = txtEmerPhone.Text;
            objPatientViewModel.EmergencyRelationship = txtEmerRelat.Text;
            objPatientViewModel.Fax_auth_detailed_info = cbFax_detailed.Checked;
            objPatientViewModel.FaxPone = txtFaxphone.Text;
            objPatientViewModel.FirstName = txtFirstName.Text;
            objPatientViewModel.HIPPA_signed = cbHippa_signed_yn.Checked;
            objPatientViewModel.HIPPA_signed_date = txtHippaSignedDate.Text != "" ? DateTime.Parse(txtHippaSignedDate.Text) : (DateTime?)null;
            objPatientViewModel.Sex = txtGender.Text;
            objPatientViewModel.Home_CB_only = cbHome_cbo.Checked;
            objPatientViewModel.Home_detailed_info = cbHome_detailed.Checked;
            objPatientViewModel.Home_NoMessage = cbHome_NoMessage.Checked;
            objPatientViewModel.HomePhone = txtHomephone.Text;
            objPatientViewModel.Inactive = false;
            objPatientViewModel.LastName = txtLastName.Text;
            objPatientViewModel.LMC_CP = ddlMLCPhysician.SelectedItem.Text;
            if (ddlMLCPhysician.SelectedItem.Text == "None")
                objPatientViewModel.ProvID = 0;
            else
                objPatientViewModel.ProvID = int.Parse(ddlMLCPhysician.SelectedValue);
            objPatientViewModel.Marketing_source = int.Parse(ddlMark_source.SelectedValue);
            objPatientViewModel.Medical = cbMedical.Checked;
            objPatientViewModel.MedicareB = cbMedicareB.Checked;
            objPatientViewModel.MedicareOptOut_Date = txtMedicareOptoutDate.Text != "" ? DateTime.Parse(txtMedicareOptoutDate.Text) : (DateTime?)null;
            objPatientViewModel.MedicareOptOut_YN = cbMedicare_opt.Checked;
            objPatientViewModel.MiddleInitial = txtMiddleInitial.Text;
            objPatientViewModel.Nickname = txtNickName.Text;
            objPatientViewModel.PCP = txtPCP.Text;
            objPatientViewModel.Prefered_Pharm = txtPreferedPharm.Text;
            objPatientViewModel.Retail = cbRetail.Checked;
           
            objPatientViewModel.Work_CB_only = cbWork_cbo.Checked;
            objPatientViewModel.Work_Detailed_info = cbWork_detailed.Checked;
            objPatientViewModel.Work_NoMessage = cbWork_NoMessage.Checked;
            objPatientViewModel.EmergencyContact = txtEmerFirst.Text + " " + txtEmerLast.Text;
            objPatientViewModel.WorkPhone = txtWorkphone.Text;
            objPatientViewModel.AllowApptReassign = false;
            objPatientViewModel.DiabetesSOC = false;
            objPatientViewModel.HeartSOC = false;
            objPatientViewModel.AutoshipNote = "";
            objPatientViewModel.AutoshipAlerts = "";
            objPatientViewModel.AutoshipEmail = false;
            objPatientViewModel.Cancel_NoShow_frm_signed = false;
            objPatientViewModel.NameAlert = false;
            objPatientViewModel.LabsMailed = cbLabMailed.Checked;
            objPatientViewModel.Affiliate = cbAffiliate.Checked;
            if (cbAffiliate.Checked)
            {
                objPatientViewModel.AffiliateID = int.Parse(ddlAffiliate.SelectedValue);
            }
            if (ddlAffiliate.SelectedIndex != 0)
            {
                try
                {
                    objPatientViewModel.AffiliateID = int.Parse(ddlAffiliate.SelectedValue);
                    objPatientViewModel.AffiliateDate = DateTime.Now;
                }
                catch { }
            }
            objPatientViewModel.IsAffiliate = cboAffiliate.Checked;
            objPatientViewModel.AuthorisedPerson = txtAuthorisedPerson.Text;

            objPatientService = new PatientService();
            int patientId=objPatientService.InsertPatientDetails(objPatientViewModel);
           // Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + patientId.ToString(),false);
            Response.Redirect("LandingPAge.aspx", false);
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }

    [System.Web.Services.WebMethod]
    public static bool GetDuplicatepatient(string firstName, string lastName, string middleInitial, int patientId)
    {

        IAppointmentConsole objAppointmentConsoleService = new AppointmentConsole();
        PatientViewModel objPatientViewModel = null;
        bool result = false;
        
        try
        {
            objPatientViewModel = new PatientViewModel();

            objPatientViewModel = objAppointmentConsoleService.GetPatientListByCriteria(firstName, lastName, middleInitial, patientId);
            if (objPatientViewModel != null)
            {
                result = true;
            }

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPatientViewModel = null;
        }
        return result;
    }

    /// <summary>
    /// cancel add
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }
    #endregion

    #region "Methods"

    private void BindDropDownControls()
    {
        try
        {
            BindClinic();
            BindddlConcierge();
            BindddlMLCPhysician();
            BindddlAffiliate();
            BindddlMark_source();
            BindManagementProgram();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// bind dropdown for Concierge
    /// </summary>
    private void BindddlConcierge()
    {
        List<StaffViewModel> lstStaffViewModel = null;
        try
        {
            IStaffService objStaffService = new StaffService();
            lstStaffViewModel = new List<StaffViewModel>();
            lstStaffViewModel = objStaffService.GetStaff().Where(p => p.Active_YN == true && p.IsHARep == true).ToList();

            ddlConcierge.DataSource = lstStaffViewModel;

            ddlConcierge.DataSource = lstStaffViewModel;
            ddlConcierge.DataTextField = "EmployeeName";
            ddlConcierge.DataValueField = "EmployeeID";
            ddlConcierge.DataBind();
            ddlConcierge.Items.Insert(0, new ListItem("None","0"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstStaffViewModel = null;
            objAppointmentConsoleService = null;
        }
    }

    private void BindManagementProgram()
    {
        List<ManagementProgramViewModel> lstMgtProgramViewModel = null;
        try
        {
            IRenewalPackagesService objService = new RenewalPackagesService();
            lstMgtProgramViewModel = new List<ManagementProgramViewModel>();
            lstMgtProgramViewModel = objService.GetManagementPrograms().Where(p => p.IsActive == true).ToList();

            ddlManagementProgram.DataSource = lstMgtProgramViewModel;
            ddlManagementProgram.DataTextField = "ProgramName";
            ddlManagementProgram.DataValueField = "Id";
            ddlManagementProgram.DataBind();
            ddlManagementProgram.Items.Insert(0, new ListItem("None"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstMgtProgramViewModel = null;
          
        }
    }

    /// <summary>
    /// bind dropdown for LMC Physician
    /// </summary>
    private void BindddlMLCPhysician()
    {
        List<ProviderViewModel> lstProviderViewModel = null;
        try
        {
            objProviderService = new ProviderService();
            lstProviderViewModel = new List<ProviderViewModel>();
            lstProviderViewModel = objProviderService.GetProviderDetails().Where(p => p.Active == true && p.External == false).OrderBy(p => p.ProviderName).ToList();

            ddlMLCPhysician.DataSource = lstProviderViewModel;
            ddlMLCPhysician.DataTextField = "ProviderName";
            ddlMLCPhysician.DataValueField = "id";
            ddlMLCPhysician.DataBind();
            ddlMLCPhysician.Items.Insert(0, new ListItem("None"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objProviderService = null;
            lstProviderViewModel = null;
        }
    }

    /// <summary>
    /// bind dropdown for Affiliate Name
    /// </summary>
    private void BindddlAffiliate()
    {
        List<ResellersViewModel> lstStaffViewModel = null;
        try
        {
            objAppointmentConsoleService = new AppointmentConsole();
            lstStaffViewModel = new List<ResellersViewModel>();
            lstStaffViewModel = objAppointmentConsoleService.GetRellersDetails();

            ddlAffiliate.DataSource = lstStaffViewModel;
            ddlAffiliate.DataTextField = "BusinessName";
            ddlAffiliate.DataValueField = "ResellerID";
            ddlAffiliate.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstStaffViewModel = null;
            objAppointmentConsoleService = null;
        }
    }

    /// <summary>
    /// bind dropdown for Event
    /// </summary>
    private void BindddlMark_source()
    {
        List<CRMEventsViewModel> lstCRMEventsViewModel = null;
        try
        {
            objCRMEventsService = new CRMEventsService();
            lstCRMEventsViewModel = new List<CRMEventsViewModel>();
            lstCRMEventsViewModel = objCRMEventsService.GetCRMEventsDetails().OrderBy(p => p.EventName).ToList();

            ddlMark_source.DataSource = lstCRMEventsViewModel;
            ddlMark_source.DataTextField = "EventName";
            ddlMark_source.DataValueField = "EventID";
            ddlMark_source.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstCRMEventsViewModel = null;
            objCRMEventsService = null;
        }
    }

    /// <summary>
    /// Added by jaswinder to bind the clinic from database
    /// 4th sept 2013
    /// </summary>
    public void BindClinic()
    {
        try
        {
            objPatientService = new PatientService();
            ddlClinic.DataSource = objPatientService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("Select a clinic"));
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPatientService = null;
        }
    }
    #endregion
}
