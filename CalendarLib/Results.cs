using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Calendar
{
    public static class Results
    {

        public static List<Result> getResultsList()
        {
            List<Result> retVal = new List<Result>();
            //retVal.Add(new Result(0, "None Set", true,false));
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Results_Get"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    while (rd.Read())
                    {

                        retVal.Add(new Result((int)rd["ResultID"], (string)rd["ResultName"], (bool)rd["Active"], (bool)rd["IsActionRequired"], (string)rd["StatusName"], (int)rd["ResultStatusId"]));
                    }

                }
            }
            return retVal;

        }

        public static DataTable getResultListOnly()
        {

            List<Result> ret = new List<Result>();
            ret = getResultsList();


            DataTable dt = new DataTable();
            dt.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("ResultName");
            dt.Columns.Add("Active", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("IsActionRequired", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("StatusName");
            dt.Columns.Add("ResultStatusId",System.Type.GetType("System.Int32"));
           
            foreach (Result at in ret)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = at.ID;
                dr["ResultName"] = at.ResultName;
                dr["IsActionRequired"] = at.IsActionRequired;
                dr["Active"] = at.Active;
                dr["StatusName"] = at.StatusName;
                dr["ResultStatusId"] = at.ResultStatusId;
               
               

                dt.Rows.Add(dr);
            }
            return dt;
        }
        //Remove String ID parameter from the function by jaswinder

        public static void ResultsInsert(string ResultName)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Results_Insert"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ResultName", ResultName));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ResultsUpdate(string id, string ResultName, bool active,bool IsActionRequired,int ResultStausId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Results_Update"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ResultID", id));
                    cmd.Parameters.Add(new SqlParameter("@ResultName", ResultName));
                    cmd.Parameters.Add(new SqlParameter("@Active", active));
                    cmd.Parameters.Add(new SqlParameter("@IsActionRequired", IsActionRequired));
                    cmd.Parameters.Add(new SqlParameter("@ResultStatusId", ResultStausId));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static string GetResultName(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Results_GetById"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    string retVal = (string)cmd.ExecuteScalar();
                    return retVal;
                }
            }
        }

    }



    public class Result
    {
        public Result(int id, string name, bool active,bool isActionRequired,string statusname,int resultstatusid)
        {
            ID = id;
            ResultName = name;
            Active = active;
            IsActionRequired = isActionRequired;
            StatusName = statusname;
            ResultStatusId = resultstatusid;
        }
        public int ID { get; set; }
        public string ResultName { get; set; }
        public bool Active { get; set; }
        public bool IsActionRequired { get; set; }
        public string StatusName { get; set; }
        public int ResultStatusId { get; set; }
    }
}
