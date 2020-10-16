using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer
{
    public interface IRepositary
    {
        void Create<T>(T entityToCreate) where T : class;
        void Edit<T>(T entityToEdit) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        IQueryable<T> List<T>() where T : class;
        T GetAll<T>() where T : class;
        IList<T> GetAll<T>(Expression<Func<T, bool>> whereCondition) where T : class;
        T Get<T>(Expression<Func<T, bool>> whereCondition) where T : class;
        long Count<T>(Expression<Func<T, bool>> whereCondition) where T : class;
        IList<T> GetDetails<T>() where T : class;
        //IQueryable<T> ListSelected<T>(Expression<Func<T, bool>> whereCondition) where T : class;
        //IList<T> GetAll<T>(Expression<Func<T, bool>> whereCondition) where T : class;
    }
}
