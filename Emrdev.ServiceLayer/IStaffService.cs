using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStaffService" in both code and config file together.
    [ServiceContract]
    public interface IStaffService
    {
        [OperationContract]
        List<StaffViewModel> GetStaffDetailsByDepartment(int DeptId);

        [OperationContract]
        List<StaffViewModel> GetStaffDetailsByEmployeeId(int EmployeeID);

        [OperationContract]
        StaffViewModel GetFirstOrDefaultStaffByEmployeeId(int EmployeeID);

        [OperationContract]
        void InsertTicketIntoContactTbl(int PatinetId, string messageBody, int employeeID, int FollowupId);

        [OperationContract]
        StaffViewModel GetFirstOrDefaultStaffByUserName(string userName);

        [OperationContract]
        List<StaffViewModel> GetStaffList(int PageIndex, int PageSize);

        [OperationContract]
        void UpdateStaffLogin(string EmployeeName, string Password, bool CanWritePrescript, string AccessLevel, int EmployeeId, bool HARep,bool IsActive);

        [OperationContract]
        int InsertStaffLogin(string UserName, string Password, string EmployeeName, string Access_level, bool CanWritePrescript,bool HARep);

        [OperationContract]
        void DoWork();

        [OperationContract]
        void UpdateStaffAutoShipAccess(int staffId, string autoshipAccess);

        [OperationContract]
        string GetAutoshipAccessByEmployeeID(int EmployeeID);      

        [OperationContract]
        List<StaffViewModel> GetStaff();
    }
}
