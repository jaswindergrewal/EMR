using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminMatchLabDAL : ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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

        public List<AdminMatchLabViewModel> GetMatchLabsList(string year)
        {///Emr2017
            //var objResult = ObjectEntity1.ssp_GetAdminMatchLab(year).ToList();
            var objIList = new List<AdminMatchLabViewModel>();
           // Mapper.CreateMap<ssp_GetAdminMatchLab_Result, AdminMatchLabViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void EditMactLab(int FollowupID, int AppointmentID)
        {
             ObjectEntity1.ssp_UpdateMatchLab(FollowupID, AppointmentID);
        }
    }
}
