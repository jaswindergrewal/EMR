using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;

public partial class Controls_FrequencyControl : System.Web.UI.UserControl
{
	public int FrequencyID { get; set; }
	public string Days { get; set; }

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{

		}
		FrequencySource.ConnectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
		if (ddFrequemcy.SelectedValue != "")
			FrequencyID = int.Parse(ddFrequemcy.SelectedValue);
	}


	protected void ddFrequemcy_SelectedIndexChanged(Object sender, EventArgs e)
	{

		FrequencyID = int.Parse(ddFrequemcy.SelectedValue);
	}
	protected void txtDays_TextChanged(Object sender, EventArgs e)
	{
		
	}
}