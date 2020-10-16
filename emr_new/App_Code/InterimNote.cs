using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InterimNote
/// </summary>
public class InterimNote
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime? DateEntered { get; set; }
	public DateTime? DateDue { get; set; }
	public int? DaysOld { get; set; }
	public int? FollowUp_Cat { get; set; }
	public string Priority { get; set; }
	public string Subject { get; set; }
	public int? FollowUp_ID { get; set; }
	public int? Entered_By { get; set; }
	public int? Assigned { get; set; }
	public bool? FollowUp_Completed_YN { get; set; }
	public int? PatientID { get; set; }
	public int? Severity { get; set; }
	public string FollowUp_Subject { get; set; }
	public int? DepartmentAssign { get; set; }
	public int? Responses { get; set; }

}