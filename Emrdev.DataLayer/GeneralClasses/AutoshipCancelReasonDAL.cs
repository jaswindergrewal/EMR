using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
   public class AutoshipCancelReasonDAL: ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<AutoshipCancelReasonViewModel> GetAutoshipCancelReasonList()
        {
            var objResult = GetDetails<AutoshipCancelReason>().ToList();
            var objIList = new List<AutoshipCancelReasonViewModel>();
            Mapper.CreateMap<AutoshipCancelReason, AutoshipCancelReasonViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void DeleteExcepProfileItemsByPatient(int PatientId)
        {
            ObjectEntity1.ssp_DeleteExcepProfileItemsByPatient(PatientId);
        }

        public void AddContactTbl(int AptType, int PatientId, string MessageBody, int EmployeeID)
        {
            ObjectEntity1.ssp_contact_tbl_AS_Insert(AptType,PatientId,MessageBody,EmployeeID);
        }

        public List<AutoshipDiscountViewModel> GetAutoshipDiscount()
        {
            var objResult = GetDetails<Autoship_Discounts>().ToList();
            var objIList = new List<AutoshipDiscountViewModel>();
            Mapper.CreateMap<Autoship_Discounts, AutoshipDiscountViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AutoshipProductsViewModel> GetAutoshipProductList()
        {
            var objResult = GetDetails<AutoshipProduct>().ToList();
            var objIList = new List<AutoshipProductsViewModel>();
            Mapper.CreateMap<AutoshipProduct, AutoshipProductsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertAutoShipCancelReason(string Reason, bool Active, int ReasonId)
        {
            ObjectEntity1.ssp_Insert_Update_CancelReason(Reason, Active, ReasonId);
        }

        public void DeleteAutoshipCancelReasons(int Id)
        {
            ObjectEntity1.ssp_DeleteAutoshipCancelReasons(Id);
        }

        public List<AutoshipProductsViewModel> GetAutoshipProductDropDownList()
        {
            var objList = from d in ObjectEntity1.AutoshipProducts
                          where d.Active == true
                              && d.Viewable == true
                              && d.Reviewed == true
                          select new AutoshipProductsViewModel
                          {
                              ProductID = d.ProductID,
                              ProductName = d.ProductName
                          };
            return objList.ToList();
        }
    }
}
