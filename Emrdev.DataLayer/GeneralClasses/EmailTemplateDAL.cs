using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses 
{
    public class EmailTemplateDAL :ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntityPart2.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntityPart2.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
        public void SaveSalesAccountCode(int salesAccountCode)
        {
            ObjectEntityPart2.ssp_SaveSalesAccountCode(salesAccountCode);
        }

        public SalesAccountCodeViewModel GetSalesAccountCode()
        {
            var objResult = ObjectEntityPart2.ssp_GetSalesAccountCode().FirstOrDefault();
            var objIList = new SalesAccountCodeViewModel();
            Mapper.CreateMap<ssp_GetSalesAccountCode_Result, SalesAccountCodeViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
           

        }
      
    }
}
