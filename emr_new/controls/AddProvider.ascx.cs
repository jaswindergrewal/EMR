using System;
using Calendar;

public partial class Controls_AddProvider : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AddButton_OnClick(object sender, EventArgs e)
    {
        Providers.AddProvider(ProviderName.Text, true);
    }
}