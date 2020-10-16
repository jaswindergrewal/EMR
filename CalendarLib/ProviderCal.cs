using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public class ProviderCal
	{
		public ProviderCal(int providerID, string appointmentTypeID, string statusID)
		{
			Appts = new List<Appointment>();
			appts = new List<Appointment>();
			ProviderID = providerID;
			AppointmentTypeID = int.Parse(appointmentTypeID);
			StatusID = int.Parse(statusID);
			GetAppointments();
		}

		public List<Appointment> Appts { get; set; }

		private List<Appointment> appts;

		public int ProviderID { get; set; }

		public int AppointmentTypeID { get; set; }

		public int StatusID { get; set; }

		public static Appointment GetApptByID(string ID)
		{

			Appointment appt = new Appointment();
			if (ID == null)
			{
				appt.PatientID = 0;
				appt.Patient = "";
				appt.ApptStart = DateTime.Now;
				appt.ApptEnd = DateTime.Now;
				appt.ProviderID = 0;
				appt.ApptTypeID = 0;
				appt.StatusID = 0;
				appt.EventID = 0;
				appt.AllDay = false;
				appt.EmailOnChange = false;
				appt.Notes = "";
				appt.Email = "";
				appt.Results = 0;
				appt.IsRecurring = false;
				appt.ActionNeeded = "No";
                appt.WufooFormKey = "";
                appt.clinic = "";
                appt.category = "";
                appt.HARep = 0;
			}
			else
			{
				using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Appointment_GetByID";
                        cmd.Parameters.Add(new SqlParameter("@EventID", ID));
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataReader rd = Utils.OpenReader(cmd);
                        while (rd.Read())
                        {
                            appt.patient = new Patient((int)rd["Patient_ID"]);
                            string sPatient = (String)rd["Patient"] + " (";
                            switch (appt.patient.Clinic)
                            {
                                case "Kirkland":
                                    sPatient += "K) - (";
                                    break;
                                case "Lynnwood":
                                    sPatient += "L) - (";
                                    break;
                                case "South":
                                    sPatient += "T) - (";
                                    break;
                                case "Seattle":
                                    sPatient += "S) - (";
                                    break;
                                case "China-Beijing":
                                    sPatient += "C) - (";
                                    break;
                            }
                            sPatient += appt.patient.Birthday.ToShortDateString() + ")";
                            appt.Patient = rd["Patient"].GetType().Name == "DBNull" ? "" : sPatient;
                            appt.PatientID = (int)rd["Patient_ID"];
                            if (rd["ApptStart"] != DBNull.Value)
                                appt.ApptStart = (DateTime)rd["ApptStart"];
                            if (rd["ApptEnd"] != DBNull.Value)
                                appt.ApptEnd = (DateTime)rd["ApptEnd"];
                            if (rd["ProviderID"] != DBNull.Value)
                                appt.ProviderID = (int)rd["ProviderID"];
                            if (rd["AppointmentTypeID"] != DBNull.Value)
                                appt.ApptTypeID = (int)rd["AppointmentTypeID"];
                            if (rd["StatusID"] != DBNull.Value)
                                appt.StatusID = (int)rd["StatusID"];
                            if (rd["apt_ID"] != DBNull.Value)
                                appt.EventID = (int)rd["apt_ID"];
                            if (rd["AllDay"] != DBNull.Value)
                                appt.AllDay = (Boolean)rd["AllDay"];
                            if (rd["Results"] != DBNull.Value)
                                appt.Results = (int)rd["Results"];
                            if (rd["EmailOnChange"] != DBNull.Value)
                                appt.EmailOnChange = (Boolean)rd["EmailOnChange"];
                            if (rd["SaleMade_yn"] != DBNull.Value)
                                appt.SaleMadeYn = (int)rd["SaleMade_yn"];  // Added By Rakesh for Sale Made Functionality
                            if (rd["category"] != DBNull.Value)
                                appt.category = (string)rd["category"];

                            try
                            {
                                appt.Email = (String)rd["Email"];
                               
                            }
                            catch
                            {
                                appt.Email = "";
                                
                            }
                            try
                            {
                               
                                appt.WufooFormKey = (String)rd["WufooFormKey"];
                            }
                            catch
                            {
                                
                                appt.WufooFormKey = "";
                            }
                            try
                            {
                                appt.Notes = (String)rd["Notes"];
                            }
                            catch
                            {
                                appt.Notes = "";
                            }
                            appt.ActionNeeded = (string)rd["ActionNeeded"];
                            appt.LabsCheckedIn = (bool)rd["LabsCheckedIn"];
                            if (rd["clinic"] != DBNull.Value)
                                appt.clinic = (string)rd["clinic"];
                            if (rd["HARep"] != DBNull.Value)
                                appt.HARep = (int)rd["HARep"];

                        }
                    }
				}
			}
			return appt;
		}

		public static List<Appointment> GetCal(string ProviderID, string AppointmentTypeID, string StatusID,DateTime start, int days)
		{
			
			List<Appointment> ret = new List<Appointment>();
			List<Appointment> appts = new List<Appointment>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "ApptsByProviderID_Get";
                    cmd.Parameters.Add(new SqlParameter("@ProviderID", ProviderID));
                    cmd.Parameters.AddWithValue("@StartDate", start);
                    cmd.Parameters.AddWithValue("@EndDate", start.AddDays(days));
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if ((int)rd["Patient_ID"] != 0)
                        {
                            Appointment appt = new Appointment();
                            appt.Patient = (String)rd["Patient"];
                            appt.ApptStart = (DateTime)rd["ApptStart"];
                            appt.ApptEnd = (DateTime)rd["ApptEnd"];
                            appt.ProviderID = (int)rd["ProviderID"];
                            appt.ApptTypeID = (int)rd["ApptTypeID"];
                            appt.StatusID = (int)rd["StatusID"];
                            appt.EventID = (int)rd["apt_ID"];
                            appt.AllDay = (Boolean)rd["AllDay"];
                            appt.EmailOnChange = (Boolean)rd["EmailOnChange"];
                            try
                            {
                                appt.Email = (String)rd["Email"];
                            }
                            catch
                            {
                                appt.Email = "";
                            }
                            try
                            {
                                appt.Notes = (String)rd["notes"];
                            }
                            catch
                            {
                                appt.Notes = "";
                            }
                            appt.ActionNeeded = (string)rd["ActionNeeded"];
                            appts.Add(appt);
                        }
                    }
                    if ((AppointmentTypeID == "0" || AppointmentTypeID == null) && (StatusID == "0" || StatusID == null))
                    {
                        var tAppts = from a in appts
                                     where a.ActionNeeded == "No"
                                     select a;
                        foreach (var t in tAppts)
                        {
                            ret.Add(t);
                        }
                        return ret;
                    }
                    if (AppointmentTypeID != "0" && (StatusID == "0" || StatusID == null))
                    {
                        var tAppts = from a in appts
                                     where a.ApptTypeID == int.Parse(AppointmentTypeID) && a.ActionNeeded == "No"
                                     select a;

                        foreach (var t in tAppts)
                        {
                            ret.Add(t);
                        }
                    }
                    if ((AppointmentTypeID == "0" || AppointmentTypeID == null) && StatusID != "0")
                    {
                        var tAppts = from a in appts
                                     where a.StatusID == int.Parse(StatusID) && a.ActionNeeded == "No"
                                     select a;

                        foreach (var t in tAppts)
                        {
                            ret.Add(t);
                        }
                    }

                    if (AppointmentTypeID != "0" && StatusID != "0")
                    {
                        var tAppts = from a in appts
                                     where a.StatusID == int.Parse(StatusID) && a.ApptTypeID == int.Parse(AppointmentTypeID) && a.ActionNeeded == "No"
                                     select a;

                        foreach (var t in tAppts)
                        {
                            ret.Add(t);
                        }
                    }

                }
            }

			return ret;
		}
		private void GetAppointments()
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "ApptsByProviderID_Get";
				cmd.Parameters.Add(new SqlParameter("@ProviderID", ProviderID));
				cmd.CommandType = CommandType.StoredProcedure;
				conn.Open();
				SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
				while (rd.Read())
				{
					Appointment appt = new Appointment();
					appt.Patient = (String)rd["name"];
					appt.ApptStart = (DateTime)rd["eventstart"];
					appt.ApptEnd = (DateTime)rd["eventend"];
					appt.ProviderID = (int)rd["ProviderID"];
					appt.ApptTypeID = (int)rd["ApptTypeID"];
					appt.StatusID = (int)rd["StatusID"];
					appt.EventID = (int)rd["ID"];
					appt.AllDay = (Boolean)rd["AllDay"];
					appt.ActionNeeded = (string)rd["ActionNeeded"];
					appts.Add(appt);
				}
				if (AppointmentTypeID == 0 && StatusID == 0)
					Appts = appts;
				else if (AppointmentTypeID != 0 && StatusID == 0)
				{
					var tAppts = from a in appts
								 where a.ApptTypeID == AppointmentTypeID && a.ActionNeeded == "No"
								 select a;

					foreach (var t in tAppts)
					{
						Appts.Add(t);
					}
				}
				else if (AppointmentTypeID == 0 && StatusID != 0)
				{
					var tAppts = from a in appts
								 where a.StatusID == StatusID && a.ActionNeeded == "No"
								 select a;

					foreach (var t in tAppts)
					{
						Appts.Add(t);
					}
				}

				else if (AppointmentTypeID != 0 && StatusID != 0)
				{
					var tAppts = from a in appts
								 where a.StatusID == StatusID && a.ApptTypeID == AppointmentTypeID && a.ActionNeeded == "No"
								 select a;

					foreach (var t in tAppts)
					{
						Appts.Add(t);
					}
				}

			}
		}

        //Functions commented by jaswinder
        //public static void Update(int EventID, string Patient, DateTime ApptStart, DateTime ApptEnd, bool AllDay, bool EmailOnChange, string Email)
        //{

        //}

        //public static void Update(int EventID)
        //{

        //}

		public static void Insert(Appointment appt)
		{

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				con.Open();

                using (SqlCommand cmd = new SqlCommand("Appointment_Insert", con))
                {
                    cmd.Parameters.AddWithValue("@Patient", appt.Patient);
                    cmd.Parameters.AddWithValue("@ApptStart", appt.ApptStart);
                    cmd.Parameters.AddWithValue("@ApptEnd", appt.ApptEnd);
                    cmd.Parameters.AddWithValue("@ProviderID", appt.ProviderID);
                    cmd.Parameters.AddWithValue("@AppointmentTypeID", appt.ApptTypeID);
                    cmd.Parameters.AddWithValue("@StatusID", appt.StatusID);
                    cmd.Parameters.AddWithValue("@AllDay", appt.AllDay);
                    cmd.Parameters.AddWithValue("@EmailOnChange", appt.EmailOnChange);
                    cmd.Parameters.AddWithValue("@Results", appt.Results);
                    cmd.Parameters.AddWithValue("@Notes", appt.Notes == null ? "" : appt.Notes);
                    cmd.Parameters.AddWithValue("@Email", appt.Email == null ? "" : appt.Email);
                    cmd.ExecuteNonQuery();
                }
			}
		}

		public static List<ScheduleItem> GetSchedule(DateTime start, DateTime end, string ProviderID)
		{
			List<ScheduleItem> appts = new List<ScheduleItem>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentsSchedule";
                    cmd.Parameters.Add(new SqlParameter("@StartDate", start));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", end));
                    cmd.Parameters.Add(new SqlParameter("@ProviderID", ProviderID));
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    while (rd.Read())
                    {

                        ScheduleItem appt = new ScheduleItem();
                        appt.ProviderName = (String)rd["ProviderName"];
                        appt.PatientName = (string)rd["PatientName"];
                        appt.ApptStart = (DateTime)rd["ApptStart"];
                        appt.ApptEnd = (DateTime)rd["ApptEnd"];
                        appt.AppointmentType = (string)rd["TypeName"];
                        appt.Notes = (string)rd["Notes"];
                        appt.PatientID = (int)rd["PatientID"];
                        appt.Color = (string)rd["Color"];
                        appts.Add(appt);

                    }
                }
			}
			return appts;
		}


	}
	public class ScheduleItem
	{
		public string ProviderName { get; set; }
		public string PatientName { get; set; }
		public int PatientID { get; set; }
		public DateTime ApptStart { get; set; }
		public DateTime ApptEnd { get; set; }
		public string AppointmentType { get; set; }
		public string Notes { get; set; }
		public string Color { get; set; }
	}
}
