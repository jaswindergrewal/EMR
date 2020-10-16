using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class DepartmentBAL
    {
        DepartmentDAL objDAL = new DepartmentDAL();

        /// <summary>
        /// Delete the department from the list
        /// </summary>
        /// <param name="DepartmentID"></param>
        public void DeleteDepartments(int DepartmentID)
        {
            objDAL.DeleteDepartments(DepartmentID);
        }

        /// <summary>
        /// Add new department in tha table Department
        /// </summary>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public DepartmentViewModel InsertDepartments(DepartmentViewModel Dept)
        {
            Department DepartmentEntity = new Department();

            Mapper.CreateMap<DepartmentViewModel, Department>();
            DepartmentEntity = AutoMapper.Mapper.Map(Dept, DepartmentEntity);
            //insert data in apt_FollowUps table
            objDAL.Create(DepartmentEntity);

            Dept.DepartmentID = DepartmentEntity.DepartmentID;
            Dept.DepartmentName = DepartmentEntity.DepartmentName;

            return Dept;
        }

        /// <summary>
        /// Get the list all the departments
        /// </summary>
        /// <returns></returns>
        public List<DepartmentViewModel> GetDepartments()
        {
            var _objDeptList = new List<DepartmentViewModel>();
            var DepartmentEntity = new List<Department>();
            DepartmentEntity = objDAL.GetAll<Department>(o=>o.DepartmentName!=null).OrderBy(o => o.DepartmentName).ToList();

            Mapper.CreateMap<Department, DepartmentViewModel>();
            _objDeptList = Mapper.Map(DepartmentEntity, _objDeptList);
            return _objDeptList;
        }

        /// <summary>
        /// Get the staff within the Departments
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public List<DepartmentStaffViewModel> GetDepartmentStaff(int DeptID)
        {
            return objDAL.GetDepartmentStaff(DeptID);
        }

        public List<DepartmentViewModel> GetDepartmentsforStaff(int staffID)
        {
            var _objDeptList = new List<DepartmentViewModel>();
            var DepartmentEntity = new List<DepartmentStaff>();
            DepartmentEntity = objDAL.GetAll<DepartmentStaff>(o => o.StaffID == staffID).ToList();

            Mapper.CreateMap<DepartmentStaff, DepartmentViewModel>();
            _objDeptList = Mapper.Map(DepartmentEntity, _objDeptList);
            return _objDeptList;
        }
    }
}
