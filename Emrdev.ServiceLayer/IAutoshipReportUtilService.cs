using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Emrdev.ViewModelLayer;
using System.Data.SqlClient;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAutoshipReportUtilService" in both code and config file together.
    [ServiceContract]
    public interface IAutoshipReportUtilService
    {
        [OperationContract]
        DataTable CancelledOrders();

        [OperationContract]
        DataTable OpenOrders();

        [OperationContract]
        DataTable ProductDemand(DateTime EndDate);

        [OperationContract]
        DataTable GetCandidates(DateTime StartDate, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        DataTable GetPreview(int PatientID, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        DataTable GetExceptions(DateTime StartDate, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        int CreateOrder(int PatientID, int BatchNum, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        int CreateOrder(int PatientID, int BatchNum, DateTime OrderDate, System.Data.SqlClient.SqlConnection conn, int StaffID);

        [OperationContract]
        void CreateOrderItem(int OrderID, int ProfileItemID, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        DataTable GetOrderItems(int BatchNum, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        void CleanBatch(int BatchNum, System.Data.SqlClient.SqlConnection conn);

        [OperationContract]
        List<PatientProfile> GetProfileException(int PatientID);

        [OperationContract]
        void RemoveBacth(int BatchID, SqlConnection conn);

        [OperationContract]
        int GenOrders(DataTable Candidates, bool Preview, System.Data.SqlClient.SqlConnection conn, DateTime StartDate, int EmpID);

       
    }
}
