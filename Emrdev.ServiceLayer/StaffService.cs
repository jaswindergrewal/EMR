using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StaffService" in both code and config file together.
    public class StaffService : IStaffService
    {
        StaffBAL objStaffBAL = new StaffBAL();
        public void DoWork()
        {
        }

        public List<StaffViewModel> GetStaffDetailsByDepartment(int DeptId)
        {
            List<StaffViewModel> objLst = objStaffBAL.GetStaffDetailsByDepartment(DeptId);
            return objLst;
        }


        public List<StaffViewModel> GetStaffDetailsByEmployeeId(int EmployeeID)
        {
            List<StaffViewModel> lstViewModel = objStaffBAL.GetStaffDetailsByEmployeeId(EmployeeID);
            return lstViewModel;
        }


        public StaffViewModel GetFirstOrDefaultStaffByEmployeeId(int EmployeeID)
        {
            StaffViewModel viewModel = objStaffBAL.GetFirstOrDefaultStaffByEmployeeId(EmployeeID);
            return viewModel;
        }


        public void InsertTicketIntoContactTbl(int PatinetId, string messageBody, int employeeID, int FollowupId)
        {
            objStaffBAL.InsertTicketIntoContactTbl(PatinetId, messageBody, employeeID, FollowupId);
        }


        public StaffViewModel GetFirstOrDefaultStaffByUserName(string userName)
        {
            StaffViewModel clsStaff = objStaffBAL.GetFirstOrDefaultStaffByUserName(userName);
            return clsStaff;
        }

        public List<StaffViewModel> GetStaffList(int PageIndex, int PageSize)
        {
            return objStaffBAL.GetStaffList(PageIndex, PageSize);
        }

        public void UpdateStaffLogin(string EmployeeName, string Password, bool CanWritePrescript, string AccessLevel, int EmployeeId, bool HARep,bool IsActive)
        {
            objStaffBAL.UpdateStaffLogin(EmployeeName, Password, CanWritePrescript, AccessLevel, EmployeeId,HARep, IsActive);
        }

        public int InsertStaffLogin(string UserName, string Password, string EmployeeName, string Access_level, bool CanWritePrescript,bool HARep)
        {
            return objStaffBAL.InsertStaffLogin(UserName, Password, EmployeeName, Access_level, CanWritePrescript, HARep);
        }

        public void UpdateStaffAutoShipAccess(int staffId, string autoshipAccess)
        {
            objStaffBAL = new Emrdev.BusinessLayer.GeneralClasses.StaffBAL();
            objStaffBAL.UpdateStaffAutoShipAccess(staffId, autoshipAccess);
        }

        public string GetAutoshipAccessByEmployeeID(int EmployeeID)
        {
            objStaffBAL = new Emrdev.BusinessLayer.GeneralClasses.StaffBAL();
            return objStaffBAL.GetAutoshipAccessByStaffId(EmployeeID);
        }
        public List<StaffViewModel> GetStaff()
        {
            List<StaffViewModel> objLst = objStaffBAL.GetStaff();
            return objLst;
        }
    }
}
