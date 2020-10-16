using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class BatchImport : LMCBase
{
	private List<OneAppt> theList = new List<OneAppt>();

	protected void Page_Load(object sender, EventArgs e)
	{
		//MakeAppointments();
	}
	protected void btnUpload_Click(object sender, EventArgs e)
	{

		
	}

	//private void MakeAppointments()
	//{
	//    EMRDataContext ctx = new EMRDataContext("Data Source=lmcsql\\lmcsql;Initial Catalog=lmc_020505;Integrated Security=true;Connect Timeout=120");
	//    List<Dori_> dori = (from d in ctx.Dori_s select d).ToList();
	//    List<Julia_> julia = (from d in ctx.Julia_s select d).ToList();
	//    OneAppt appt = new OneAppt(7, 30, 63, 7530, "Living Sociall Treatment (K)()");
	//    theList.Add(appt);
	//    appt = new OneAppt(49, 30, 63, 7530, "Living Social Treatment (K)()");
	//    theList.Add(appt);
	//    appt = new OneAppt(91, 30, 63, 7530, "Living Social Treatment (K)()");
	//    theList.Add(appt);
	//    appt = new OneAppt(133, 30, 63, 7530, "Living Social Treatment (K)()");
	//    theList.Add(appt);
	//    appt = new OneAppt(175, 30, 63, 7530, "Living Social Treatment (K)()");
	//    theList.Add(appt);
	//    appt = new OneAppt(217, 30, 63, 7530, "Living Social Treatment (K)()");
	//    theList.Add(appt);
	//    //StreamReader sr = new StreamReader(Server.MapPath("./Output_Files") + "\\upload.csv");
	//    int ApptType = 62;
	//    string PatientName = "Living Social Consult (K)()";
	//    int PatientID = 7529;

	//    foreach (Dori_ d in dori)
	//    {
	//        Calendar.Appointment newAppt = new Calendar.Appointment();
	//        DateTime tt = DateTime.Parse(((DateTime)d.Date).ToShortDateString() + " " + ((DateTime)d.Time).ToShortTimeString());
	//        newAppt.AllDay = false;
	//        newAppt.ApptStart = tt;
	//        newAppt.ApptEnd = newAppt.ApptStart.AddMinutes(30);
	//        newAppt.ApptTypeID = ApptType;
	//        newAppt.Email = "";
	//        newAppt.EmailOnChange = false;
	//        newAppt.EventID = 0;
	//        newAppt.Notes = "";
	//        newAppt.Patient = PatientName;
	//        newAppt.PatientID = PatientID;
	//        newAppt.ProviderID = 23;
	//        newAppt.Results = 0;
	//        newAppt.StatusID = 8;
	//        newAppt.ActionNeeded = "No";
	//        Calendar.Appointments.dbUpdateEvent(newAppt);
	//        foreach (OneAppt FollowOn in theList)
	//        {
	//            if (FollowOn.DaysOut == 0) break;
	//            newAppt = new Calendar.Appointment();
	//            newAppt.AllDay = false;
	//            newAppt.ApptStart = tt.AddDays(FollowOn.DaysOut);
	//            newAppt.ApptEnd = newAppt.ApptStart.AddMinutes(FollowOn.Minutes);
	//            newAppt.ApptTypeID = FollowOn.AppoitntmentType;
	//            newAppt.Email = "";
	//            newAppt.EmailOnChange = false;
	//            newAppt.EventID = 0;
	//            newAppt.Notes = "";
	//            newAppt.Patient = FollowOn.PatientName;
	//            newAppt.PatientID = FollowOn.Patient;
	//            newAppt.ProviderID = 23;
	//            newAppt.Results = 0;
	//            newAppt.StatusID = 8;
	//            newAppt.ActionNeeded = "No";
	//            Calendar.Appointments.dbUpdateEvent(newAppt);
	//        }
	//    }

	//    foreach (Julia_ d in julia)
	//    {
	//        Calendar.Appointment newAppt = new Calendar.Appointment();
	//        DateTime tt = DateTime.Parse(((DateTime)d.Date).ToShortDateString() + " " + ((DateTime)d.Time).ToShortTimeString());
	//        newAppt.AllDay = false;
	//        newAppt.ApptStart = tt;
	//        newAppt.ApptEnd = newAppt.ApptStart.AddMinutes(30);
	//        newAppt.ApptTypeID = ApptType;
	//        newAppt.Email = "";
	//        newAppt.EmailOnChange = false;
	//        newAppt.EventID = 0;
	//        newAppt.Notes = "";
	//        newAppt.Patient = PatientName;
	//        newAppt.PatientID = PatientID;
	//        newAppt.ProviderID = 26;
	//        newAppt.Results = 0;
	//        newAppt.StatusID = 8;
	//        newAppt.ActionNeeded = "No";
	//        Calendar.Appointments.dbUpdateEvent(newAppt);
	//        foreach (OneAppt FollowOn in theList)
	//        {
	//            if (FollowOn.DaysOut == 0) break;
	//            newAppt = new Calendar.Appointment();
	//            newAppt.AllDay = false;
	//            newAppt.ApptStart = tt.AddDays(FollowOn.DaysOut);
	//            newAppt.ApptEnd = newAppt.ApptStart.AddMinutes(FollowOn.Minutes);
	//            newAppt.ApptTypeID = FollowOn.AppoitntmentType;
	//            newAppt.Email = "";
	//            newAppt.EmailOnChange = false;
	//            newAppt.EventID = 0;
	//            newAppt.Notes = "";
	//            newAppt.Patient = FollowOn.PatientName;
	//            newAppt.PatientID = FollowOn.Patient;
	//            newAppt.ProviderID = 26;
	//            newAppt.Results = 0;
	//            newAppt.StatusID = 8;
	//            newAppt.ActionNeeded = "No";
	//            Calendar.Appointments.dbUpdateEvent(newAppt);
	//        }
	//    }
	//}

}


[Serializable]
public class OneAppt
{
	public OneAppt(int daysOut, int minutes, int appointmentType, int patient, string patientName)
	{
		DaysOut = daysOut;
		Minutes = minutes;
		AppoitntmentType = appointmentType;
		Patient = patient;
		PatientName = patientName;
	}

	public int DaysOut { get; set; }
	public int Minutes { get; set; }
	public int AppoitntmentType { get; set; }
	public int Patient { get; set; }
	public string PatientName { get; set; }
}