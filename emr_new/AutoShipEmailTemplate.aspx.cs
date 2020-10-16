using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AutoShipEmailTemplate : System.Web.UI.Page
{
    IEmailTemplateService objTemplateService = null;
    IAppointmentConsole objAppointmentService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmailTemplateViewModel TemplateViewModel = null;
            objTemplateService = new EmailTemplateService();
            TemplateViewModel = objTemplateService.GetAutoShipEmailTemplate();
            if (TemplateViewModel != null)
            {
                edContent.Content = TemplateViewModel.TemplateDesc;
            }
            else { edContent.Content = ""; }

        }
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        objTemplateService = new EmailTemplateService();
        objTemplateService.SaveAutoShipEmailTemplate(edContent.Content);
    }
   
    protected void btnUserName_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {UserName}";
    }
   
    protected void btnOrderData_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {OrderData}";
    }
}