using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Data.SqlClient;
using System.Configuration;


public partial class Patient_Update : LMCBase
{
    #region "Variables"
    IQBCustMatchPatientService objIQBCustMatchPatientService = null;
    ICRMEventsService objICRMEventsService = null;
    IAppointmentConsole objAppointmentConsoleService = null;
    IProviderService objProviderService = null;
    IPatientService objIPatientService = null;

    #endregion

    #region "Events"
    /// <summary>
    /// get patient details based on pateint id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtBirthday.Attributes.Add("readonly", "readonly");
        txtHIPPASignedDate.Attributes.Add("readonly", "readonly");
        txtOptOutDate.Attributes.Add("readonly", "readonly");

        if (!IsPostBack)
        {
            //Added by jaswinder on 5th sept 2013

            txtPatientId.Text = Request.QueryString["PatientID"];
            objIQBCustMatchPatientService = new QBCustMatchPatientService();
            PatientViewModel pat = objIQBCustMatchPatientService.GetPatientDetailById(int.Parse(Request.QueryString["PatientID"]));

            objICRMEventsService = new CRMEventsService();
            List<CRMEventsViewModel> mktg = objICRMEventsService.GetCRMEventsDetails().OrderByDescending(p => p.EventDate).ToList();
            mktg.Insert(0, new CRMEventsViewModel { EventName = "None", EventID = -1, EventDate = DateTime.MinValue, Venue = "", CampaignID = 1, });

            ddlMktg.DataTextField = "EventName";
            ddlMktg.DataValueField = "EventID";
            ddlMktg.DataSource = mktg;
            ddlMktg.DataBind();

            BindddlConcierge();
            BindManagementProgram();
            BindddlMLCPhysician();
            BindClinic();
            BindddlAffiliate();
            txtXeroPatientId.Text = pat.XeropatientId.ToString();
            txtFirstName.Text = pat.FirstName;
            txtLastName.Text = pat.LastName;
            txtFaxPhone.Text = pat.FaxPone;
            txtBillAddress.Text = pat.BillingStreet;
            txtBillCity.Text = pat.BillingCity;
            txtBillZip.Text = pat.BillingZip;
            //txtBirthday.Text = pat.Birthday != null ? ((DateTime)pat.Birthday).ToShortDateString() : "";
            DateTime DOB = DateTime.Now;
            if (pat.Birthday != null)
            {
                DOB = ((DateTime)pat.Birthday);
            }
            txtBirthday.Text = pat.Birthday != null ? (DOB.ToString("MM/dd/yyyy")) : "";

            DateTime RenewalDate = DateTime.Now;
            if (pat.RenewalDate != null)
            {
                RenewalDate = ((DateTime)pat.RenewalDate);
            }
            txtRenewalDate.Text = pat.RenewalDate != null ? (RenewalDate.ToString("MM/dd/yyyy")) : "";
           
                chkCallBeforeShip.Checked = pat.CallBeforeShip != null ? (bool)pat.CallBeforeShip : false;
            
            txtCellPhone.Text = pat.CellPhone;
            txtContactPref.Text = pat.ContactPreference;
            txtEmail.Text = pat.Email;
            txtEmerFirst.Text = pat.EmergencyFirstName;
            txtEmerLast.Text = pat.EmergencyLastName;
            txtEmerPhone.Text = pat.EmergencyPhone;
            txtEmerRelat.Text = pat.EmergencyRelationship;
            txtFaxPhone.Text = pat.FaxPone;
            txtFirstName.Text = pat.FirstName;
            txtHIPPASignedDate.Text = pat.HIPPA_signed_date != null ? ((DateTime)pat.HIPPA_signed_date).ToShortDateString() : "";
            txtHomePhone.Text = pat.HomePhone;
            txtLastName.Text = pat.LastName;

            txtMiddleInitial.Text = pat.MiddleInitial;
            txtNickName.Text = pat.Nickname;
            txtOptOutDate.Text = pat.MedicareOptOut_Date != null ? ((DateTime)pat.MedicareOptOut_Date).ToShortDateString() : "";
            txtPCP.Text = pat.PCP;
            txtPreferredPharm.Text = pat.Prefered_Pharm;
            txtShipAddress.Text = pat.ShippingStreet;
            txtShipCity.Text = pat.ShippingCity;
            txtShipZip.Text = pat.ShippingZip;
            txtWorkPhone.Text = pat.WorkPhone;
            ddlBillState.SelectedValue = pat.BillingState;
            ddlClinic.SelectedValue = pat.Clinic;
            ddlConcierge.SelectedValue = pat.ConciergeID;
            ddlManagementProgram.SelectedValue = Convert.ToString(pat.PatientManagementProgramId);
            ddlLMCPhysician.SelectedValue = pat.ProvID.ToString();
            ddlGender.SelectedValue = pat.Sex;
            ddlMktg.SelectedValue = pat.Marketing_source.ToString();
            ddlShipState.SelectedValue = pat.ShippingState;
            cbAesthetics.Checked = pat.Aesthetics;
            cbAutoship.Checked = pat.Autoship;
            cbMedical.Checked = pat.Medical;
            cboCell_cbo.Checked = pat.Cell_CB_Only != null ? (bool)pat.Cell_CB_Only : false;
            cboCell_detailed.Checked = pat.Cell_Detailed_info != null ? (bool)pat.Cell_Detailed_info : false;
            cboCell_NoMessage.Checked = pat.Cell_NoMessage != null ? (bool)pat.Cell_NoMessage : false;
            cboEatingplan.Checked = pat.EatingPlanReceived_YN;
            cboEmail_detailed.Checked = pat.Email_auth_detailed_info != null ? (bool)pat.Email_auth_detailed_info : false; ;
            cboFax_detailed.Checked = pat.Fax_auth_detailed_info != null ? (bool)pat.Fax_auth_detailed_info : false;
            switch (pat.SOC)
            {
                case null:
                    rdoSOC.SelectedValue = "None";
                    break;
                case true:
                    rdoSOC.SelectedValue = "Yes";
                    break;
                case false:
                    rdoSOC.SelectedValue = "No";
                    break;
            }
            cboHippa_signed.Checked = pat.HIPPA_signed != null ? (bool)pat.HIPPA_signed : false;
            cboHome_cbo.Checked = pat.Home_CB_only != null ? (bool)pat.Home_CB_only : false;
            cboHome_detailed.Checked = pat.Home_detailed_info != null ? (bool)pat.Home_detailed_info : false;
            cboHome_NoMessage.Checked = pat.Home_NoMessage != null ? (bool)pat.Home_NoMessage : false;
            cboInactive.Checked = pat.Inactive != null ? (bool)pat.Inactive : false;
            cboMedicare_opt.Checked = pat.MedicareOptOut_YN != null ? (bool)pat.MedicareOptOut_YN : false;
            cboNamealert.Checked = false;
            cboNoShow.Checked = pat.NoShowPol_Signed_YN != null ? (bool)pat.NoShowPol_Signed_YN : false;
            cboWork_cbo.Checked = pat.Work_CB_only != null ? (bool)pat.Work_CB_only : false;
            cboWork_detailed.Checked = pat.Work_Detailed_info != null ? (bool)pat.Work_Detailed_info : false;
            cboWork_NoMessage.Checked = pat.Work_NoMessage != null ? (bool)pat.Work_NoMessage : false;
            cboMedicareB.Checked = pat.MedicareB != null ? (bool)pat.MedicareB : false;
            if (pat.AffiliateID != (int?)null)
                ddlAffiliate.SelectedValue = pat.AffiliateID.ToString();
            else
                ddlAffiliate.SelectedValue = "0";

            cbRetail.Checked = pat.Retail;
            cboAffiliate.Checked = pat.IsAffiliate;
            cbLabMailed.Checked = pat.LabsMailed;
            txtChinapatientid.Text = pat.ChinaPatientId != null ? pat.ChinaPatientId.ToString() : string.Empty;
            txtbillCountry.Text = pat.BillingCountry;
            txtShipCountry.Text = pat.ShippingCountry;
            txtAuthorisedPerson.Text = pat.AuthorisedPerson != null ? pat.AuthorisedPerson : string.Empty;

        }
    }

    //private void ShowValidateMessage()
    //{
    //    System.Text.StringBuilder strScript = new System.Text.StringBuilder();
    //    strScript.Append("<script language=JavaScript>");
    //    strScript.Append("alert('You can not enter future date in birthday.')</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", strScript.ToString());
    //    txtBirthday.Focus();
    //}

    /// <summary>
    /// update the patient details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objIQBCustMatchPatientService = new QBCustMatchPatientService();
        PatientViewModel pat1 = objIQBCustMatchPatientService.GetPatientDetailById(int.Parse(Request.QueryString["PatientID"]));

        try
        {
            //if (cboInactive.Checked)
            //{
            //    if ((from p in ctx.ProfileItems where p.PatientID == int.Parse(Request.QueryString["PatientID"]) select p).Count() > 0) return;
            //}

            pat1.Aesthetics = cbAesthetics.Checked;
            pat1.Autoship = cbAutoship.Checked;
            pat1.BillingCity = txtBillCity.Text;
            pat1.BillingState = ddlBillState.SelectedValue;
            pat1.BillingStreet = txtBillAddress.Text;
            pat1.BillingZip = txtBillZip.Text;
            pat1.Birthday = txtBirthday.Text == "" ? (DateTime?)null : DateTime.Parse(txtBirthday.Text);
            pat1.RenewalDate = txtRenewalDate.Text == "" ? (DateTime?)null : DateTime.Parse(txtRenewalDate.Text);
            pat1.Cancel_NoShow_frm_signed = cboNoShow.Checked;
            pat1.Cell_CB_Only = cboCell_cbo.Checked;
            pat1.Cell_Detailed_info = cboCell_detailed.Checked;
            pat1.Cell_NoMessage = cboCell_NoMessage.Checked;
            pat1.CellPhone = txtCellPhone.Text;
            pat1.Clinic = ddlClinic.SelectedValue;
            pat1.ConciergeID = ddlConcierge.SelectedValue;
            pat1.ContactPreference = txtContactPref.Text;
            pat1.EatingPlanReceived_YN = cboEatingplan.Checked;
            pat1.Email = txtEmail.Text;
            pat1.Email_auth_detailed_info = cboEmail_detailed.Checked;
            pat1.EmergencyFirstName = txtEmerFirst.Text;
            pat1.EmergencyLastName = txtEmerLast.Text;
            pat1.EmergencyPhone = txtEmerPhone.Text;
            pat1.EmergencyRelationship = txtEmerRelat.Text;
            pat1.Fax_auth_detailed_info = cboFax_detailed.Checked;
            pat1.FaxPone = txtFaxPhone.Text;
            pat1.FirstName = txtFirstName.Text;
            if (rdoSOC.SelectedValue == "No") pat1.SOC = false;
            if (rdoSOC.SelectedValue == "Yes") pat1.SOC = true;
            if (rdoSOC.SelectedValue == "None") pat1.SOC = null;
            pat1.HIPPA_signed = cboHippa_signed.Checked;
            pat1.HIPPA_signed_date = txtHIPPASignedDate.Text == "" ? (DateTime?)null : DateTime.Parse(txtHIPPASignedDate.Text);
            pat1.Home_CB_only = cboHome_cbo.Checked;
            pat1.Home_detailed_info = cboHome_detailed.Checked;
            pat1.Home_NoMessage = cboHome_NoMessage.Checked;
            pat1.HomePhone = txtHomePhone.Text;
            pat1.Inactive = cboInactive.Checked;
            pat1.LastName = txtLastName.Text;
            pat1.LastUpdated = DateTime.Now;
            pat1.LMC_CP = ddlLMCPhysician.SelectedItem.Text;
            if (ddlLMCPhysician.SelectedItem.Text == "None")
                pat1.ProvID = 0;
            else
                pat1.ProvID = int.Parse(ddlLMCPhysician.SelectedValue);
            pat1.Marketing_source = int.Parse(ddlMktg.SelectedValue);
            pat1.Medical = cbMedical.Checked;
            pat1.MedicareOptOut_Date = txtOptOutDate.Text == "" ? (DateTime?)null : DateTime.Parse(txtOptOutDate.Text);
            pat1.MedicareOptOut_YN = cboMedicare_opt.Checked;
            pat1.MiddleInitial = txtMiddleInitial.Text;
            pat1.NameAlert = cboNamealert.Checked;
            pat1.Nickname = txtNickName.Text;
            pat1.NoShowPol_Signed_YN = cboNoShow.Checked;
            pat1.PCP = txtPCP.Text;
            pat1.Prefered_Pharm = txtPreferredPharm.Text;
            pat1.Retail = cbRetail.Checked;
            pat1.Sex = ddlGender.SelectedValue;
            pat1.ShippingCity = txtShipCity.Text;
            pat1.ShippingState = ddlShipState.SelectedValue;
            pat1.ShippingStreet = txtShipAddress.Text;
            pat1.ShippingZip = txtShipZip.Text;
            pat1.Work_CB_only = cboWork_cbo.Checked;
            pat1.Work_Detailed_info = cboWork_detailed.Checked;
            pat1.WorkPhone = txtWorkPhone.Text;
            pat1.Work_NoMessage = cboWork_NoMessage.Checked;
            pat1.MedicareB = cboMedicareB.Checked;
            if (ddlAffiliate.SelectedValue != "0")
            {
                pat1.AffiliateID = int.Parse(ddlAffiliate.SelectedValue);
                if (pat1.AffiliateDate == null)
                {
                    pat1.AffiliateDate = DateTime.Now;
                }
            }
            else
            {
                pat1.AffiliateID = (int?)null;
                pat1.AffiliateDate = null;
            }
            pat1.IsAffiliate = cboAffiliate.Checked;
            pat1.LabsMailed = cbLabMailed.Checked;
            pat1.ChinaPatientId = txtChinapatientid.Text == "" ? (int?)null : int.Parse(txtChinapatientid.Text);
            pat1.ShippingCountry = txtShipCountry.Text;
            pat1.BillingCountry = txtbillCountry.Text;
            pat1.AuthorisedPerson = txtAuthorisedPerson.Text;
            pat1.PatientManagementProgramId = Convert.ToInt32(ddlManagementProgram.SelectedValue);
            pat1.CallBeforeShip = chkCallBeforeShip.Checked;
            objIPatientService = new PatientService();
            objIPatientService.UpdatePatientDetails(pat1);
            string script = "window.parent.location.href=window.parent.location.href;";
            int patientId = pat1.PatientID;
            if (pat1.XeropatientId != Guid.Empty)
            {
                if (pat1.XeropatientId != null)
                {
                    if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);
                    }
                    Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + patientId.ToString(), false);
                }
                else
                {
                    pnlXeroCreation.Style["display"] = "normal";
                    modXeroCreation.Show();
                }
            }
            else
            {
                pnlXeroCreation.Style["display"] = "normal";
                modXeroCreation.Show();
            }
            //ClientScript.RegisterStartupScript(Me.GetType, "RefreshParent", "<script type=text/javascript>RefreshParent();</script>", False)

            //if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
            //    ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);




            //Response.Redirect("PatientInfo.aspx?PatientID=" + Request.QueryString["PatientID"], false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIQBCustMatchPatientService = null;
        }
    }
    /// <summary>
    /// cancel update and redirecting to patient info page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?PatientID=" + Request.QueryString["PatientID"]);
    }
    #endregion
    #region "Methods"
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
            lstStaffViewModel = objStaffService.GetStaff().Where(p => p.Active_YN == true && p.IsHARep==true).ToList();

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
           // ddlManagementProgram.Items.Insert(0, new ListItem("None", "0"));
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

            ddlLMCPhysician.DataSource = lstProviderViewModel;
            ddlLMCPhysician.DataTextField = "ProviderName";
            ddlLMCPhysician.DataValueField = "id";
            ddlLMCPhysician.DataBind();
            ddlLMCPhysician.Items.Insert(0, new ListItem("None"));
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
    /// Added by jaswinder to bind the clinic from database
    /// 5th sept 2013
    /// </summary>
    public void BindClinic()
    {
        try
        {
            objIPatientService = new PatientService();
            ddlClinic.DataSource = objIPatientService.GetClinics();
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
            objIPatientService = null;
        }
    }

    /// <summary>
    /// bind dropdown for Affiliate Name
    /// Added by jaswinder on 5th sept 2013
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

    #endregion
    protected void btnSaveXeroPatientId_Click(object sender, EventArgs e)
    {
        QB_MatchViewModel _QbMatchModel = new QB_MatchViewModel();
        IXeroAPIService objService = new XeroAPIService();
        if (txtXeroPatientId.Text != "")
        {
            _QbMatchModel.PatientID = int.Parse(Request.QueryString["PatientID"]);
            _QbMatchModel.QBid = txtXeroPatientId.Text.ToString();
            objService.InsertXeroMatch(_QbMatchModel);
        }

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string script = "this.window.parent.location=this.window.parent.location;";
        int patientId = int.Parse(Request.QueryString["PatientID"]);
        modXeroCreation.Hide();
        if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);
        }
        Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + patientId.ToString(), false);

    }


    protected void btnNo_Click(object sender, EventArgs e)

    {
        modXeroCreation.Hide();
        string script = "this.window.parent.location=this.window.parent.location;";

        if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);
        }
    }
}
