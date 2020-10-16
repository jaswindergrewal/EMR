using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DepartmentService" in both code and config file together.
    public class DepartmentService : IDepartmentService
    {
        DepartmentBAL objBAL = new DepartmentBAL();
        public void DeleteDepartments(int DepartmentID)
        {
            objBAL.DeleteDepartments(DepartmentID);
        }

        public DepartmentViewModel InsertDepartments(DepartmentViewModel Dept)
        {
            return objBAL.InsertDepartments( Dept);
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            return objBAL.GetDepartments();
        }

        public List<DepartmentStaffViewModel> GetDepartmentStaff(int DeptID)
        {
            return objBAL.GetDepartmentStaff(DeptID);
        }

        public List<DepartmentViewModel> GetDepartmentsforStaff(int staffID)
        {
            return objBAL.GetDepartmentsforStaff(staffID);
        }
    }
}
