using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Calendar
{
	public class User
	{
		public int EmployeeID { get; set; }
		public string Username { get; set; }
		public string AccessLevel { get; set; }
		public string EmployeeName { get; set; }

		public User(string UserID)
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Staff_GetByID";
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID", UserID));
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Read();
                        EmployeeID = (int)rd["EmployeeID"];
                        Username = (string)rd["username"];
                        AccessLevel = (string)rd["access_level"];
                        EmployeeName = (string)rd["EmployeeName"];
                    }
                    else
                    {
                        EmployeeID = 0;
                        Username = "";
                        AccessLevel = "";
                        EmployeeName = "";
                    }
                }
			}
		}
	}
}
