using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;
using System.Data.SqlClient;
using System.Globalization;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutoshipReportUtilService" in both code and config file together.
    public class AutoshipReportUtilService : IAutoshipReportUtilService
    {
        
         public DataTable CancelledOrders()
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.CancelledOrders();
            return reportTable;
        }


        public DataTable OpenOrders()
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.OpenOrders();
            return reportTable;
        }

        public DataTable ProductDemand(DateTime EndDate)
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.ProductDemand(EndDate);
            return reportTable;
        }


        public DataTable GetCandidates(DateTime StartDate, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.GetCandidates(StartDate, conn);
            return reportTable;
        }


        public DataTable GetPreview(int PatientID, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.GetPreview(PatientID, conn);
            return reportTable;
        }


        public DataTable GetExceptions(DateTime StartDate, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.GetExceptions(StartDate, conn);
            return reportTable;
        }


        public int CreateOrder(int PatientID, int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            int i = 0;
            i = AutoshipReportUtilBAL.CreateOrder(PatientID, BatchNum, conn);
            return i;
        }


        public int CreateOrder(int PatientID, int BatchNum, DateTime OrderDate, System.Data.SqlClient.SqlConnection conn, int StaffID)
        {
            int i = 0;
            i = AutoshipReportUtilBAL.CreateOrder(PatientID, BatchNum, OrderDate, conn, StaffID);
            return i;
        }


        public void CreateOrderItem(int OrderID, int ProfileItemID, System.Data.SqlClient.SqlConnection conn)
        {
            AutoshipReportUtilBAL.CreateOrderItem(OrderID, ProfileItemID, conn);
        }


        public DataTable GetOrderItems(int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            DataTable reportTable = new DataTable();
            reportTable.Locale = CultureInfo.InvariantCulture;
            reportTable = AutoshipReportUtilBAL.GetOrderItems(BatchNum, conn);
            return reportTable;
        }


        public void CleanBatch(int BatchNum, System.Data.SqlClient.SqlConnection conn)
        {
            AutoshipReportUtilBAL.CleanBatch(BatchNum, conn);
        }


        public List<PatientProfile> GetProfileException(int PatientID)
        {
            List<PatientProfile> pprf = new List<PatientProfile>();
            pprf = AutoshipReportUtilBAL.GetProfileException(PatientID);
            return pprf;
        }


        public void RemoveBacth(int BatchID, SqlConnection conn)
        {
            AutoshipReportUtilBAL.RemoveBacth(BatchID,conn);
        }


        public int GenOrders(DataTable Candidates, bool Preview, System.Data.SqlClient.SqlConnection conn, DateTime StartDate, int EmpID)
        {
            int i = 0;
            i = AutoshipReportUtilBAL.GenOrders(Candidates, Preview, conn, StartDate, EmpID);
            return i;
        }
    }
}
