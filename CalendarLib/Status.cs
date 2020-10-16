using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public static class Status
	{

		public static List<OneStatus> dbGetStatus(bool all)
		{
			List<OneStatus> StatusList = new List<OneStatus>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Status_Get"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    OneStatus NoneStat = new OneStatus(-1, "", false);
                    if (all)
                        StatusList.Add(new OneStatus(0, "All", true));
                    while (rd.Read())
                    {
                        if ((string)rd["StatusName"] != "None")
                            StatusList.Add(new OneStatus(int.Parse(rd["id"].ToString()), rd["StatusName"].ToString(), (bool)rd["Active"]));
                        else
                        {
                            NoneStat = new OneStatus(int.Parse(rd["id"].ToString()), rd["StatusName"].ToString(), (bool)rd["Active"]);
                        }
                    }
                    if (NoneStat.id != -1)
                        StatusList.Add(NoneStat);
                }
                return StatusList;
            }
		}

		public static List<OneStatus> getStatusList()
		{
			return dbGetStatus(true);
		}

		public static List<OneStatus> getStatusListOnly()
		{
			return dbGetStatus(false);
		}

		public static void StatusUpdate(string id, string StatusName, bool active)
		{
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Status_Update"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StatusID", id));
                    cmd.Parameters.Add(new SqlParameter("@StatusName", StatusName));
                    cmd.Parameters.Add(new SqlParameter("@Active", active));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
		}

        //Remove string id parameter from the Function
        public static void StatusInsert(string StatusName)
		{
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Status_Insert"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StatusName", StatusName));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                
            }
		}
		public static string GetStatusName(int id)
		{
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Status_GetById"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    string retVal = (string)cmd.ExecuteScalar();
                    return retVal;
                }
            }
		}

	}
	public class OneStatus
	{
		public OneStatus(int ID, string statusName, bool active)
		{
			id = ID;
			StatusName = statusName;
			Active = active;
		}
		public int id { get; set; }
		public string StatusName { get; set; }
		public bool Active { get; set; }
	}
}
