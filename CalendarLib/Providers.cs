using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace Calendar
{

    public static class Providers
    {


        private static List<Provider> ProviderList
        { get; set; }

        private static void dbGetProviders()
        {

            ProviderList = new List<Provider>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Providers_Get"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rd = Utils.OpenReader(cmd))
                    {//cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            Provider prov = new Provider(int.Parse(rd["id"].ToString()), rd["ProviderName"].ToString(), (bool)rd["Active"]);
                            prov.EmployeeID = (int)rd["EmployeeID"];

                            ProviderList.Add(prov);

                        }

                    }
                }
            }

        }
        private static void dbGetProvidersManage()
        {

            ProviderList = new List<Provider>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Providers_GetManage"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rd = Utils.OpenReader(cmd))//cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Provider prov = new Provider(int.Parse(rd[0].ToString()), rd[1].ToString(), (bool)rd["Active"]);
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
                            ProviderList.Add(prov);
                        }

                    }
                }
            }

        }



        public static List<Provider> getProviderList()
        {
            dbGetProviders();
            return ProviderList;
        }

        public static List<Provider> getProviderListManage()
        {
            dbGetProvidersManage();
            return ProviderList;
        }

        //public static void udpateProvider(string id, string ProviderName, bool Active)
        //{

        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "Providers_Update"; ;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@ProviderID", id));
        //    cmd.Parameters.Add(new SqlParameter("@ProviderName", ProviderName));
        //    cmd.Parameters.Add(new SqlParameter("@Active", Active));
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}

        public static void udpateProvider(Provider prov)
        {
            if (prov.Category == null) prov.Category = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Providers_Update"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProviderID", prov.id));
                    cmd.Parameters.Add(new SqlParameter("@ProviderName", prov.ProviderName));
                    cmd.Parameters.Add(new SqlParameter("@Active", prov.Active));
                    cmd.Parameters.Add(new SqlParameter("@MondayStart", prov.MondayStart));
                    cmd.Parameters.Add(new SqlParameter("@MondayEnd", prov.MondayEnd));
                    cmd.Parameters.Add(new SqlParameter("@TuesdayStart", prov.TuesdayStart));
                    cmd.Parameters.Add(new SqlParameter("@TuesdayEnd", prov.TuesdayEnd));
                    cmd.Parameters.Add(new SqlParameter("@WednesdayStart", prov.WednesdayStart));
                    cmd.Parameters.Add(new SqlParameter("@WednesdayEnd", prov.WednesdayEnd));
                    cmd.Parameters.Add(new SqlParameter("@ThursdayStart", prov.ThursdayStart));
                    cmd.Parameters.Add(new SqlParameter("@ThursdayEnd", prov.ThursdayEnd));
                    cmd.Parameters.Add(new SqlParameter("@FridayStart", prov.FridayStart));
                    cmd.Parameters.Add(new SqlParameter("@FridayEnd", prov.FridayEnd));
                    cmd.Parameters.Add(new SqlParameter("@Category", prov.Category));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Removed parameter 'string id' from the function by jaswinder
        public static void AddProvider(string ProviderName, bool Active)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Providers_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProviderName", ProviderName));
                    cmd.Parameters.Add(new SqlParameter("@Active", Active));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Function Commented by Jaswinder as it is not in used

        //public static void DeleteProvider(string id, string ProviderName)
        //{

        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "Providers_Insert"; ;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@ProviderID", id));
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}

    }
}
