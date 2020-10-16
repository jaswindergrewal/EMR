using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class DepartmentDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public void DeleteDepartments(int DepartmentID)
        {
            ObjectEntity1.ssp_DeleteDepartments(DepartmentID);
        }

        public List<DepartmentStaffViewModel> GetDepartmentStaff(int DeptID)
        {
            var Staff=ObjectEntity1.ssp_GetstaffForDepartments(DeptID);
            List<DepartmentStaffViewModel> _objStaffList = new List<DepartmentStaffViewModel>();
            foreach(var data in Staff)
            {
                DepartmentStaffViewModel oStaff = new DepartmentStaffViewModel();
                oStaff.EmployeeName = data.EmployeeName.ToString();
                oStaff.DepartmentStaffID = Convert.ToInt16(data.DepartmentStaffID);
                _objStaffList.Add(oStaff);
            }
            return _objStaffList;
        }
    }
}
