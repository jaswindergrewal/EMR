using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for TicketUtils
/// </summary>
public class TicketUtils
{
	public static int MakeTicket(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject)
	{
		return MakeTicket(StaffID, Message, Category, PatientID, Severity, AssignType, AssignTo, Subject, null);
	}

	public static int MakeTicket(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject, DateTime dueDate)
	{
		int span = (dueDate - DateTime.Now).Days;
		return MakeTicket(StaffID, Message, Category, PatientID, Severity, AssignType, AssignTo, Subject, span);
	}

	public static int MakeTicket(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject, int? DueOffset)
	{
		EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
		apt_FollowUp theFollow = new apt_FollowUp();
		theFollow.DateEntered = DateTime.Now;
		theFollow.Entered_By = StaffID;
		theFollow.FollowUp_Body = Message;
		theFollow.FollowUp_Cat = Category;
		theFollow.FollowUp_Completed_YN = false;
		theFollow.PatientID = PatientID;
		theFollow.Severity = Severity;
		if (AssignType == "i")
			theFollow.Assigned = AssignTo;
		else
			theFollow.DepartmentAssign = AssignTo;
		theFollow.FollowUp_Subject = Subject;
		if (DueOffset == null)
		{
			theFollow.DueDate = DateTime.Today;
		}
		else
		{
			theFollow.DueDate = DateTime.Today.AddDays((int)DueOffset);
		}
		ctx.apt_FollowUps.InsertOnSubmit(theFollow);
		ctx.SubmitChanges();
		return theFollow.FollowUp_ID;
	}
}