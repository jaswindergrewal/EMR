using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public class Appointments
	{

		public static void FollowupComplete(string apt_id)
		{
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{

				con.Open();
                //Code review point: Dispose obect
                using (SqlCommand cmd1 = new SqlCommand("Appointment_FollowUpComplete", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@AppointmentID", apt_id));
                    cmd1.ExecuteNonQuery();
                }
			}
		}

		public static void ToggleFollowup(int AptID, string FollowupID,bool Detach)
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				conn.Open();
                //Code review point: Dispose obect
                using (SqlCommand cmd = new SqlCommand("FollowUps_Toggle", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AptID", AptID);
                    cmd.Parameters.AddWithValue("@FollowupID", FollowupID);
                    cmd.Parameters.AddWithValue("@Detach", Detach);
                    cmd.ExecuteNonQuery();
                }
			}
		}

		public static string dbUpdateEvent(Appointment appt,int StaffId)
		{
			string[] searcher = appt.Patient.Split('(');
			if (searcher.Count() != 3)
				return "";
			string patName = searcher[0].Trim();
			string Clinic = searcher[1].Split(')')[0].Trim();
			switch (Clinic)
			{
				case "T":
					Clinic = "South";
					break;
				case "S":
					Clinic = "Seattle";
					break;
				case "K":
					Clinic = "Kirkland";
					break;
				case "L":
					Clinic = "Lynnwood";
					break;
                case "C":
                    Clinic = "China-Beijing";
                    break;
			}
			string sBirthday = null;
			try
			{
				sBirthday = DateTime.Parse(appt.Patient.Split('(')[2].Split(')')[0]).ToShortDateString();
			}
			catch { }
			if (sBirthday == "1/1/0001") sBirthday = null;
			if (appt.EventID == 0)
			{
				using (SqlConnection con = Utils.GetConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					//con.Open();
                    //SqlCommand cmd1 = new SqlCommand("Patients_FindID", con);
                    //Code review point: Dispose obect
                    using (SqlCommand cmd1 = new SqlCommand("Calendar_Patients_FindID", con))
                    {

                        cmd1.CommandType = CommandType.StoredProcedure;

                        SqlCommand cmd = new SqlCommand("Appointment_Insert", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.Add(new SqlParameter("@PatientName", patName));
                        cmd1.Parameters.AddWithValue("@Birthday", sBirthday);
                        cmd1.Parameters.AddWithValue("@Clinic", Clinic);
                        if (appt.PatientID == -1)
                            appt.PatientID = (int)cmd1.ExecuteScalar();


                        cmd.Parameters.Add(new SqlParameter("@PatientID", appt.PatientID));
                        cmd.Parameters.Add(new SqlParameter("@ApptStart", appt.ApptStart));
                        cmd.Parameters.Add(new SqlParameter("@ApptEnd", appt.ApptEnd));
                        cmd.Parameters.Add(new SqlParameter("@ProviderID", appt.ProviderID));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentTypeID", appt.ApptTypeID));
                        cmd.Parameters.Add(new SqlParameter("@StatusID", appt.StatusID));
                        cmd.Parameters.Add(new SqlParameter("@AllDay", appt.AllDay));
                        cmd.Parameters.Add(new SqlParameter("@EmailOnChange", appt.EmailOnChange));
                        cmd.Parameters.Add(new SqlParameter("@Results", appt.Results));
                        cmd.Parameters.Add(new SqlParameter("@Notes", appt.Notes));
                        cmd.Parameters.Add(new SqlParameter("@Email", appt.Email));
                        cmd.Parameters.Add(new SqlParameter("@LabsCheckedIn", appt.LabsCheckedIn));
                        cmd.Parameters.Add(new SqlParameter("@Clinic", appt.clinic));
                        cmd.Parameters.Add(new SqlParameter("@HARep", appt.HARep));
                        cmd.Parameters.Add(new SqlParameter("@StaffId", StaffId));
                        // cmd.Parameters.Add(new SqlParameter("@LockedAppointment", appt.LockedAppointment));
                        int iEventID = (int)cmd.ExecuteScalar();
                        appt.EventID = (int)iEventID;
                    }
				}
			}
			else
			{
				using (SqlConnection con = Utils.GetConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					//con.Open();

                    using (SqlCommand cmd1 = new SqlCommand("Calendar_Patients_FindID", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@PatientName", patName));
                        cmd1.Parameters.AddWithValue("@Birthday", sBirthday);
                        cmd1.Parameters.AddWithValue("@Clinic", Clinic);
                       // if (appt.PatientID == -1)
                            appt.PatientID = (int)cmd1.ExecuteScalar();
                        
                    }
                    //Code review point: Dispose obect
                    using (SqlCommand cmd = new SqlCommand("Appointment_Update", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EventID", appt.EventID));
                        cmd.Parameters.Add(new SqlParameter("@PatientID", appt.PatientID));
                        cmd.Parameters.Add(new SqlParameter("@ApptStart", appt.ApptStart));
                        cmd.Parameters.Add(new SqlParameter("@ApptEnd", appt.ApptEnd));
                        cmd.Parameters.Add(new SqlParameter("@ProviderID", appt.ProviderID));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentTypeID", appt.ApptTypeID));
                        cmd.Parameters.Add(new SqlParameter("@StatusID", appt.StatusID));
                        cmd.Parameters.Add(new SqlParameter("@AllDay", appt.AllDay));
                        cmd.Parameters.Add(new SqlParameter("@EmailOnChange", appt.EmailOnChange));
                        cmd.Parameters.Add(new SqlParameter("@Results", appt.Results));
                        cmd.Parameters.Add(new SqlParameter("@Notes", appt.Notes));
                        cmd.Parameters.Add(new SqlParameter("@Email", appt.Email));
                        cmd.Parameters.Add(new SqlParameter("@ActionNeeded", appt.ActionNeeded));
                        cmd.Parameters.Add(new SqlParameter("@LabsCheckedIn", appt.LabsCheckedIn));
                        if(appt.clinic==null)
                            cmd.Parameters.Add(new SqlParameter("@Clinic", "NULL"));
                        else
                            cmd.Parameters.Add(new SqlParameter("@Clinic", appt.clinic));
                        cmd.Parameters.Add(new SqlParameter("@HARep", appt.HARep));
                        cmd.Parameters.Add(new SqlParameter("@StaffId", StaffId));
                        // cmd.Parameters.Add(new SqlParameter("@LockedAppointment", appt.LockedAppointment));

                        cmd.ExecuteNonQuery();
                    }
				}
			}
			return appt.EventID.ToString();
		}

        //Remover Appointment oldapp parameter by jaswinder
        private static string GetPatMessage(Appointment newAppt)
		{

			string retVal = "";
			retVal += "Patient: " + newAppt.Patient + "\r\n";
			retVal += "Date and Time : [" + newAppt.ApptStart + "] to [" + newAppt.ApptEnd + "].\r\n";
			retVal += "Provider: [" + Provider.GetProviderName(newAppt.ProviderID.ToString()) + "].\r\n";
			retVal += "Appointment type: [" + AppointmentTypes.GetAptTypeName(newAppt.ApptTypeID) + "].\r\n";
			retVal += "Notes: [" + newAppt.Notes + "].\r\n";

			return retVal;

		}


		private static string CompareAppointments(Appointment oldAppt, Appointment newAppt)
		{
			string retVal = "";
			string newProvName = Provider.GetProviderName(newAppt.ProviderID);
			string newAptTypeName = AppointmentTypes.GetAptTypeName(newAppt.ApptTypeID);
			string newResultName = Results.GetResultName(newAppt.Results);
			string newStatusName = Status.GetStatusName(newAppt.StatusID);
			string oldProvName = Provider.GetProviderName(oldAppt.ProviderID);
			string oldAptTypeName = AppointmentTypes.GetAptTypeName(oldAppt.ApptTypeID);
			string oldResultName = Results.GetResultName(oldAppt.Results);
			string oldStatusName = Status.GetStatusName(oldAppt.StatusID);
			if (newAppt.Results == 100)
			{
				newResultName = "Deleted";
			}
			if (oldAppt.Patient != newAppt.Patient)
			{
				retVal += "Patient changed from [" + oldAppt.Patient + "] to [" + newAppt.Patient + "].<br />\r\n";
			}
			if (oldAppt.ApptStart != newAppt.ApptStart)
			{
				retVal += "Start time changed. <br />\r\n PreviousTimes:  [" + oldAppt.ApptStart + "] to [" + oldAppt.ApptEnd + "].<br />\r\n New Times: [" + newAppt.ApptStart + "] to [" + newAppt.ApptEnd + "].<br />\r\n";
			}
			if (oldAppt.ApptEnd != newAppt.ApptEnd)
			{
				retVal += "End time changed. <br />\r\n Previous Times:  [" + oldAppt.ApptStart + " - " + oldAppt.ApptEnd.ToShortTimeString() + "].<br />\r\n New Times: [" + newAppt.ApptStart + " - " + newAppt.ApptEnd.ToShortTimeString() + "].<br />\r\n";
			}
			if (oldAppt.ProviderID != newAppt.ProviderID)
			{
				retVal += "Provider id changed from [" + oldProvName + "] to [" + newProvName + "].<br />\r\n";
			}
			if (oldAppt.ApptTypeID != newAppt.ApptTypeID)
			{
				retVal += "Apppointment Type ID changed from [" + oldAptTypeName + "] to [" + newAptTypeName + "].<br />\r\n";
			}
			if (oldAppt.StatusID != newAppt.StatusID)
			{
				retVal += "Status ID changed from [" + oldStatusName + "] to [" + newStatusName + "].<br />\r\n";
			}
			if (oldAppt.AllDay != newAppt.AllDay)
			{
				retVal += "All Day changed from [" + oldAppt.AllDay + "] to [" + newAppt.AllDay + "].<br />\r\n";
			}
			if (oldAppt.EmailOnChange != newAppt.EmailOnChange)
			{
				retVal += "Email on change changed from [" + oldAppt.EmailOnChange + "] to [" + newAppt.EmailOnChange + "].<br />\r\n";
			}
			if (oldAppt.Email != newAppt.Email)
			{
				retVal += "Email changed from [" + oldAppt.Email + "] to [" + newAppt.Email + "].<br />\r\n";
			}
			if (oldAppt.Results != newAppt.Results)
			{
				retVal += "Results ID changed from [" + oldResultName + "] to [" + newResultName + "].<br />\r\n";
			}
			if (oldAppt.Notes != newAppt.Notes)
			{
				retVal += "Notes changed.  New Note:<br />\r\n" + newAppt.Notes;
			}
            //Code review point: (retVal != "")
			if (!string.IsNullOrEmpty(retVal))
			{
				retVal += "<br /><br />Original Appointment info";
				retVal += "<br />Start Date and Time: " + oldAppt.ApptStart.ToShortDateString() + " " + oldAppt.ApptStart.ToShortTimeString();
				retVal += "<br />Appointment Type: " + oldAptTypeName;
				retVal += "<br />Provider: " + oldProvName;
			}
			return retVal;
		}

		public static void LogEmail(int AptType, int PatientID, int EmployeeID, Appointment newAppt, bool Snail)
		{
			string MessageBody = "";
			if (Snail)
			{
				MessageBody = "No email address.  Information recorded for manual send. Appointment Tvpe: " + (Calendar.AppointmentTypes.GetApptType(newAppt.ApptTypeID)).TypeName;
			}
			else
			{
				MessageBody = "Confirmation email sent. Appointment Tvpe: " + (Calendar.AppointmentTypes.GetApptType(newAppt.ApptTypeID)).TypeName;
			}
			if (PatientID != 0)
			{

				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					con.Open();
                    using (SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                        SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                        rd.Read();
                        string StaffName = (string)rd["EmployeeName"];

                        rd.Close();

                        MessageBody += "\r\n<br>Changed by " + StaffName;

                        PatientID = newAppt.patient.ID;
                        using (SqlCommand cmd = new SqlCommand("contact_tbl_Cal_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@AptType", 57));
                            cmd.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                            cmd.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));
                            cmd.Parameters.Add(new SqlParameter("@Apt_ID", newAppt.EventID));

                            cmd.ExecuteNonQuery();
                        }
                    }
				}
			}
			else
			{

				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					con.Open();
                    using (SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                        SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                        rd.Read();
                        string StaffName = (string)rd["EmployeeName"];
                        rd.Close();

                        PatientID = newAppt.patient.ID;
                        MessageBody = "New appointment. Date Time: " + newAppt.ApptStart;

                        using (SqlCommand cmd = new SqlCommand("contact_tbl_Cal_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@AptType", AptType));
                            cmd.Parameters.Add(new SqlParameter("@PatientID", PatientID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                            cmd.Parameters.Add(new SqlParameter("@MessageBody", MessageBody));

                            cmd.ExecuteNonQuery();
                        }
                    }
				}

			}

		}

		public static void LogChange(int aptType, int patientID, int employeeID, Appointment oldAppt, Appointment newAppt, string addressFrom, string nameFrom, string apt_id)
		{


			string messageBody = "";
			if (oldAppt != null)
			{
				messageBody = CompareAppointments(oldAppt, newAppt);
			}
			else
			{
				string provName = Provider.GetProviderName(newAppt.ProviderID);
				string aptTypeName = AppointmentTypes.GetAptTypeName(newAppt.ApptTypeID);
                messageBody = "<b>New appointment added.<br/>  Start Time: </b>" + newAppt.ApptStart.ToString("MM/dd/yyyy hh:mm") + " <br/><b>End Time: </b>" + newAppt.ApptEnd.ToString("MM/dd/yyyy hh:mm");
                messageBody += "<br/><b>Provider: </b>" + provName + " <br/><b>Appointment Type: </b>" + aptTypeName + "<br/><b>Notes: </b>" + newAppt.Notes;
			}
            //Code review point:messageBody == "" 
            if (string.IsNullOrEmpty(messageBody))
				return;

			if (patientID != 0)
			{

				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					con.Open();
					//Code review points: dispose
                    using (SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                        SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                        string StaffName = "Unknown";
                        if (rd.Read())
                            StaffName = (string)rd["EmployeeName"];

                        rd.Close();
                        messageBody += "<br/>Changed by " + StaffName;

                        //PatientID = oldAppt.patient.ID;

                        //Code review points: dispose
                        using (SqlCommand cmd = new SqlCommand("contact_tbl_Cal_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@AptType", 57));
                            cmd.Parameters.Add(new SqlParameter("@PatientID", patientID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                            cmd.Parameters.Add(new SqlParameter("@MessageBody", messageBody));
                            cmd.Parameters.Add(new SqlParameter("@Apt_ID", apt_id));

                            cmd.ExecuteNonQuery();
                        }
                    }
					//send email if checked
					if (newAppt.EmailOnChange)
					{
						//Commented by jaswinder to remove oldAppt
                        //string msg = GetPatMessage(oldAppt, newAppt);
                        string msg = GetPatMessage(newAppt);
						//Below line commented by jaswinder
                        //if (msg != "")
                        if(!string.IsNullOrEmpty(msg))
						{
							System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(addressFrom, nameFrom);
							System.Net.Mail.MailMessage msg1 = new System.Net.Mail.MailMessage();//, newAppt.Email, "Appointment Change", MessageBody);
							msg1.From = from;
							msg1.To.Add(new System.Net.Mail.MailAddress(newAppt.Email));
							msg1.Subject = "Appointment Change";
							msg1.Body = msg;
							System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient("longevity-1.LongevityMedicalClinic.local");
							Client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
							Client.Send(msg1);
						}
					}
					if (oldAppt != null && (oldAppt.ApptStart != newAppt.ApptStart || oldAppt.ApptEnd != newAppt.ApptEnd))
					{
                        //Code review points: dispose
                        using (SqlCommand cmd2 = new SqlCommand("RemoveResult", con))
                        {
                            cmd2.Parameters.AddWithValue("@EventID", newAppt.EventID);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.ExecuteNonQuery();
                        }
					}
				}
			}
			else
			{

				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{

					con.Open();
                    using (SqlCommand cmd1 = new SqlCommand("Staff_GetByID", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                        SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                        rd.Read();
                        string StaffName = (string)rd["EmployeeName"];
                        rd.Close();

                        patientID = newAppt.patient.ID;
                        messageBody = "New appointment. Date Time: " + newAppt.ApptStart;

                        using (SqlCommand cmd = new SqlCommand("contact_tbl_Cal_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@AptType", aptType));
                            cmd.Parameters.Add(new SqlParameter("@PatientID", patientID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                            cmd.Parameters.Add(new SqlParameter("@MessageBody", messageBody));

                            cmd.ExecuteNonQuery();
                        }
                    }
				}

			}
		}

		public static string GetLastTouched(string aptID)
		{
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{

				con.Open();
                using (SqlCommand cmd1 = new SqlCommand("ContactTbl_GetLastTouched", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@Apt_ID", aptID));
                    string staffName = (string)cmd1.ExecuteScalar();
                    string initials = "";
                    if (staffName != null)
                    {
                        string[] names = staffName.Trim().Split(' ');

                        foreach (string name in names)
                        {
                            initials += name.First();
                        }
                    }
                    else
                    {
                        initials = "Not Available.";
                    }
                    return initials;
                }
			}
		}
	}
}
