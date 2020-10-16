using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Configuration;
public partial class SharePointPatientsAddEdit : System.Web.UI.Page
{
    IPatientService objService = null;
    IProviderService objProviderService = null;
    IAcuitySchedulingService objSchService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new PatientService();
                ddlClinic.DataSource = objService.GetClinics();
                ddlClinic.DataTextField = "ClinicName";
                ddlClinic.DataValueField = "ClinicID";
                ddlClinic.DataBind();
                ddlClinic.Items.Insert(0, new ListItem("Select Clinic","0"));

                objProviderService = new ProviderService();
                ddlProvider.DataSource = objProviderService.GetProviderDetails().Where(p => p.Active == true ).OrderBy(p => p.ProviderName).ToList(); 
                ddlProvider.DataTextField = "ProviderName";
                ddlProvider.DataValueField = "Id";
                ddlProvider.DataBind();
                ddlProvider.Items.Insert(0, new ListItem("Select Provider","0"));
                if (Request.QueryString["Id"] != null)
                {
                    if(Convert.ToInt32(Request.QueryString["Id"])>0)
                    {
                        btnUpdate.Text = "Update";
                        SharePointPatientViewModel patientsDetails = new SharePointPatientViewModel();
                        objSchService = new AcuitySchedulingService();
                        patientsDetails = objSchService.GetSharePointPatientsById(Convert.ToInt32(Request.QueryString["Id"]));
                        if(patientsDetails!=null)
                        {
                            
                            txtDuration.Text = patientsDetails.ApptDuration>0? patientsDetails.ApptDuration.ToString():"";
                            txtFirstName.Text = patientsDetails.FirstName;
                            txtLastName.Text = patientsDetails.LastName;
                            txtPhone.Text = patientsDetails.Phone;
                            txtNotes.Text = patientsDetails.Notes;
                            txtEndRange.Text = patientsDetails.EndRange !=null? Convert.ToDateTime(patientsDetails.EndRange).ToShortDateString():"";
                            txtStartRange.Text = patientsDetails.StartRange != null ?  Convert.ToDateTime(patientsDetails.StartRange).ToShortDateString():"";
                            ddlClinic.SelectedValue = patientsDetails.ClinicId.ToString();
                            ddlProvider.SelectedValue = patientsDetails.ProviderId.ToString();

                        }
                    }
                    else
                    {
                        btnUpdate.Text = "Save";
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
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SharePointPatientViewModel patientsDetails = new SharePointPatientViewModel();
        patientsDetails.FirstName = txtFirstName.Text;
        patientsDetails.LastName = txtLastName.Text;
        patientsDetails.Phone = txtPhone.Text;
        patientsDetails.Notes = txtNotes.Text;
        patientsDetails.ProviderId = Convert.ToInt16(ddlProvider.SelectedValue);
        patientsDetails.ClinicId = Convert.ToInt16(ddlClinic.SelectedValue);
        if (txtStartRange.Text != "")
        {
            patientsDetails.StartRange = Convert.ToDateTime(txtStartRange.Text);
        }
        else
            patientsDetails.StartRange = null;
        if (txtEndRange.Text != "")
            patientsDetails.EndRange = Convert.ToDateTime(txtEndRange.Text);
        else
            patientsDetails.EndRange = null;
        patientsDetails.CreatedDate = DateTime.Now;
        patientsDetails.ApptDuration = txtDuration.Text!=""? Convert.ToInt16(txtDuration.Text):0;
        patientsDetails.Id = Convert.ToInt32(Request.QueryString["Id"]);
        objSchService = new AcuitySchedulingService();
        objSchService.SaveUpdateSharePointPatients(patientsDetails);
        Response.Redirect("listsharepointpatients.aspx");

    }
}