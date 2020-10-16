using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class StaffBAL
    {
        StaffDAL objStaffDAL = new StaffDAL();

        public List<StaffViewModel> GetStaffDetailsByDepartment(int DepartmentId)
        {
            List<StaffViewModel> lstViewModel = objStaffDAL.GetStaffDetailsByDepartment(DepartmentId);
            return lstViewModel;
        }

        public List<StaffViewModel> GetStaffDetailsByEmployeeId(int EmployeeID)
        {
            List<StaffViewModel> lstViewModel = objStaffDAL.GetStaffDetailsByEmployeeId(EmployeeID);
            return lstViewModel;
        }

        public StaffViewModel GetFirstOrDefaultStaffByEmployeeId(int EmployeeID)
        {
            StaffViewModel viewModel = objStaffDAL.GetFirstOrDefaultStaffByEmployeeId(EmployeeID);
            return viewModel;
        }

        /// <summary>
        /// Insert the staff ticket data for contacts
        /// </summary>
        /// <param name="PatinetId"></param>
        /// <param name="messageBody"></param>
        /// <param name="employeeID"></param>
        /// <param name="FollowupId"></param>
        public void InsertTicketIntoContactTbl(int PatinetId, string messageBody, int employeeID, int FollowupId)
        {
            objStaffDAL.InsertTicketIntoContactTbl(PatinetId, messageBody, employeeID, FollowupId);
        }

        /// <summary>
        /// Get the staff details by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public StaffViewModel GetFirstOrDefaultStaffByUserName(string userName)
        {
            StaffViewModel clsStaff = objStaffDAL.GetFirstOrDefaultStaffByUserName(userName);
            return clsStaff;
        }

        /// <summary>
        /// Get the list of all the employees to show in the staff list table
        /// Jaswinder 5th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<StaffViewModel> GetStaffList(int PageIndex, int PageSize)
        {
            return objStaffDAL.GetStaffList(PageIndex, PageSize);
        }

        /// <summary>
        /// Update the staff details
        /// Jaswinder 5th aug 2013
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="Password"></param>
        /// <param name="CanWritePrescript"></param>
        /// <param name="AccessLevel"></param>
        /// <param name="EmployeeId"></param>
        public void UpdateStaffLogin(string EmployeeName, string Password, bool CanWritePrescript, string AccessLevel, int EmployeeId, bool HARep,bool IsActive)
        {
            objStaffDAL.UpdateStaffLogin(EmployeeName, Password, CanWritePrescript, AccessLevel, EmployeeId,HARep, IsActive);
        }

        /// <summary>
        /// Insert new staff Details
        /// by jaswinder on 5 th aug 2013
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="Access_level"></param>
        /// <param name="CanWritePrescript"></param>
        public int InsertStaffLogin(string UserName, string Password, string EmployeeName, string Access_level, bool CanWritePrescript,bool HARep)
        {
            return objStaffDAL.InsertStaffLogin(UserName, Password, EmployeeName, Access_level, CanWritePrescript, HARep);
        }

        /// <summary>
        /// Update AutoshipAccess By EmployeId
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="autoshipAccess"></param>
        public void UpdateStaffAutoShipAccess(int staffId, string autoshipAccess)
        {
            objStaffDAL = new Emrdev.DataLayer.GeneralClasses.StaffDAL();
            objStaffDAL.UpdateStaffAutoShipAccess(staffId, autoshipAccess);
        }

        /// <summary>
        /// Select AutoshipAccess By Employee Id
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public string GetAutoshipAccessByStaffId(int EmployeeID)
        {
            objStaffDAL = new Emrdev.DataLayer.GeneralClasses.StaffDAL();
            return objStaffDAL.GetAutoshipAccessByEmployeeID(EmployeeID);
        }



        public List<StaffViewModel> GetStaff()
        {
            var _objStaffList = new List<StaffViewModel>();
            var StaffEntity = new List<Staff>();
            StaffEntity = objStaffDAL.GetAll<Staff>(o=>o.Active_YN==true).ToList();
            Mapper.CreateMap<Staff, StaffViewModel>();
            _objStaffList = Mapper.Map(StaffEntity, _objStaffList);
            return _objStaffList;
        }

    }
}
