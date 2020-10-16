using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class OneTimeSaleDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

        #endregion

        public List<OneTimeSaleViewModel> GetAutoshipOneTimeOrderDetails(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetAutoshipOneTimeOrderDetails(PatientId).ToList();
            var objIList = new List<OneTimeSaleViewModel>();
            Mapper.CreateMap<ssp_GetAutoshipOneTimeOrderDetails_Result, OneTimeSaleViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void AddUpdateOneTimeSaleData(int OneTimeSaleID, int DiscountID, int PatientID, int ProductID, int Quantity, bool Affiliate)
        {
            ObjectEntity1.ssp_AddUpdateOneTimeSaleData(OneTimeSaleID, DiscountID, PatientID, ProductID, Quantity, Affiliate);
        }

        public void DeleteOneTimeSaleData(int OneTimeSaleID)
        {
            ObjectEntity1.ssp_DeleteOneTimeSaleData(OneTimeSaleID);
        }
    }
}
