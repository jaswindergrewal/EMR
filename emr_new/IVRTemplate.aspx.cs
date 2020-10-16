using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class IVRTemplate : System.Web.UI.Page
{
    IEmailTemplateService objTemplateService = null;
    IAppointmentConsole objAppointmentService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objAppointmentService = new AppointmentConsole(); ;
            ddlAppointmentType.DataSource = objAppointmentService.GetAppointmentTypeList();
            ddlAppointmentType.DataTextField = "TypeName";
            ddlAppointmentType.DataValueField = "ID";
            ddlAppointmentType.DataBind();

        }
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        objTemplateService = new EmailTemplateService();
        objTemplateService.SaveIVRTemplate(edContent.Value, Convert.ToInt16(ddlAppointmentType.SelectedValue));
    }
    protected void ddlAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmailTemplateViewModel TemplateViewModel = null;
        objTemplateService = new EmailTemplateService();
        TemplateViewModel = objTemplateService.GetIVRTemplate(Convert.ToInt16(ddlAppointmentType.SelectedValue));
        if (TemplateViewModel != null)
        {
            edContent.Value = TemplateViewModel.TemplateDesc;
        }
        else { edContent.Value = ""; }
    }
    protected void btnUserName_Click(object sender, EventArgs e)
    {
        edContent.Value = edContent.Value + " {UserName}";
    }
    protected void btnURL_Click(object sender, EventArgs e)
    {
        edContent.Value = edContent.Value + " {AppointmentDate}";
    }
}