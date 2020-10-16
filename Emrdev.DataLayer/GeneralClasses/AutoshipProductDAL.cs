using AutoMapper;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AutoshipProductDAL : ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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

        #region Update Product

        public void EditProduct<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
        }

        public void CreateProduct<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }

        public T GetEntity<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public void UpdateProduct(Emrdev.ViewModelLayer.AutoshipProductViewModel objModel)
        {
            /*Create object of Entity and set properties from Model*/
            AutoshipProduct objEntity=ObjectEntityPart2.AutoshipProducts.SingleOrDefault(i => i.ProductID == objModel.ProductID);
            objEntity.ProductID = objModel.ProductID;
            objEntity.ProductName = objModel.ProductName;
            objEntity.AutoshipPrice = objModel.AutoshipPrice;
            objEntity.Active = objModel.Active;
            objEntity.Viewable = objModel.Viewable;
            objEntity.Reviewed = objModel.Reviewed;
            objEntity.Weight = objModel.Weight;
            objEntity.Sku = objModel.Sku;
            objEntity.Bundle = objModel.Bundle;
            EditProduct<AutoshipProduct>(objEntity);
        }

        public void AddBundledata(int BundleId, int ProductId)
        {
            ObjectEntityPart2.ssp_AddBundleProducts(BundleId, ProductId);
        }

        public List<ViewModelLayer.AutoshipProductViewModel> GetBundleGridData(int BundleId)
        {
           
            var objResult = ObjectEntityPart2.ssp_GetBundleProducts(BundleId).ToList();
            var objIList = new List<ViewModelLayer.AutoshipProductViewModel>();
            Mapper.CreateMap<ssp_GetBundleProducts_Result, ViewModelLayer.AutoshipProductViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void AddShippingValues(decimal ShippingFee, decimal OrderLimit)
        {
            Admin_ShippingValues objEntity = ObjectEntityPart2.Admin_ShippingValues.SingleOrDefault();
            if(objEntity!=null)
            {
                objEntity.ShippingFee = ShippingFee;
                objEntity.OrderTotalLimit = OrderLimit;
                EditProduct<Admin_ShippingValues>(objEntity);
            }
            else
            {
                objEntity.ShippingFee = ShippingFee;
                objEntity.OrderTotalLimit = OrderLimit;
                CreateProduct<Admin_ShippingValues>(objEntity);
            }
        }

       public void InsertProplemSuppliments(int IcdCodeId,int ProductId)
        {
            ObjectEntity1.ssp_SaveProplemSuppliment(IcdCodeId, ProductId);
        }
        #endregion

    }
}
