using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;
using System.Data.SqlClient;

namespace Emrdev.BusinessLayer.GeneralClasses
{

    public class AutoshipReportUtilBAL
    {
        public static DataTable CancelledOrders()
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.CancelledOrders();
            return reportTable;
        }

        public static DataTable OpenOrders()
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.OpenOrders();
            return reportTable;
        }

        public static DataTable ProductDemand(DateTime EndDate)
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.ProductDemand(EndDate);
            return reportTable;
        }

        public static DataTable GetCandidates(DateTime StartDate, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.GetCandidates(StartDate, conn);
            return reportTable;
        }

        public static DataTable GetPreview(int PatientID, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.GetPreview(PatientID, conn);
            return reportTable;
        }

        public static DataTable GetExceptions(DateTime StartDate, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.GetExceptions(StartDate, conn);
            return reportTable;
        }

        public static int CreateOrder(int PatientID, int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            int i = 0;
            i = AutoshipReportUtilDAL.CreateOrder(PatientID, BatchNum, conn);
            return i;
        }

        public static int CreateOrder(int PatientID, int BatchNum, DateTime OrderDate, System.Data.SqlClient.SqlConnection conn, int StaffID)
        {
            int i = 0;
            i = AutoshipReportUtilDAL.CreateOrder(PatientID, BatchNum, OrderDate, conn, StaffID);
            return i;
        }

        public static void CreateOrderItem(int OrderID, int ProfileItemID, System.Data.SqlClient.SqlConnection conn)
        {
            AutoshipReportUtilDAL.CreateOrderItem(OrderID, ProfileItemID, conn);
        }

        public static DataTable GetOrderItems(int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable = AutoshipReportUtilDAL.GetOrderItems(BatchNum, conn);
            return reportTable;
        }

        public static void CleanBatch(int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            AutoshipReportUtilDAL.CleanBatch(BatchNum, conn);
        }

        public static List<PatientProfile> GetProfileException(int PatientID)
        {
            List<PatientProfile> pprf =new List<PatientProfile>();
            pprf = AutoshipReportUtilDAL.GetProfileException(PatientID);
            return pprf;           
        }

        public static void RemoveBacth(int BatchID,SqlConnection conn)
        {
            AutoshipReportUtilDAL.RemoveBacth(BatchID,conn);
        }

        public static int GenOrders(DataTable Candidates, bool Preview, System.Data.SqlClient.SqlConnection conn, DateTime StartDate, int EmpID)
        {
            int i = 0;
            i = AutoshipReportUtilDAL.GenOrders(Candidates, Preview, conn, StartDate, EmpID);
            return i;
        }
    }
}
