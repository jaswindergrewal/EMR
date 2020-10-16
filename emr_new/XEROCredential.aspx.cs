using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class XEROCredential : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IXeroAPIService xeroapiservece = new XeroAPIService();
        if (!Page.IsPostBack) { 
            XeroCredentialViewModel xeroCredentialViewModel=xeroapiservece.GetXeroCredential();
            XeroConsumerKey.Text = xeroCredentialViewModel.XeroConsumerKey;
            XeroConsumerSecret.Text = xeroCredentialViewModel.XeroConsumerSecret;
            IDXeroCredentials.Value = Convert.ToString(xeroCredentialViewModel.Id);
            XeroConsumerKeyEdit.Text = xeroCredentialViewModel.XeroConsumerKey;
            XeroConsumerSecretEdit.Text = xeroCredentialViewModel.XeroConsumerSecret;
        }
    }
    /// <summary>
    /// This method is a click handler which Edit Xero credentials
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void SaveXeroCredentialsButton_Click(object sender, EventArgs e) {
        IXeroAPIService xeroapiservece = new XeroAPIService();
        XeroCredentialViewModel xeroCredentialViewModel = new XeroCredentialViewModel();
        int Id1 =Convert.ToInt32(IDXeroCredentials.Value);
        string XeroConsumerKey1 = XeroConsumerKeyEdit.Text;
        string XeroConsumerSecret1 = XeroConsumerSecretEdit.Text;

        xeroCredentialViewModel.XeroConsumerKey = XeroConsumerKey1;
        xeroCredentialViewModel.XeroConsumerSecret = XeroConsumerSecret1;
        xeroCredentialViewModel.Id = Id1;
        xeroapiservece.EditXeroCredential(xeroCredentialViewModel);

        xeroCredentialViewModel = xeroapiservece.GetXeroCredential();
        XeroConsumerKey.Text = xeroCredentialViewModel.XeroConsumerKey;
        XeroConsumerSecret.Text = xeroCredentialViewModel.XeroConsumerSecret;
        IDXeroCredentials.Value = Convert.ToString(xeroCredentialViewModel.Id);
        XeroConsumerKeyEdit.Text = xeroCredentialViewModel.XeroConsumerKey;
        XeroConsumerSecretEdit.Text = xeroCredentialViewModel.XeroConsumerSecret;
    }
}