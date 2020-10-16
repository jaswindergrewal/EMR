using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Calendar;

public partial class Recurring : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			txtEndDate.Text = DateTime.Today.ToString("d");
			txtStartDate.Text = DateTime.Today.ToString("d");
		}
    }

	protected void ProviderDropDown_DataBound(object sender, EventArgs e)
	{
		ApptTypeDropDown.DataSource = AppointmentTypes.getApptTypeListOnly(ProviderDropDown.Items[0].Value);
		ApptTypeDropDown.DataBind();
	}
	protected void ProviderDropDown_SelectedIndexChanged(object sender, EventArgs e)
	{		
		ApptTypeDropDown.DataSource = AppointmentTypes.getApptTypeListOnly(ProviderDropDown.SelectedValue);
		ApptTypeDropDown.DataBind();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		int ProviderID = int.Parse(ProviderDropDown.SelectedValue);
		int ApptTypeID = int.Parse(ApptTypeDropDown.SelectedValue);
		DateTime startDate = DateTime.Parse(txtStartDate.Text + " " + txtStartTime.Text);
		DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + txtEndTime.Text);
		int weekCount = int.Parse(DropDownList1.SelectedValue);
		while ((int)startDate.DayOfWeek > 1)
		{
			startDate = startDate.AddDays(-1);
		}
		foreach (ListItem lst in CheckBoxList1.Items)
		{
			DateTime currentSet = startDate;
			int CurrentWeek = weekCount;
			
			if (lst.Selected)
			{
				
				DayOfWeek thisDay = DayOfWeek.Sunday;
				switch (lst.Value)
				{
					case "Monday":
						thisDay = DayOfWeek.Monday;
						break;
					case "Tuesday":
						thisDay = DayOfWeek.Tuesday;
						break;
					case "Wednesday":
						thisDay = DayOfWeek.Wednesday;
						break;
					case "Thursday":
						thisDay = DayOfWeek.Thursday;
						break;
					case "Friday":
						thisDay = DayOfWeek.Friday;
						break;
				}
				Calendar.Patient patient = (Calendar.Patient)ViewState["patient"];
				//process all dates
				bool ScheduledOne = false;
				while (currentSet < endDate)
				{
					//increment the week count on Sunday
					if (currentSet.DayOfWeek == DayOfWeek.Sunday && ScheduledOne)
					{
						CurrentWeek++;
					}

					//process if this day of the week is checked and it is a muktiple of the week setting
					if (currentSet.DayOfWeek == thisDay && CurrentWeek % weekCount == 0  && currentSet >= DateTime.Today)
					{
						ScheduledOne = true;
						Appointment newAppt = new Appointment();
						if (!cbAllDay.Checked)
							newAppt.AllDay = false;
						else
						{
							txtEndTime.Text = "11:59 PM";
							txtStartTime.Text = "12:01 AM";
							newAppt.AllDay = true;
						}
						newAppt.ApptEnd = DateTime.Parse(currentSet.ToString("d") + " " + txtEndTime.Text);
						newAppt.ApptStart = DateTime.Parse(currentSet.ToString("d") + " " + txtStartTime.Text);
						
						newAppt.ApptTypeID = int.Parse(ApptTypeDropDown.SelectedValue);
						newAppt.Email = "";
						newAppt.EmailOnChange = false;
						newAppt.EventID = 0;
						newAppt.Notes = txtNote.Text;
						newAppt.Patient = txtPatient.Text;
						newAppt.PatientID = patient.ID;
                        newAppt.clinic = patient.Clinic;
						newAppt.ProviderID = int.Parse(ProviderDropDown.SelectedValue);
						newAppt.Results = 0;
						newAppt.StatusID = 8;
						newAppt.ActionNeeded = "No";
						Calendar.Appointments.dbUpdateEvent(newAppt, int.Parse(Session["UserID"].ToString()));						
					}
					currentSet = currentSet.AddDays(1);
				}
			}
			
		}
		lblFinished.Visible = true;
	}
	protected void txtPatient_TextChanged(object sender, EventArgs e)
	{
		Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);
		if (pat.LastName != null)
		{
			//Patient patient = new Patient(txtPatient.Text);
			ViewState.Add("patient", pat);

		}
	}
}