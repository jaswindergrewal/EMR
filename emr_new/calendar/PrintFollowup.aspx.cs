using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calendar;

public partial class PrintFollowup : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string[] AllFollowups = Request.QueryString["FollowUpID"].Split(',');
		foreach (string id in AllFollowups)
		{
			FollowUp follow = new FollowUp(int.Parse(id));
			Appointment appt = ProviderCal.GetApptByID(follow.Apt_ID.ToString());
			Calendar.AppointmentType theType = Calendar.AppointmentTypes.GetApptType(appt.ApptTypeID);
			Calendar.Patient pat = new Calendar.Patient(follow.PatientID);
			TableCell ApptType = new TableCell();
			ApptType.ColumnSpan = 4;
			ApptType.Text = "<strong>Appointment type: " + theType.TypeName + "</strong>";
			TableCell cap = new TableCell();
			cap.ColumnSpan = 4;
			cap.Text = "<strong>Patient: " + pat.FirstName + " " + pat.LastName + " " + "ID: " + pat.ID + "</strong>";
			TableCell EnteredBy = new TableCell();
			EnteredBy.ColumnSpan = 4;
			EnteredBy.Text = "<strong>Entered by: " + follow.EmployeeName + "</strong>";
			TableCell FollowType = new TableCell();
			FollowType.Text = follow.FollowUp_Type_Desc;
			TableCell Entered = new TableCell();
			Entered.Text = follow.DateEntered.ToString();
			TableCell Range = new TableCell();
			if (follow.Range_Start != "")
				Range.Text = follow.Range_Start + "-";
			else
				Range.Text = "[]";
			if (follow.Range_End != null)
				Range.Text += follow.Range_End;
			else
				Range.Text += "[]";
			TableCell Details = new TableCell();
			Details.Text = follow.FollowUp_Body;
			TableRow row = new TableRow();
			row.Cells.Add(ApptType);
			tbl.Rows.Add(row);
			row = new TableRow();
			row.Cells.Add(cap);
			tbl.Rows.Add(row);
			row = new TableRow();
			row.Cells.Add(EnteredBy);
			tbl.Rows.Add(row);
			row = new TableRow();
			TableCell theCell = new TableCell();
			theCell.HorizontalAlign= HorizontalAlign.Center;
			theCell.Text="<strong>Follow-up type</strong>";
			row.Cells.Add(theCell);
			theCell = new TableCell();
			theCell.HorizontalAlign = HorizontalAlign.Center;
			theCell.Text = "<strong>Date Entered</strong>";
			row.Cells.Add(theCell);
			theCell = new TableCell();
			theCell.HorizontalAlign = HorizontalAlign.Center;
			theCell.Text = "<strong>Date range for follow-up</strong>";
			row.Cells.Add(theCell);
			theCell = new TableCell();
			theCell.HorizontalAlign = HorizontalAlign.Center;
			theCell.Text = "<strong>Details</strong>";
			row.Cells.Add(theCell);
			tbl.Rows.Add(row);
			row = new TableRow();
			row.Cells.Add(FollowType);
			row.Cells.Add(Entered);
			row.Cells.Add(Range);
			row.Cells.Add(Details);
			tbl.Rows.Add(row);
			foreach (TableRow row1 in tbl.Rows)
			{
				row1.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
				row1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
				row.BorderColor = System.Drawing.Color.LightGray;
				foreach (TableCell cell in row1.Cells)
				{
					cell.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
					cell.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
					cell.BorderColor = System.Drawing.Color.LightGray;
				}
			}
		}
	}

	protected void rptFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		e.Row.Cells[4].Attributes.Add("style", "white-space:normal;");
		e.Row.Cells[0].Attributes.Add("style", "white-space:nowrap;");
	}
}