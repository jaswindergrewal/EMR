using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    class XeroPatientsDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart1.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntityPart1.Set<T>().Remove(entityToDelete);
            ObjectEntityPart1.SaveChanges();
            //throw new NotImplementedException();
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
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).Count();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
