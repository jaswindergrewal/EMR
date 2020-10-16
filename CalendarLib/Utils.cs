using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Calendar
{
	public class Utils
	{
		public static string FixUpString(object checker)
		{
			if (checker != DBNull.Value)
				return (string)checker;
			else
				return "";
		}

		public static SqlConnection GetConnection(string connectionString)
		{
			SqlConnection conn = new SqlConnection(connectionString);
			bool success = false;
			int tryCount = 0;
			while (tryCount < 5)
			{
				try
				{
					conn.Open();
					success = true;
					break;
				}
				catch(Exception)
				{
					tryCount++;
					if (tryCount >= 5) throw ;
				}
				if (success)
					break;
			}
			return conn;
		}

		public static SqlDataReader OpenReader(SqlCommand cmd)
		{
			SqlDataReader reader = null;
			int Counter = 0;
			while (Counter < 10)
			{
				try
				{
					reader = cmd.ExecuteReader();
					break;
				}
				catch(Exception)
				{
					if (cmd.Connection.State == ConnectionState.Open)
						cmd.Connection.Close();
					cmd.Connection.Open();
					Counter++;
					if (Counter >= 10) throw;
				}
			}
			return reader;
		}

		public static DataTable OpenTable(SqlCommand cmd)
		{
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {

                DataTable dt = new DataTable();
                int Counter = 0;
                while (Counter < 10)
                {
                    try
                    {
                        da.Fill(dt);
                        break;
                    }
                    catch (Exception)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                        cmd.Connection.Open();
                        Counter++;
                        if (Counter >= 10) throw;
                    }

                }
                return dt;
            }
			
		}

	}
}
