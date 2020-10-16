using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class AdminEmailTemplate : System.Web.UI.Page
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
            ddlAppointmentType.Items.Insert(1, new ListItem("ShareFile", "0"));

        }
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        objTemplateService = new EmailTemplateService();
        objTemplateService.SaveEmailTemplate(edContent.Content, Convert.ToInt16(ddlAppointmentType.SelectedValue));
    }
    protected void ddlAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmailTemplateViewModel TemplateViewModel = null;
        objTemplateService = new EmailTemplateService();
        TemplateViewModel = objTemplateService.GetEmailTemplate(Convert.ToInt16(ddlAppointmentType.SelectedValue));
        if (TemplateViewModel != null)
        {
            edContent.Content = TemplateViewModel.TemplateDesc;
        }
        else { edContent.Content = ""; }
    }
    protected void btnUserName_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {UserName}";
    }
    protected void btnURL_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {Url}";
    }
}