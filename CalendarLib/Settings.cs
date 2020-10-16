using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public class Settings
	{
		public Settings()
		{
			getValues();
		}

		private void getValues()
		{
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{

				con.Open();

                using (SqlCommand cmd = new SqlCommand("CalendarSettings_Get", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        BusinessBeginsHour = (int)reader["BusinessBeginsHour"];
                        BusinessEndsHour = (int)reader["BusinessEndsHour"];
                        EventCorners = (string)reader["EventCorners"];
                        EventSelectColor = (string)reader["EventSelectColor"];
                        ShowAllDayEvents = (bool)reader["ShowAllDayEvents"];
                        AllDayEventBackColor = (string)reader["AllDayEventBackColor"];
                        BackColor = (string)reader["BackColor"];
                        BorderColor = (string)reader["BorderColor"];
                        CellSelectColor = (string)reader["CellSelectColor"];
                        EventBackColor = (string)reader["EventBackColor"];
                        EventBorderColor = (string)reader["EventBorderColor"];
                        HourBorderColor = (string)reader["HourBorderColor"];
                        HourHalfBorderColor = (string)reader["HourHalfBorderColor"];
                        HourNameBackColor = (string)reader["HourNameBackColor"];
                        HourNameBorderColor = (string)reader["HourNameBorderColor"];
                        NonBusinessBackColor = (string)reader["NonBusinessBackColor"];
                        ScrollPositionHour = (int)reader["ScrollPositionHour"];
                        TodayBorderColor = (string)reader["TodayBorderColor"];
                        TodayBorderStyle = (string)reader["TodayBorderStyle"];
                        TodayBorderWisth = (string)reader["TodayBorderWisth"];
                        SelectedDayBackColor = (string)reader["SelectedDayBackColor"];
                        SelectedDayForeColor = (string)reader["SelectedDayForeColor"];
                        SelectedDayStyle = (string)reader["SelectedDayStyle"];
                        TitleBackColor = (string)reader["TitleBackColor"];
                        OtherDayForeColor = (string)reader["OtherDayForeColor"];
                        ContactAptType = (int)reader["ContactAptType"];
                        EmailFromAddress = (string)reader["EmailFromAddress"];
                        EmailFromName = (string)reader["EmailFromName"];
                    }
                }
			}
		}

		public int BusinessBeginsHour { get; set; }
		public int BusinessEndsHour { get; set; }
		public string EventCorners { get; set; }
		public string EventSelectColor { get; set; }
		public bool ShowAllDayEvents { get; set; }
		public string AllDayEventBackColor { get; set; }
		public string BackColor { get; set; }
		public string BorderColor { get; set; }
		public string CellSelectColor { get; set; }
		public string EventBackColor { get; set; }
		public string EventBorderColor { get; set; }
		public string HourBorderColor { get; set; }
		public string HourHalfBorderColor { get; set; }
		public string HourNameBackColor { get; set; }
		public string HourNameBorderColor { get; set; }
		public string NonBusinessBackColor { get; set; }
		public int ScrollPositionHour { get; set; }
		public string TodayBorderColor { get; set; }
		public string TodayBorderStyle { get; set; }
		public string TodayBorderWisth { get; set; }
		public string SelectedDayBackColor { get; set; }
		public string SelectedDayForeColor { get; set; }
		public string SelectedDayStyle { get; set; }
		public string TitleBackColor { get; set; }
		public string OtherDayForeColor { get; set; }
		public int ContactAptType { get; set; }
		public string EmailFromName { get; set; }
		public string EmailFromAddress { get; set; }
	}
}
