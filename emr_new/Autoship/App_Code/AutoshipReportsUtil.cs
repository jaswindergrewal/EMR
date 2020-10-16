﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

/// <summary>
/// Summary description for Reports
/// </summary>
public class AutoshipReportsUtil
{

    public static DataTable CancelledOrders()
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.CancelledOrders();
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();
        //    DataTable reportTable = new DataTable();
        //    SqlCommand cmd = new SqlCommand("Orders_GetCancelled", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    da.Fill(reportTable);
        //    return reportTable;
        //}


    }

    public static DataTable OpenOrders()
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.OpenOrders();
            return reportTable;
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();
        //    DataTable reportTable = new DataTable();
        //    SqlCommand cmd = new SqlCommand("Orders_GetOpen", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    da.Fill(reportTable);
        //    return reportTable;
        //}
    }

    public static DataTable ProductDemand(DateTime EndDate)
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.ProductDemand(EndDate);
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        #region "Old Code"
        //DataTable reportTable = new DataTable();
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();
        //    DateTime StartDate = EndDate;
        //    DataTable Candidates = GetCandidates(StartDate, conn);


        //    //Get Exeptions list
        //    DataTable Exceptions = GetExceptions(StartDate, conn);

        //    int BatchNum = -1;




        //    //Generate Orders and OrderItems
        //    int CurrentPatient = 0;
        //    int OrderID = 0;
        //    foreach (DataRow ProfItem in Candidates.Rows)
        //    {
        //        //create the order if patient changes
        //        if (CurrentPatient != (int)ProfItem["PatientID"])
        //        {
        //            OrderID = CreateOrder((int)ProfItem["PatientID"], BatchNum, conn);
        //        }
        //        CreateOrderItem(OrderID, (int)ProfItem["ProfileItemID"], conn);
        //        CurrentPatient = (int)ProfItem["PatientID"];
        //    }
        //    //Retrieve Order Items
        //    DataTable OrderItems = GetOrderItems(BatchNum, conn);

        //    //Modify Order Items based on Exceptions

        //    foreach (DataRow dr in OrderItems.Rows)
        //    {
        //        //Filter Exceptions based on PRofileItemID

        //        DataRow[] FilteredRows = Exceptions.Select("ProfileItemID=" + dr["ProfileItemID"].ToString());

        //        foreach (DataRow Exc in FilteredRows)
        //        {
        //            int FrequencyDays = -int.Parse((string)Exc["Frequency"]);
        //            //if Today is between start and end, and shipped is more than freequency in exception, modify Order Item
        //            if (dr["CloseDate"] == DBNull.Value || DateTime.Today.AddDays(FrequencyDays) >= (DateTime)dr["CloseDate"])
        //            {
        //                SqlCommand ExceptionUpdate = new SqlCommand();
        //                ExceptionUpdate.Connection = conn;
        //                ExceptionUpdate.CommandType = CommandType.StoredProcedure;
        //                ExceptionUpdate.CommandText = "OrderItem_Update";
        //                ExceptionUpdate.Parameters.AddWithValue("@OrderItemID", (int)dr["OrderItemID"]);
        //                ExceptionUpdate.Parameters.AddWithValue("@Quantity", (int)Exc["Quantity"]);
        //                ExceptionUpdate.ExecuteNonQuery();
        //            }
        //        }

        //    }
        //    CleanBatch(BatchNum, conn);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    //Get candidates for shipping
        //    SqlCommand cmd = new SqlCommand("ProductReport", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand = cmd;

        //    da.Fill(reportTable);


        //}

        //return reportTable;
        #endregion
    }

    public static DataTable GetCandidates(DateTime StartDate, SqlConnection conn)
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.GetCandidates(StartDate, conn);
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        //SqlDataAdapter da = new SqlDataAdapter();
        ////Get candidates for shipping.  All items with a start date before day entered
        //SqlCommand cmd = new SqlCommand("ProfileItemsOrders_Get", conn);
        //cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.Add(new SqlParameter("@StartDate", StartDate));
        //da.SelectCommand = cmd;
        //DataTable Candidates = new DataTable();
        //da.Fill(Candidates);
        //return Candidates;
    }

    public static DataTable GetPreview(int PatientID, SqlConnection conn)
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.GetPreview(PatientID, conn);
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        //SqlDataAdapter da = new SqlDataAdapter();
        ////Get candidates for shipping
        //SqlCommand cmd = new SqlCommand("ProfileItemsOrders_GetByPatient", conn);
        //cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.Add(new SqlParameter("@PatientID", PatientID));
        //da.SelectCommand = cmd;
        //DataTable Candidates = new DataTable();
        //da.Fill(Candidates);
        //return Candidates;
    }

    public static DataTable GetExceptions(DateTime StartDate, SqlConnection conn)
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.GetExceptions(StartDate, conn);
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        //SqlDataAdapter da = new SqlDataAdapter();
        ////Get candidates for shipping
        //SqlCommand cmd = new SqlCommand("Exceptions_GetByDate", conn);
        //cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.Add(new SqlParameter("@StartDate", StartDate.AddDays(1)));
        //da.SelectCommand = cmd;
        ////DataTable Candidates = new DataTable();
        ////da.Fill(Candidates);

        //DataTable Exceptions = new DataTable();
        //da.Fill(Exceptions);
        //return Exceptions;
    }

    public static int CreateOrder(int PatientID, int BatchNum, SqlConnection conn)
    {
        int i = 0;
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            i = objService.CreateOrder(PatientID, BatchNum, conn);
            return i;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }

        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Connection = conn;
        //cmd.CommandText = "Patients_GetByID";
        //cmd.Parameters.AddWithValue("@PatientID", PatientID);
        //SqlDataReader reader = cmd.ExecuteReader();
        //reader.Read();
        //bool HasShipAddress = false;

        //if (reader["ShippingStreet"] != DBNull.Value) HasShipAddress = true;

        //SqlCommand CreateComand = new SqlCommand();
        //CreateComand.CommandText = "Order_Create";
        //CreateComand.Connection = conn;
        //CreateComand.CommandType = CommandType.StoredProcedure;
        //CreateComand.Parameters.AddWithValue("@PatientID", PatientID);
        //CreateComand.Parameters.AddWithValue("@DatePrep", DateTime.Today);
        //CreateComand.Parameters.AddWithValue("@ShipName", (string)reader["FirstName"] + " " + (string)reader["LastName"]);
        //if (HasShipAddress)
        //{
        //    CreateComand.Parameters.AddWithValue("@ShipAddress1", reader["ShippingStreet"]);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress2", "");
        //    CreateComand.Parameters.AddWithValue("@ShipCity", reader["ShippingCity"]);
        //    CreateComand.Parameters.AddWithValue("@ShipState", reader["ShippingState"]);
        //    CreateComand.Parameters.AddWithValue("@ShipZip", reader["ShippingZip"]);
        //}
        //else
        //{
        //    CreateComand.Parameters.AddWithValue("@ShipAddress1", reader["BillingStreet"]);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress2", "");
        //    CreateComand.Parameters.AddWithValue("@ShipCity", reader["BillingCity"]);
        //    CreateComand.Parameters.AddWithValue("@ShipState", reader["BillingState"]);
        //    CreateComand.Parameters.AddWithValue("@ShipZip", reader["BillingZip"]);
        //}
        //CreateComand.Parameters.AddWithValue("@Batch", BatchNum);
        //reader.Close();
        //decimal res = (decimal)CreateComand.ExecuteScalar();

        //return Convert.ToInt32(res);

    }

    public static int CreateOrder(int PatientID, int BatchNum, DateTime OrderDate, SqlConnection conn, int StaffID)
    {
        int i = 0;
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            i = objService.CreateOrder(PatientID, BatchNum, OrderDate, conn, StaffID);
            return i;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }

        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Connection = conn;
        //cmd.CommandText = "Patients_GetByID";
        //cmd.Parameters.AddWithValue("@PatientID", PatientID);
        //SqlDataReader reader = cmd.ExecuteReader();
        //reader.Read();
        //bool HasShipAddress = false;

        //if (reader["ShippingStreet"] != DBNull.Value) HasShipAddress = true;

        ////SqlCommand ExceptionCommand = new SqlCommand("ProfileExceptionGet",conn");
        //SqlCommand CreateComand = new SqlCommand();
        //CreateComand.CommandText = "Order_Create";
        //CreateComand.Connection = conn;
        //CreateComand.CommandType = CommandType.StoredProcedure;
        //CreateComand.Parameters.AddWithValue("@PatientID", PatientID);
        //CreateComand.Parameters.AddWithValue("@DatePrep", OrderDate);
        //string ShipName = (string)reader["FirstName"] + " " + (string)reader["LastName"];
        //CreateComand.Parameters.AddWithValue("@ShipName", ShipName);
        //if (HasShipAddress)
        //{
        //    CreateComand.Parameters.AddWithValue("@ShipAddress1", reader["ShippingStreet"]);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress2", "");
        //    CreateComand.Parameters.AddWithValue("@ShipCity", reader["ShippingCity"]);
        //    CreateComand.Parameters.AddWithValue("@ShipState", reader["ShippingState"]);
        //    CreateComand.Parameters.AddWithValue("@ShipZip", reader["ShippingZip"]);
        //}
        //else
        //{
        //    CreateComand.Parameters.AddWithValue("@ShipAddress1", reader["BillingStreet"]);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress2", "");
        //    CreateComand.Parameters.AddWithValue("@ShipCity", reader["BillingCity"]);
        //    CreateComand.Parameters.AddWithValue("@ShipState", reader["BillingState"]);
        //    CreateComand.Parameters.AddWithValue("@ShipZip", reader["BillingZip"]);
        //}
        //CreateComand.Parameters.AddWithValue("@EmployeeID", StaffID);
        //CreateComand.Parameters.AddWithValue("@Batch", BatchNum);
        //reader.Close();

        //cmd.CommandText = "ProfileExceptions_GetByPatient";
        //reader = cmd.ExecuteReader();
        //if (reader.HasRows)
        //{
        //    reader.Read();
        //    CreateComand.Parameters.Clear();
        //    CreateComand.Parameters.AddWithValue("@PatientID", PatientID);
        //    CreateComand.Parameters.AddWithValue("@DatePrep", OrderDate);
        //    CreateComand.Parameters.AddWithValue("@ShipName", ShipName);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress1", reader["ShippingStreet"]);
        //    CreateComand.Parameters.AddWithValue("@ShipAddress2", "");
        //    CreateComand.Parameters.AddWithValue("@ShipCity", reader["ShippingCity"]);
        //    CreateComand.Parameters.AddWithValue("@ShipState", reader["ShippingState"]);
        //    CreateComand.Parameters.AddWithValue("@ShipZip", reader["ShippingZip"]);
        //    CreateComand.Parameters.AddWithValue("@EmployeeID", StaffID);
        //    CreateComand.Parameters.AddWithValue("@Batch", BatchNum);
        //}
        //reader.Close();

        //decimal res = (decimal)CreateComand.ExecuteScalar();

        //return Convert.ToInt32(res);

    }

    public static void CreateOrderItem(int OrderID, int ProfileItemID, SqlConnection conn)
    {
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            objService.CreateOrderItem(OrderID, ProfileItemID, conn);
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }

        //SqlCommand CreateItem = new SqlCommand();
        //CreateItem.CommandType = CommandType.StoredProcedure;
        //CreateItem.Connection = conn;
        //CreateItem.CommandText = "OrderItem_Create";
        //CreateItem.Parameters.AddWithValue("@OrderID", OrderID);
        //CreateItem.Parameters.AddWithValue("@ProfileItemID", ProfileItemID);

        //CreateItem.ExecuteNonQuery();
    }

    public static DataTable GetOrderItems(int BatchNum, SqlConnection conn)
    {
        DataTable reportTable = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            reportTable = new DataTable();
            objService = new AutoshipReportUtilService();
            reportTable = objService.GetOrderItems(BatchNum, conn);
            return reportTable;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            reportTable = null;
            objService = null;
        }

        //SqlDataAdapter da = new SqlDataAdapter();
        ////Get candidates for shipping
        //SqlCommand cmd = new SqlCommand("OrderItems_GetBatch", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@BatchID", BatchNum);

        //DataTable OrderItems = new DataTable();
        //da.SelectCommand = cmd;
        //da.Fill(OrderItems);
        //return OrderItems;
    }

    public static void CleanBatch(int BatchNum, SqlConnection conn)
    {
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            objService.GetOrderItems(BatchNum, conn);
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }
        ////Advance start dates for items with 0 quantity
        //SqlCommand ChangeDate = new SqlCommand("", conn);
        //ChangeDate.CommandText = "select * from OrderItems where Quantity=0";
        //ChangeDate.CommandType = CommandType.Text;
        //SqlDataAdapter da = new SqlDataAdapter(ChangeDate);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //foreach (DataRow dr in dt.Rows)
        //{
        //    SqlCommand theItemCommand = new SqlCommand("select * from ProfileItems where ProfileItemID=" + dr["ProfileItemID"].ToString(), conn);
        //    theItemCommand.CommandType = CommandType.Text;
        //    SqlDataAdapter da1 = new SqlDataAdapter(theItemCommand);
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    if (dt1.Rows.Count > 0)
        //    {
        //        int freq = int.Parse(dt1.Rows[0]["Frequency"].ToString());
        //        DateTime NextShipDate = DateTime.Parse(dt1.Rows[0]["NextShipDate"].ToString());
        //        string sql = "update ProfileItems set NextShipDate='" + NextShipDate.AddMonths(freq).ToString() + "' where ProfileItemID=" + dr["ProfileItemID"].ToString();
        //        SqlCommand UpdateItem = new SqlCommand(sql, conn);
        //        UpdateItem.CommandType = CommandType.Text;
        //        UpdateItem.ExecuteNonQuery();
        //    }
        //}

        ////clean all 0 balance items from OrderIems and any orders with non items
        //SqlCommand cmd = new SqlCommand("CleanBatch", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@BatchID", BatchNum);
        //cmd.ExecuteNonQuery();
    }

    public static List<Emrdev.ViewModelLayer.PatientProfile> GetProfileException(int PatientID)
    {
        List<Emrdev.ViewModelLayer.PatientProfile> pprf = null;
        IAutoshipReportUtilService objService = null;
        try
        {
            pprf = new List<Emrdev.ViewModelLayer.PatientProfile>();
            objService = new AutoshipReportUtilService();
            pprf = objService.GetProfileException(PatientID);
            return pprf;
        }
        catch (System.Exception)
        {
            throw;
        }
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("GetProfileException", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@PatientID", PatientID));
        //    SqlDataReader reader1 = cmd.ExecuteReader();
        //    PatientProfile prf = new PatientProfile();
        //    reader1.Read();

        //    prf.ShippingStreet = (string)reader1["ShippingStreet"];
        //    prf.ShippingCity = (string)reader1["ShippingCity"];
        //    prf.ShippingState = (string)reader1["ShippingState"];
        //    prf.ShippingZip = (string)reader1["ShippingZip"];
        //    prf.PatientID = (int)reader1["PatientID"];
        //    try
        //    {
        //        prf.StartDate = ((DateTime)reader1["StartDate"]).ToShortDateString();
        //        prf.EndDate = ((DateTime)reader1["EndDate"]).ToShortDateString();
        //        prf.Exception = "Yes";
        //    }
        //    catch
        //    {
        //        prf.StartDate = "NA";
        //        prf.EndDate = "NA";
        //        prf.Exception = "No";
        //    }


        //    List<PatientProfile> pprf = new List<PatientProfile>();
        //    reader1.Close();
        //    pprf.Add(prf);
        //    return pprf;
        //}
    }

    public static void RemoveBacth(int BatchID,SqlConnection conn)
    {
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            objService.RemoveBacth(BatchID,conn);
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("Orders_RemoveBatch", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@BatchID", BatchID));
        //    cmd.ExecuteNonQuery();
        //}
    }

    public static int GenOrders(DataTable Candidates, bool Preview, SqlConnection conn, DateTime StartDate, int EmpID)
    {
        int i = 0;
        IAutoshipReportUtilService objService = null;
        try
        {
            objService = new AutoshipReportUtilService();
            i = objService.GenOrders(Candidates, Preview, conn, StartDate, EmpID);
            return i;
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            objService = null;
        }

        //Get Exeptions list
        //DataTable Exceptions = AutoshipReportsUtil.GetExceptions(StartDate, conn);
        //if (EmpID == null || EmpID == 0) EmpID = 146;
        ////assign a batch number
        //SqlCommand Batch = new SqlCommand();
        //Batch.Connection = conn;
        //Batch.CommandType = CommandType.Text;
        //Batch.CommandText = "SELECT MAX(BATCH) from Orders";
        //int BatchNum = 0;
        //if (!Preview)
        //{

        //    //if there are no recorrds, 0 is the batch number
        //    try
        //    {
        //        BatchNum = (int)Batch.ExecuteScalar();
        //    }
        //    catch
        //    {

        //    }
        //    //increment it to the next digit

        //    BatchNum++;
        //}
        //else
        //{
        //    BatchNum = -1;
        //}
        ////Session["Batch"] = BatchNum;

        ////Generate Orders and OrderItems
        //int CurrentPatient = 0;
        //int OrderID = 0;
        //DateTime currentDate = DateTime.MinValue;
        ////these are ordered by patient
        //foreach (DataRow ProfItem in Candidates.Rows)
        //{
        //    if (ProfItem.RowState != DataRowState.Deleted)
        //    {

        //        //create the order if patient changes
        //        if (CurrentPatient != (int)ProfItem["PatientID"] || OrderID == 0 || currentDate != (DateTime)ProfItem["NextShipDate"])
        //        {
        //            if (!Preview)
        //                OrderID = CreateOrder((int)ProfItem["PatientID"], BatchNum, StartDate, conn, EmpID);
        //            else
        //            {
        //                if (int.Parse((string)ProfItem["Frequency"]) > 0)
        //                    OrderID = CreateOrder((int)ProfItem["PatientID"], BatchNum, ((DateTime)ProfItem["LastShipped"]).AddMonths(int.Parse((string)ProfItem["Frequency"])), conn, EmpID);
        //                else
        //                    OrderID = CreateOrder((int)ProfItem["PatientID"], BatchNum, ((DateTime)ProfItem["LastShipped"]).AddDays(Math.Abs(int.Parse((string)ProfItem["Frequency"]))), conn, EmpID);
        //            }
        //        }
        //        //add the item
        //        CreateOrderItem(OrderID, (int)ProfItem["ProfileItemID"], conn);

        //        CurrentPatient = (int)ProfItem["PatientID"];
        //        currentDate = (DateTime)ProfItem["NextShipDate"];
        //    }
        //}
        ////Retrieve Order Items All items that are due are here.
        //DataTable OrderItems = GetOrderItems(BatchNum, conn);

        ////Modify Order Items based on Exceptions

        //foreach (DataRow dr in OrderItems.Rows)
        //{
        //    //Filter Exceptions based on PRofileItemID


        //    DataRow[] FilteredRows = Exceptions.Select("ProfileItemID=" + dr["ProfileItemID"].ToString());

        //    foreach (DataRow Exc in FilteredRows)
        //    {
        //        //if ShipDate is between start and end date of exeption 
        //        if ((DateTime)dr["ShipDate"] >= (DateTime)Exc["DateStart"] && (DateTime)dr["ShipDate"] <= (DateTime)Exc["DateEnd"])
        //        {
        //            if ((DateTime)Exc["NextShipDate"] > StartDate)
        //            {
        //                SqlCommand ExceptionUpdate = new SqlCommand();
        //                ExceptionUpdate.Connection = conn;
        //                ExceptionUpdate.CommandType = CommandType.StoredProcedure;
        //                ExceptionUpdate.CommandText = "OrderItem_Update";
        //                ExceptionUpdate.Parameters.AddWithValue("@OrderItemID", (int)dr["OrderItemID"]);
        //                ExceptionUpdate.Parameters.AddWithValue("@Quantity", 0);
        //                ExceptionUpdate.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                SqlCommand ExceptionUpdate = new SqlCommand();
        //                ExceptionUpdate.Connection = conn;
        //                ExceptionUpdate.CommandType = CommandType.StoredProcedure;
        //                ExceptionUpdate.CommandText = "OrderItem_Update";
        //                ExceptionUpdate.Parameters.AddWithValue("@OrderItemID", (int)dr["OrderItemID"]);
        //                ExceptionUpdate.Parameters.AddWithValue("@Quantity", (int)Exc["Quantity"]);
        //                ExceptionUpdate.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //}
        ////This removes any items with a quantity of 0 
        //CleanBatch(BatchNum, conn);
        //return BatchNum;
    }


}