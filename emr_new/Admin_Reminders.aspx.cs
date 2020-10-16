using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reminders : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
    /// <summary>
    /// Change the iframe on tab changed event
    /// </summary>
	protected void ConsoleContainer_ActiveTabChanged(object sender, EventArgs e)
	{
		if (pnlLabs.Visible == false)
		{
			ifrLabs.Attributes["src"] = "Admin_LabReminders.aspx";
			pnlLabs.Visible = true;
		}
	}
}