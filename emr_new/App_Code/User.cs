﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public class User
{
    public int EmployeeID { get; set; }
    public string Username { get; set; }
    public string AccessLevel { get; set; }
    public string EmployeeName { get; set; }
    public string AutoshipAccess { get; set; }
    public bool Recurring { get; set; }

    public User(string UserID)
    {
        IStaffService objIStaffService = new StaffService();
        StaffViewModel lstViewModel = objIStaffService.GetFirstOrDefaultStaffByEmployeeId(int.Parse(UserID));

        if (lstViewModel != null)
        {
            EmployeeID = lstViewModel.EmployeeID;
            Username = lstViewModel.username;
            AccessLevel = lstViewModel.access_level;
            EmployeeName = lstViewModel.EmployeeName;
            AutoshipAccess = lstViewModel.AutoshipAccess;
            Recurring = lstViewModel.AllowRecurring;
        }
        else
        {
            EmployeeID = 0;
            Username = "";
            AccessLevel = "";
            EmployeeName = "";
        }

        #region "Old Code"
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "Staff_GetByID";
        //    cmd.Parameters.Add(new SqlParameter("@EmployeeID", UserID));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    conn.Open();
        //    SqlDataReader rd = Calendar.Utils.OpenReader(cmd);
        //    if (rd.HasRows)
        //    {
        //        rd.Read();
        //        EmployeeID = (int)rd["EmployeeID"];
        //        Username = (string)rd["username"];
        //        AccessLevel = (string)rd["access_level"];
        //        EmployeeName = (string)rd["EmployeeName"];
        //        if (rd["AutoshipAccess"] != DBNull.Value)
        //            AutoshipAccess = (string)rd["AutoshipAccess"];
        //        else
        //            AutoshipAccess = "none";
        //        Recurring = (bool)rd["AllowRecurring"];
        //    }
        //    else
        //    {
        //        EmployeeID = 0;
        //        Username = "";
        //        AccessLevel = "";
        //        EmployeeName = "";
        //    }
        //}
        #endregion
    }
}