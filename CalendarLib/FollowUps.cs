using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public class FollowU
	{
		public int PatientID { get; set; }
		public List<FollowUp> Fups = new List<FollowUp>();
		public List<FollowUp> Fups_Open = new List<FollowUp>();
		public FollowU(int patientID)
		{
			PatientID = patientID;
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.FollowUps_Get", conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@PatientID", patientID);
				conn.Open();
				SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						FollowUp t = new FollowUp();
						if (reader["Apt_ID"] != DBNull.Value)
							t.Apt_ID = (int)reader["Apt_ID"];
						if (reader["AptAssigned"] != DBNull.Value)
							t.AptAssigned = (int)reader["AptAssigned"];
						if (reader["FollowUp_Body"] != DBNull.Value)
							t.FollowUp_Body = (string)reader["FollowUp_Body"];
						else
							t.FollowUp_Body = "";
						t.FollowUp_Type_Desc = (string)reader["FollowUp_Type_Desc"];
						t.FollowUpID = (int)reader["FollowUp_ID"];
						t.DateEntered = t.DateEntered != null ? (DateTime)reader["DateEntered"] : (DateTime?)null;
						t.PatientID = patientID;
						if (reader["Range_Start"] != DBNull.Value)
							t.Range_Start = "[" + ((DateTime)reader["Range_Start"]).ToShortDateString() + "]";
						else
							t.Range_Start = "";
						if (reader["Range_End"] != DBNull.Value)
							t.Range_End = "[" + ((DateTime)reader["Range_End"]).ToShortDateString() + "]";
						else
							t.Range_End = "[]";
						t.Complete = reader["Complete"] != DBNull.Value ? (string)reader["Complete"] : "";
						t.Assigned = (string)reader["Assigned"];
						//t.DateEntered = (DateTime)reader["DateEntered"];
						Fups.Add(t);
						t.Label = t.Range_Start + "-" + t.Range_End + " " + t.FollowUp_Type_Desc;
						if (t.Complete == "No" && t.FollowUp_Type_Desc != "AS/Order Adjustment")
							Fups_Open.Add(t);
					}
					
				}
				else
				{
					FollowUp t = new FollowUp();
					t.FollowUp_Type_Desc = "No Follow-ups.";
					t.FollowUp_Body = "";
					t.FollowUpID = 0;
					t.PatientID = 0;
					t.Range_End = "";
					t.Range_Start = "";
					t.Complete = "";
					Fups.Add(t);
				}
			}
		}

	}

	[Serializable]
	public class FollowUp
	{
		public FollowUp() { }

		public FollowUp(string label)
		{
			Label = label;
			FollowUpID = 0;
		}

		public FollowUp(int Apt_ID, string marker)
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand("dbo.FollowUps_GetByAptID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Apt_ID", Apt_ID);
                    conn.Open();
                    SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string Marker = marker;
                        FollowUp_Body = (string)reader["FollowUp_Body"];
                        FollowUp_Type_Desc = (string)reader["FollowUp_Type_Desc"];
                        FollowUpID = (int)reader["FollowUp_ID"];
                        PatientID = (int)reader["patientID"];
                        if (reader["Range_Start"] != DBNull.Value)
                            Range_Start = "[" + ((DateTime)reader["Range_Start"]).ToShortDateString() + "]";
                        else
                            Range_Start = "";
                        if (reader["Range_End"] != DBNull.Value)
                            Range_End = "[" + ((DateTime)reader["Range_Start"]).ToShortDateString() + "]";
                        else
                            Range_End = "";
                        Complete = (string)reader["Complete"];
                        Assigned = (string)reader["Assigned"];
                        DateEntered = (DateTime)reader["DateEntered"];
                        Label = Range_Start + "-" + Range_End + " " + FollowUp_Type_Desc;
                        if (reader["Apt_ID"] != DBNull.Value)
                            Apt_ID = (int)reader["Apt_ID"];
                        if (reader["AptAssigned"] != DBNull.Value)
                            AptAssigned = (int)reader["AptAssigned"];
                        if (reader["EmployeeName"] != DBNull.Value)
                            EmployeeName = (string)reader["EmployeeName"];
                    }
                }
			}

		}
		public FollowUp(int followUpID)
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand("dbo.FollowUps_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FollowupID", followUpID);
                    conn.Open();
                    SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        FollowUp_Body = (string)reader["FollowUp_Body"];
                        FollowUp_Type_Desc = (string)reader["FollowUp_Type_Desc"];
                        FollowUpID = (int)reader["FollowUp_ID"];
                        PatientID = (int)reader["patientID"];
                        if (reader["Range_Start"] != DBNull.Value)
                            Range_Start = "[" + ((DateTime)reader["Range_Start"]).ToShortDateString() + "]";
                        else
                            Range_Start = "";
                        if (reader["Range_End"] != DBNull.Value)
                            Range_End = "[" + ((DateTime)reader["Range_Start"]).ToShortDateString() + "]";
                        else
                            Range_End = "";
                        Complete = (string)reader["Complete"];
                        Assigned = (string)reader["Assigned"];
                        DateEntered = !string.IsNullOrEmpty(reader["DateEntered"].ToString()) ? (DateTime)reader["DateEntered"] : (DateTime?)null;
                        Label = Range_Start + "-" + Range_End + " " + FollowUp_Type_Desc;
                        if (reader["Apt_ID"] != DBNull.Value)
                            Apt_ID = (int)reader["Apt_ID"];
                        if (reader["AptAssigned"] != DBNull.Value)
                            AptAssigned = (int)reader["AptAssigned"];
                        if (reader["EmployeeName"] != DBNull.Value)
                            EmployeeName = (string)reader["EmployeeName"];
                    }
                }

			}
		}

		public int Apt_ID { get; set; }
		public int AptAssigned { get; set; }
		public int PatientID { get; set; }
		public int FollowUpID { get; set; }
		public string Range_Start { get; set; }
		public string Range_End { get; set; }
		public string FollowUp_Type_Desc { get; set; }
		public string FollowUp_Body { get; set; }
		public string Complete { get; set; }
		public string Assigned { get; set; }
		public string Label { get; set; }
		public DateTime? DateEntered { get; set; }
		public string EmployeeName { get; set; }

		public bool Close(int apt_ID)
		{
			bool success = true;
			try
			{
				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{
					con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update apt_FollowUps set FollowUp_Assigned_YN = 1, AptAssigned=" + apt_ID.ToString() + " where FollowUp_ID = " + FollowUpID.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
				}
			}
			catch
			{
				success = false;
			}

			return success;
		}

		public bool Detach()
		{
			bool success = false;
			try
			{
				using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
				{
					con.Open();
                    using (SqlCommand cmd = new SqlCommand("Update apt_FollowUps set FollowUp_Assigned_YN = 0, AptAssigned=NULL where FollowUp_ID = " + FollowUpID.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
				}
			}
			catch
			{
				success = false;
			}

			return success;
		}
	}
}
