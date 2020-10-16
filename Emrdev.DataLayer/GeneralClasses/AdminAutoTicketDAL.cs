using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminAutoTicketDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
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
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
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

        public List<DepartmentViewModel> GetAutoshipDepartments()
        {
            //Emr2017
            //var objDept = ObjectEntity1.ssp_GetAutosipDepartments();
            List<DepartmentViewModel> _listDept = new List<DepartmentViewModel>();

            //foreach (var data in objDept)
            //{
            //    DepartmentViewModel oDept = new DepartmentViewModel();
            //    oDept.DepartmentID = Convert.ToInt16(data.DepartmentID);
            //    oDept.DepartmentName = data.DepartmentName;
            //    _listDept.Add(oDept);
            //}

            return _listDept;
        }

        public List<AdminAutoTicketViewModel> GetAutoticketList()
        {
            //Emr2017
            //var objResult = ObjectEntity1.ssp_GetRecurringTicket().ToList();
            var objIList = new List<AdminAutoTicketViewModel>();
            //Mapper.CreateMap<ssp_GetRecurringTicket_Result, AdminAutoTicketViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        

    }
}
