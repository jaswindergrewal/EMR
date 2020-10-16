using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact_record_add : System.Web.UI.Page
{
    #region Global

    Emrdev.ServiceLayer.ContactService objContactService;
    Emrdev.ViewModelLayer.Contact_tblViewModel objContactModel;
    Emrdev.ServiceLayer.PatientService objPatientService;
    public int patientId=0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request.QueryString["patientid"], out patientId);
        if (!IsPostBack)
        {
            BindAppType();
        }
    }
    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        try
        {
            /* Save Note */
            objContactService = new Emrdev.ServiceLayer.ContactService();
            objContactModel = new Emrdev.ViewModelLayer.Contact_tblViewModel();
            objContactModel.AptType = int.Parse(ddlApptType.SelectedValue);
            objContactModel.PatientID = patientId;
            objContactModel.MessageBody = edTicket.Content.Trim();
            objContactModel.EnteredBy = (int)Session["StaffID"];
            objContactModel.Apt_ID = null;
            objContactModel.ContactDateEntered = DateTime.Now;
            objContactService.InsertContactDetail(objContactModel);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        Response.Redirect("~/PatientInfo.aspx?PatientID=" + patientId + "");
    }

    void BindAppType()
    {
        /* Bind Contact Type Dropdown List */
        objContactService = new Emrdev.ServiceLayer.ContactService();
        ddlApptType.DataSource = objContactService.SelectAllContactType();
        ddlApptType.DataBind();
        if (patientId > 0)
        {
            objPatientService = new Emrdev.ServiceLayer.PatientService();
            lblPatientName.Text = objPatientService.GetPatientFullName(patientId);
        }
    }
}