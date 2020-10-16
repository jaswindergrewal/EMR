using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public class Provider
	{
		public int id { get; set; }
		public string ProviderName { get; set; }
		public string Category { get; set; }
		public string MondayStart { get; set; }
		public string MondayEnd { get; set; }
		public string TuesdayStart { get; set; }
		public string TuesdayEnd { get; set; }
		public string WednesdayStart { get; set; }
		public string WednesdayEnd { get; set; }
		public string ThursdayStart { get; set; }
		public string ThursdayEnd { get; set; }
		public string FridayStart { get; set; }
		public string FridayEnd { get; set; }
		public bool Active { get; set; }
        //Added Employee id  on 17 jan 2014
        public int EmployeeID { get; set; }

		public Provider(int idParam, string ProviderNameParam, bool active)
		{
			id = idParam;
			ProviderName = ProviderNameParam;
			Active = active;
            
		}

     
		public static string GetProviderName(string providerID)
		{
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Provider_GetByID";
                    cmd.Parameters.Add(new SqlParameter("@ProviderID", providerID));
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rd = Utils.OpenReader(cmd))
                    {//cmd.ExecuteReader();
                        string provName = "";
                        if (rd.Read())
                            provName = (string)rd["ProviderName"];
                        return provName;
                    }
                }
			}
		}

		public static string GetProviderName(int providerID)
		{
			return GetProviderName(providerID.ToString());
		}

		public static List<Provider> GetProvider(string providerID)
		{
			List<Provider> ret = new List<Provider>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Provider_GetByID";
                    cmd.Parameters.Add(new SqlParameter("@ProviderID", providerID));
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    Provider prov;
                    if (rd.HasRows)
                    {
                        rd.Read();
                        prov = new Provider((int)rd["id"], (string)rd["ProviderName"], (bool)rd["Active"]);
                        prov.MondayStart = (string)rd["MondayStart"];
                        prov.MondayEnd = (string)rd["MondayEnd"];
                        prov.TuesdayStart = (string)rd["TuesdayStart"];
                        prov.TuesdayEnd = (string)rd["TuesdayEnd"];
                        prov.WednesdayStart = (string)rd["WednesdayStart"];
                        prov.WednesdayEnd = (string)rd["WednesdayEnd"];
                        prov.ThursdayStart = (string)rd["ThursdayStart"];
                        prov.ThursdayEnd = (string)rd["ThursdayEnd"];
                        prov.FridayStart = (string)rd["FridayStart"];
                        prov.FridayEnd = (string)rd["FridayEnd"];
                        prov.Category = (string)rd["Category"];
                        ret.Add(prov);
                        return ret;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
		}
	}
}
