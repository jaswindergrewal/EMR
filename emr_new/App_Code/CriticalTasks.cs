using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for CriticalTasks
/// </summary>
public class CriticalTasks
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	public CriticalTasks()
	{

	}

	public DataTable PatientsTasks(string sort)
	{
		List<PatientCriticalTask> Patients = new List<PatientCriticalTask>();
		if (sort == "Date")
		{
			Patients = (from p in ctx.Patients_CriticalTasks
						join pat in ctx.Patients on p.PatientID equals pat.PatientID
						join apt in ctx.apt_recs on p.PatientID equals apt.patient_id
						where !(bool)p.Received
						&& !(bool)pat.Inactive
						&& (apt.ApptStart <= DateTime.Now || apt.ApptStart == null)
						&& !(bool)pat.Aesthetics
						&& !(bool)pat.Aesthetic_YN
						orderby apt.ApptStart descending
						select new PatientCriticalTask
						{
							LastName = pat.LastName,
							FirstName = pat.FirstName,
							ID = pat.PatientID,
							Reviewed = p.Reviewed,
							ApptStart = apt.ApptStart,
						}).ToList();
		}
		else
		{
			Patients = (from p in ctx.Patients_CriticalTasks
						join pat in ctx.Patients on p.PatientID equals pat.PatientID
						join apt in ctx.apt_recs on p.PatientID equals apt.patient_id
						where !(bool)p.Received
						&& !(bool)pat.Inactive
						&& (apt.ApptStart <= DateTime.Now || apt.ApptStart == null)
						&& !(bool)pat.Aesthetics
						&& !(bool)pat.Aesthetic_YN
						orderby pat.LastName,pat.FirstName
						select new PatientCriticalTask
						{
							LastName = pat.LastName,
							FirstName = pat.FirstName,
							ID = pat.PatientID,
							Reviewed = p.Reviewed,
							ApptStart = apt.ApptStart,
						}).ToList();
		}
		DataTable ret = new DataTable();
		ret.Columns.Add("LastName");
		ret.Columns.Add("FirstName");
		ret.Columns.Add("ID", (1).GetType());
		ret.Columns.Add("ApptStart");
		foreach (var pat in Patients)
		{
			if (ret.Select("ID=" + pat.ID).Count() == 0 && pat.ID != 7447 && pat.ID != 7530)
			{
				DataRow dr = ret.NewRow();
				dr["LastName"] = pat.LastName;
				dr["FirstName"] = pat.FirstName;
				dr["ID"] = pat.ID;
				dr["ApptStart"] = pat.ApptStart;
				ret.Rows.Add(dr);
			}
		}
		return ret;
	}

	class PatientCriticalTask
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int ID { get; set; }
		public bool Reviewed { get; set; }
		public DateTime? ApptStart { get; set; }
	}

}