using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SiteDAL : ObjectEntity, IRepositary
    {

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            //throw new NotImplementedException();
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            //throw new NotImplementedException();
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            //throw new NotImplementedException();
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {

            throw new NotImplementedException();
            // _ObjectEntity.Set<T>();
        }
        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }


        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {

            return ObjectEntity1.Set<T>().Where(whereCondition).Count();
        }



        #region IRepositary Members


        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public dynamic GetAllPatients(string prefixText)
        {
          
            var objGetPatients = ObjectEntity1.ssp_GetPatientForSearch(prefixText);
            return objGetPatients;
        }



        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
