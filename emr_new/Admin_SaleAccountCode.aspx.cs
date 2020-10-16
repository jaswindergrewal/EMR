using Emrdev.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SaleAccountCode : System.Web.UI.Page
{
    IEmailTemplateService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        objService = new EmailTemplateService(); ;
        objService.SaveSalesAccountCode(Convert.ToInt32(txtSalesAccount.Text));
        txtSalesAccount.Text="";
    }
}