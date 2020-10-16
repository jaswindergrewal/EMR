using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar;

public partial class ShortFollowUps : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void FollowupGrid_SelectedIndexChanged(object sender, EventArgs e)
	{
		string apt_id = FollowupGrid.SelectedRow.Cells[14].Text;
		Calendar.Appointments.FollowupComplete(apt_id);
		FollowupGrid.DataBind();
	}

	protected void btnComplete_Click(object sender, EventArgs e)
	{
		foreach (GridViewRow theRow in FollowupGrid.Rows)
		{
			CheckBox cbComplete = (CheckBox)theRow.Cells[0].Controls[1];
			if (cbComplete.Checked)
			{
				string apt_id = theRow.Cells[14].Text;
				Calendar.Appointments.FollowupComplete(apt_id);
			}
		}
		FollowupGrid.DataBind();
	}
}